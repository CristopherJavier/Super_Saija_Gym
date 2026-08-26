using Npgsql;
using NpgsqlTypes;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Datos
{
    public static class CobroRepositorio
    {
        public static async Task<Cobro> GuardarAsync(Cobro cobro, List<CobroDetalle> detalles)
        {
            if (detalles.Count == 0)
            {
                throw new InvalidOperationException("Debe agregar al menos un servicio o producto al cobro.");
            }

            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlTransaction transaccion = await conexion.BeginTransactionAsync();

            try
            {
                cobro.NombreCliente = await ObtenerNombreClienteActivoAsync(
                    conexion,
                    transaccion,
                    cobro.IdCliente);
                cobro.NombreMetodoPago = await ObtenerNombreMetodoPagoActivoAsync(
                    conexion,
                    transaccion,
                    cobro.IdMetodoPago);

                List<CobroDetalle> detallesValidados = new List<CobroDetalle>();

                foreach (CobroDetalle detalle in detalles)
                {
                    string tipo = detalle.TipoDetalle.Trim().ToUpperInvariant();

                    if (tipo == "SERVICIO")
                    {
                        detallesValidados.Add(await ValidarServicioAsync(
                            conexion,
                            transaccion,
                            cobro.IdCliente,
                            detalle));
                    }
                    else if (tipo == "PRODUCTO")
                    {
                        detallesValidados.Add(await ValidarProductoAsync(
                            conexion,
                            transaccion,
                            detalle));
                    }
                    else
                    {
                        throw new InvalidOperationException("El tipo de detalle del cobro no es válido.");
                    }
                }

                decimal total = detallesValidados.Sum(x => x.Subtotal);

                if (total <= 0)
                {
                    throw new InvalidOperationException("El total del cobro debe ser mayor que cero.");
                }

                const string insercionCobro = @"
                    INSERT INTO cobros (
                        id_cliente,
                        id_usuario,
                        id_metodo_pago,
                        total,
                        estado
                    )
                    VALUES (
                        @idCliente,
                        @idUsuario,
                        @idMetodoPago,
                        @total,
                        TRUE
                    )
                    RETURNING id_cobro, fecha;";

                using (NpgsqlCommand comando = new NpgsqlCommand(insercionCobro, conexion, transaccion))
                {
                    comando.Parameters.AddWithValue("idCliente", cobro.IdCliente);
                    comando.Parameters.AddWithValue("idUsuario", cobro.IdUsuario);
                    comando.Parameters.AddWithValue("idMetodoPago", cobro.IdMetodoPago);
                    comando.Parameters.AddWithValue("total", total);
                    using NpgsqlDataReader lector = await comando.ExecuteReaderAsync();
                    await lector.ReadAsync();
                    cobro.IdCobro = lector.GetInt32(lector.GetOrdinal("id_cobro"));
                    cobro.Fecha = lector.GetDateTime(lector.GetOrdinal("fecha"));
                }

                foreach (CobroDetalle detalle in detallesValidados)
                {
                    const string insercionDetalle = @"
                        INSERT INTO cobros_detalle (
                            id_cobro,
                            tipo_detalle,
                            id_cargo,
                            id_producto,
                            descripcion,
                            cantidad,
                            precio,
                            subtotal
                        )
                        VALUES (
                            @idCobro,
                            @tipoDetalle,
                            @idCargo,
                            @idProducto,
                            @descripcion,
                            @cantidad,
                            @precio,
                            @subtotal
                        )
                        RETURNING id_detalle_cobro;";

                    using NpgsqlCommand comando = new NpgsqlCommand(insercionDetalle, conexion, transaccion);
                    comando.Parameters.AddWithValue("idCobro", cobro.IdCobro);
                    comando.Parameters.AddWithValue("tipoDetalle", detalle.TipoDetalle);
                    comando.Parameters.Add("idCargo", NpgsqlDbType.Integer).Value = detalle.IdCargo.HasValue
                        ? detalle.IdCargo.Value
                        : DBNull.Value;
                    comando.Parameters.Add("idProducto", NpgsqlDbType.Integer).Value = detalle.IdProducto.HasValue
                        ? detalle.IdProducto.Value
                        : DBNull.Value;
                    comando.Parameters.AddWithValue("descripcion", detalle.Descripcion);
                    comando.Parameters.AddWithValue("cantidad", detalle.Cantidad);
                    comando.Parameters.AddWithValue("precio", detalle.Precio);
                    comando.Parameters.AddWithValue("subtotal", detalle.Subtotal);
                    detalle.IdDetalleCobro = Convert.ToInt32(await comando.ExecuteScalarAsync());

                    if (detalle.TipoDetalle == "SERVICIO")
                    {
                        const string actualizarCargo = @"
                            UPDATE cargos
                            SET saldo = saldo - @monto
                            WHERE id_cargo = @idCargo;";

                        using NpgsqlCommand comandoCargo = new NpgsqlCommand(actualizarCargo, conexion, transaccion);
                        comandoCargo.Parameters.AddWithValue("monto", detalle.Subtotal);
                        comandoCargo.Parameters.AddWithValue("idCargo", detalle.IdCargo!.Value);
                        await comandoCargo.ExecuteNonQueryAsync();
                    }
                    else
                    {
                        const string actualizarProducto = @"
                            UPDATE productos
                            SET stock = stock - @cantidad
                            WHERE id_producto = @idProducto;

                            INSERT INTO movimientos_inventario (
                                id_producto,
                                tipo_movimiento,
                                cantidad,
                                id_usuario
                            )
                            VALUES (
                                @idProducto,
                                'SALIDA',
                                @cantidad,
                                @idUsuario
                            );";

                        using NpgsqlCommand comandoProducto = new NpgsqlCommand(actualizarProducto, conexion, transaccion);
                        comandoProducto.Parameters.AddWithValue("cantidad", detalle.Cantidad);
                        comandoProducto.Parameters.AddWithValue("idProducto", detalle.IdProducto!.Value);
                        comandoProducto.Parameters.AddWithValue("idUsuario", cobro.IdUsuario);
                        await comandoProducto.ExecuteNonQueryAsync();
                    }
                }

                await transaccion.CommitAsync();
                cobro.Total = total;
                cobro.Estado = true;
                detalles.Clear();
                detalles.AddRange(detallesValidados);
                return cobro;
            }
            catch
            {
                await transaccion.RollbackAsync();
                throw;
            }
        }

        public static async Task<List<Cobro>> ListarAsync(
            DateTime? desde = null,
            DateTime? hasta = null,
            string texto = "")
        {
            const string consulta = @"
                SELECT co.id_cobro,
                       co.fecha,
                       co.id_cliente,
                       c.nombre || ' ' || c.apellido AS nombre_cliente,
                       co.id_usuario,
                       u.nombre_completo AS nombre_usuario,
                       co.id_metodo_pago,
                       mp.nombre AS nombre_metodo_pago,
                       COALESCE((
                           SELECT STRING_AGG(cd.descripcion, ', ' ORDER BY cd.id_detalle_cobro)
                           FROM cobros_detalle cd
                           WHERE cd.id_cobro = co.id_cobro
                       ), '') AS concepto,
                       co.total,
                       co.estado
                FROM cobros co
                INNER JOIN clientes c ON c.id_cliente = co.id_cliente
                INNER JOIN usuarios u ON u.id_usuario = co.id_usuario
                INNER JOIN metodos_pago mp ON mp.id_metodo_pago = co.id_metodo_pago
                WHERE (@desde IS NULL OR co.fecha >= @desde)
                  AND (@hasta IS NULL OR co.fecha < @hasta)
                  AND (
                      @texto = ''
                      OR c.nombre ILIKE @busqueda
                      OR c.apellido ILIKE @busqueda
                      OR u.nombre_completo ILIKE @busqueda
                      OR mp.nombre ILIKE @busqueda
                      OR CAST(co.id_cobro AS TEXT) ILIKE @busqueda
                      OR EXISTS (
                          SELECT 1
                          FROM cobros_detalle cd
                          WHERE cd.id_cobro = co.id_cobro
                            AND cd.descripcion ILIKE @busqueda
                      )
                  )
                ORDER BY co.fecha DESC;";

            List<Cobro> cobros = new List<Cobro>();
            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            comando.Parameters.Add("desde", NpgsqlDbType.Timestamp).Value = desde.HasValue
                ? desde.Value
                : DBNull.Value;
            comando.Parameters.Add("hasta", NpgsqlDbType.Timestamp).Value = hasta.HasValue
                ? hasta.Value
                : DBNull.Value;
            comando.Parameters.AddWithValue("texto", texto.Trim());
            comando.Parameters.AddWithValue("busqueda", $"%{texto.Trim()}%");
            using NpgsqlDataReader lector = await comando.ExecuteReaderAsync();

            while (await lector.ReadAsync())
            {
                cobros.Add(new Cobro
                {
                    IdCobro = lector.GetInt32(lector.GetOrdinal("id_cobro")),
                    Fecha = lector.GetDateTime(lector.GetOrdinal("fecha")),
                    IdCliente = lector.GetInt32(lector.GetOrdinal("id_cliente")),
                    NombreCliente = lector.GetString(lector.GetOrdinal("nombre_cliente")),
                    IdUsuario = lector.GetInt32(lector.GetOrdinal("id_usuario")),
                    NombreUsuario = lector.GetString(lector.GetOrdinal("nombre_usuario")),
                    IdMetodoPago = lector.GetInt32(lector.GetOrdinal("id_metodo_pago")),
                    NombreMetodoPago = lector.GetString(lector.GetOrdinal("nombre_metodo_pago")),
                    Concepto = lector.GetString(lector.GetOrdinal("concepto")),
                    Total = lector.GetDecimal(lector.GetOrdinal("total")),
                    Estado = lector.GetBoolean(lector.GetOrdinal("estado"))
                });
            }

            return cobros;
        }

        public static async Task<List<CobroDetalle>> ListarDetallesAsync(int idCobro)
        {
            const string consulta = @"
                SELECT id_detalle_cobro,
                       id_cobro,
                       tipo_detalle,
                       id_cargo,
                       id_producto,
                       descripcion,
                       cantidad,
                       precio,
                       subtotal
                FROM cobros_detalle
                WHERE id_cobro = @idCobro
                ORDER BY id_detalle_cobro;";

            List<CobroDetalle> detalles = new List<CobroDetalle>();
            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("idCobro", idCobro);
            using NpgsqlDataReader lector = await comando.ExecuteReaderAsync();

            while (await lector.ReadAsync())
            {
                int indiceCargo = lector.GetOrdinal("id_cargo");
                int indiceProducto = lector.GetOrdinal("id_producto");
                detalles.Add(new CobroDetalle
                {
                    IdDetalleCobro = lector.GetInt32(lector.GetOrdinal("id_detalle_cobro")),
                    IdCobro = lector.GetInt32(lector.GetOrdinal("id_cobro")),
                    TipoDetalle = lector.GetString(lector.GetOrdinal("tipo_detalle")),
                    IdCargo = lector.IsDBNull(indiceCargo) ? null : lector.GetInt32(indiceCargo),
                    IdProducto = lector.IsDBNull(indiceProducto) ? null : lector.GetInt32(indiceProducto),
                    Descripcion = lector.GetString(lector.GetOrdinal("descripcion")),
                    Cantidad = lector.GetInt32(lector.GetOrdinal("cantidad")),
                    Precio = lector.GetDecimal(lector.GetOrdinal("precio")),
                    Subtotal = lector.GetDecimal(lector.GetOrdinal("subtotal"))
                });
            }

            return detalles;
        }

        private static async Task<string> ObtenerNombreClienteActivoAsync(
            NpgsqlConnection conexion,
            NpgsqlTransaction transaccion,
            int idCliente)
        {
            const string consulta = @"
                SELECT nombre || ' ' || apellido AS nombre_cliente, estado
                FROM clientes
                WHERE id_cliente = @idCliente
                FOR UPDATE;";

            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion, transaccion);
            comando.Parameters.AddWithValue("idCliente", idCliente);
            using NpgsqlDataReader lector = await comando.ExecuteReaderAsync();

            if (!await lector.ReadAsync() || !lector.GetBoolean(lector.GetOrdinal("estado")))
            {
                throw new InvalidOperationException("El cliente seleccionado no está activo.");
            }

            return lector.GetString(lector.GetOrdinal("nombre_cliente"));
        }

        private static async Task<string> ObtenerNombreMetodoPagoActivoAsync(
            NpgsqlConnection conexion,
            NpgsqlTransaction transaccion,
            int idMetodoPago)
        {
            const string consulta = @"
                SELECT nombre, estado
                FROM metodos_pago
                WHERE id_metodo_pago = @idMetodoPago
                FOR UPDATE;";

            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion, transaccion);
            comando.Parameters.AddWithValue("idMetodoPago", idMetodoPago);
            using NpgsqlDataReader lector = await comando.ExecuteReaderAsync();

            if (!await lector.ReadAsync() || !lector.GetBoolean(lector.GetOrdinal("estado")))
            {
                throw new InvalidOperationException("El método de pago seleccionado no está activo.");
            }

            return lector.GetString(lector.GetOrdinal("nombre"));
        }

        private static async Task<CobroDetalle> ValidarServicioAsync(
            NpgsqlConnection conexion,
            NpgsqlTransaction transaccion,
            int idCliente,
            CobroDetalle detalle)
        {
            if (!detalle.IdCargo.HasValue || detalle.Subtotal <= 0)
            {
                throw new InvalidOperationException("El servicio seleccionado no tiene un monto válido.");
            }

            const string consulta = @"
                SELECT id_cliente, concepto, saldo, estado
                FROM cargos
                WHERE id_cargo = @idCargo
                FOR UPDATE;";

            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion, transaccion);
            comando.Parameters.AddWithValue("idCargo", detalle.IdCargo.Value);
            using NpgsqlDataReader lector = await comando.ExecuteReaderAsync();

            if (!await lector.ReadAsync())
            {
                throw new InvalidOperationException("Uno de los cargos seleccionados ya no existe.");
            }

            if (lector.GetInt32(lector.GetOrdinal("id_cliente")) != idCliente)
            {
                throw new InvalidOperationException("El cargo seleccionado no pertenece al cliente del cobro.");
            }

            decimal saldo = lector.GetDecimal(lector.GetOrdinal("saldo"));

            if (!lector.GetBoolean(lector.GetOrdinal("estado")) || saldo <= 0)
            {
                throw new InvalidOperationException("Uno de los cargos seleccionados ya no está pendiente.");
            }

            if (detalle.Subtotal > saldo)
            {
                throw new InvalidOperationException("El monto del servicio no puede superar su saldo pendiente.");
            }

            return new CobroDetalle
            {
                TipoDetalle = "SERVICIO",
                IdCargo = detalle.IdCargo,
                Descripcion = lector.GetString(lector.GetOrdinal("concepto")),
                Cantidad = 1,
                Precio = detalle.Subtotal,
                Subtotal = detalle.Subtotal
            };
        }

        private static async Task<CobroDetalle> ValidarProductoAsync(
            NpgsqlConnection conexion,
            NpgsqlTransaction transaccion,
            CobroDetalle detalle)
        {
            if (!detalle.IdProducto.HasValue || detalle.Cantidad <= 0)
            {
                throw new InvalidOperationException("El producto seleccionado no tiene una cantidad válida.");
            }

            const string consulta = @"
                SELECT nombre, precio_venta, stock, estado
                FROM productos
                WHERE id_producto = @idProducto
                FOR UPDATE;";

            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion, transaccion);
            comando.Parameters.AddWithValue("idProducto", detalle.IdProducto.Value);
            using NpgsqlDataReader lector = await comando.ExecuteReaderAsync();

            if (!await lector.ReadAsync())
            {
                throw new InvalidOperationException("Uno de los productos seleccionados ya no existe.");
            }

            if (!lector.GetBoolean(lector.GetOrdinal("estado")))
            {
                throw new InvalidOperationException("Uno de los productos seleccionados ya no está activo.");
            }

            int stock = lector.GetInt32(lector.GetOrdinal("stock"));

            if (detalle.Cantidad > stock)
            {
                string nombre = lector.GetString(lector.GetOrdinal("nombre"));
                throw new InvalidOperationException($"No hay suficiente existencia de {nombre}. Stock disponible: {stock}.");
            }

            decimal precio = lector.GetDecimal(lector.GetOrdinal("precio_venta"));
            return new CobroDetalle
            {
                TipoDetalle = "PRODUCTO",
                IdProducto = detalle.IdProducto,
                Descripcion = lector.GetString(lector.GetOrdinal("nombre")),
                Cantidad = detalle.Cantidad,
                Precio = precio,
                Subtotal = precio * detalle.Cantidad
            };
        }
    }
}

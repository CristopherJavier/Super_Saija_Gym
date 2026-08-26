using Npgsql;
using NpgsqlTypes;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Datos
{
    public static class VentaRepositorio
    {
        public static async Task<Venta> GuardarAsync(
            Venta venta,
            List<VentaDetalle> detalles,
            decimal porcentajeDescuento,
            DateTime? fechaVencimientoCredito)
        {
            if (detalles.Count == 0)
            {
                throw new InvalidOperationException("Debe agregar al menos un producto a la venta.");
            }

            if (porcentajeDescuento < 0 || porcentajeDescuento > 100)
            {
                throw new InvalidOperationException("El descuento debe estar entre 0 y 100 por ciento.");
            }

            string tipoPago = venta.TipoPago.Trim().ToUpperInvariant();

            if (tipoPago != "CONTADO" && tipoPago != "CREDITO")
            {
                throw new InvalidOperationException("Debe seleccionar un tipo de venta válido.");
            }

            if (tipoPago == "CREDITO" && !venta.IdCliente.HasValue)
            {
                throw new InvalidOperationException("Las ventas a crédito requieren un cliente.");
            }

            if (tipoPago == "CREDITO" && !fechaVencimientoCredito.HasValue)
            {
                throw new InvalidOperationException("Debe seleccionar la fecha de vencimiento del crédito.");
            }

            if (fechaVencimientoCredito.HasValue && fechaVencimientoCredito.Value.Date < DateTime.Today)
            {
                throw new InvalidOperationException("La fecha de vencimiento no puede ser anterior a la fecha actual.");
            }

            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlTransaction transaccion = await conexion.BeginTransactionAsync();

            try
            {
                if (venta.IdCliente.HasValue)
                {
                    await ValidarClienteActivoAsync(conexion, transaccion, venta.IdCliente.Value);
                }

                List<VentaDetalle> detallesValidados = new List<VentaDetalle>();

                foreach (VentaDetalle detalle in detalles)
                {
                    if (detalle.Cantidad <= 0)
                    {
                        throw new InvalidOperationException("La cantidad de cada producto debe ser mayor que cero.");
                    }

                    const string consultaProducto = @"
                        SELECT nombre, precio_venta, stock, estado
                        FROM productos
                        WHERE id_producto = @idProducto
                        FOR UPDATE;";

                    using NpgsqlCommand comandoProducto = new NpgsqlCommand(consultaProducto, conexion, transaccion);
                    comandoProducto.Parameters.AddWithValue("idProducto", detalle.IdProducto);
                    using NpgsqlDataReader lector = await comandoProducto.ExecuteReaderAsync();

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
                    detallesValidados.Add(new VentaDetalle
                    {
                        IdProducto = detalle.IdProducto,
                        NombreProducto = lector.GetString(lector.GetOrdinal("nombre")),
                        Cantidad = detalle.Cantidad,
                        Precio = precio,
                        Descuento = 0,
                        Subtotal = precio * detalle.Cantidad
                    });
                }

                decimal subtotal = detallesValidados.Sum(x => x.Subtotal);
                decimal descuento = Math.Round(subtotal * porcentajeDescuento / 100m, 2);
                decimal total = subtotal - descuento;

                const string insercionVenta = @"
                    INSERT INTO ventas (
                        id_cliente,
                        id_usuario,
                        tipo_pago,
                        subtotal,
                        descuento,
                        impuesto,
                        total,
                        estado
                    )
                    VALUES (
                        @idCliente,
                        @idUsuario,
                        @tipoPago,
                        @subtotal,
                        @descuento,
                        0,
                        @total,
                        TRUE
                    )
                    RETURNING id_venta, fecha;";

                using (NpgsqlCommand comando = new NpgsqlCommand(insercionVenta, conexion, transaccion))
                {
                    comando.Parameters.Add("idCliente", NpgsqlDbType.Integer).Value = venta.IdCliente.HasValue
                        ? venta.IdCliente.Value
                        : DBNull.Value;
                    comando.Parameters.AddWithValue("idUsuario", venta.IdUsuario);
                    comando.Parameters.AddWithValue("tipoPago", tipoPago);
                    comando.Parameters.AddWithValue("subtotal", subtotal);
                    comando.Parameters.AddWithValue("descuento", descuento);
                    comando.Parameters.AddWithValue("total", total);
                    using NpgsqlDataReader lector = await comando.ExecuteReaderAsync();
                    await lector.ReadAsync();
                    venta.IdVenta = lector.GetInt32(lector.GetOrdinal("id_venta"));
                    venta.Fecha = lector.GetDateTime(lector.GetOrdinal("fecha"));
                }

                foreach (VentaDetalle detalle in detallesValidados)
                {
                    const string insercionDetalle = @"
                        INSERT INTO ventas_detalle (
                            id_venta,
                            id_producto,
                            cantidad,
                            precio,
                            descuento,
                            subtotal
                        )
                        VALUES (
                            @idVenta,
                            @idProducto,
                            @cantidad,
                            @precio,
                            0,
                            @subtotal
                        );

                        UPDATE productos
                        SET stock = stock - @cantidad
                        WHERE id_producto = @idProducto;

                        INSERT INTO movimientos_inventario (
                            id_producto,
                            tipo_movimiento,
                            cantidad,
                            id_usuario,
                            id_venta
                        )
                        VALUES (
                            @idProducto,
                            'SALIDA',
                            @cantidad,
                            @idUsuario,
                            @idVenta
                        );";

                    using NpgsqlCommand comando = new NpgsqlCommand(insercionDetalle, conexion, transaccion);
                    comando.Parameters.AddWithValue("idVenta", venta.IdVenta);
                    comando.Parameters.AddWithValue("idProducto", detalle.IdProducto);
                    comando.Parameters.AddWithValue("cantidad", detalle.Cantidad);
                    comando.Parameters.AddWithValue("precio", detalle.Precio);
                    comando.Parameters.AddWithValue("subtotal", detalle.Subtotal);
                    comando.Parameters.AddWithValue("idUsuario", venta.IdUsuario);
                    await comando.ExecuteNonQueryAsync();
                }

                if (tipoPago == "CREDITO")
                {
                    const string insercionCuenta = @"
                        INSERT INTO cuentas_cobrar (
                            id_venta,
                            id_cliente,
                            saldo,
                            fecha_vencimiento,
                            estado
                        )
                        VALUES (
                            @idVenta,
                            @idCliente,
                            @saldo,
                            @fechaVencimiento,
                            TRUE
                        );";

                    using NpgsqlCommand comando = new NpgsqlCommand(insercionCuenta, conexion, transaccion);
                    comando.Parameters.AddWithValue("idVenta", venta.IdVenta);
                    comando.Parameters.AddWithValue("idCliente", venta.IdCliente!.Value);
                    comando.Parameters.AddWithValue("saldo", total);
                    comando.Parameters.AddWithValue("fechaVencimiento", fechaVencimientoCredito!.Value.Date);
                    await comando.ExecuteNonQueryAsync();
                }

                await transaccion.CommitAsync();
                venta.TipoPago = tipoPago;
                venta.Subtotal = subtotal;
                venta.Descuento = descuento;
                venta.Impuesto = 0;
                venta.Total = total;
                venta.Estado = true;
                detalles.Clear();
                detalles.AddRange(detallesValidados);
                return venta;
            }
            catch
            {
                await transaccion.RollbackAsync();
                throw;
            }
        }

        public static async Task<List<Venta>> ListarAsync(
            DateTime? desde = null,
            DateTime? hasta = null,
            string texto = "")
        {
            const string consulta = @"
                SELECT v.id_venta,
                       v.fecha,
                       v.id_cliente,
                       COALESCE(c.nombre || ' ' || c.apellido, 'SIN CLIENTE') AS nombre_cliente,
                       v.id_usuario,
                       u.nombre_completo AS nombre_usuario,
                       v.tipo_pago,
                       v.subtotal,
                       v.descuento,
                       v.impuesto,
                       v.total,
                       v.estado
                FROM ventas v
                LEFT JOIN clientes c ON c.id_cliente = v.id_cliente
                INNER JOIN usuarios u ON u.id_usuario = v.id_usuario
                WHERE (@desde IS NULL OR v.fecha >= @desde)
                  AND (@hasta IS NULL OR v.fecha < @hasta)
                  AND (
                      @texto = ''
                      OR COALESCE(c.nombre || ' ' || c.apellido, 'SIN CLIENTE') ILIKE @busqueda
                      OR u.nombre_completo ILIKE @busqueda
                      OR v.tipo_pago ILIKE @busqueda
                      OR CAST(v.id_venta AS TEXT) ILIKE @busqueda
                  )
                ORDER BY v.fecha DESC;";

            List<Venta> ventas = new List<Venta>();
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
                int indiceCliente = lector.GetOrdinal("id_cliente");
                ventas.Add(new Venta
                {
                    IdVenta = lector.GetInt32(lector.GetOrdinal("id_venta")),
                    Fecha = lector.GetDateTime(lector.GetOrdinal("fecha")),
                    IdCliente = lector.IsDBNull(indiceCliente) ? null : lector.GetInt32(indiceCliente),
                    NombreCliente = lector.GetString(lector.GetOrdinal("nombre_cliente")),
                    IdUsuario = lector.GetInt32(lector.GetOrdinal("id_usuario")),
                    NombreUsuario = lector.GetString(lector.GetOrdinal("nombre_usuario")),
                    TipoPago = lector.GetString(lector.GetOrdinal("tipo_pago")),
                    Subtotal = lector.GetDecimal(lector.GetOrdinal("subtotal")),
                    Descuento = lector.GetDecimal(lector.GetOrdinal("descuento")),
                    Impuesto = lector.GetDecimal(lector.GetOrdinal("impuesto")),
                    Total = lector.GetDecimal(lector.GetOrdinal("total")),
                    Estado = lector.GetBoolean(lector.GetOrdinal("estado"))
                });
            }

            return ventas;
        }

        private static async Task ValidarClienteActivoAsync(
            NpgsqlConnection conexion,
            NpgsqlTransaction transaccion,
            int idCliente)
        {
            const string consulta = @"
                SELECT estado
                FROM clientes
                WHERE id_cliente = @idCliente
                FOR UPDATE;";

            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion, transaccion);
            comando.Parameters.AddWithValue("idCliente", idCliente);
            object? resultado = await comando.ExecuteScalarAsync();

            if (resultado is not bool activo || !activo)
            {
                throw new InvalidOperationException("El cliente seleccionado no está activo.");
            }
        }
    }
}

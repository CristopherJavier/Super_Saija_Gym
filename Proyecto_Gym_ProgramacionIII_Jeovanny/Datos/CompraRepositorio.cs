using Npgsql;
using NpgsqlTypes;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Datos
{
    public static class CompraRepositorio
    {
        public static async Task<Compra> GuardarAsync(Compra compra, List<CompraDetalle> detalles)
        {
            if (detalles.Count == 0)
            {
                throw new InvalidOperationException("Debe agregar al menos un producto a la compra.");
            }

            if (detalles.Any(x => x.Cantidad <= 0 || x.Precio < 0))
            {
                throw new InvalidOperationException("Las cantidades y precios de la compra no son válidos.");
            }

            decimal subtotal = detalles.Sum(x => x.Cantidad * x.Precio);
            decimal total = subtotal;

            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlTransaction transaccion = await conexion.BeginTransactionAsync();

            try
            {
                await ValidarProveedorAsync(conexion, transaccion, compra.IdProveedor);
                await ValidarMetodoPagoAsync(conexion, transaccion, compra.IdMetodoPago);

                foreach (CompraDetalle detalle in detalles)
                {
                    const string consultaProducto = @"
                        SELECT nombre, stock, estado
                        FROM productos
                        WHERE id_producto = @idProducto
                        FOR UPDATE;";

                    using NpgsqlCommand comando = new NpgsqlCommand(consultaProducto, conexion, transaccion);
                    comando.Parameters.AddWithValue("idProducto", detalle.IdProducto);
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

                    if (detalle.Cantidad > int.MaxValue - stock)
                    {
                        throw new InvalidOperationException("La cantidad de la compra supera el límite permitido para el stock.");
                    }

                    detalle.NombreProducto = lector.GetString(lector.GetOrdinal("nombre"));
                    detalle.Subtotal = detalle.Cantidad * detalle.Precio;
                }

                const string insercionCompra = @"
                    INSERT INTO compras (
                        id_proveedor,
                        id_usuario,
                        id_metodo_pago,
                        subtotal,
                        impuesto,
                        total,
                        estado
                    )
                    VALUES (
                        @idProveedor,
                        @idUsuario,
                        @idMetodoPago,
                        @subtotal,
                        0,
                        @total,
                        TRUE
                    )
                    RETURNING id_compra, fecha;";

                using (NpgsqlCommand comando = new NpgsqlCommand(insercionCompra, conexion, transaccion))
                {
                    comando.Parameters.AddWithValue("idProveedor", compra.IdProveedor);
                    comando.Parameters.AddWithValue("idUsuario", compra.IdUsuario);
                    comando.Parameters.AddWithValue("idMetodoPago", compra.IdMetodoPago);
                    comando.Parameters.AddWithValue("subtotal", subtotal);
                    comando.Parameters.AddWithValue("total", total);
                    using NpgsqlDataReader lector = await comando.ExecuteReaderAsync();
                    await lector.ReadAsync();
                    compra.IdCompra = lector.GetInt32(lector.GetOrdinal("id_compra"));
                    compra.Fecha = lector.GetDateTime(lector.GetOrdinal("fecha"));
                }

                foreach (CompraDetalle detalle in detalles)
                {
                    const string insercionDetalle = @"
                        INSERT INTO compras_detalle (
                            id_compra,
                            id_producto,
                            cantidad,
                            precio,
                            subtotal
                        )
                        VALUES (
                            @idCompra,
                            @idProducto,
                            @cantidad,
                            @precio,
                            @subtotal
                        );

                        UPDATE productos
                        SET stock = stock + @cantidad
                        WHERE id_producto = @idProducto;

                        INSERT INTO movimientos_inventario (
                            id_producto,
                            tipo_movimiento,
                            cantidad,
                            id_usuario,
                            id_compra
                        )
                        VALUES (
                            @idProducto,
                            'ENTRADA',
                            @cantidad,
                            @idUsuario,
                            @idCompra
                        );";

                    using NpgsqlCommand comando = new NpgsqlCommand(insercionDetalle, conexion, transaccion);
                    comando.Parameters.AddWithValue("idCompra", compra.IdCompra);
                    comando.Parameters.AddWithValue("idProducto", detalle.IdProducto);
                    comando.Parameters.AddWithValue("cantidad", detalle.Cantidad);
                    comando.Parameters.AddWithValue("precio", detalle.Precio);
                    comando.Parameters.AddWithValue("subtotal", detalle.Subtotal);
                    comando.Parameters.AddWithValue("idUsuario", compra.IdUsuario);
                    await comando.ExecuteNonQueryAsync();
                }

                await transaccion.CommitAsync();
                compra.Subtotal = subtotal;
                compra.Impuesto = 0;
                compra.Total = total;
                compra.Estado = true;
                return compra;
            }
            catch
            {
                await transaccion.RollbackAsync();
                throw;
            }
        }

        public static async Task<List<Compra>> ListarAsync(
            DateTime? desde = null,
            DateTime? hasta = null,
            string texto = "")
        {
            const string consulta = @"
                SELECT co.id_compra,
                       co.fecha,
                       co.id_proveedor,
                       p.nombre AS nombre_proveedor,
                       co.id_usuario,
                       u.nombre_completo AS nombre_usuario,
                       co.id_metodo_pago,
                       mp.nombre AS nombre_metodo_pago,
                       co.subtotal,
                       co.impuesto,
                       co.total,
                       co.estado
                FROM compras co
                INNER JOIN proveedores p ON p.id_proveedor = co.id_proveedor
                INNER JOIN usuarios u ON u.id_usuario = co.id_usuario
                INNER JOIN metodos_pago mp ON mp.id_metodo_pago = co.id_metodo_pago
                WHERE (@desde IS NULL OR co.fecha >= @desde)
                  AND (@hasta IS NULL OR co.fecha < @hasta)
                  AND (
                      @texto = ''
                      OR p.nombre ILIKE @busqueda
                      OR u.nombre_completo ILIKE @busqueda
                      OR mp.nombre ILIKE @busqueda
                      OR CAST(co.id_compra AS TEXT) ILIKE @busqueda
                  )
                ORDER BY co.fecha DESC;";

            List<Compra> compras = new List<Compra>();
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
                compras.Add(new Compra
                {
                    IdCompra = lector.GetInt32(lector.GetOrdinal("id_compra")),
                    Fecha = lector.GetDateTime(lector.GetOrdinal("fecha")),
                    IdProveedor = lector.GetInt32(lector.GetOrdinal("id_proveedor")),
                    NombreProveedor = lector.GetString(lector.GetOrdinal("nombre_proveedor")),
                    IdUsuario = lector.GetInt32(lector.GetOrdinal("id_usuario")),
                    NombreUsuario = lector.GetString(lector.GetOrdinal("nombre_usuario")),
                    IdMetodoPago = lector.GetInt32(lector.GetOrdinal("id_metodo_pago")),
                    NombreMetodoPago = lector.GetString(lector.GetOrdinal("nombre_metodo_pago")),
                    Subtotal = lector.GetDecimal(lector.GetOrdinal("subtotal")),
                    Impuesto = lector.GetDecimal(lector.GetOrdinal("impuesto")),
                    Total = lector.GetDecimal(lector.GetOrdinal("total")),
                    Estado = lector.GetBoolean(lector.GetOrdinal("estado"))
                });
            }

            return compras;
        }

        private static async Task ValidarProveedorAsync(
            NpgsqlConnection conexion,
            NpgsqlTransaction transaccion,
            int idProveedor)
        {
            const string consulta = @"
                SELECT estado
                FROM proveedores
                WHERE id_proveedor = @idProveedor
                FOR UPDATE;";

            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion, transaccion);
            comando.Parameters.AddWithValue("idProveedor", idProveedor);
            object? resultado = await comando.ExecuteScalarAsync();

            if (resultado is not bool activo || !activo)
            {
                throw new InvalidOperationException("El proveedor seleccionado no está activo.");
            }
        }

        private static async Task ValidarMetodoPagoAsync(
            NpgsqlConnection conexion,
            NpgsqlTransaction transaccion,
            int idMetodoPago)
        {
            const string consulta = @"
                SELECT estado
                FROM metodos_pago
                WHERE id_metodo_pago = @idMetodoPago
                FOR UPDATE;";

            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion, transaccion);
            comando.Parameters.AddWithValue("idMetodoPago", idMetodoPago);
            object? resultado = await comando.ExecuteScalarAsync();

            if (resultado is not bool activo || !activo)
            {
                throw new InvalidOperationException("El método de pago seleccionado no está activo.");
            }
        }
    }
}

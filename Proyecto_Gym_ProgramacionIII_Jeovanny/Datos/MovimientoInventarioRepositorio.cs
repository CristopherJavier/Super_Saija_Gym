using Npgsql;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Datos
{
    public static class MovimientoInventarioRepositorio
    {
        public static async Task<MovimientoInventario> RegistrarAsync(MovimientoInventario movimiento)
        {
            string tipo = movimiento.TipoMovimiento.Trim().ToUpperInvariant();

            if (tipo != "ENTRADA" && tipo != "SALIDA")
            {
                throw new InvalidOperationException("Debe seleccionar un tipo de movimiento válido.");
            }

            if (movimiento.Cantidad <= 0)
            {
                throw new InvalidOperationException("La cantidad debe ser mayor que cero.");
            }

            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlTransaction transaccion = await conexion.BeginTransactionAsync();

            try
            {
                const string consultaProducto = @"
                    SELECT nombre, stock, estado
                    FROM productos
                    WHERE id_producto = @idProducto
                    FOR UPDATE;";

                int stock;
                using (NpgsqlCommand comando = new NpgsqlCommand(consultaProducto, conexion, transaccion))
                {
                    comando.Parameters.AddWithValue("idProducto", movimiento.IdProducto);
                    using NpgsqlDataReader lector = await comando.ExecuteReaderAsync();

                    if (!await lector.ReadAsync())
                    {
                        throw new InvalidOperationException("El producto seleccionado ya no existe.");
                    }

                    if (!lector.GetBoolean(lector.GetOrdinal("estado")))
                    {
                        throw new InvalidOperationException("El producto seleccionado no está activo.");
                    }

                    movimiento.NombreProducto = lector.GetString(lector.GetOrdinal("nombre"));
                    stock = lector.GetInt32(lector.GetOrdinal("stock"));
                }

                if (tipo == "SALIDA" && movimiento.Cantidad > stock)
                {
                    throw new InvalidOperationException($"La salida supera el stock disponible de {stock} unidades.");
                }

                if (tipo == "ENTRADA" && movimiento.Cantidad > int.MaxValue - stock)
                {
                    throw new InvalidOperationException("La entrada supera el límite permitido para el stock.");
                }

                const string insercion = @"
                    INSERT INTO movimientos_inventario (
                        id_producto,
                        tipo_movimiento,
                        cantidad,
                        id_usuario
                    )
                    VALUES (
                        @idProducto,
                        @tipoMovimiento,
                        @cantidad,
                        @idUsuario
                    )
                    RETURNING id_movimiento, fecha;

                    UPDATE productos
                    SET stock = stock + CASE
                        WHEN @tipoMovimiento = 'ENTRADA' THEN @cantidad
                        ELSE -@cantidad
                    END
                    WHERE id_producto = @idProducto;";

                using (NpgsqlCommand comando = new NpgsqlCommand(insercion, conexion, transaccion))
                {
                    comando.Parameters.AddWithValue("idProducto", movimiento.IdProducto);
                    comando.Parameters.AddWithValue("tipoMovimiento", tipo);
                    comando.Parameters.AddWithValue("cantidad", movimiento.Cantidad);
                    comando.Parameters.AddWithValue("idUsuario", movimiento.IdUsuario);
                    using NpgsqlDataReader lector = await comando.ExecuteReaderAsync();
                    await lector.ReadAsync();
                    movimiento.IdMovimiento = lector.GetInt32(lector.GetOrdinal("id_movimiento"));
                    movimiento.Fecha = lector.GetDateTime(lector.GetOrdinal("fecha"));
                }

                await transaccion.CommitAsync();
                movimiento.TipoMovimiento = tipo;
                return movimiento;
            }
            catch
            {
                await transaccion.RollbackAsync();
                throw;
            }
        }

        public static async Task<List<MovimientoInventario>> ListarAsync()
        {
            const string consulta = @"
                SELECT mi.id_movimiento,
                       mi.id_producto,
                       p.nombre AS nombre_producto,
                       mi.tipo_movimiento,
                       mi.cantidad,
                       mi.fecha,
                       mi.id_usuario,
                       u.nombre_completo AS nombre_usuario,
                       mi.id_venta,
                       mi.id_compra
                FROM movimientos_inventario mi
                INNER JOIN productos p ON p.id_producto = mi.id_producto
                INNER JOIN usuarios u ON u.id_usuario = mi.id_usuario
                ORDER BY mi.fecha DESC;";

            List<MovimientoInventario> movimientos = new List<MovimientoInventario>();
            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            using NpgsqlDataReader lector = await comando.ExecuteReaderAsync();

            while (await lector.ReadAsync())
            {
                int indiceVenta = lector.GetOrdinal("id_venta");
                int indiceCompra = lector.GetOrdinal("id_compra");
                movimientos.Add(new MovimientoInventario
                {
                    IdMovimiento = lector.GetInt32(lector.GetOrdinal("id_movimiento")),
                    IdProducto = lector.GetInt32(lector.GetOrdinal("id_producto")),
                    NombreProducto = lector.GetString(lector.GetOrdinal("nombre_producto")),
                    TipoMovimiento = lector.GetString(lector.GetOrdinal("tipo_movimiento")),
                    Cantidad = lector.GetInt32(lector.GetOrdinal("cantidad")),
                    Fecha = lector.GetDateTime(lector.GetOrdinal("fecha")),
                    IdUsuario = lector.GetInt32(lector.GetOrdinal("id_usuario")),
                    NombreUsuario = lector.GetString(lector.GetOrdinal("nombre_usuario")),
                    IdVenta = lector.IsDBNull(indiceVenta) ? null : lector.GetInt32(indiceVenta),
                    IdCompra = lector.IsDBNull(indiceCompra) ? null : lector.GetInt32(indiceCompra)
                });
            }

            return movimientos;
        }
    }
}

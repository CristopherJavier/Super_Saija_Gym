using Npgsql;
using NpgsqlTypes;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Datos
{
    public static class ProductoRepositorio
    {
        public static async Task<List<Producto>> ListarAsync(string texto = "")
        {
            const string consulta = @"
                SELECT p.id_producto, p.codigo, p.nombre, p.descripcion,
                       p.id_categoria, c.nombre AS nombre_categoria,
                       p.id_marca, COALESCE(m.nombre, '') AS nombre_marca,
                       p.precio_compra, p.precio_venta, p.stock, p.stock_minimo,
                       p.imagen, p.estado
                FROM productos p
                INNER JOIN categorias_productos c ON c.id_categoria = p.id_categoria
                LEFT JOIN marcas m ON m.id_marca = p.id_marca
                WHERE @texto = ''
                   OR p.codigo ILIKE @busqueda
                   OR p.nombre ILIKE @busqueda
                   OR p.descripcion ILIKE @busqueda
                   OR c.nombre ILIKE @busqueda
                   OR m.nombre ILIKE @busqueda
                ORDER BY p.estado DESC, p.nombre;";

            List<Producto> productos = new List<Producto>();
            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("texto", texto.Trim());
            comando.Parameters.AddWithValue("busqueda", $"%{texto.Trim()}%");
            using NpgsqlDataReader lector = await comando.ExecuteReaderAsync();
            while (await lector.ReadAsync())
            {
                int indiceDescripcion = lector.GetOrdinal("descripcion");
                int indiceImagen = lector.GetOrdinal("imagen");
                int indiceMarca = lector.GetOrdinal("id_marca");
                productos.Add(new Producto
                {
                    IdProducto = lector.GetInt32(lector.GetOrdinal("id_producto")),
                    Codigo = lector.GetString(lector.GetOrdinal("codigo")),
                    Nombre = lector.GetString(lector.GetOrdinal("nombre")),
                    Descripcion = lector.IsDBNull(indiceDescripcion) ? string.Empty : lector.GetString(indiceDescripcion),
                    IdCategoria = lector.GetInt32(lector.GetOrdinal("id_categoria")),
                    NombreCategoria = lector.GetString(lector.GetOrdinal("nombre_categoria")),
                    IdMarca = lector.IsDBNull(indiceMarca) ? null : lector.GetInt32(indiceMarca),
                    NombreMarca = lector.GetString(lector.GetOrdinal("nombre_marca")),
                    PrecioCompra = lector.GetDecimal(lector.GetOrdinal("precio_compra")),
                    PrecioVenta = lector.GetDecimal(lector.GetOrdinal("precio_venta")),
                    Stock = lector.GetInt32(lector.GetOrdinal("stock")),
                    StockMinimo = lector.GetInt32(lector.GetOrdinal("stock_minimo")),
                    Imagen = lector.IsDBNull(indiceImagen) ? string.Empty : lector.GetString(indiceImagen),
                    Estado = lector.GetBoolean(lector.GetOrdinal("estado"))
                });
            }
            return productos;
        }

        public static async Task<bool> ExisteCodigoAsync(string codigo, int idIgnorar = 0)
        {
            const string consulta = @"
                SELECT EXISTS (
                    SELECT 1 FROM productos
                    WHERE UPPER(TRIM(codigo)) = UPPER(TRIM(@codigo))
                      AND (@idIgnorar = 0 OR id_producto <> @idIgnorar)
                );";
            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("codigo", codigo.Trim());
            comando.Parameters.AddWithValue("idIgnorar", idIgnorar);
            object? resultado = await comando.ExecuteScalarAsync();
            return resultado is bool existe && existe;
        }

        public static async Task GuardarAsync(Producto producto)
        {
            const string consulta = @"
                INSERT INTO productos
                    (codigo, nombre, descripcion, id_categoria, id_marca,
                     precio_compra, precio_venta, stock, stock_minimo, imagen, estado)
                VALUES
                    (@codigo, @nombre, @descripcion, @idCategoria, @idMarca,
                     @precioCompra, @precioVenta, @stock, @stockMinimo, @imagen, @estado);";
            await EjecutarGuardadoAsync(consulta, producto);
        }

        public static async Task ActualizarAsync(Producto producto)
        {
            const string consulta = @"
                UPDATE productos
                SET codigo = @codigo,
                    nombre = @nombre,
                    descripcion = @descripcion,
                    id_categoria = @idCategoria,
                    id_marca = @idMarca,
                    precio_compra = @precioCompra,
                    precio_venta = @precioVenta,
                    stock = @stock,
                    stock_minimo = @stockMinimo,
                    imagen = @imagen,
                    estado = @estado
                WHERE id_producto = @idProducto;";
            await EjecutarGuardadoAsync(consulta, producto);
        }

        public static async Task CambiarEstadoAsync(int idProducto, bool estado)
        {
            const string consulta = "UPDATE productos SET estado = @estado WHERE id_producto = @idProducto;";
            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("estado", estado);
            comando.Parameters.AddWithValue("idProducto", idProducto);
            await comando.ExecuteNonQueryAsync();
        }

        private static async Task EjecutarGuardadoAsync(string consulta, Producto producto)
        {
            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("codigo", producto.Codigo.Trim());
            comando.Parameters.AddWithValue("nombre", producto.Nombre.Trim());
            comando.Parameters.Add("descripcion", NpgsqlDbType.Varchar).Value = string.IsNullOrWhiteSpace(producto.Descripcion) ? DBNull.Value : producto.Descripcion.Trim();
            comando.Parameters.AddWithValue("idCategoria", producto.IdCategoria);
            comando.Parameters.Add("idMarca", NpgsqlDbType.Integer).Value = producto.IdMarca.HasValue ? producto.IdMarca.Value : DBNull.Value;
            comando.Parameters.AddWithValue("precioCompra", producto.PrecioCompra);
            comando.Parameters.AddWithValue("precioVenta", producto.PrecioVenta);
            comando.Parameters.AddWithValue("stock", producto.Stock);
            comando.Parameters.AddWithValue("stockMinimo", producto.StockMinimo);
            comando.Parameters.Add("imagen", NpgsqlDbType.Text).Value = string.IsNullOrWhiteSpace(producto.Imagen) ? DBNull.Value : producto.Imagen;
            comando.Parameters.AddWithValue("estado", producto.Estado);
            if (producto.IdProducto > 0)
            {
                comando.Parameters.AddWithValue("idProducto", producto.IdProducto);
            }
            await comando.ExecuteNonQueryAsync();
        }
    }
}

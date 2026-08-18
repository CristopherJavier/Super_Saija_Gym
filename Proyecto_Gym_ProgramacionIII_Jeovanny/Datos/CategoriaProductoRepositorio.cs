using Npgsql;
using NpgsqlTypes;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Datos
{
    public static class CategoriaProductoRepositorio
    {
        public static async Task<List<CategoriaProducto>> ListarAsync(string texto = "", bool soloActivas = false)
        {
            const string consulta = @"
                SELECT id_categoria, nombre, descripcion, estado
                FROM categorias_productos
                WHERE (@texto = '' OR nombre ILIKE @busqueda OR descripcion ILIKE @busqueda)
                  AND (@soloActivas = FALSE OR estado = TRUE)
                ORDER BY estado DESC, nombre;";
            List<CategoriaProducto> categorias = new List<CategoriaProducto>();
            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("texto", texto.Trim());
            comando.Parameters.AddWithValue("busqueda", $"%{texto.Trim()}%");
            comando.Parameters.AddWithValue("soloActivas", soloActivas);
            using NpgsqlDataReader lector = await comando.ExecuteReaderAsync();
            while (await lector.ReadAsync())
            {
                int indiceDescripcion = lector.GetOrdinal("descripcion");
                categorias.Add(new CategoriaProducto
                {
                    IdCategoria = lector.GetInt32(lector.GetOrdinal("id_categoria")),
                    Nombre = lector.GetString(lector.GetOrdinal("nombre")),
                    Descripcion = lector.IsDBNull(indiceDescripcion) ? string.Empty : lector.GetString(indiceDescripcion),
                    Estado = lector.GetBoolean(lector.GetOrdinal("estado"))
                });
            }
            return categorias;
        }

        public static async Task<bool> ExisteNombreAsync(string nombre, int idIgnorar = 0)
        {
            const string consulta = @"
                SELECT EXISTS (
                    SELECT 1 FROM categorias_productos
                    WHERE UPPER(TRIM(nombre)) = UPPER(TRIM(@nombre))
                      AND (@idIgnorar = 0 OR id_categoria <> @idIgnorar)
                );";
            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("nombre", nombre.Trim());
            comando.Parameters.AddWithValue("idIgnorar", idIgnorar);
            object? resultado = await comando.ExecuteScalarAsync();
            return resultado is bool existe && existe;
        }

        public static async Task GuardarAsync(CategoriaProducto categoria)
        {
            const string consulta = "INSERT INTO categorias_productos (nombre, descripcion, estado) VALUES (@nombre, @descripcion, @estado);";
            await EjecutarGuardadoAsync(consulta, categoria);
        }

        public static async Task ActualizarAsync(CategoriaProducto categoria)
        {
            const string consulta = @"
                UPDATE categorias_productos
                SET nombre = @nombre, descripcion = @descripcion, estado = @estado
                WHERE id_categoria = @idCategoria;";
            await EjecutarGuardadoAsync(consulta, categoria);
        }

        public static async Task CambiarEstadoAsync(int idCategoria, bool estado)
        {
            const string consulta = "UPDATE categorias_productos SET estado = @estado WHERE id_categoria = @idCategoria;";
            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("estado", estado);
            comando.Parameters.AddWithValue("idCategoria", idCategoria);
            await comando.ExecuteNonQueryAsync();
        }

        private static async Task EjecutarGuardadoAsync(string consulta, CategoriaProducto categoria)
        {
            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("nombre", categoria.Nombre.Trim());
            comando.Parameters.Add("descripcion", NpgsqlDbType.Varchar).Value = string.IsNullOrWhiteSpace(categoria.Descripcion)
                ? DBNull.Value
                : categoria.Descripcion.Trim();
            comando.Parameters.AddWithValue("estado", categoria.Estado);
            if (categoria.IdCategoria > 0)
            {
                comando.Parameters.AddWithValue("idCategoria", categoria.IdCategoria);
            }
            await comando.ExecuteNonQueryAsync();
        }
    }
}

using Npgsql;
using NpgsqlTypes;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Datos
{
    public static class ClaseActividadRepositorio
    {
        public static async Task<List<ClaseActividad>> ListarAsync(string texto = "", bool soloActivas = false)
        {
            const string consulta = @"
                SELECT id_clase, nombre, descripcion, cupo_maximo, estado
                FROM clases_actividades
                WHERE (@texto = '' OR nombre ILIKE @busqueda OR descripcion ILIKE @busqueda)
                  AND (@soloActivas = FALSE OR estado = TRUE)
                ORDER BY estado DESC, nombre;";

            List<ClaseActividad> clases = new List<ClaseActividad>();
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
                clases.Add(new ClaseActividad
                {
                    IdClase = lector.GetInt32(lector.GetOrdinal("id_clase")),
                    Nombre = lector.GetString(lector.GetOrdinal("nombre")),
                    Descripcion = lector.IsDBNull(indiceDescripcion) ? string.Empty : lector.GetString(indiceDescripcion),
                    CupoMaximo = lector.GetInt32(lector.GetOrdinal("cupo_maximo")),
                    Estado = lector.GetBoolean(lector.GetOrdinal("estado"))
                });
            }

            return clases;
        }

        public static async Task<bool> ExisteNombreAsync(string nombre, int idIgnorar = 0)
        {
            const string consulta = @"
                SELECT EXISTS (
                    SELECT 1 FROM clases_actividades
                    WHERE UPPER(TRIM(nombre)) = UPPER(TRIM(@nombre))
                      AND (@idIgnorar = 0 OR id_clase <> @idIgnorar)
                );";

            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("nombre", nombre.Trim());
            comando.Parameters.AddWithValue("idIgnorar", idIgnorar);
            object? resultado = await comando.ExecuteScalarAsync();
            return resultado is bool existe && existe;
        }

        public static async Task GuardarAsync(ClaseActividad clase)
        {
            const string consulta = @"
                INSERT INTO clases_actividades (nombre, descripcion, cupo_maximo, estado)
                VALUES (@nombre, @descripcion, @cupoMaximo, @estado);";
            await EjecutarGuardadoAsync(consulta, clase);
        }

        public static async Task ActualizarAsync(ClaseActividad clase)
        {
            const string consulta = @"
                UPDATE clases_actividades
                SET nombre = @nombre,
                    descripcion = @descripcion,
                    cupo_maximo = @cupoMaximo,
                    estado = @estado
                WHERE id_clase = @idClase;";
            await EjecutarGuardadoAsync(consulta, clase);
        }

        public static async Task CambiarEstadoAsync(int idClase, bool estado)
        {
            const string consulta = "UPDATE clases_actividades SET estado = @estado WHERE id_clase = @idClase;";
            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("estado", estado);
            comando.Parameters.AddWithValue("idClase", idClase);
            await comando.ExecuteNonQueryAsync();
        }

        private static async Task EjecutarGuardadoAsync(string consulta, ClaseActividad clase)
        {
            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("nombre", clase.Nombre.Trim());
            comando.Parameters.Add("descripcion", NpgsqlDbType.Varchar).Value = string.IsNullOrWhiteSpace(clase.Descripcion)
                ? DBNull.Value
                : clase.Descripcion.Trim();
            comando.Parameters.AddWithValue("cupoMaximo", clase.CupoMaximo);
            comando.Parameters.AddWithValue("estado", clase.Estado);
            if (clase.IdClase > 0)
            {
                comando.Parameters.AddWithValue("idClase", clase.IdClase);
            }
            await comando.ExecuteNonQueryAsync();
        }
    }
}

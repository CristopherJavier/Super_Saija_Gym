using Npgsql;
using NpgsqlTypes;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Datos
{
    public static class PermisoRepositorio
    {
        public static async Task<List<Permiso>> ListarAsync(string texto = "", bool soloActivos = false)
        {
            const string consulta = @"
                SELECT id_permiso, clave, nombre, descripcion, estado
                FROM permisos
                WHERE (@texto = ''
                       OR clave ILIKE @busqueda
                       OR nombre ILIKE @busqueda
                       OR descripcion ILIKE @busqueda)
                  AND (@soloActivos = FALSE OR estado = TRUE)
                ORDER BY estado DESC, nombre;";

            List<Permiso> permisos = new List<Permiso>();
            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("texto", texto.Trim());
            comando.Parameters.AddWithValue("busqueda", $"%{texto.Trim()}%");
            comando.Parameters.AddWithValue("soloActivos", soloActivos);
            using NpgsqlDataReader lector = await comando.ExecuteReaderAsync();

            while (await lector.ReadAsync())
            {
                int indiceDescripcion = lector.GetOrdinal("descripcion");
                permisos.Add(new Permiso
                {
                    IdPermiso = lector.GetInt32(lector.GetOrdinal("id_permiso")),
                    Clave = lector.GetString(lector.GetOrdinal("clave")),
                    Nombre = lector.GetString(lector.GetOrdinal("nombre")),
                    Descripcion = lector.IsDBNull(indiceDescripcion)
                        ? string.Empty
                        : lector.GetString(indiceDescripcion),
                    Estado = lector.GetBoolean(lector.GetOrdinal("estado"))
                });
            }

            return permisos;
        }

        public static async Task<bool> ExisteClaveAsync(string clave, int idIgnorar = 0)
        {
            const string consulta = @"
                SELECT EXISTS (
                    SELECT 1
                    FROM permisos
                    WHERE UPPER(TRIM(clave)) = UPPER(TRIM(@valor))
                      AND (@idIgnorar = 0 OR id_permiso <> @idIgnorar)
                );";

            return await ExisteAsync(consulta, clave, idIgnorar);
        }

        public static async Task<bool> ExisteNombreAsync(string nombre, int idIgnorar = 0)
        {
            const string consulta = @"
                SELECT EXISTS (
                    SELECT 1
                    FROM permisos
                    WHERE UPPER(TRIM(nombre)) = UPPER(TRIM(@valor))
                      AND (@idIgnorar = 0 OR id_permiso <> @idIgnorar)
                );";

            return await ExisteAsync(consulta, nombre, idIgnorar);
        }

        public static async Task GuardarAsync(Permiso permiso)
        {
            const string consulta = @"
                INSERT INTO permisos (clave, nombre, descripcion, estado)
                VALUES (@clave, @nombre, @descripcion, @estado);";

            await EjecutarGuardadoAsync(consulta, permiso);
        }

        public static async Task ActualizarAsync(Permiso permiso)
        {
            const string consulta = @"
                UPDATE permisos
                SET clave = @clave,
                    nombre = @nombre,
                    descripcion = @descripcion,
                    estado = @estado
                WHERE id_permiso = @idPermiso;";

            await EjecutarGuardadoAsync(consulta, permiso);
        }

        private static async Task<bool> ExisteAsync(string consulta, string valor, int idIgnorar)
        {
            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("valor", valor.Trim());
            comando.Parameters.AddWithValue("idIgnorar", idIgnorar);
            return Convert.ToBoolean(await comando.ExecuteScalarAsync());
        }

        private static async Task EjecutarGuardadoAsync(string consulta, Permiso permiso)
        {
            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("clave", permiso.Clave.Trim().ToUpperInvariant());
            comando.Parameters.AddWithValue("nombre", permiso.Nombre.Trim());
            comando.Parameters.Add("descripcion", NpgsqlDbType.Varchar).Value =
                string.IsNullOrWhiteSpace(permiso.Descripcion)
                    ? DBNull.Value
                    : permiso.Descripcion.Trim();
            comando.Parameters.AddWithValue("estado", permiso.Estado);

            if (permiso.IdPermiso > 0)
            {
                comando.Parameters.AddWithValue("idPermiso", permiso.IdPermiso);
            }

            await comando.ExecuteNonQueryAsync();
        }
    }
}

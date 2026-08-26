using Npgsql;
using NpgsqlTypes;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Datos
{
    public static class RolRepositorio
    {
        public static async Task<List<Rol>> ListarAsync(string texto = "", bool soloActivos = false)
        {
            const string consulta = @"
                SELECT id_rol, nombre, descripcion, estado
                FROM roles
                WHERE (@texto = '' OR nombre ILIKE @busqueda OR descripcion ILIKE @busqueda)
                  AND (@soloActivos = FALSE OR estado = TRUE)
                ORDER BY estado DESC, nombre;";

            List<Rol> roles = new List<Rol>();
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
                roles.Add(new Rol
                {
                    IdRol = lector.GetInt32(lector.GetOrdinal("id_rol")),
                    Nombre = lector.GetString(lector.GetOrdinal("nombre")),
                    Descripcion = lector.IsDBNull(indiceDescripcion)
                        ? string.Empty
                        : lector.GetString(indiceDescripcion),
                    Estado = lector.GetBoolean(lector.GetOrdinal("estado"))
                });
            }

            return roles;
        }

        public static async Task<bool> ExisteNombreAsync(string nombre, int idIgnorar = 0)
        {
            const string consulta = @"
                SELECT EXISTS (
                    SELECT 1
                    FROM roles
                    WHERE UPPER(TRIM(nombre)) = UPPER(TRIM(@nombre))
                      AND (@idIgnorar = 0 OR id_rol <> @idIgnorar)
                );";

            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("nombre", nombre.Trim());
            comando.Parameters.AddWithValue("idIgnorar", idIgnorar);
            return Convert.ToBoolean(await comando.ExecuteScalarAsync());
        }

        public static async Task GuardarAsync(Rol rol)
        {
            const string consulta = @"
                INSERT INTO roles (nombre, descripcion, estado)
                VALUES (@nombre, @descripcion, @estado);";

            await EjecutarGuardadoAsync(consulta, rol);
        }

        public static async Task ActualizarAsync(Rol rol)
        {
            const string consulta = @"
                UPDATE roles
                SET nombre = @nombre,
                    descripcion = @descripcion,
                    estado = @estado
                WHERE id_rol = @idRol;";

            await EjecutarGuardadoAsync(consulta, rol);
        }

        public static async Task<bool> TieneUsuariosActivosAsync(int idRol)
        {
            const string consulta = @"
                SELECT EXISTS (
                    SELECT 1
                    FROM usuarios
                    WHERE id_rol = @idRol
                      AND activo = TRUE
                );";

            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("idRol", idRol);
            return Convert.ToBoolean(await comando.ExecuteScalarAsync());
        }

        private static async Task EjecutarGuardadoAsync(string consulta, Rol rol)
        {
            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("nombre", rol.Nombre.Trim());
            comando.Parameters.Add("descripcion", NpgsqlDbType.Varchar).Value =
                string.IsNullOrWhiteSpace(rol.Descripcion)
                    ? DBNull.Value
                    : rol.Descripcion.Trim();
            comando.Parameters.AddWithValue("estado", rol.Estado);

            if (rol.IdRol > 0)
            {
                comando.Parameters.AddWithValue("idRol", rol.IdRol);
            }

            await comando.ExecuteNonQueryAsync();
        }
    }
}

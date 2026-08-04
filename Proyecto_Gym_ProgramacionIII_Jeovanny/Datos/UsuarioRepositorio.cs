using Npgsql;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Datos
{
    public static class UsuarioRepositorio
    {
        public static async Task<bool> HayUsuariosAsync()
        {
            const string consulta = "SELECT EXISTS (SELECT 1 FROM usuarios);";

            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();

            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            object? resultado = await comando.ExecuteScalarAsync();

            return resultado is bool hayUsuarios && hayUsuarios;
        }

        public static async Task<bool> ExisteNombreUsuarioAsync(string nombreUsuario)
        {
            const string consulta = @"
                SELECT EXISTS (
                    SELECT 1
                    FROM usuarios
                    WHERE LOWER(nombre_usuario) = LOWER(@nombreUsuario)
                );";

            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();

            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("nombreUsuario", nombreUsuario.Trim());
            object? resultado = await comando.ExecuteScalarAsync();

            return resultado is bool existe && existe;
        }

        public static async Task<int> CrearAdministradorAsync(
            string nombreUsuario,
            string nombreCompleto,
            string contrasenaHash,
            string contrasenaSalt)
        {
            const string consulta = @"
                INSERT INTO usuarios (
                    nombre_usuario,
                    nombre_completo,
                    contrasena_hash,
                    contrasena_salt,
                    id_rol,
                    activo
                )
                SELECT
                    @nombreUsuario,
                    @nombreCompleto,
                    @contrasenaHash,
                    @contrasenaSalt,
                    id_rol,
                    TRUE
                FROM roles
                WHERE nombre = 'ADMIN'
                RETURNING id_usuario;";

            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();

            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("nombreUsuario", nombreUsuario.Trim());
            comando.Parameters.AddWithValue("nombreCompleto", nombreCompleto.Trim());
            comando.Parameters.AddWithValue("contrasenaHash", contrasenaHash);
            comando.Parameters.AddWithValue("contrasenaSalt", contrasenaSalt);

            object? resultado = await comando.ExecuteScalarAsync();

            if (resultado is null)
            {
                throw new InvalidOperationException("No se encontró el rol ADMIN en la base de datos.");
            }

            return Convert.ToInt32(resultado);
        }
    }
}

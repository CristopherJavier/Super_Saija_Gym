using Npgsql;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

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

        public static async Task<bool> ExisteNombreUsuarioAsync(string nombreUsuario, int idIgnorar = 0)
        {
            const string consulta = @"
                SELECT EXISTS (
                    SELECT 1
                    FROM usuarios
                    WHERE LOWER(nombre_usuario) = LOWER(@nombreUsuario)
                      AND (@idIgnorar = 0 OR id_usuario <> @idIgnorar)
                );";

            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();

            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("nombreUsuario", nombreUsuario.Trim());
            comando.Parameters.AddWithValue("idIgnorar", idIgnorar);
            object? resultado = await comando.ExecuteScalarAsync();

            return resultado is bool existe && existe;
        }

        public static async Task<Usuario?> ObtenerPorNombreUsuarioAsync(string nombreUsuario)
        {
            const string consulta = @"
                SELECT
                    u.id_usuario,
                    u.nombre_usuario,
                    u.nombre_completo,
                    u.contrasena_hash,
                    u.contrasena_salt,
                    u.id_rol,
                    r.nombre AS nombre_rol,
                    r.estado AS rol_activo,
                    u.activo,
                    u.fecha_creacion
                FROM usuarios u
                INNER JOIN roles r ON u.id_rol = r.id_rol
                WHERE LOWER(u.nombre_usuario) = LOWER(@nombreUsuario)
                LIMIT 1;";

            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();

            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("nombreUsuario", nombreUsuario);

            using NpgsqlDataReader lector = await comando.ExecuteReaderAsync();

            if (!await lector.ReadAsync())
            {
                return null;
            }

            return new Usuario
            {
                IdUsuario = lector.GetInt32(lector.GetOrdinal("id_usuario")),
                NombreUsuario = lector.GetString(lector.GetOrdinal("nombre_usuario")),
                NombreCompleto = lector.GetString(lector.GetOrdinal("nombre_completo")),
                ContrasenaHash = lector.GetString(lector.GetOrdinal("contrasena_hash")),
                ContrasenaSalt = lector.GetString(lector.GetOrdinal("contrasena_salt")),
                IdRol = lector.GetInt32(lector.GetOrdinal("id_rol")),
                NombreRol = lector.GetString(lector.GetOrdinal("nombre_rol")),
                RolActivo = lector.GetBoolean(lector.GetOrdinal("rol_activo")),
                Activo = lector.GetBoolean(lector.GetOrdinal("activo")),
                FechaCreacion = lector.GetDateTime(lector.GetOrdinal("fecha_creacion"))
            };
        }

        public static async Task<Usuario?> ObtenerPorIdAsync(int idUsuario)
        {
            const string consulta = @"
                SELECT
                    u.id_usuario,
                    u.nombre_usuario,
                    u.nombre_completo,
                    u.contrasena_hash,
                    u.contrasena_salt,
                    u.id_rol,
                    r.nombre AS nombre_rol,
                    r.estado AS rol_activo,
                    u.activo,
                    u.fecha_creacion
                FROM usuarios u
                INNER JOIN roles r ON u.id_rol = r.id_rol
                WHERE u.id_usuario = @idUsuario
                LIMIT 1;";

            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("idUsuario", idUsuario);
            using NpgsqlDataReader lector = await comando.ExecuteReaderAsync();

            if (!await lector.ReadAsync())
            {
                return null;
            }

            return LeerUsuario(lector);
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

        public static async Task<List<Usuario>> ListarAsync(string texto = "")
        {
            const string consulta = @"
                SELECT
                    u.id_usuario,
                    u.nombre_usuario,
                    u.nombre_completo,
                    u.contrasena_hash,
                    u.contrasena_salt,
                    u.id_rol,
                    r.nombre AS nombre_rol,
                    r.estado AS rol_activo,
                    u.activo,
                    u.fecha_creacion
                FROM usuarios u
                INNER JOIN roles r ON r.id_rol = u.id_rol
                WHERE @texto = ''
                   OR u.nombre_usuario ILIKE @busqueda
                   OR u.nombre_completo ILIKE @busqueda
                   OR r.nombre ILIKE @busqueda
                ORDER BY u.activo DESC, u.nombre_completo;";

            List<Usuario> usuarios = new List<Usuario>();
            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("texto", texto.Trim());
            comando.Parameters.AddWithValue("busqueda", $"%{texto.Trim()}%");
            using NpgsqlDataReader lector = await comando.ExecuteReaderAsync();

            while (await lector.ReadAsync())
            {
                usuarios.Add(LeerUsuario(lector));
            }

            return usuarios;
        }

        public static async Task<int> GuardarAsync(
            Usuario usuario,
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
                VALUES (
                    @nombreUsuario,
                    @nombreCompleto,
                    @contrasenaHash,
                    @contrasenaSalt,
                    @idRol,
                    @activo
                )
                RETURNING id_usuario;";

            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("nombreUsuario", usuario.NombreUsuario.Trim());
            comando.Parameters.AddWithValue("nombreCompleto", usuario.NombreCompleto.Trim());
            comando.Parameters.AddWithValue("contrasenaHash", contrasenaHash);
            comando.Parameters.AddWithValue("contrasenaSalt", contrasenaSalt);
            comando.Parameters.AddWithValue("idRol", usuario.IdRol);
            comando.Parameters.AddWithValue("activo", usuario.Activo);
            object? resultado = await comando.ExecuteScalarAsync();
            return Convert.ToInt32(resultado);
        }

        public static async Task ActualizarAsync(Usuario usuario)
        {
            const string consulta = @"
                UPDATE usuarios
                SET nombre_usuario = @nombreUsuario,
                    nombre_completo = @nombreCompleto,
                    id_rol = @idRol,
                    activo = @activo
                WHERE id_usuario = @idUsuario;";

            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("nombreUsuario", usuario.NombreUsuario.Trim());
            comando.Parameters.AddWithValue("nombreCompleto", usuario.NombreCompleto.Trim());
            comando.Parameters.AddWithValue("idRol", usuario.IdRol);
            comando.Parameters.AddWithValue("activo", usuario.Activo);
            comando.Parameters.AddWithValue("idUsuario", usuario.IdUsuario);
            await comando.ExecuteNonQueryAsync();
        }

        public static async Task ActualizarContrasenaAsync(
            int idUsuario,
            string contrasenaHash,
            string contrasenaSalt)
        {
            const string consulta = @"
                UPDATE usuarios
                SET contrasena_hash = @contrasenaHash,
                    contrasena_salt = @contrasenaSalt
                WHERE id_usuario = @idUsuario;";

            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("contrasenaHash", contrasenaHash);
            comando.Parameters.AddWithValue("contrasenaSalt", contrasenaSalt);
            comando.Parameters.AddWithValue("idUsuario", idUsuario);
            await comando.ExecuteNonQueryAsync();
        }

        public static async Task<List<string>> ListarClavesPermisosAsync(int idUsuario)
        {
            const string consulta = @"
                SELECT p.clave
                FROM usuarios u
                INNER JOIN roles r ON r.id_rol = u.id_rol
                INNER JOIN roles_permisos rp ON rp.id_rol = r.id_rol
                INNER JOIN permisos p ON p.id_permiso = rp.id_permiso
                WHERE u.id_usuario = @idUsuario
                  AND u.activo = TRUE
                  AND r.estado = TRUE
                  AND p.estado = TRUE
                ORDER BY p.clave;";

            List<string> claves = new List<string>();
            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("idUsuario", idUsuario);
            using NpgsqlDataReader lector = await comando.ExecuteReaderAsync();

            while (await lector.ReadAsync())
            {
                claves.Add(lector.GetString(lector.GetOrdinal("clave")));
            }

            return claves;
        }

        public static async Task<bool> EsUltimoAdministradorActivoAsync(int idUsuario)
        {
            const string consulta = @"
                SELECT
                    EXISTS (
                        SELECT 1
                        FROM usuarios u
                        INNER JOIN roles r ON r.id_rol = u.id_rol
                        WHERE u.id_usuario = @idUsuario
                          AND u.activo = TRUE
                          AND r.nombre = 'ADMIN'
                    )
                    AND (
                        SELECT COUNT(*)
                        FROM usuarios u
                        INNER JOIN roles r ON r.id_rol = u.id_rol
                        WHERE u.activo = TRUE
                          AND r.nombre = 'ADMIN'
                    ) = 1;";

            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("idUsuario", idUsuario);
            object? resultado = await comando.ExecuteScalarAsync();
            return resultado is bool esUltimo && esUltimo;
        }

        private static Usuario LeerUsuario(NpgsqlDataReader lector)
        {
            return new Usuario
            {
                IdUsuario = lector.GetInt32(lector.GetOrdinal("id_usuario")),
                NombreUsuario = lector.GetString(lector.GetOrdinal("nombre_usuario")),
                NombreCompleto = lector.GetString(lector.GetOrdinal("nombre_completo")),
                ContrasenaHash = lector.GetString(lector.GetOrdinal("contrasena_hash")),
                ContrasenaSalt = lector.GetString(lector.GetOrdinal("contrasena_salt")),
                IdRol = lector.GetInt32(lector.GetOrdinal("id_rol")),
                NombreRol = lector.GetString(lector.GetOrdinal("nombre_rol")),
                RolActivo = lector.GetBoolean(lector.GetOrdinal("rol_activo")),
                Activo = lector.GetBoolean(lector.GetOrdinal("activo")),
                FechaCreacion = lector.GetDateTime(lector.GetOrdinal("fecha_creacion"))
            };
        }
    }
}

using Npgsql;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Datos
{
    public static class RolPermisoRepositorio
    {
        public static async Task<List<int>> ListarIdsPermisosAsync(int idRol)
        {
            const string consulta = @"
                SELECT id_permiso
                FROM roles_permisos
                WHERE id_rol = @idRol
                ORDER BY id_permiso;";

            List<int> idsPermisos = new List<int>();
            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("idRol", idRol);
            using NpgsqlDataReader lector = await comando.ExecuteReaderAsync();

            while (await lector.ReadAsync())
            {
                idsPermisos.Add(lector.GetInt32(lector.GetOrdinal("id_permiso")));
            }

            return idsPermisos;
        }

        public static async Task GuardarAsync(int idRol, List<int> idsPermisos)
        {
            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlTransaction transaccion = await conexion.BeginTransactionAsync();

            try
            {
                const string consultaRol = @"
                    SELECT nombre
                    FROM roles
                    WHERE id_rol = @idRol
                    FOR UPDATE;";

                string nombreRol;
                using (NpgsqlCommand comando = new NpgsqlCommand(consultaRol, conexion, transaccion))
                {
                    comando.Parameters.AddWithValue("idRol", idRol);
                    object? resultado = await comando.ExecuteScalarAsync();

                    if (resultado is null)
                    {
                        throw new InvalidOperationException("El rol seleccionado ya no existe.");
                    }

                    nombreRol = Convert.ToString(resultado) ?? string.Empty;
                }

                if (nombreRol.Equals("ADMIN", StringComparison.OrdinalIgnoreCase))
                {
                    throw new InvalidOperationException("El rol ADMIN siempre conserva acceso completo.");
                }

                const string eliminar = @"
                    DELETE FROM roles_permisos
                    WHERE id_rol = @idRol;";

                using (NpgsqlCommand comando = new NpgsqlCommand(eliminar, conexion, transaccion))
                {
                    comando.Parameters.AddWithValue("idRol", idRol);
                    await comando.ExecuteNonQueryAsync();
                }

                const string insertar = @"
                    INSERT INTO roles_permisos (id_rol, id_permiso)
                    SELECT @idRol, id_permiso
                    FROM permisos
                    WHERE id_permiso = @idPermiso
                      AND estado = TRUE
                    ON CONFLICT DO NOTHING;";

                foreach (int idPermiso in idsPermisos.Distinct())
                {
                    using NpgsqlCommand comando = new NpgsqlCommand(insertar, conexion, transaccion);
                    comando.Parameters.AddWithValue("idRol", idRol);
                    comando.Parameters.AddWithValue("idPermiso", idPermiso);
                    await comando.ExecuteNonQueryAsync();
                }

                await transaccion.CommitAsync();
            }
            catch
            {
                await transaccion.RollbackAsync();
                throw;
            }
        }
    }
}

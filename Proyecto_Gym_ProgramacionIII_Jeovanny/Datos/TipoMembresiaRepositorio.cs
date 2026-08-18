using Npgsql;
using NpgsqlTypes;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Datos
{
    public static class TipoMembresiaRepositorio
    {
        public static async Task<List<TipoMembresia>> ListarAsync(string texto = "")
        {
            const string consulta = @"
                SELECT id_tipo_membresia, nombre, descripcion, duracion_dias, precio, estado
                FROM tipos_membresias
                WHERE @texto = ''
                   OR nombre ILIKE @busqueda
                   OR descripcion ILIKE @busqueda
                ORDER BY estado DESC, nombre;";

            List<TipoMembresia> tipos = new List<TipoMembresia>();

            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("texto", texto.Trim());
            comando.Parameters.AddWithValue("busqueda", $"%{texto.Trim()}%");
            using NpgsqlDataReader lector = await comando.ExecuteReaderAsync();

            while (await lector.ReadAsync())
            {
                int indiceDescripcion = lector.GetOrdinal("descripcion");
                tipos.Add(new TipoMembresia
                {
                    IdTipoMembresia = lector.GetInt32(lector.GetOrdinal("id_tipo_membresia")),
                    Nombre = lector.GetString(lector.GetOrdinal("nombre")),
                    Descripcion = lector.IsDBNull(indiceDescripcion) ? string.Empty : lector.GetString(indiceDescripcion),
                    DuracionDias = lector.GetInt32(lector.GetOrdinal("duracion_dias")),
                    Precio = lector.GetDecimal(lector.GetOrdinal("precio")),
                    Estado = lector.GetBoolean(lector.GetOrdinal("estado"))
                });
            }

            return tipos;
        }

        public static async Task<bool> ExisteNombreAsync(string nombre, int idIgnorar = 0)
        {
            const string consulta = @"
                SELECT EXISTS (
                    SELECT 1 FROM tipos_membresias
                    WHERE UPPER(TRIM(nombre)) = UPPER(TRIM(@nombre))
                      AND (@idIgnorar = 0 OR id_tipo_membresia <> @idIgnorar)
                );";

            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("nombre", nombre.Trim());
            comando.Parameters.AddWithValue("idIgnorar", idIgnorar);
            object? resultado = await comando.ExecuteScalarAsync();
            return resultado is bool existe && existe;
        }

        public static async Task GuardarAsync(TipoMembresia tipo)
        {
            const string consulta = @"
                INSERT INTO tipos_membresias (nombre, descripcion, duracion_dias, precio, estado)
                VALUES (@nombre, @descripcion, @duracionDias, @precio, @estado);";

            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            AgregarParametros(comando, tipo);
            await comando.ExecuteNonQueryAsync();
        }

        public static async Task ActualizarAsync(TipoMembresia tipo)
        {
            const string consulta = @"
                UPDATE tipos_membresias
                SET nombre = @nombre,
                    descripcion = @descripcion,
                    duracion_dias = @duracionDias,
                    precio = @precio,
                    estado = @estado
                WHERE id_tipo_membresia = @idTipoMembresia;";

            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            AgregarParametros(comando, tipo);
            comando.Parameters.AddWithValue("idTipoMembresia", tipo.IdTipoMembresia);
            await comando.ExecuteNonQueryAsync();
        }

        public static async Task CambiarEstadoAsync(int idTipoMembresia, bool estado)
        {
            const string consulta = @"
                UPDATE tipos_membresias
                SET estado = @estado
                WHERE id_tipo_membresia = @idTipoMembresia;";

            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("estado", estado);
            comando.Parameters.AddWithValue("idTipoMembresia", idTipoMembresia);
            await comando.ExecuteNonQueryAsync();
        }

        private static void AgregarParametros(NpgsqlCommand comando, TipoMembresia tipo)
        {
            comando.Parameters.AddWithValue("nombre", tipo.Nombre.Trim());
            comando.Parameters.Add("descripcion", NpgsqlDbType.Varchar).Value = string.IsNullOrWhiteSpace(tipo.Descripcion)
                ? DBNull.Value
                : tipo.Descripcion.Trim();
            comando.Parameters.AddWithValue("duracionDias", tipo.DuracionDias);
            comando.Parameters.AddWithValue("precio", tipo.Precio);
            comando.Parameters.AddWithValue("estado", tipo.Estado);
        }
    }
}

using Npgsql;
using NpgsqlTypes;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Datos
{
    public static class MetodoPagoRepositorio
    {
        public static async Task<List<MetodoPago>> ListarAsync(string texto = "", bool soloActivos = false)
        {
            const string consulta = @"
                SELECT id_metodo_pago, nombre, descripcion, estado
                FROM metodos_pago
                WHERE (@texto = '' OR nombre ILIKE @busqueda OR descripcion ILIKE @busqueda)
                  AND (@soloActivos = FALSE OR estado = TRUE)
                ORDER BY estado DESC, nombre;";

            List<MetodoPago> metodos = new List<MetodoPago>();
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
                metodos.Add(new MetodoPago
                {
                    IdMetodoPago = lector.GetInt32(lector.GetOrdinal("id_metodo_pago")),
                    Nombre = lector.GetString(lector.GetOrdinal("nombre")),
                    Descripcion = lector.IsDBNull(indiceDescripcion) ? string.Empty : lector.GetString(indiceDescripcion),
                    Estado = lector.GetBoolean(lector.GetOrdinal("estado"))
                });
            }

            return metodos;
        }

        public static async Task<bool> ExisteNombreAsync(string nombre, int idIgnorar = 0)
        {
            const string consulta = @"
                SELECT EXISTS (
                    SELECT 1
                    FROM metodos_pago
                    WHERE UPPER(TRIM(nombre)) = UPPER(TRIM(@nombre))
                      AND (@idIgnorar = 0 OR id_metodo_pago <> @idIgnorar)
                );";

            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("nombre", nombre.Trim());
            comando.Parameters.AddWithValue("idIgnorar", idIgnorar);
            object? resultado = await comando.ExecuteScalarAsync();
            return resultado is bool existe && existe;
        }

        public static async Task GuardarAsync(MetodoPago metodo)
        {
            const string consulta = @"
                INSERT INTO metodos_pago (nombre, descripcion, estado)
                VALUES (@nombre, @descripcion, @estado);";

            await EjecutarGuardadoAsync(consulta, metodo);
        }

        public static async Task ActualizarAsync(MetodoPago metodo)
        {
            const string consulta = @"
                UPDATE metodos_pago
                SET nombre = @nombre,
                    descripcion = @descripcion,
                    estado = @estado
                WHERE id_metodo_pago = @idMetodoPago;";

            await EjecutarGuardadoAsync(consulta, metodo);
        }

        private static async Task EjecutarGuardadoAsync(string consulta, MetodoPago metodo)
        {
            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("nombre", metodo.Nombre.Trim());
            comando.Parameters.Add("descripcion", NpgsqlDbType.Varchar).Value = string.IsNullOrWhiteSpace(metodo.Descripcion)
                ? DBNull.Value
                : metodo.Descripcion.Trim();
            comando.Parameters.AddWithValue("estado", metodo.Estado);

            if (metodo.IdMetodoPago > 0)
            {
                comando.Parameters.AddWithValue("idMetodoPago", metodo.IdMetodoPago);
            }

            await comando.ExecuteNonQueryAsync();
        }
    }
}

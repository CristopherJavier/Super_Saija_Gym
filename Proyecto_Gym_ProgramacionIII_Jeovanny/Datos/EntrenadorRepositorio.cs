using Npgsql;
using NpgsqlTypes;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Datos
{
    public static class EntrenadorRepositorio
    {
        public static async Task<List<Entrenador>> ListarAsync()
        {
            const string consulta = @"
                SELECT id_entrenador, nombre, apellido, cedula, telefono,
                       correo, especialidad, fecha_contratacion, estado
                FROM entrenadores
                ORDER BY estado DESC, nombre, apellido;";

            return await EjecutarListadoAsync(consulta, null);
        }

        public static async Task<List<Entrenador>> BuscarAsync(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
            {
                return await ListarAsync();
            }

            const string consulta = @"
                SELECT id_entrenador, nombre, apellido, cedula, telefono,
                       correo, especialidad, fecha_contratacion, estado
                FROM entrenadores
                WHERE nombre ILIKE @texto
                   OR apellido ILIKE @texto
                   OR cedula ILIKE @texto
                   OR telefono ILIKE @texto
                   OR correo ILIKE @texto
                   OR especialidad ILIKE @texto
                ORDER BY estado DESC, nombre, apellido;";

            return await EjecutarListadoAsync(consulta, $"%{texto.Trim()}%");
        }

        public static async Task<bool> ExisteCedulaAsync(string cedula, int idEntrenadorIgnorar = 0)
        {
            const string consulta = @"
                SELECT EXISTS (
                    SELECT 1
                    FROM entrenadores
                    WHERE REPLACE(cedula, '-', '') = @cedula
                      AND (@idEntrenadorIgnorar = 0 OR id_entrenador <> @idEntrenadorIgnorar)
                );";

            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();

            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("cedula", cedula.Trim());
            comando.Parameters.AddWithValue("idEntrenadorIgnorar", idEntrenadorIgnorar);

            object? resultado = await comando.ExecuteScalarAsync();
            return resultado is bool existe && existe;
        }

        public static async Task<int> GuardarAsync(Entrenador entrenador)
        {
            const string consulta = @"
                INSERT INTO entrenadores (
                    nombre, apellido, cedula, telefono, correo,
                    especialidad, fecha_contratacion, estado
                )
                VALUES (
                    @nombre, @apellido, @cedula, @telefono, @correo,
                    @especialidad, @fechaContratacion, @estado
                )
                RETURNING id_entrenador;";

            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();

            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            AgregarParametros(comando, entrenador);
            object? resultado = await comando.ExecuteScalarAsync();

            if (resultado is null)
            {
                throw new InvalidOperationException("No fue posible obtener el identificador del entrenador.");
            }

            return Convert.ToInt32(resultado);
        }

        public static async Task ActualizarAsync(Entrenador entrenador)
        {
            const string consulta = @"
                UPDATE entrenadores
                SET nombre = @nombre,
                    apellido = @apellido,
                    cedula = @cedula,
                    telefono = @telefono,
                    correo = @correo,
                    especialidad = @especialidad,
                    fecha_contratacion = @fechaContratacion,
                    estado = @estado
                WHERE id_entrenador = @idEntrenador;";

            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();

            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            AgregarParametros(comando, entrenador);
            comando.Parameters.AddWithValue("idEntrenador", entrenador.IdEntrenador);
            await comando.ExecuteNonQueryAsync();
        }

        public static async Task CambiarEstadoAsync(int idEntrenador, bool nuevoEstado)
        {
            const string consulta = @"
                UPDATE entrenadores
                SET estado = @nuevoEstado
                WHERE id_entrenador = @idEntrenador;";

            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();

            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("nuevoEstado", nuevoEstado);
            comando.Parameters.AddWithValue("idEntrenador", idEntrenador);
            await comando.ExecuteNonQueryAsync();
        }

        private static async Task<List<Entrenador>> EjecutarListadoAsync(string consulta, string? texto)
        {
            List<Entrenador> entrenadores = new List<Entrenador>();

            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();

            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);

            if (texto is not null)
            {
                comando.Parameters.AddWithValue("texto", texto);
            }

            using NpgsqlDataReader lector = await comando.ExecuteReaderAsync();

            while (await lector.ReadAsync())
            {
                int indiceCorreo = lector.GetOrdinal("correo");
                entrenadores.Add(new Entrenador
                {
                    IdEntrenador = lector.GetInt32(lector.GetOrdinal("id_entrenador")),
                    Nombre = lector.GetString(lector.GetOrdinal("nombre")),
                    Apellido = lector.GetString(lector.GetOrdinal("apellido")),
                    Cedula = lector.GetString(lector.GetOrdinal("cedula")),
                    Telefono = lector.GetString(lector.GetOrdinal("telefono")),
                    Correo = lector.IsDBNull(indiceCorreo) ? string.Empty : lector.GetString(indiceCorreo),
                    Especialidad = lector.GetString(lector.GetOrdinal("especialidad")),
                    FechaContratacion = lector.GetDateTime(lector.GetOrdinal("fecha_contratacion")),
                    Estado = lector.GetBoolean(lector.GetOrdinal("estado"))
                });
            }

            return entrenadores;
        }

        private static void AgregarParametros(NpgsqlCommand comando, Entrenador entrenador)
        {
            comando.Parameters.AddWithValue("nombre", entrenador.Nombre.Trim());
            comando.Parameters.AddWithValue("apellido", entrenador.Apellido.Trim());
            comando.Parameters.AddWithValue("cedula", entrenador.Cedula.Trim());
            comando.Parameters.AddWithValue("telefono", entrenador.Telefono.Trim());
            comando.Parameters.Add("correo", NpgsqlDbType.Varchar).Value = string.IsNullOrWhiteSpace(entrenador.Correo)
                ? DBNull.Value
                : entrenador.Correo.Trim();
            comando.Parameters.AddWithValue("especialidad", entrenador.Especialidad.Trim());
            comando.Parameters.Add("fechaContratacion", NpgsqlDbType.Date).Value = entrenador.FechaContratacion.Date;
            comando.Parameters.AddWithValue("estado", entrenador.Estado);
        }
    }
}

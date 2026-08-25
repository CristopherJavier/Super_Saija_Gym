using Npgsql;
using NpgsqlTypes;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Datos
{
    public static class AsistenciaRepositorio
    {
        public static async Task<Asistencia> RegistrarAsync(Asistencia asistencia)
        {
            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlTransaction transaccion = await conexion.BeginTransactionAsync();

            try
            {
                const string consultaCliente = @"
                    SELECT nombre || ' ' || apellido AS nombre_cliente, estado
                    FROM clientes
                    WHERE id_cliente = @idCliente
                    FOR UPDATE;";

                using (NpgsqlCommand comando = new NpgsqlCommand(consultaCliente, conexion, transaccion))
                {
                    comando.Parameters.AddWithValue("idCliente", asistencia.IdCliente);
                    using NpgsqlDataReader lector = await comando.ExecuteReaderAsync();

                    if (!await lector.ReadAsync() || !lector.GetBoolean(lector.GetOrdinal("estado")))
                    {
                        throw new InvalidOperationException("El cliente seleccionado no está activo.");
                    }

                    asistencia.NombreCliente = lector.GetString(lector.GetOrdinal("nombre_cliente"));
                }

                if (asistencia.IdReserva.HasValue)
                {
                    const string consultaReserva = @"
                        SELECT cl.nombre AS nombre_clase,
                               r.id_cliente,
                               r.fecha_clase,
                               r.estado
                        FROM reservas_clases r
                        INNER JOIN horarios_clases h ON h.id_horario = r.id_horario
                        INNER JOIN clases_actividades cl ON cl.id_clase = h.id_clase
                        WHERE r.id_reserva = @idReserva
                        FOR UPDATE OF r;";

                    using NpgsqlCommand comando = new NpgsqlCommand(consultaReserva, conexion, transaccion);
                    comando.Parameters.AddWithValue("idReserva", asistencia.IdReserva.Value);
                    using NpgsqlDataReader lector = await comando.ExecuteReaderAsync();

                    if (!await lector.ReadAsync())
                    {
                        throw new InvalidOperationException("La reserva seleccionada ya no existe.");
                    }

                    if (!lector.GetBoolean(lector.GetOrdinal("estado"))
                        || lector.GetInt32(lector.GetOrdinal("id_cliente")) != asistencia.IdCliente
                        || lector.GetDateTime(lector.GetOrdinal("fecha_clase")).Date != DateTime.Today)
                    {
                        throw new InvalidOperationException("La reserva seleccionada no es válida para la asistencia de hoy.");
                    }

                    asistencia.NombreClase = lector.GetString(lector.GetOrdinal("nombre_clase"));
                }

                const string insercion = @"
                    INSERT INTO asistencias (
                        id_cliente,
                        id_reserva,
                        id_usuario
                    )
                    VALUES (
                        @idCliente,
                        @idReserva,
                        @idUsuario
                    )
                    RETURNING id_asistencia, fecha_hora;";

                using (NpgsqlCommand comando = new NpgsqlCommand(insercion, conexion, transaccion))
                {
                    comando.Parameters.AddWithValue("idCliente", asistencia.IdCliente);
                    comando.Parameters.Add("idReserva", NpgsqlDbType.Integer).Value = asistencia.IdReserva.HasValue
                        ? asistencia.IdReserva.Value
                        : DBNull.Value;
                    comando.Parameters.AddWithValue("idUsuario", asistencia.IdUsuario);
                    using NpgsqlDataReader lector = await comando.ExecuteReaderAsync();
                    await lector.ReadAsync();
                    asistencia.IdAsistencia = lector.GetInt32(lector.GetOrdinal("id_asistencia"));
                    asistencia.FechaHora = lector.GetDateTime(lector.GetOrdinal("fecha_hora"));
                }

                await transaccion.CommitAsync();
                return asistencia;
            }
            catch
            {
                await transaccion.RollbackAsync();
                throw;
            }
        }

        public static async Task<List<Asistencia>> ListarAsync(DateTime? fecha = null)
        {
            const string consulta = @"
                SELECT a.id_asistencia,
                       a.id_cliente,
                       c.nombre || ' ' || c.apellido AS nombre_cliente,
                       a.id_reserva,
                       COALESCE(cl.nombre, '') AS nombre_clase,
                       a.fecha_hora,
                       a.id_usuario,
                       u.nombre_completo AS nombre_usuario
                FROM asistencias a
                INNER JOIN clientes c ON c.id_cliente = a.id_cliente
                LEFT JOIN reservas_clases r ON r.id_reserva = a.id_reserva
                LEFT JOIN horarios_clases h ON h.id_horario = r.id_horario
                LEFT JOIN clases_actividades cl ON cl.id_clase = h.id_clase
                INNER JOIN usuarios u ON u.id_usuario = a.id_usuario
                WHERE (@fecha IS NULL OR a.fecha_hora::DATE = @fecha)
                ORDER BY a.fecha_hora DESC;";

            List<Asistencia> asistencias = new List<Asistencia>();
            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            comando.Parameters.Add("fecha", NpgsqlDbType.Date).Value = fecha.HasValue
                ? fecha.Value.Date
                : DBNull.Value;
            using NpgsqlDataReader lector = await comando.ExecuteReaderAsync();

            while (await lector.ReadAsync())
            {
                int indiceReserva = lector.GetOrdinal("id_reserva");
                asistencias.Add(new Asistencia
                {
                    IdAsistencia = lector.GetInt32(lector.GetOrdinal("id_asistencia")),
                    IdCliente = lector.GetInt32(lector.GetOrdinal("id_cliente")),
                    NombreCliente = lector.GetString(lector.GetOrdinal("nombre_cliente")),
                    IdReserva = lector.IsDBNull(indiceReserva) ? null : lector.GetInt32(indiceReserva),
                    NombreClase = lector.GetString(lector.GetOrdinal("nombre_clase")),
                    FechaHora = lector.GetDateTime(lector.GetOrdinal("fecha_hora")),
                    IdUsuario = lector.GetInt32(lector.GetOrdinal("id_usuario")),
                    NombreUsuario = lector.GetString(lector.GetOrdinal("nombre_usuario"))
                });
            }

            return asistencias;
        }
    }
}

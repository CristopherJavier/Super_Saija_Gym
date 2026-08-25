using Npgsql;
using NpgsqlTypes;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Datos
{
    public static class ReservaClaseRepositorio
    {
        public static async Task<int> GuardarAsync(ReservaClase reserva)
        {
            if (reserva.FechaClase.Date < DateTime.Today)
            {
                throw new InvalidOperationException("La fecha de la clase no puede ser anterior a la fecha actual.");
            }

            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlTransaction transaccion = await conexion.BeginTransactionAsync();

            try
            {
                const string consultaCliente = @"
                    SELECT estado
                    FROM clientes
                    WHERE id_cliente = @idCliente
                    FOR UPDATE;";

                using (NpgsqlCommand comando = new NpgsqlCommand(consultaCliente, conexion, transaccion))
                {
                    comando.Parameters.AddWithValue("idCliente", reserva.IdCliente);
                    object? estado = await comando.ExecuteScalarAsync();

                    if (estado is not bool activo || !activo)
                    {
                        throw new InvalidOperationException("El cliente seleccionado no está activo.");
                    }
                }

                const string consultaHorario = @"
                    SELECT h.dia_semana,
                           h.estado AS horario_activo,
                           c.cupo_maximo,
                           c.estado AS clase_activa
                    FROM horarios_clases h
                    INNER JOIN clases_actividades c ON c.id_clase = h.id_clase
                    WHERE h.id_horario = @idHorario
                    FOR UPDATE OF h;";

                string diaSemana;
                int cupoMaximo;

                using (NpgsqlCommand comando = new NpgsqlCommand(consultaHorario, conexion, transaccion))
                {
                    comando.Parameters.AddWithValue("idHorario", reserva.IdHorario);
                    using NpgsqlDataReader lector = await comando.ExecuteReaderAsync();

                    if (!await lector.ReadAsync())
                    {
                        throw new InvalidOperationException("El horario seleccionado ya no existe.");
                    }

                    if (!lector.GetBoolean(lector.GetOrdinal("horario_activo"))
                        || !lector.GetBoolean(lector.GetOrdinal("clase_activa")))
                    {
                        throw new InvalidOperationException("El horario seleccionado no está activo.");
                    }

                    diaSemana = lector.GetString(lector.GetOrdinal("dia_semana"));
                    cupoMaximo = lector.GetInt32(lector.GetOrdinal("cupo_maximo"));
                }

                if (diaSemana != ObtenerDiaSemana(reserva.FechaClase.DayOfWeek))
                {
                    throw new InvalidOperationException("La fecha seleccionada no corresponde al día del horario.");
                }

                const string consultaDuplicada = @"
                    SELECT EXISTS (
                        SELECT 1
                        FROM reservas_clases
                        WHERE id_cliente = @idCliente
                          AND id_horario = @idHorario
                          AND fecha_clase = @fechaClase
                    );";

                using (NpgsqlCommand comando = new NpgsqlCommand(consultaDuplicada, conexion, transaccion))
                {
                    comando.Parameters.AddWithValue("idCliente", reserva.IdCliente);
                    comando.Parameters.AddWithValue("idHorario", reserva.IdHorario);
                    comando.Parameters.AddWithValue("fechaClase", reserva.FechaClase.Date);

                    if (Convert.ToBoolean(await comando.ExecuteScalarAsync()))
                    {
                        throw new InvalidOperationException("El cliente ya tiene una reserva para esa clase y fecha.");
                    }
                }

                const string consultaCupo = @"
                    SELECT COUNT(*)
                    FROM reservas_clases
                    WHERE id_horario = @idHorario
                      AND fecha_clase = @fechaClase
                      AND estado = TRUE;";

                using (NpgsqlCommand comando = new NpgsqlCommand(consultaCupo, conexion, transaccion))
                {
                    comando.Parameters.AddWithValue("idHorario", reserva.IdHorario);
                    comando.Parameters.AddWithValue("fechaClase", reserva.FechaClase.Date);
                    int reservados = Convert.ToInt32(await comando.ExecuteScalarAsync());

                    if (reservados >= cupoMaximo)
                    {
                        throw new InvalidOperationException("La clase seleccionada ya alcanzó su cupo máximo.");
                    }
                }

                const string insercion = @"
                    INSERT INTO reservas_clases (
                        id_cliente,
                        id_horario,
                        fecha_clase,
                        estado
                    )
                    VALUES (
                        @idCliente,
                        @idHorario,
                        @fechaClase,
                        TRUE
                    )
                    RETURNING id_reserva;";

                using NpgsqlCommand comandoInsercion = new NpgsqlCommand(insercion, conexion, transaccion);
                comandoInsercion.Parameters.AddWithValue("idCliente", reserva.IdCliente);
                comandoInsercion.Parameters.AddWithValue("idHorario", reserva.IdHorario);
                comandoInsercion.Parameters.AddWithValue("fechaClase", reserva.FechaClase.Date);
                int idReserva = Convert.ToInt32(await comandoInsercion.ExecuteScalarAsync());
                await transaccion.CommitAsync();
                return idReserva;
            }
            catch
            {
                await transaccion.RollbackAsync();
                throw;
            }
        }

        public static async Task<List<ReservaClase>> ListarAsync(DateTime? desde = null)
        {
            const string consulta = @"
                SELECT r.id_reserva,
                       r.id_cliente,
                       c.nombre || ' ' || c.apellido AS nombre_cliente,
                       r.id_horario,
                       cl.nombre AS nombre_clase,
                       e.nombre || ' ' || e.apellido AS nombre_entrenador,
                       r.fecha_clase,
                       r.fecha_reserva,
                       r.estado
                FROM reservas_clases r
                INNER JOIN clientes c ON c.id_cliente = r.id_cliente
                INNER JOIN horarios_clases h ON h.id_horario = r.id_horario
                INNER JOIN clases_actividades cl ON cl.id_clase = h.id_clase
                INNER JOIN entrenadores e ON e.id_entrenador = h.id_entrenador
                WHERE (@desde IS NULL OR r.fecha_clase >= @desde)
                ORDER BY r.fecha_clase DESC, r.fecha_reserva DESC;";

            List<ReservaClase> reservas = new List<ReservaClase>();
            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            comando.Parameters.Add("desde", NpgsqlDbType.Date).Value = desde.HasValue
                ? desde.Value.Date
                : DBNull.Value;
            using NpgsqlDataReader lector = await comando.ExecuteReaderAsync();

            while (await lector.ReadAsync())
            {
                reservas.Add(LeerReserva(lector));
            }

            return reservas;
        }

        public static async Task<List<ReservaClase>> ListarActivasDelClienteAsync(
            int idCliente,
            DateTime fecha)
        {
            const string consulta = @"
                SELECT r.id_reserva,
                       r.id_cliente,
                       c.nombre || ' ' || c.apellido AS nombre_cliente,
                       r.id_horario,
                       cl.nombre AS nombre_clase,
                       e.nombre || ' ' || e.apellido AS nombre_entrenador,
                       r.fecha_clase,
                       r.fecha_reserva,
                       r.estado
                FROM reservas_clases r
                INNER JOIN clientes c ON c.id_cliente = r.id_cliente
                INNER JOIN horarios_clases h ON h.id_horario = r.id_horario
                INNER JOIN clases_actividades cl ON cl.id_clase = h.id_clase
                INNER JOIN entrenadores e ON e.id_entrenador = h.id_entrenador
                WHERE r.id_cliente = @idCliente
                  AND r.fecha_clase = @fecha
                  AND r.estado = TRUE
                  AND NOT EXISTS (
                      SELECT 1
                      FROM asistencias a
                      WHERE a.id_reserva = r.id_reserva
                  )
                ORDER BY h.hora_inicio;";

            List<ReservaClase> reservas = new List<ReservaClase>();
            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("idCliente", idCliente);
            comando.Parameters.AddWithValue("fecha", fecha.Date);
            using NpgsqlDataReader lector = await comando.ExecuteReaderAsync();

            while (await lector.ReadAsync())
            {
                reservas.Add(LeerReserva(lector));
            }

            return reservas;
        }

        private static ReservaClase LeerReserva(NpgsqlDataReader lector)
        {
            return new ReservaClase
            {
                IdReserva = lector.GetInt32(lector.GetOrdinal("id_reserva")),
                IdCliente = lector.GetInt32(lector.GetOrdinal("id_cliente")),
                NombreCliente = lector.GetString(lector.GetOrdinal("nombre_cliente")),
                IdHorario = lector.GetInt32(lector.GetOrdinal("id_horario")),
                NombreClase = lector.GetString(lector.GetOrdinal("nombre_clase")),
                NombreEntrenador = lector.GetString(lector.GetOrdinal("nombre_entrenador")),
                FechaClase = lector.GetDateTime(lector.GetOrdinal("fecha_clase")),
                FechaReserva = lector.GetDateTime(lector.GetOrdinal("fecha_reserva")),
                Estado = lector.GetBoolean(lector.GetOrdinal("estado"))
            };
        }

        private static string ObtenerDiaSemana(DayOfWeek dia)
        {
            return dia switch
            {
                DayOfWeek.Monday => "LUNES",
                DayOfWeek.Tuesday => "MARTES",
                DayOfWeek.Wednesday => "MIERCOLES",
                DayOfWeek.Thursday => "JUEVES",
                DayOfWeek.Friday => "VIERNES",
                DayOfWeek.Saturday => "SABADO",
                _ => "DOMINGO"
            };
        }
    }
}

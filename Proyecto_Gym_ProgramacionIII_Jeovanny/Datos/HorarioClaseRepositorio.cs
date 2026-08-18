using Npgsql;
using NpgsqlTypes;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Datos
{
    public static class HorarioClaseRepositorio
    {
        public static async Task<List<HorarioClase>> ListarAsync(string texto = "")
        {
            const string consulta = @"
                SELECT h.id_horario, h.id_clase, c.nombre AS nombre_clase,
                       h.id_entrenador, e.nombre || ' ' || e.apellido AS nombre_entrenador,
                       h.dia_semana, h.hora_inicio, h.hora_fin, h.estado
                FROM horarios_clases h
                INNER JOIN clases_actividades c ON c.id_clase = h.id_clase
                INNER JOIN entrenadores e ON e.id_entrenador = h.id_entrenador
                WHERE @texto = ''
                   OR c.nombre ILIKE @busqueda
                   OR e.nombre ILIKE @busqueda
                   OR e.apellido ILIKE @busqueda
                   OR h.dia_semana ILIKE @busqueda
                ORDER BY h.estado DESC,
                         CASE h.dia_semana
                             WHEN 'LUNES' THEN 1 WHEN 'MARTES' THEN 2 WHEN 'MIERCOLES' THEN 3
                             WHEN 'JUEVES' THEN 4 WHEN 'VIERNES' THEN 5 WHEN 'SABADO' THEN 6
                             ELSE 7
                         END,
                         h.hora_inicio;";

            List<HorarioClase> horarios = new List<HorarioClase>();
            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("texto", texto.Trim());
            comando.Parameters.AddWithValue("busqueda", $"%{texto.Trim()}%");
            using NpgsqlDataReader lector = await comando.ExecuteReaderAsync();

            while (await lector.ReadAsync())
            {
                horarios.Add(new HorarioClase
                {
                    IdHorario = lector.GetInt32(lector.GetOrdinal("id_horario")),
                    IdClase = lector.GetInt32(lector.GetOrdinal("id_clase")),
                    NombreClase = lector.GetString(lector.GetOrdinal("nombre_clase")),
                    IdEntrenador = lector.GetInt32(lector.GetOrdinal("id_entrenador")),
                    NombreEntrenador = lector.GetString(lector.GetOrdinal("nombre_entrenador")),
                    DiaSemana = lector.GetString(lector.GetOrdinal("dia_semana")),
                    HoraInicio = lector.GetTimeSpan(lector.GetOrdinal("hora_inicio")),
                    HoraFin = lector.GetTimeSpan(lector.GetOrdinal("hora_fin")),
                    Estado = lector.GetBoolean(lector.GetOrdinal("estado"))
                });
            }

            return horarios;
        }

        public static async Task<bool> ExisteAsync(int idClase, string diaSemana, TimeSpan horaInicio, int idIgnorar = 0)
        {
            const string consulta = @"
                SELECT EXISTS (
                    SELECT 1 FROM horarios_clases
                    WHERE id_clase = @idClase
                      AND dia_semana = @diaSemana
                      AND hora_inicio = @horaInicio
                      AND (@idIgnorar = 0 OR id_horario <> @idIgnorar)
                );";

            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("idClase", idClase);
            comando.Parameters.AddWithValue("diaSemana", diaSemana);
            comando.Parameters.Add("horaInicio", NpgsqlDbType.Time).Value = horaInicio;
            comando.Parameters.AddWithValue("idIgnorar", idIgnorar);
            object? resultado = await comando.ExecuteScalarAsync();
            return resultado is bool existe && existe;
        }

        public static async Task GuardarAsync(HorarioClase horario)
        {
            const string consulta = @"
                INSERT INTO horarios_clases
                    (id_clase, id_entrenador, dia_semana, hora_inicio, hora_fin, estado)
                VALUES
                    (@idClase, @idEntrenador, @diaSemana, @horaInicio, @horaFin, @estado);";
            await EjecutarGuardadoAsync(consulta, horario);
        }

        public static async Task ActualizarAsync(HorarioClase horario)
        {
            const string consulta = @"
                UPDATE horarios_clases
                SET id_clase = @idClase,
                    id_entrenador = @idEntrenador,
                    dia_semana = @diaSemana,
                    hora_inicio = @horaInicio,
                    hora_fin = @horaFin,
                    estado = @estado
                WHERE id_horario = @idHorario;";
            await EjecutarGuardadoAsync(consulta, horario);
        }

        public static async Task CambiarEstadoAsync(int idHorario, bool estado)
        {
            const string consulta = "UPDATE horarios_clases SET estado = @estado WHERE id_horario = @idHorario;";
            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("estado", estado);
            comando.Parameters.AddWithValue("idHorario", idHorario);
            await comando.ExecuteNonQueryAsync();
        }

        private static async Task EjecutarGuardadoAsync(string consulta, HorarioClase horario)
        {
            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("idClase", horario.IdClase);
            comando.Parameters.AddWithValue("idEntrenador", horario.IdEntrenador);
            comando.Parameters.AddWithValue("diaSemana", horario.DiaSemana);
            comando.Parameters.Add("horaInicio", NpgsqlDbType.Time).Value = horario.HoraInicio;
            comando.Parameters.Add("horaFin", NpgsqlDbType.Time).Value = horario.HoraFin;
            comando.Parameters.AddWithValue("estado", horario.Estado);
            if (horario.IdHorario > 0)
            {
                comando.Parameters.AddWithValue("idHorario", horario.IdHorario);
            }
            await comando.ExecuteNonQueryAsync();
        }
    }
}

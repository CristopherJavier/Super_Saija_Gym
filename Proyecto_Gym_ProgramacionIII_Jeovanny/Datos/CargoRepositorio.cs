using Npgsql;
using NpgsqlTypes;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Datos
{
    public static class CargoRepositorio
    {
        public static async Task<List<MembresiaCliente>> ListarMembresiasSinCargoAsync()
        {
            const string consulta = @"
                SELECT mc.id_membresia_cliente,
                       mc.id_cliente,
                       c.nombre || ' ' || c.apellido AS nombre_cliente,
                       mc.id_tipo_membresia,
                       tm.nombre AS nombre_tipo_membresia,
                       mc.id_membresia_anterior,
                       mc.fecha_inicio,
                       mc.fecha_vencimiento,
                       mc.precio_aplicado,
                       mc.estado
                FROM membresias_clientes mc
                INNER JOIN clientes c ON c.id_cliente = mc.id_cliente
                INNER JOIN tipos_membresias tm ON tm.id_tipo_membresia = mc.id_tipo_membresia
                WHERE mc.estado = TRUE
                  AND c.estado = TRUE
                  AND NOT EXISTS (
                      SELECT 1
                      FROM cargos ca
                      WHERE ca.id_membresia_cliente = mc.id_membresia_cliente
                  )
                ORDER BY c.nombre, c.apellido, mc.fecha_inicio;";

            List<MembresiaCliente> membresias = new List<MembresiaCliente>();
            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            using NpgsqlDataReader lector = await comando.ExecuteReaderAsync();

            while (await lector.ReadAsync())
            {
                int indiceAnterior = lector.GetOrdinal("id_membresia_anterior");
                membresias.Add(new MembresiaCliente
                {
                    IdMembresiaCliente = lector.GetInt32(lector.GetOrdinal("id_membresia_cliente")),
                    IdCliente = lector.GetInt32(lector.GetOrdinal("id_cliente")),
                    NombreCliente = lector.GetString(lector.GetOrdinal("nombre_cliente")),
                    IdTipoMembresia = lector.GetInt32(lector.GetOrdinal("id_tipo_membresia")),
                    NombreTipoMembresia = lector.GetString(lector.GetOrdinal("nombre_tipo_membresia")),
                    IdMembresiaAnterior = lector.IsDBNull(indiceAnterior)
                        ? null
                        : lector.GetInt32(indiceAnterior),
                    FechaInicio = lector.GetDateTime(lector.GetOrdinal("fecha_inicio")),
                    FechaVencimiento = lector.GetDateTime(lector.GetOrdinal("fecha_vencimiento")),
                    PrecioAplicado = lector.GetDecimal(lector.GetOrdinal("precio_aplicado")),
                    Estado = lector.GetBoolean(lector.GetOrdinal("estado"))
                });
            }

            return membresias;
        }

        public static async Task<List<Cargo>> ListarAsync(int? idCliente = null, bool soloPendientes = false)
        {
            const string consulta = @"
                SELECT ca.id_cargo,
                       ca.id_cliente,
                       c.nombre || ' ' || c.apellido AS nombre_cliente,
                       ca.id_membresia_cliente,
                       ca.concepto,
                       ca.fecha_cargo,
                       ca.fecha_vencimiento,
                       ca.monto,
                       ca.saldo,
                       ca.estado
                FROM cargos ca
                INNER JOIN clientes c ON c.id_cliente = ca.id_cliente
                WHERE (@idCliente IS NULL OR ca.id_cliente = @idCliente)
                  AND (@soloPendientes = FALSE OR (ca.estado = TRUE AND ca.saldo > 0))
                ORDER BY ca.fecha_vencimiento, ca.id_cargo;";

            List<Cargo> cargos = new List<Cargo>();
            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            comando.Parameters.Add("idCliente", NpgsqlDbType.Integer).Value = idCliente.HasValue
                ? idCliente.Value
                : DBNull.Value;
            comando.Parameters.AddWithValue("soloPendientes", soloPendientes);
            using NpgsqlDataReader lector = await comando.ExecuteReaderAsync();

            while (await lector.ReadAsync())
            {
                int indiceMembresia = lector.GetOrdinal("id_membresia_cliente");
                cargos.Add(new Cargo
                {
                    IdCargo = lector.GetInt32(lector.GetOrdinal("id_cargo")),
                    IdCliente = lector.GetInt32(lector.GetOrdinal("id_cliente")),
                    NombreCliente = lector.GetString(lector.GetOrdinal("nombre_cliente")),
                    IdMembresiaCliente = lector.IsDBNull(indiceMembresia)
                        ? null
                        : lector.GetInt32(indiceMembresia),
                    Concepto = lector.GetString(lector.GetOrdinal("concepto")),
                    FechaCargo = lector.GetDateTime(lector.GetOrdinal("fecha_cargo")),
                    FechaVencimiento = lector.GetDateTime(lector.GetOrdinal("fecha_vencimiento")),
                    Monto = lector.GetDecimal(lector.GetOrdinal("monto")),
                    Saldo = lector.GetDecimal(lector.GetOrdinal("saldo")),
                    Estado = lector.GetBoolean(lector.GetOrdinal("estado"))
                });
            }

            return cargos;
        }

        public static async Task<int> GenerarAsync(
            int idMembresiaCliente,
            string concepto,
            DateTime fechaVencimiento)
        {
            if (string.IsNullOrWhiteSpace(concepto))
            {
                throw new InvalidOperationException("El concepto del cargo es obligatorio.");
            }

            if (concepto.Trim().Length > 150)
            {
                throw new InvalidOperationException("El concepto no puede superar 150 caracteres.");
            }

            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlTransaction transaccion = await conexion.BeginTransactionAsync();

            try
            {
                const string consultaMembresia = @"
                    SELECT mc.id_cliente, mc.precio_aplicado, mc.estado
                    FROM membresias_clientes mc
                    INNER JOIN clientes c ON c.id_cliente = mc.id_cliente
                    WHERE mc.id_membresia_cliente = @idMembresiaCliente
                      AND c.estado = TRUE
                    FOR UPDATE OF mc;";

                int idCliente;
                decimal monto;

                using (NpgsqlCommand comando = new NpgsqlCommand(consultaMembresia, conexion, transaccion))
                {
                    comando.Parameters.AddWithValue("idMembresiaCliente", idMembresiaCliente);
                    using NpgsqlDataReader lector = await comando.ExecuteReaderAsync();

                    if (!await lector.ReadAsync())
                    {
                        throw new InvalidOperationException("La membresía seleccionada ya no está disponible.");
                    }

                    if (!lector.GetBoolean(lector.GetOrdinal("estado")))
                    {
                        throw new InvalidOperationException("La membresía seleccionada ya no está activa.");
                    }

                    idCliente = lector.GetInt32(lector.GetOrdinal("id_cliente"));
                    monto = lector.GetDecimal(lector.GetOrdinal("precio_aplicado"));
                }

                if (monto <= 0)
                {
                    throw new InvalidOperationException("La membresía seleccionada no tiene un monto válido para generar el cargo.");
                }

                const string consultaExistente = @"
                    SELECT EXISTS (
                        SELECT 1
                        FROM cargos
                        WHERE id_membresia_cliente = @idMembresiaCliente
                    );";

                using (NpgsqlCommand comando = new NpgsqlCommand(consultaExistente, conexion, transaccion))
                {
                    comando.Parameters.AddWithValue("idMembresiaCliente", idMembresiaCliente);
                    bool existe = Convert.ToBoolean(await comando.ExecuteScalarAsync());

                    if (existe)
                    {
                        throw new InvalidOperationException("La membresía seleccionada ya tiene un cargo generado.");
                    }
                }

                const string insercion = @"
                    INSERT INTO cargos (
                        id_cliente,
                        id_membresia_cliente,
                        concepto,
                        fecha_cargo,
                        fecha_vencimiento,
                        monto,
                        saldo,
                        estado
                    )
                    VALUES (
                        @idCliente,
                        @idMembresiaCliente,
                        @concepto,
                        CURRENT_DATE,
                        @fechaVencimiento,
                        @monto,
                        @monto,
                        TRUE
                    )
                    RETURNING id_cargo;";

                int idCargo;
                using (NpgsqlCommand comando = new NpgsqlCommand(insercion, conexion, transaccion))
                {
                    comando.Parameters.AddWithValue("idCliente", idCliente);
                    comando.Parameters.AddWithValue("idMembresiaCliente", idMembresiaCliente);
                    comando.Parameters.AddWithValue("concepto", concepto.Trim());
                    comando.Parameters.AddWithValue("fechaVencimiento", fechaVencimiento.Date);
                    comando.Parameters.AddWithValue("monto", monto);
                    idCargo = Convert.ToInt32(await comando.ExecuteScalarAsync());
                }

                await transaccion.CommitAsync();
                return idCargo;
            }
            catch
            {
                await transaccion.RollbackAsync();
                throw;
            }
        }
    }
}

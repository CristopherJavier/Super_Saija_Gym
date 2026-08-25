using Npgsql;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Datos
{
    public static class MembresiaClienteRepositorio
    {
        public static async Task<List<MembresiaCliente>> ListarAsync()
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
                ORDER BY mc.fecha_inicio DESC, mc.id_membresia_cliente DESC;";

            return await EjecutarListadoAsync(consulta);
        }

        public static async Task<List<MembresiaCliente>> ListarRenovablesAsync()
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
                  AND tm.estado = TRUE
                ORDER BY c.nombre, c.apellido;";

            return await EjecutarListadoAsync(consulta);
        }

        public static async Task<MembresiaCliente> AsignarAsync(
            int idCliente,
            int idTipoMembresia,
            DateTime fechaInicio)
        {
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
                    comando.Parameters.AddWithValue("idCliente", idCliente);
                    object? estadoCliente = await comando.ExecuteScalarAsync();

                    if (estadoCliente is not bool activo || !activo)
                    {
                        throw new InvalidOperationException("El cliente seleccionado no está activo.");
                    }
                }

                const string consultaExistente = @"
                    SELECT id_membresia_cliente
                    FROM membresias_clientes
                    WHERE id_cliente = @idCliente
                      AND estado = TRUE
                    LIMIT 1
                    FOR UPDATE;";

                using (NpgsqlCommand comando = new NpgsqlCommand(consultaExistente, conexion, transaccion))
                {
                    comando.Parameters.AddWithValue("idCliente", idCliente);
                    object? existente = await comando.ExecuteScalarAsync();

                    if (existente is not null)
                    {
                        throw new InvalidOperationException("El cliente ya tiene una membresía asignada. Debe utilizar la renovación.");
                    }
                }

                TipoMembresia tipo = await ObtenerTipoActivoAsync(
                    conexion,
                    transaccion,
                    idTipoMembresia);
                DateTime inicio = fechaInicio.Date;
                DateTime vencimiento = inicio.AddDays(tipo.DuracionDias - 1);

                const string insercion = @"
                    INSERT INTO membresias_clientes (
                        id_cliente,
                        id_tipo_membresia,
                        id_membresia_anterior,
                        fecha_inicio,
                        fecha_vencimiento,
                        precio_aplicado,
                        estado
                    )
                    VALUES (
                        @idCliente,
                        @idTipoMembresia,
                        NULL,
                        @fechaInicio,
                        @fechaVencimiento,
                        @precioAplicado,
                        TRUE
                    )
                    RETURNING id_membresia_cliente;";

                int idMembresia;
                using (NpgsqlCommand comando = new NpgsqlCommand(insercion, conexion, transaccion))
                {
                    comando.Parameters.AddWithValue("idCliente", idCliente);
                    comando.Parameters.AddWithValue("idTipoMembresia", idTipoMembresia);
                    comando.Parameters.AddWithValue("fechaInicio", inicio);
                    comando.Parameters.AddWithValue("fechaVencimiento", vencimiento);
                    comando.Parameters.AddWithValue("precioAplicado", tipo.Precio);
                    idMembresia = Convert.ToInt32(await comando.ExecuteScalarAsync());
                }

                await transaccion.CommitAsync();

                return new MembresiaCliente
                {
                    IdMembresiaCliente = idMembresia,
                    IdCliente = idCliente,
                    IdTipoMembresia = idTipoMembresia,
                    NombreTipoMembresia = tipo.Nombre,
                    FechaInicio = inicio,
                    FechaVencimiento = vencimiento,
                    PrecioAplicado = tipo.Precio,
                    Estado = true
                };
            }
            catch
            {
                await transaccion.RollbackAsync();
                throw;
            }
        }

        public static async Task<MembresiaCliente> RenovarAsync(int idMembresiaAnterior)
        {
            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlTransaction transaccion = await conexion.BeginTransactionAsync();

            try
            {
                const string consultaAnterior = @"
                    SELECT mc.id_cliente,
                           mc.id_tipo_membresia,
                           mc.fecha_vencimiento,
                           mc.estado,
                           c.estado AS cliente_activo,
                           tm.nombre,
                           tm.duracion_dias,
                           tm.precio,
                           tm.estado AS tipo_activo
                    FROM membresias_clientes mc
                    INNER JOIN clientes c ON c.id_cliente = mc.id_cliente
                    INNER JOIN tipos_membresias tm ON tm.id_tipo_membresia = mc.id_tipo_membresia
                    WHERE mc.id_membresia_cliente = @idMembresiaAnterior
                    FOR UPDATE OF mc;";

                int idCliente;
                int idTipoMembresia;
                DateTime vencimientoAnterior;
                string nombreTipo;
                int duracionDias;
                decimal precio;

                using (NpgsqlCommand comando = new NpgsqlCommand(consultaAnterior, conexion, transaccion))
                {
                    comando.Parameters.AddWithValue("idMembresiaAnterior", idMembresiaAnterior);
                    using NpgsqlDataReader lector = await comando.ExecuteReaderAsync();

                    if (!await lector.ReadAsync())
                    {
                        throw new InvalidOperationException("La membresía seleccionada ya no existe.");
                    }

                    if (!lector.GetBoolean(lector.GetOrdinal("estado")))
                    {
                        throw new InvalidOperationException("La membresía seleccionada ya fue renovada.");
                    }

                    if (!lector.GetBoolean(lector.GetOrdinal("cliente_activo")))
                    {
                        throw new InvalidOperationException("El cliente seleccionado no está activo.");
                    }

                    if (!lector.GetBoolean(lector.GetOrdinal("tipo_activo")))
                    {
                        throw new InvalidOperationException("El tipo de membresía ya no está activo.");
                    }

                    idCliente = lector.GetInt32(lector.GetOrdinal("id_cliente"));
                    idTipoMembresia = lector.GetInt32(lector.GetOrdinal("id_tipo_membresia"));
                    vencimientoAnterior = lector.GetDateTime(lector.GetOrdinal("fecha_vencimiento"));
                    nombreTipo = lector.GetString(lector.GetOrdinal("nombre"));
                    duracionDias = lector.GetInt32(lector.GetOrdinal("duracion_dias"));
                    precio = lector.GetDecimal(lector.GetOrdinal("precio"));
                }

                DateTime inicio = vencimientoAnterior.Date >= DateTime.Today
                    ? vencimientoAnterior.Date.AddDays(1)
                    : DateTime.Today;
                DateTime vencimiento = inicio.AddDays(duracionDias - 1);

                const string desactivar = @"
                    UPDATE membresias_clientes
                    SET estado = FALSE
                    WHERE id_membresia_cliente = @idMembresiaAnterior;";

                using (NpgsqlCommand comando = new NpgsqlCommand(desactivar, conexion, transaccion))
                {
                    comando.Parameters.AddWithValue("idMembresiaAnterior", idMembresiaAnterior);
                    await comando.ExecuteNonQueryAsync();
                }

                const string insercion = @"
                    INSERT INTO membresias_clientes (
                        id_cliente,
                        id_tipo_membresia,
                        id_membresia_anterior,
                        fecha_inicio,
                        fecha_vencimiento,
                        precio_aplicado,
                        estado
                    )
                    VALUES (
                        @idCliente,
                        @idTipoMembresia,
                        @idMembresiaAnterior,
                        @fechaInicio,
                        @fechaVencimiento,
                        @precioAplicado,
                        TRUE
                    )
                    RETURNING id_membresia_cliente;";

                int idNuevaMembresia;
                using (NpgsqlCommand comando = new NpgsqlCommand(insercion, conexion, transaccion))
                {
                    comando.Parameters.AddWithValue("idCliente", idCliente);
                    comando.Parameters.AddWithValue("idTipoMembresia", idTipoMembresia);
                    comando.Parameters.AddWithValue("idMembresiaAnterior", idMembresiaAnterior);
                    comando.Parameters.AddWithValue("fechaInicio", inicio);
                    comando.Parameters.AddWithValue("fechaVencimiento", vencimiento);
                    comando.Parameters.AddWithValue("precioAplicado", precio);
                    idNuevaMembresia = Convert.ToInt32(await comando.ExecuteScalarAsync());
                }

                await transaccion.CommitAsync();

                return new MembresiaCliente
                {
                    IdMembresiaCliente = idNuevaMembresia,
                    IdCliente = idCliente,
                    IdTipoMembresia = idTipoMembresia,
                    IdMembresiaAnterior = idMembresiaAnterior,
                    NombreTipoMembresia = nombreTipo,
                    FechaInicio = inicio,
                    FechaVencimiento = vencimiento,
                    PrecioAplicado = precio,
                    Estado = true
                };
            }
            catch
            {
                await transaccion.RollbackAsync();
                throw;
            }
        }

        private static async Task<List<MembresiaCliente>> EjecutarListadoAsync(string consulta)
        {
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

        private static async Task<TipoMembresia> ObtenerTipoActivoAsync(
            NpgsqlConnection conexion,
            NpgsqlTransaction transaccion,
            int idTipoMembresia)
        {
            const string consulta = @"
                SELECT nombre, duracion_dias, precio, estado
                FROM tipos_membresias
                WHERE id_tipo_membresia = @idTipoMembresia
                FOR UPDATE;";

            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion, transaccion);
            comando.Parameters.AddWithValue("idTipoMembresia", idTipoMembresia);
            using NpgsqlDataReader lector = await comando.ExecuteReaderAsync();

            if (!await lector.ReadAsync())
            {
                throw new InvalidOperationException("El tipo de membresía seleccionado ya no existe.");
            }

            if (!lector.GetBoolean(lector.GetOrdinal("estado")))
            {
                throw new InvalidOperationException("El tipo de membresía seleccionado no está activo.");
            }

            return new TipoMembresia
            {
                IdTipoMembresia = idTipoMembresia,
                Nombre = lector.GetString(lector.GetOrdinal("nombre")),
                DuracionDias = lector.GetInt32(lector.GetOrdinal("duracion_dias")),
                Precio = lector.GetDecimal(lector.GetOrdinal("precio")),
                Estado = true
            };
        }
    }
}

using Npgsql;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Datos
{
    public static class CuentaCobrarRepositorio
    {
        public static async Task<List<CuentaCobrar>> ListarAsync(bool soloPendientes = false)
        {
            const string consulta = @"
                SELECT cc.id_cuenta,
                       cc.id_venta,
                       cc.id_cliente,
                       c.nombre || ' ' || c.apellido AS nombre_cliente,
                       cc.saldo,
                       cc.fecha_vencimiento,
                       cc.estado
                FROM cuentas_cobrar cc
                INNER JOIN clientes c ON c.id_cliente = cc.id_cliente
                WHERE (@soloPendientes = FALSE OR (cc.estado = TRUE AND cc.saldo > 0))
                ORDER BY cc.fecha_vencimiento, cc.id_cuenta;";

            List<CuentaCobrar> cuentas = new List<CuentaCobrar>();
            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("soloPendientes", soloPendientes);
            using NpgsqlDataReader lector = await comando.ExecuteReaderAsync();

            while (await lector.ReadAsync())
            {
                cuentas.Add(new CuentaCobrar
                {
                    IdCuenta = lector.GetInt32(lector.GetOrdinal("id_cuenta")),
                    IdVenta = lector.GetInt32(lector.GetOrdinal("id_venta")),
                    IdCliente = lector.GetInt32(lector.GetOrdinal("id_cliente")),
                    NombreCliente = lector.GetString(lector.GetOrdinal("nombre_cliente")),
                    Saldo = lector.GetDecimal(lector.GetOrdinal("saldo")),
                    FechaVencimiento = lector.GetDateTime(lector.GetOrdinal("fecha_vencimiento")),
                    Estado = lector.GetBoolean(lector.GetOrdinal("estado"))
                });
            }

            return cuentas;
        }

        public static async Task<Abono> RegistrarAbonoAsync(Abono abono)
        {
            if (abono.Monto <= 0)
            {
                throw new InvalidOperationException("El monto del abono debe ser mayor que cero.");
            }

            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlTransaction transaccion = await conexion.BeginTransactionAsync();

            try
            {
                const string consultaCuenta = @"
                    SELECT saldo, estado
                    FROM cuentas_cobrar
                    WHERE id_cuenta = @idCuenta
                    FOR UPDATE;";

                decimal saldo;
                using (NpgsqlCommand comando = new NpgsqlCommand(consultaCuenta, conexion, transaccion))
                {
                    comando.Parameters.AddWithValue("idCuenta", abono.IdCuenta);
                    using NpgsqlDataReader lector = await comando.ExecuteReaderAsync();

                    if (!await lector.ReadAsync())
                    {
                        throw new InvalidOperationException("La cuenta por cobrar seleccionada ya no existe.");
                    }

                    saldo = lector.GetDecimal(lector.GetOrdinal("saldo"));

                    if (!lector.GetBoolean(lector.GetOrdinal("estado")) || saldo <= 0)
                    {
                        throw new InvalidOperationException("La cuenta seleccionada ya no tiene saldo pendiente.");
                    }
                }

                if (abono.Monto > saldo)
                {
                    throw new InvalidOperationException("El abono no puede superar el saldo pendiente.");
                }

                const string consultaMetodo = @"
                    SELECT nombre, estado
                    FROM metodos_pago
                    WHERE id_metodo_pago = @idMetodoPago
                    FOR UPDATE;";

                using (NpgsqlCommand comando = new NpgsqlCommand(consultaMetodo, conexion, transaccion))
                {
                    comando.Parameters.AddWithValue("idMetodoPago", abono.IdMetodoPago);
                    using NpgsqlDataReader lector = await comando.ExecuteReaderAsync();

                    if (!await lector.ReadAsync() || !lector.GetBoolean(lector.GetOrdinal("estado")))
                    {
                        throw new InvalidOperationException("El método de pago seleccionado no está activo.");
                    }

                    abono.NombreMetodoPago = lector.GetString(lector.GetOrdinal("nombre"));
                }

                const string insercion = @"
                    INSERT INTO abonos (
                        id_cuenta,
                        monto,
                        id_metodo_pago,
                        id_usuario
                    )
                    VALUES (
                        @idCuenta,
                        @monto,
                        @idMetodoPago,
                        @idUsuario
                    )
                    RETURNING id_abono, fecha;";

                using (NpgsqlCommand comando = new NpgsqlCommand(insercion, conexion, transaccion))
                {
                    comando.Parameters.AddWithValue("idCuenta", abono.IdCuenta);
                    comando.Parameters.AddWithValue("monto", abono.Monto);
                    comando.Parameters.AddWithValue("idMetodoPago", abono.IdMetodoPago);
                    comando.Parameters.AddWithValue("idUsuario", abono.IdUsuario);
                    using NpgsqlDataReader lector = await comando.ExecuteReaderAsync();
                    await lector.ReadAsync();
                    abono.IdAbono = lector.GetInt32(lector.GetOrdinal("id_abono"));
                    abono.Fecha = lector.GetDateTime(lector.GetOrdinal("fecha"));
                }

                const string actualizar = @"
                    UPDATE cuentas_cobrar
                    SET saldo = saldo - @monto
                    WHERE id_cuenta = @idCuenta;";

                using (NpgsqlCommand comando = new NpgsqlCommand(actualizar, conexion, transaccion))
                {
                    comando.Parameters.AddWithValue("monto", abono.Monto);
                    comando.Parameters.AddWithValue("idCuenta", abono.IdCuenta);
                    await comando.ExecuteNonQueryAsync();
                }

                await transaccion.CommitAsync();
                return abono;
            }
            catch
            {
                await transaccion.RollbackAsync();
                throw;
            }
        }

        public static async Task<List<Abono>> ListarAbonosAsync()
        {
            const string consulta = @"
                SELECT a.id_abono,
                       a.id_cuenta,
                       a.fecha,
                       a.monto,
                       a.id_metodo_pago,
                       mp.nombre AS nombre_metodo_pago,
                       a.id_usuario,
                       u.nombre_completo AS nombre_usuario
                FROM abonos a
                INNER JOIN metodos_pago mp ON mp.id_metodo_pago = a.id_metodo_pago
                INNER JOIN usuarios u ON u.id_usuario = a.id_usuario
                ORDER BY a.fecha DESC;";

            List<Abono> abonos = new List<Abono>();
            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            using NpgsqlDataReader lector = await comando.ExecuteReaderAsync();

            while (await lector.ReadAsync())
            {
                abonos.Add(new Abono
                {
                    IdAbono = lector.GetInt32(lector.GetOrdinal("id_abono")),
                    IdCuenta = lector.GetInt32(lector.GetOrdinal("id_cuenta")),
                    Fecha = lector.GetDateTime(lector.GetOrdinal("fecha")),
                    Monto = lector.GetDecimal(lector.GetOrdinal("monto")),
                    IdMetodoPago = lector.GetInt32(lector.GetOrdinal("id_metodo_pago")),
                    NombreMetodoPago = lector.GetString(lector.GetOrdinal("nombre_metodo_pago")),
                    IdUsuario = lector.GetInt32(lector.GetOrdinal("id_usuario")),
                    NombreUsuario = lector.GetString(lector.GetOrdinal("nombre_usuario"))
                });
            }

            return abonos;
        }
    }
}

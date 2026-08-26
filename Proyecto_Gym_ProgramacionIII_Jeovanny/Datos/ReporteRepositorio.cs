using System.Data;
using Npgsql;
using NpgsqlTypes;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Datos
{
    public static class ReporteRepositorio
    {
        public static async Task<DataTable> ObtenerBalanceClientesAsync(string cedula)
        {
            const string consulta = @"
                SELECT c.cedula AS ""Cédula"",
                       c.nombre || ' ' || c.apellido AS ""Cliente"",
                       COALESCE(ca.saldo, 0) AS ""Cargos pendientes"",
                       COALESCE(cc.saldo, 0) AS ""Ventas a crédito pendientes"",
                       COALESCE(ca.saldo, 0) + COALESCE(cc.saldo, 0) AS ""Balance pendiente""
                FROM clientes c
                LEFT JOIN (
                    SELECT id_cliente, SUM(saldo) AS saldo
                    FROM cargos
                    WHERE estado = TRUE AND saldo > 0
                    GROUP BY id_cliente
                ) ca ON ca.id_cliente = c.id_cliente
                LEFT JOIN (
                    SELECT id_cliente, SUM(saldo) AS saldo
                    FROM cuentas_cobrar
                    WHERE estado = TRUE AND saldo > 0
                    GROUP BY id_cliente
                ) cc ON cc.id_cliente = c.id_cliente
                WHERE COALESCE(ca.saldo, 0) + COALESCE(cc.saldo, 0) > 0
                  AND (@cedula = '' OR c.cedula = @cedula)
                ORDER BY ""Balance pendiente"" DESC, ""Cliente"";";

            return await EjecutarAsync(
                consulta,
                new NpgsqlParameter("cedula", cedula.Trim()));
        }

        public static async Task<DataTable> ObtenerClientesAsync(string estado)
        {
            const string consulta = @"
                SELECT c.cedula AS ""Cédula"",
                       c.nombre AS ""Nombre"",
                       c.apellido AS ""Apellido"",
                       c.telefono AS ""Teléfono"",
                       COALESCE(c.correo, '') AS ""Correo"",
                       c.fecha_registro::DATE AS ""Fecha de registro"",
                       CASE WHEN c.estado THEN 'ACTIVO' ELSE 'INACTIVO' END AS ""Estado""
                FROM clientes c
                WHERE @estado = 'TODOS'
                   OR (@estado = 'ACTIVOS' AND c.estado = TRUE)
                   OR (@estado = 'INACTIVOS' AND c.estado = FALSE)
                ORDER BY c.estado DESC, c.nombre, c.apellido;";

            return await EjecutarAsync(
                consulta,
                new NpgsqlParameter("estado", estado.Trim().ToUpperInvariant()));
        }

        public static async Task<DataTable> ObtenerMembresiasAsync(string estado)
        {
            const string consulta = @"
                SELECT c.cedula AS ""Cédula"",
                       c.nombre || ' ' || c.apellido AS ""Cliente"",
                       tm.nombre AS ""Membresía"",
                       mc.fecha_inicio AS ""Fecha de inicio"",
                       mc.fecha_vencimiento AS ""Fecha de vencimiento"",
                       GREATEST(mc.fecha_vencimiento - CURRENT_DATE, 0) AS ""Días restantes"",
                       CASE
                           WHEN mc.fecha_vencimiento < CURRENT_DATE THEN 'VENCIDA'
                           ELSE 'ACTIVA'
                       END AS ""Estado""
                FROM membresias_clientes mc
                INNER JOIN clientes c ON c.id_cliente = mc.id_cliente
                INNER JOIN tipos_membresias tm ON tm.id_tipo_membresia = mc.id_tipo_membresia
                WHERE mc.estado = TRUE
                  AND (
                      @estado = 'TODAS'
                      OR (@estado = 'ACTIVAS' AND mc.fecha_vencimiento >= CURRENT_DATE)
                      OR (@estado = 'VENCIDAS' AND mc.fecha_vencimiento < CURRENT_DATE)
                  )
                ORDER BY mc.fecha_vencimiento, ""Cliente"";";

            return await EjecutarAsync(
                consulta,
                new NpgsqlParameter("estado", estado.Trim().ToUpperInvariant()));
        }

        public static async Task<DataTable> ObtenerCobrosAsync(DateTime desde, DateTime hasta)
        {
            const string consulta = @"
                SELECT co.id_cobro AS ""Número"",
                       co.fecha AS ""Fecha"",
                       c.nombre || ' ' || c.apellido AS ""Cliente"",
                       mp.nombre AS ""Método de pago"",
                       u.nombre_completo AS ""Usuario"",
                       co.total AS ""Total""
                FROM cobros co
                INNER JOIN clientes c ON c.id_cliente = co.id_cliente
                INNER JOIN metodos_pago mp ON mp.id_metodo_pago = co.id_metodo_pago
                INNER JOIN usuarios u ON u.id_usuario = co.id_usuario
                WHERE co.fecha >= @desde
                  AND co.fecha < @hasta
                  AND co.estado = TRUE
                ORDER BY co.fecha;";

            return await EjecutarPorFechasAsync(consulta, desde, hasta);
        }

        public static async Task<DataTable> ObtenerVentasAsync(DateTime desde, DateTime hasta)
        {
            const string consulta = @"
                SELECT v.id_venta AS ""Número"",
                       v.fecha AS ""Fecha"",
                       COALESCE(c.nombre || ' ' || c.apellido, 'SIN CLIENTE') AS ""Cliente"",
                       v.tipo_pago AS ""Tipo de pago"",
                       v.subtotal AS ""Subtotal"",
                       v.descuento AS ""Descuento"",
                       v.total AS ""Total""
                FROM ventas v
                LEFT JOIN clientes c ON c.id_cliente = v.id_cliente
                WHERE v.fecha >= @desde
                  AND v.fecha < @hasta
                  AND v.estado = TRUE
                ORDER BY v.fecha;";

            return await EjecutarPorFechasAsync(consulta, desde, hasta);
        }

        public static async Task<DataTable> ObtenerComprasAsync(DateTime desde, DateTime hasta)
        {
            const string consulta = @"
                SELECT co.id_compra AS ""Número"",
                       co.fecha AS ""Fecha"",
                       p.nombre AS ""Proveedor"",
                       mp.nombre AS ""Método de pago"",
                       co.subtotal AS ""Subtotal"",
                       co.total AS ""Total""
                FROM compras co
                INNER JOIN proveedores p ON p.id_proveedor = co.id_proveedor
                INNER JOIN metodos_pago mp ON mp.id_metodo_pago = co.id_metodo_pago
                WHERE co.fecha >= @desde
                  AND co.fecha < @hasta
                  AND co.estado = TRUE
                ORDER BY co.fecha;";

            return await EjecutarPorFechasAsync(consulta, desde, hasta);
        }

        public static async Task<DataTable> ObtenerCargosAsync(string estado)
        {
            const string consulta = @"
                SELECT ca.id_cargo AS ""Número"",
                       c.nombre || ' ' || c.apellido AS ""Cliente"",
                       ca.concepto AS ""Concepto"",
                       ca.fecha_cargo AS ""Fecha del cargo"",
                       ca.fecha_vencimiento AS ""Fecha de vencimiento"",
                       ca.monto AS ""Monto"",
                       ca.saldo AS ""Saldo"",
                       CASE
                           WHEN ca.fecha_vencimiento < CURRENT_DATE THEN 'VENCIDO'
                           ELSE 'PENDIENTE'
                       END AS ""Estado""
                FROM cargos ca
                INNER JOIN clientes c ON c.id_cliente = ca.id_cliente
                WHERE ca.estado = TRUE
                  AND ca.saldo > 0
                  AND (
                      @estado = 'TODOS'
                      OR (@estado = 'PENDIENTES' AND ca.fecha_vencimiento >= CURRENT_DATE)
                      OR (@estado = 'VENCIDOS' AND ca.fecha_vencimiento < CURRENT_DATE)
                  )
                ORDER BY ca.fecha_vencimiento, ""Cliente"";";

            return await EjecutarAsync(
                consulta,
                new NpgsqlParameter("estado", estado.Trim().ToUpperInvariant()));
        }

        private static async Task<DataTable> EjecutarPorFechasAsync(
            string consulta,
            DateTime desde,
            DateTime hasta)
        {
            if (desde.Date > hasta.Date)
            {
                throw new InvalidOperationException("La fecha inicial no puede ser posterior a la fecha final.");
            }

            return await EjecutarAsync(
                consulta,
                new NpgsqlParameter("desde", NpgsqlDbType.Timestamp) { Value = desde.Date },
                new NpgsqlParameter("hasta", NpgsqlDbType.Timestamp) { Value = hasta.Date.AddDays(1) });
        }

        private static async Task<DataTable> EjecutarAsync(
            string consulta,
            params NpgsqlParameter[] parametros)
        {
            DataTable tabla = new DataTable();
            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            comando.Parameters.AddRange(parametros);
            using NpgsqlDataReader lector = await comando.ExecuteReaderAsync();
            tabla.Load(lector);
            return tabla;
        }
    }
}

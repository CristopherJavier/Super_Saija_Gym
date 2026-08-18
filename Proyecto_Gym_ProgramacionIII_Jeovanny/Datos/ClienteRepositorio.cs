using Npgsql;
using NpgsqlTypes;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Datos
{
    public static class ClienteRepositorio
    {
        public static async Task<List<Cliente>> ListarAsync()
        {
            const string consulta = @"
                SELECT
                    id_cliente,
                    nombre,
                    apellido,
                    cedula,
                    telefono,
                    correo,
                    direccion,
                    fecha_nacimiento,
                    sexo,
                    foto,
                    fecha_registro,
                    estado
                FROM clientes
                ORDER BY estado DESC, nombre, apellido;";

            List<Cliente> clientes = new List<Cliente>();

            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();

            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            using NpgsqlDataReader lector = await comando.ExecuteReaderAsync();

            while (await lector.ReadAsync())
            {
                clientes.Add(CrearClienteDesdeLector(lector));
            }

            return clientes;
        }

        public static async Task<List<Cliente>> BuscarAsync(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
            {
                return await ListarAsync();
            }

            const string consulta = @"
                SELECT
                    id_cliente,
                    nombre,
                    apellido,
                    cedula,
                    telefono,
                    correo,
                    direccion,
                    fecha_nacimiento,
                    sexo,
                    foto,
                    fecha_registro,
                    estado
                FROM clientes
                WHERE nombre ILIKE @texto
                   OR apellido ILIKE @texto
                   OR cedula ILIKE @texto
                   OR telefono ILIKE @texto
                   OR correo ILIKE @texto
                ORDER BY estado DESC, nombre, apellido;";

            List<Cliente> clientes = new List<Cliente>();

            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();

            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("texto", $"%{texto.Trim()}%");

            using NpgsqlDataReader lector = await comando.ExecuteReaderAsync();

            while (await lector.ReadAsync())
            {
                clientes.Add(CrearClienteDesdeLector(lector));
            }

            return clientes;
        }

        public static async Task<bool> ExisteCedulaAsync(
            string cedula,
            int idClienteIgnorar = 0)
        {
            const string consulta = @"
                SELECT EXISTS (
                    SELECT 1
                    FROM clientes
                    WHERE REPLACE(cedula, '-', '') = @cedula
                      AND (@idClienteIgnorar = 0 OR id_cliente <> @idClienteIgnorar)
                );";

            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();

            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("cedula", cedula.Trim());
            comando.Parameters.AddWithValue("idClienteIgnorar", idClienteIgnorar);

            object? resultado = await comando.ExecuteScalarAsync();
            return resultado is bool existe && existe;
        }

        public static async Task<int> GuardarAsync(Cliente cliente)
        {
            const string consulta = @"
                INSERT INTO clientes (
                    nombre,
                    apellido,
                    cedula,
                    telefono,
                    correo,
                    direccion,
                    fecha_nacimiento,
                    sexo,
                    foto,
                    estado
                )
                VALUES (
                    @nombre,
                    @apellido,
                    @cedula,
                    @telefono,
                    @correo,
                    @direccion,
                    @fechaNacimiento,
                    @sexo,
                    @foto,
                    @estado
                )
                RETURNING id_cliente;";

            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();

            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            AgregarParametrosCliente(comando, cliente);

            object? resultado = await comando.ExecuteScalarAsync();

            if (resultado is null)
            {
                throw new InvalidOperationException("No fue posible obtener el identificador del cliente.");
            }

            return Convert.ToInt32(resultado);
        }

        public static async Task ActualizarAsync(Cliente cliente)
        {
            const string consulta = @"
                UPDATE clientes
                SET nombre = @nombre,
                    apellido = @apellido,
                    cedula = @cedula,
                    telefono = @telefono,
                    correo = @correo,
                    direccion = @direccion,
                    fecha_nacimiento = @fechaNacimiento,
                    sexo = @sexo,
                    foto = @foto,
                    estado = @estado
                WHERE id_cliente = @idCliente;";

            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();

            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            AgregarParametrosCliente(comando, cliente);
            comando.Parameters.AddWithValue("idCliente", cliente.IdCliente);
            await comando.ExecuteNonQueryAsync();
        }

        public static async Task CambiarEstadoAsync(
            int idCliente,
            bool nuevoEstado)
        {
            const string consulta = @"
                UPDATE clientes
                SET estado = @nuevoEstado
                WHERE id_cliente = @idCliente;";

            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();

            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("nuevoEstado", nuevoEstado);
            comando.Parameters.AddWithValue("idCliente", idCliente);
            await comando.ExecuteNonQueryAsync();
        }

        private static Cliente CrearClienteDesdeLector(NpgsqlDataReader lector)
        {
            int indiceCorreo = lector.GetOrdinal("correo");
            int indiceDireccion = lector.GetOrdinal("direccion");
            int indiceFechaNacimiento = lector.GetOrdinal("fecha_nacimiento");
            int indiceSexo = lector.GetOrdinal("sexo");
            int indiceFoto = lector.GetOrdinal("foto");

            return new Cliente
            {
                IdCliente = lector.GetInt32(lector.GetOrdinal("id_cliente")),
                Nombre = lector.GetString(lector.GetOrdinal("nombre")),
                Apellido = lector.GetString(lector.GetOrdinal("apellido")),
                Cedula = lector.GetString(lector.GetOrdinal("cedula")),
                Telefono = lector.GetString(lector.GetOrdinal("telefono")),
                Correo = lector.IsDBNull(indiceCorreo) ? string.Empty : lector.GetString(indiceCorreo),
                Direccion = lector.IsDBNull(indiceDireccion) ? string.Empty : lector.GetString(indiceDireccion),
                FechaNacimiento = lector.IsDBNull(indiceFechaNacimiento)
                    ? null
                    : lector.GetDateTime(indiceFechaNacimiento),
                Sexo = lector.IsDBNull(indiceSexo) ? string.Empty : lector.GetString(indiceSexo),
                Foto = lector.IsDBNull(indiceFoto) ? string.Empty : lector.GetString(indiceFoto),
                FechaRegistro = lector.GetDateTime(lector.GetOrdinal("fecha_registro")),
                Estado = lector.GetBoolean(lector.GetOrdinal("estado"))
            };
        }

        private static void AgregarParametrosCliente(NpgsqlCommand comando, Cliente cliente)
        {
            comando.Parameters.AddWithValue("nombre", cliente.Nombre.Trim());
            comando.Parameters.AddWithValue("apellido", cliente.Apellido.Trim());
            comando.Parameters.AddWithValue("cedula", cliente.Cedula.Trim());
            comando.Parameters.AddWithValue("telefono", cliente.Telefono.Trim());
            comando.Parameters.Add("correo", NpgsqlDbType.Varchar).Value = string.IsNullOrWhiteSpace(cliente.Correo)
                ? DBNull.Value
                : cliente.Correo.Trim();
            comando.Parameters.Add("direccion", NpgsqlDbType.Varchar).Value = string.IsNullOrWhiteSpace(cliente.Direccion)
                ? DBNull.Value
                : cliente.Direccion.Trim();
            comando.Parameters.Add("fechaNacimiento", NpgsqlDbType.Date).Value = cliente.FechaNacimiento.HasValue
                ? cliente.FechaNacimiento.Value.Date
                : DBNull.Value;
            comando.Parameters.Add("sexo", NpgsqlDbType.Varchar).Value = string.IsNullOrWhiteSpace(cliente.Sexo)
                ? DBNull.Value
                : cliente.Sexo;
            comando.Parameters.Add("foto", NpgsqlDbType.Text).Value = string.IsNullOrWhiteSpace(cliente.Foto)
                ? DBNull.Value
                : cliente.Foto;
            comando.Parameters.AddWithValue("estado", cliente.Estado);
        }
    }
}

using Npgsql;
using NpgsqlTypes;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Datos
{
    public static class ProveedorRepositorio
    {
        public static async Task<List<Proveedor>> ListarAsync(string texto = "")
        {
            const string consulta = @"
                SELECT id_proveedor, nombre, rnc_cedula, telefono, correo, direccion, estado
                FROM proveedores
                WHERE @texto = ''
                   OR nombre ILIKE @busqueda
                   OR rnc_cedula ILIKE @busqueda
                   OR telefono ILIKE @busqueda
                   OR correo ILIKE @busqueda
                ORDER BY estado DESC, nombre;";
            List<Proveedor> proveedores = new List<Proveedor>();
            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("texto", texto.Trim());
            comando.Parameters.AddWithValue("busqueda", $"%{texto.Trim()}%");
            using NpgsqlDataReader lector = await comando.ExecuteReaderAsync();
            while (await lector.ReadAsync())
            {
                int indiceCorreo = lector.GetOrdinal("correo");
                int indiceDireccion = lector.GetOrdinal("direccion");
                proveedores.Add(new Proveedor
                {
                    IdProveedor = lector.GetInt32(lector.GetOrdinal("id_proveedor")),
                    Nombre = lector.GetString(lector.GetOrdinal("nombre")),
                    RncCedula = lector.GetString(lector.GetOrdinal("rnc_cedula")),
                    Telefono = lector.GetString(lector.GetOrdinal("telefono")),
                    Correo = lector.IsDBNull(indiceCorreo) ? string.Empty : lector.GetString(indiceCorreo),
                    Direccion = lector.IsDBNull(indiceDireccion) ? string.Empty : lector.GetString(indiceDireccion),
                    Estado = lector.GetBoolean(lector.GetOrdinal("estado"))
                });
            }
            return proveedores;
        }

        public static async Task<bool> ExisteDocumentoAsync(string documento, int idIgnorar = 0)
        {
            const string consulta = @"
                SELECT EXISTS (
                    SELECT 1 FROM proveedores
                    WHERE rnc_cedula = @documento
                      AND (@idIgnorar = 0 OR id_proveedor <> @idIgnorar)
                );";
            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("documento", documento.Trim());
            comando.Parameters.AddWithValue("idIgnorar", idIgnorar);
            object? resultado = await comando.ExecuteScalarAsync();
            return resultado is bool existe && existe;
        }

        public static async Task GuardarAsync(Proveedor proveedor)
        {
            const string consulta = @"
                INSERT INTO proveedores (nombre, rnc_cedula, telefono, correo, direccion, estado)
                VALUES (@nombre, @rncCedula, @telefono, @correo, @direccion, @estado);";
            await EjecutarGuardadoAsync(consulta, proveedor);
        }

        public static async Task ActualizarAsync(Proveedor proveedor)
        {
            const string consulta = @"
                UPDATE proveedores
                SET nombre = @nombre,
                    rnc_cedula = @rncCedula,
                    telefono = @telefono,
                    correo = @correo,
                    direccion = @direccion,
                    estado = @estado
                WHERE id_proveedor = @idProveedor;";
            await EjecutarGuardadoAsync(consulta, proveedor);
        }

        public static async Task CambiarEstadoAsync(int idProveedor, bool estado)
        {
            const string consulta = "UPDATE proveedores SET estado = @estado WHERE id_proveedor = @idProveedor;";
            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("estado", estado);
            comando.Parameters.AddWithValue("idProveedor", idProveedor);
            await comando.ExecuteNonQueryAsync();
        }

        private static async Task EjecutarGuardadoAsync(string consulta, Proveedor proveedor)
        {
            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("nombre", proveedor.Nombre.Trim());
            comando.Parameters.AddWithValue("rncCedula", proveedor.RncCedula.Trim());
            comando.Parameters.AddWithValue("telefono", proveedor.Telefono.Trim());
            comando.Parameters.Add("correo", NpgsqlDbType.Varchar).Value = string.IsNullOrWhiteSpace(proveedor.Correo) ? DBNull.Value : proveedor.Correo.Trim();
            comando.Parameters.Add("direccion", NpgsqlDbType.Varchar).Value = string.IsNullOrWhiteSpace(proveedor.Direccion) ? DBNull.Value : proveedor.Direccion.Trim();
            comando.Parameters.AddWithValue("estado", proveedor.Estado);
            if (proveedor.IdProveedor > 0)
            {
                comando.Parameters.AddWithValue("idProveedor", proveedor.IdProveedor);
            }
            await comando.ExecuteNonQueryAsync();
        }
    }
}

using Npgsql;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Datos
{
    public static class ConexionBD
    {
        public static string ObtenerCadenaConexion()
        {
            string? host = Environment.GetEnvironmentVariable("SUPER_SAIJA_DB_HOST");
            string? puertoTexto = Environment.GetEnvironmentVariable("SUPER_SAIJA_DB_PORT");
            string? nombreBaseDatos = Environment.GetEnvironmentVariable("SUPER_SAIJA_DB_NAME");
            string? usuario = Environment.GetEnvironmentVariable("SUPER_SAIJA_DB_USER");
            string? contrasena = Environment.GetEnvironmentVariable("SUPER_SAIJA_DB_PASSWORD");

            if (string.IsNullOrWhiteSpace(host))
            {
                throw new InvalidOperationException("Falta la variable de entorno SUPER_SAIJA_DB_HOST.");
            }

            if (string.IsNullOrWhiteSpace(puertoTexto))
            {
                throw new InvalidOperationException("Falta la variable de entorno SUPER_SAIJA_DB_PORT.");
            }

            if (string.IsNullOrWhiteSpace(nombreBaseDatos))
            {
                throw new InvalidOperationException("Falta la variable de entorno SUPER_SAIJA_DB_NAME.");
            }

            if (string.IsNullOrWhiteSpace(usuario))
            {
                throw new InvalidOperationException("Falta la variable de entorno SUPER_SAIJA_DB_USER.");
            }

            if (string.IsNullOrWhiteSpace(contrasena))
            {
                throw new InvalidOperationException("Falta la variable de entorno SUPER_SAIJA_DB_PASSWORD.");
            }

            if (!int.TryParse(puertoTexto, out int puerto))
            {
                throw new InvalidOperationException("La variable de entorno SUPER_SAIJA_DB_PORT debe ser un número.");
            }

            NpgsqlConnectionStringBuilder constructorCadena = new NpgsqlConnectionStringBuilder
            {
                Host = host,
                Port = puerto,
                Database = nombreBaseDatos,
                Username = usuario,
                Password = contrasena
            };

            return constructorCadena.ConnectionString;
        }

        public static NpgsqlConnection CrearConexion()
        {
            string cadenaConexion = ObtenerCadenaConexion();
            return new NpgsqlConnection(cadenaConexion);
        }

        public static async Task<bool> ProbarConexionAsync()
        {
            using NpgsqlConnection conexion = CrearConexion();
            await conexion.OpenAsync();
            return true;
        }
    }
}

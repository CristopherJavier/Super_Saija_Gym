using Npgsql;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Datos
{
    public static class MarcaRepositorio
    {
        public static async Task<List<Marca>> ListarAsync(bool soloActivas = false)
        {
            const string consulta = @"
                SELECT id_marca, nombre, estado
                FROM marcas
                WHERE (@soloActivas = FALSE OR estado = TRUE)
                ORDER BY estado DESC, nombre;";

            List<Marca> marcas = new List<Marca>();
            using NpgsqlConnection conexion = ConexionBD.CrearConexion();
            await conexion.OpenAsync();
            using NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("soloActivas", soloActivas);
            using NpgsqlDataReader lector = await comando.ExecuteReaderAsync();
            while (await lector.ReadAsync())
            {
                marcas.Add(new Marca
                {
                    IdMarca = lector.GetInt32(lector.GetOrdinal("id_marca")),
                    Nombre = lector.GetString(lector.GetOrdinal("nombre")),
                    Estado = lector.GetBoolean(lector.GetOrdinal("estado"))
                });
            }
            return marcas;
        }
    }
}

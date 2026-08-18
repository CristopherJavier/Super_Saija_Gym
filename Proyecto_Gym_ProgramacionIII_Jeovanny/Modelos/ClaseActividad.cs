namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos
{
    public class ClaseActividad
    {
        public int IdClase { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int CupoMaximo { get; set; }
        public bool Estado { get; set; }
        public string EstadoTexto => Estado ? "Activo" : "Inactivo";

        public override string ToString()
        {
            return Nombre;
        }
    }
}

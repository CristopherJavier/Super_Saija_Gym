namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos
{
    public class Entrenador
    {
        public int IdEntrenador { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Cedula { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Especialidad { get; set; } = string.Empty;
        public DateTime FechaContratacion { get; set; }
        public bool Estado { get; set; }
        public string EstadoTexto => Estado ? "Activo" : "Inactivo";

        public override string ToString()
        {
            return $"{Nombre} {Apellido}";
        }
    }
}

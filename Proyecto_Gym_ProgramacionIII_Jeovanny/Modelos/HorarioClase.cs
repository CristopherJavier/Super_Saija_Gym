namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos
{
    public class HorarioClase
    {
        public int IdHorario { get; set; }
        public int IdClase { get; set; }
        public string NombreClase { get; set; } = string.Empty;
        public int IdEntrenador { get; set; }
        public string NombreEntrenador { get; set; } = string.Empty;
        public string DiaSemana { get; set; } = string.Empty;
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public bool Estado { get; set; }
        public string EstadoTexto => Estado ? "Activo" : "Inactivo";
    }
}

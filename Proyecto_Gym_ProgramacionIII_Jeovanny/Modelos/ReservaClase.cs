namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos
{
    public class ReservaClase
    {
        public int IdReserva { get; set; }
        public int IdCliente { get; set; }
        public string NombreCliente { get; set; } = string.Empty;
        public int IdHorario { get; set; }
        public string NombreClase { get; set; } = string.Empty;
        public string NombreEntrenador { get; set; } = string.Empty;
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public DateTime FechaClase { get; set; }
        public DateTime FechaReserva { get; set; }
        public bool Estado { get; set; }
        public string EstadoTexto => Estado ? "Activa" : "Inactiva";
    }
}

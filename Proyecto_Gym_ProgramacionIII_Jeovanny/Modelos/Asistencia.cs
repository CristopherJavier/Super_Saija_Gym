namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos
{
    public class Asistencia
    {
        public int IdAsistencia { get; set; }
        public int IdCliente { get; set; }
        public string NombreCliente { get; set; } = string.Empty;
        public int? IdReserva { get; set; }
        public string NombreClase { get; set; } = string.Empty;
        public DateTime FechaHora { get; set; }
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
    }
}

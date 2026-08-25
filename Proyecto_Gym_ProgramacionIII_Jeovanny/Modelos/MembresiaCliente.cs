namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos
{
    public class MembresiaCliente
    {
        public int IdMembresiaCliente { get; set; }
        public int IdCliente { get; set; }
        public string NombreCliente { get; set; } = string.Empty;
        public int IdTipoMembresia { get; set; }
        public string NombreTipoMembresia { get; set; } = string.Empty;
        public int? IdMembresiaAnterior { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public decimal PrecioAplicado { get; set; }
        public bool Estado { get; set; }
        public bool EstaVencida => FechaVencimiento.Date < DateTime.Today;
        public int DiasRestantes => Math.Max(0, (FechaVencimiento.Date - DateTime.Today).Days);
        public string EstadoTexto => !Estado ? "Inactiva" : EstaVencida ? "Vencida" : "Activa";
    }
}

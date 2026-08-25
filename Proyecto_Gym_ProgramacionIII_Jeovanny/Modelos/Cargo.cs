namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos
{
    public class Cargo
    {
        public int IdCargo { get; set; }
        public int IdCliente { get; set; }
        public string NombreCliente { get; set; } = string.Empty;
        public int? IdMembresiaCliente { get; set; }
        public string Concepto { get; set; } = string.Empty;
        public DateTime FechaCargo { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public decimal Monto { get; set; }
        public decimal Saldo { get; set; }
        public bool Estado { get; set; }
        public bool EstaVencido => Estado && Saldo > 0 && FechaVencimiento.Date < DateTime.Today;
        public string EstadoTexto => !Estado ? "Inactivo" : Saldo == 0 ? "Pagado" : EstaVencido ? "Vencido" : "Pendiente";
    }
}

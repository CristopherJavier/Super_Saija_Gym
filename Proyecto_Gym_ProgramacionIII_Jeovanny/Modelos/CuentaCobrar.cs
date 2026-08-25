namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos
{
    public class CuentaCobrar
    {
        public int IdCuenta { get; set; }
        public int IdVenta { get; set; }
        public int IdCliente { get; set; }
        public string NombreCliente { get; set; } = string.Empty;
        public decimal Saldo { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public bool Estado { get; set; }
        public bool EstaVencida => Estado && Saldo > 0 && FechaVencimiento.Date < DateTime.Today;
        public string EstadoTexto => !Estado ? "Inactiva" : Saldo == 0 ? "Pagada" : EstaVencida ? "Vencida" : "Pendiente";
    }
}

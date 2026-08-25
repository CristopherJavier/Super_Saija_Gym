namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos
{
    public class Venta
    {
        public int IdVenta { get; set; }
        public DateTime Fecha { get; set; }
        public int? IdCliente { get; set; }
        public string NombreCliente { get; set; } = string.Empty;
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public string TipoPago { get; set; } = string.Empty;
        public decimal Subtotal { get; set; }
        public decimal Descuento { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }
        public bool Estado { get; set; }
        public string EstadoTexto => Estado ? "Activa" : "Inactiva";
    }
}

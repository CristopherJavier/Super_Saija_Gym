namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos
{
    public class Cobro
    {
        public int IdCobro { get; set; }
        public DateTime Fecha { get; set; }
        public int IdCliente { get; set; }
        public string NombreCliente { get; set; } = string.Empty;
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public int IdMetodoPago { get; set; }
        public string NombreMetodoPago { get; set; } = string.Empty;
        public decimal Total { get; set; }
        public bool Estado { get; set; }
        public string EstadoTexto => Estado ? "Activo" : "Inactivo";
    }
}

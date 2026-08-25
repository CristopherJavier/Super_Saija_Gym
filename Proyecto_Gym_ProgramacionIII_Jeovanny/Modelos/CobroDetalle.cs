namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos
{
    public class CobroDetalle
    {
        public int IdDetalleCobro { get; set; }
        public int IdCobro { get; set; }
        public string TipoDetalle { get; set; } = string.Empty;
        public int? IdCargo { get; set; }
        public int? IdProducto { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Subtotal { get; set; }
    }
}

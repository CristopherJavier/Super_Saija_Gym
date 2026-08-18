namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int IdCategoria { get; set; }
        public string NombreCategoria { get; set; } = string.Empty;
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public int Stock { get; set; }
        public int StockMinimo { get; set; }
        public string Imagen { get; set; } = string.Empty;
        public bool Estado { get; set; }
        public string EstadoTexto => Estado ? "Activo" : "Inactivo";
    }
}

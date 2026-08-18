namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos
{
    public class TipoMembresia
    {
        public int IdTipoMembresia { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int DuracionDias { get; set; }
        public decimal Precio { get; set; }
        public bool Estado { get; set; }
        public string EstadoTexto => Estado ? "Activo" : "Inactivo";
    }
}

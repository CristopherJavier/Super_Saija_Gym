namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos
{
    public class Abono
    {
        public int IdAbono { get; set; }
        public int IdCuenta { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public int IdMetodoPago { get; set; }
        public string NombreMetodoPago { get; set; } = string.Empty;
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
    }
}

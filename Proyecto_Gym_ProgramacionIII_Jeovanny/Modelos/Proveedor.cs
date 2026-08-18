namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos
{
    public class Proveedor
    {
        public int IdProveedor { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string RncCedula { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public bool Estado { get; set; }
        public string EstadoTexto => Estado ? "Activo" : "Inactivo";
    }
}

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Cedula { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public DateTime? FechaNacimiento { get; set; }
        public string Sexo { get; set; } = string.Empty;
        public string Foto { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; }
        public bool Estado { get; set; }
        public string EstadoTexto => Estado ? "Activo" : "Inactivo";
    }
}

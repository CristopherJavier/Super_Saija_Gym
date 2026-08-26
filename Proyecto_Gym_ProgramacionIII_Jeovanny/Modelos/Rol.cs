namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos
{
    public class Rol
    {
        public int IdRol { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public bool Estado { get; set; }
        public string EstadoTexto => Estado ? "Activo" : "Inactivo";

        public override string ToString()
        {
            return Nombre;
        }
    }
}

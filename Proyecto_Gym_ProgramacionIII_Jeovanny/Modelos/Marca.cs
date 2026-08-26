namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos
{
    public class Marca
    {
        public int IdMarca { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public bool Estado { get; set; }
        public string EstadoTexto => Estado ? "Activo" : "Inactivo";

        public override string ToString()
        {
            return Nombre;
        }
    }
}

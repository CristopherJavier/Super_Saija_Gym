namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos
{
    public static class SesionActual
    {
        public static int IdUsuario { get; private set; }
        public static string NombreUsuario { get; private set; } = string.Empty;
        public static string NombreCompleto { get; private set; } = string.Empty;
        public static int IdRol { get; private set; }
        public static string NombreRol { get; private set; } = string.Empty;
        public static bool HaySesion { get; private set; }

        public static void Iniciar(Usuario usuario)
        {
            IdUsuario = usuario.IdUsuario;
            NombreUsuario = usuario.NombreUsuario;
            NombreCompleto = usuario.NombreCompleto;
            IdRol = usuario.IdRol;
            NombreRol = usuario.NombreRol;
            HaySesion = true;
        }

        public static void Cerrar()
        {
            IdUsuario = 0;
            NombreUsuario = string.Empty;
            NombreCompleto = string.Empty;
            IdRol = 0;
            NombreRol = string.Empty;
            HaySesion = false;
        }
    }
}

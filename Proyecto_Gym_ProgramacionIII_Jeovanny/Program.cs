using Proyecto_Gym_ProgramacionIII_Jeovanny.Datos;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            bool hayUsuarios;

            try
            {
                hayUsuarios = UsuarioRepositorio.HayUsuariosAsync().GetAwaiter().GetResult();
            }
            catch (Exception)
            {
                MessageBox.Show(
                    "No fue posible conectar con la base de datos. Verifica la configuración e inténtalo nuevamente.",
                    "Error de conexión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (hayUsuarios)
            {
                Application.Run(new FrmLogin());
                return;
            }

            using FrmConfiguracionInicial frmConfiguracionInicial = new FrmConfiguracionInicial();
            DialogResult resultado = frmConfiguracionInicial.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                Application.Run(new FrmLogin());
            }
        }
    }
}

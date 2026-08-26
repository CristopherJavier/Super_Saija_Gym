namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    partial class FrmPrincipal
    {
        private readonly Button btnMetodosPago = new Button();
        private readonly Button btnUsuarios = new Button();
        private readonly Button btnRoles = new Button();
        private readonly Button btnPermisos = new Button();
        private readonly Button btnRolesPermisos = new Button();

        private void InicializarOpcionesAdministrativasConfiguracion()
        {
            ConfigurarBotonConfiguracion(btnMetodosPago, "btnMetodosPago", "MÉTODOS DE PAGO", 40, 36);
            ConfigurarBotonConfiguracion(btnUsuarios, "btnUsuarios", "USUARIOS", 40, 36);
            ConfigurarBotonConfiguracion(btnRoles, "btnRoles", "ROLES", 40, 36);
            ConfigurarBotonConfiguracion(btnPermisos, "btnPermisos", "PERMISOS", 40, 36);
            ConfigurarBotonConfiguracion(btnRolesPermisos, "btnRolesPermisos", "ROLES Y PERMISOS", 40, 36);

            btnMetodosPago.Click += btnMetodosPago_Click;
            btnUsuarios.Click += btnUsuarios_Click;
            btnRoles.Click += btnRoles_Click;
            btnPermisos.Click += btnPermisos_Click;
            btnRolesPermisos.Click += btnRolesPermisos_Click;

            pnlSubmenuConfiguracion.Controls.Clear();
            pnlSubmenuConfiguracion.Controls.Add(btnMetodosPago);
            pnlSubmenuConfiguracion.Controls.Add(btnUsuarios);
            pnlSubmenuConfiguracion.Controls.Add(btnRoles);
            pnlSubmenuConfiguracion.Controls.Add(btnPermisos);
            pnlSubmenuConfiguracion.Controls.Add(btnRolesPermisos);
            pnlSubmenuConfiguracion.Height = 200;
        }
    }
}

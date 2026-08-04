namespace Proyecto_Gym_ProgramacionIII_Jeovanny
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void chkMostrarContrasena_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMostrarContrasena.Checked)
            {
                txtContrasena.UseSystemPasswordChar = false;
            }
            else
            {
                txtContrasena.UseSystemPasswordChar = true;
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show(
                "¿Deseas cerrar la aplicación?",
                "Confirmar salida",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                Close();
            }
        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                lblMensaje.ForeColor = Color.FromArgb(124, 142, 163);
                lblMensaje.Text = "Datos completos. La autenticación se conectará posteriormente.";
            }
        }

        private bool ValidarCampos()
        {
            string usuario = txtUsuario.Text.Trim();
            string contrasena = txtContrasena.Text;

            lblMensaje.Text = string.Empty;
            lblMensaje.ForeColor = Color.Black;

            if (string.IsNullOrEmpty(usuario))
            {
                lblMensaje.Text = "Debe escribir el nombre de usuario.";
                txtUsuario.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(contrasena))
            {
                lblMensaje.Text = "Debe escribir la contraseña.";
                txtContrasena.Focus();
                return false;
            }

            return true;
        }
    }
}

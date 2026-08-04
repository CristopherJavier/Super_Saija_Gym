using Npgsql;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Datos;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Seguridad;

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

        private async void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
            {
                return;
            }

            string usuarioEscrito = txtUsuario.Text.Trim();
            string contrasenaEscrita = txtContrasena.Text;

            btnIniciarSesion.Enabled = false;
            lblMensaje.Text = string.Empty;

            try
            {
                Usuario? usuario = await UsuarioRepositorio.ObtenerPorNombreUsuarioAsync(usuarioEscrito);

                if (usuario is null)
                {
                    lblMensaje.Text = "Usuario o contraseña incorrectos.";
                    txtContrasena.Clear();
                    txtContrasena.Focus();
                    return;
                }

                if (!usuario.Activo)
                {
                    lblMensaje.Text = "Este usuario se encuentra inactivo.";
                    txtContrasena.Clear();
                    txtUsuario.Focus();
                    return;
                }

                bool contrasenaCorrecta = PasswordHelper.VerificarContrasena(
                    contrasenaEscrita,
                    usuario.ContrasenaHash,
                    usuario.ContrasenaSalt);

                if (!contrasenaCorrecta)
                {
                    lblMensaje.Text = "Usuario o contraseña incorrectos.";
                    txtContrasena.Clear();
                    txtContrasena.Focus();
                    return;
                }

                SesionActual.Iniciar(usuario);
                Hide();

                using FrmPrincipal frmPrincipal = new FrmPrincipal();
                frmPrincipal.ShowDialog();

                if (frmPrincipal.CerrarSesionSolicitada)
                {
                    SesionActual.Cerrar();
                    txtUsuario.Clear();
                    txtContrasena.Clear();
                    chkMostrarContrasena.Checked = false;
                    txtContrasena.UseSystemPasswordChar = true;
                    lblMensaje.Text = string.Empty;
                    Show();
                    txtUsuario.Focus();
                }
                else
                {
                    SesionActual.Cerrar();
                    Close();
                }
            }
            catch (NpgsqlException)
            {
                SesionActual.Cerrar();

                if (!Visible)
                {
                    Show();
                }

                lblMensaje.Text = "No fue posible conectar con la base de datos.";
            }
            catch (Exception)
            {
                SesionActual.Cerrar();

                if (!Visible)
                {
                    Show();
                }

                lblMensaje.Text = "Ocurrió un error al iniciar sesión.";
            }
            finally
            {
                if (!IsDisposed && !Disposing)
                {
                    btnIniciarSesion.Enabled = true;
                }
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

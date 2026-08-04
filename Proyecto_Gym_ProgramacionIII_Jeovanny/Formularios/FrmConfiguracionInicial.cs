using Npgsql;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Datos;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Seguridad;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    public partial class FrmConfiguracionInicial : Form
    {
        public FrmConfiguracionInicial()
        {
            InitializeComponent();
        }

        private async void btnCrearAdministrador_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
            {
                return;
            }

            btnCrearAdministrador.Enabled = false;
            lblMensaje.Text = string.Empty;
            bool administradorCreado = false;

            try
            {
                string nombreUsuario = txtNombreUsuario.Text.Trim();
                string nombreCompleto = txtNombreCompleto.Text.Trim();

                bool nombreUsuarioExiste = await UsuarioRepositorio.ExisteNombreUsuarioAsync(nombreUsuario);

                if (nombreUsuarioExiste)
                {
                    lblMensaje.Text = "El nombre de usuario ya existe.";
                    txtNombreUsuario.Focus();
                    return;
                }

                PasswordHelper.CrearHash(
                    txtContrasena.Text,
                    out string contrasenaHash,
                    out string contrasenaSalt);

                await UsuarioRepositorio.CrearAdministradorAsync(
                    nombreUsuario,
                    nombreCompleto,
                    contrasenaHash,
                    contrasenaSalt);

                administradorCreado = true;

                MessageBox.Show(
                    "Administrador creado correctamente.",
                    "Configuración completada",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (NpgsqlException)
            {
                lblMensaje.Text = "No fue posible guardar el administrador. Verifica la conexión e inténtalo nuevamente.";
            }
            catch (InvalidOperationException ex)
            {
                lblMensaje.Text = ex.Message;
            }
            catch (Exception)
            {
                lblMensaje.Text = "Ocurrió un error al crear el administrador.";
            }
            finally
            {
                if (!administradorCreado)
                {
                    btnCrearAdministrador.Enabled = true;
                }
            }
        }

        private bool ValidarCampos()
        {
            string nombreCompleto = txtNombreCompleto.Text.Trim();
            string nombreUsuario = txtNombreUsuario.Text.Trim();
            string contrasena = txtContrasena.Text;
            string confirmarContrasena = txtConfirmarContrasena.Text;

            lblMensaje.Text = string.Empty;

            if (string.IsNullOrEmpty(nombreCompleto))
            {
                lblMensaje.Text = "Debe escribir el nombre completo.";
                txtNombreCompleto.Focus();
                return false;
            }

            if (nombreCompleto.Length < 4)
            {
                lblMensaje.Text = "El nombre completo debe tener al menos 4 caracteres.";
                txtNombreCompleto.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(nombreUsuario))
            {
                lblMensaje.Text = "Debe escribir el nombre de usuario.";
                txtNombreUsuario.Focus();
                return false;
            }

            if (nombreUsuario.Length < 4)
            {
                lblMensaje.Text = "El nombre de usuario debe tener al menos 4 caracteres.";
                txtNombreUsuario.Focus();
                return false;
            }

            if (txtNombreUsuario.Text.Contains(' '))
            {
                lblMensaje.Text = "El nombre de usuario no puede contener espacios.";
                txtNombreUsuario.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(contrasena))
            {
                lblMensaje.Text = "Debe escribir una contraseña.";
                txtContrasena.Focus();
                return false;
            }

            if (contrasena.Length < 8)
            {
                lblMensaje.Text = "La contraseña debe tener al menos 8 caracteres.";
                txtContrasena.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(confirmarContrasena))
            {
                lblMensaje.Text = "Debe confirmar la contraseña.";
                txtConfirmarContrasena.Focus();
                return false;
            }

            if (contrasena != confirmarContrasena)
            {
                lblMensaje.Text = "Las contraseñas no coinciden.";
                txtConfirmarContrasena.Focus();
                return false;
            }

            return true;
        }

        private void chkMostrarContrasena_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMostrarContrasena.Checked)
            {
                txtContrasena.UseSystemPasswordChar = false;
                txtConfirmarContrasena.UseSystemPasswordChar = false;
            }
            else
            {
                txtContrasena.UseSystemPasswordChar = true;
                txtConfirmarContrasena.UseSystemPasswordChar = true;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show(
                "¿Deseas cerrar la configuración inicial?",
                "Confirmar salida",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }
    }
}

using Npgsql;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Datos;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Seguridad;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    public partial class FrmCambiarContrasena : Form
    {
        public FrmCambiarContrasena()
        {
            InitializeComponent();
        }

        private bool ValidarCampos()
        {
            lblMensaje.Text = string.Empty;

            if (string.IsNullOrWhiteSpace(txtContrasenaActual.Text))
            {
                lblMensaje.Text = "Debe escribir la contraseña actual.";
                txtContrasenaActual.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtContrasenaNueva.Text))
            {
                lblMensaje.Text = "Debe escribir la contraseña nueva.";
                txtContrasenaNueva.Focus();
                return false;
            }

            if (txtContrasenaNueva.Text.Length < 4)
            {
                lblMensaje.Text = "La contraseña nueva debe tener al menos 4 caracteres.";
                txtContrasenaNueva.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtConfirmarContrasena.Text))
            {
                lblMensaje.Text = "Debe confirmar la contraseña nueva.";
                txtConfirmarContrasena.Focus();
                return false;
            }

            if (txtContrasenaNueva.Text != txtConfirmarContrasena.Text)
            {
                lblMensaje.Text = "Las contraseñas nuevas no coinciden.";
                txtConfirmarContrasena.Focus();
                return false;
            }

            return true;
        }

        private async void btnGuardar_Click(object? sender, EventArgs e)
        {
            if (!ValidarCampos())
            {
                return;
            }

            btnGuardar.Enabled = false;
            try
            {
                Usuario? usuario = await UsuarioRepositorio.ObtenerPorIdAsync(SesionActual.IdUsuario);
                if (usuario is null)
                {
                    lblMensaje.Text = "No fue posible localizar el usuario de la sesión.";
                    return;
                }

                if (!PasswordHelper.VerificarContrasena(
                    txtContrasenaActual.Text,
                    usuario.ContrasenaHash,
                    usuario.ContrasenaSalt))
                {
                    lblMensaje.Text = "La contraseña actual es incorrecta.";
                    txtContrasenaActual.Clear();
                    txtContrasenaActual.Focus();
                    return;
                }

                PasswordHelper.CrearHash(
                    txtContrasenaNueva.Text,
                    out string contrasenaHash,
                    out string contrasenaSalt);

                await UsuarioRepositorio.ActualizarContrasenaAsync(
                    usuario.IdUsuario,
                    contrasenaHash,
                    contrasenaSalt);

                MessageBox.Show("Contraseña cambiada correctamente.", "Cambiar contraseña", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (NpgsqlException)
            {
                lblMensaje.Text = "No fue posible cambiar la contraseña en la base de datos.";
            }
            catch (Exception)
            {
                lblMensaje.Text = "Ocurrió un error al cambiar la contraseña.";
            }
            finally
            {
                btnGuardar.Enabled = true;
            }
        }

        private void chkMostrarContrasena_CheckedChanged(object? sender, EventArgs e)
        {
            bool mostrar = chkMostrarContrasena.Checked;
            txtContrasenaActual.UseSystemPasswordChar = !mostrar;
            txtContrasenaNueva.UseSystemPasswordChar = !mostrar;
            txtConfirmarContrasena.UseSystemPasswordChar = !mostrar;
        }
    }
}

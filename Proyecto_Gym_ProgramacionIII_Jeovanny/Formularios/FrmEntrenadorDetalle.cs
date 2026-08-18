using Npgsql;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Datos;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    public partial class FrmEntrenadorDetalle : Form
    {
        private readonly Entrenador? entrenadorEditar;

        public FrmEntrenadorDetalle()
        {
            InitializeComponent();
            PrepararNuevoEntrenador();
        }

        public FrmEntrenadorDetalle(Entrenador entrenador)
        {
            entrenadorEditar = entrenador;
            InitializeComponent();
            Text = "Editar entrenador";
            lblTitulo.Text = Text;
            CargarEntrenador();
        }

        private void PrepararNuevoEntrenador()
        {
            txtCedula.Mask = string.Empty;
            txtTelefono.Mask = string.Empty;
        }

        private void CargarEntrenador()
        {
            if (entrenadorEditar is null)
            {
                return;
            }

            txtNombre.Text = entrenadorEditar.Nombre;
            txtApellido.Text = entrenadorEditar.Apellido;
            txtCedula.Text = entrenadorEditar.Cedula;
            txtTelefono.Text = entrenadorEditar.Telefono;
            txtCorreo.Text = entrenadorEditar.Correo;
            txtEspecialidad.Text = entrenadorEditar.Especialidad;
            dtpFechaContratacion.Value = entrenadorEditar.FechaContratacion;
            chkEstado.Checked = entrenadorEditar.Estado;
        }

        private bool ValidarCampos()
        {
            lblMensaje.Text = string.Empty;

            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                lblMensaje.Text = "El nombre y el apellido son obligatorios.";
                return false;
            }

            if (!EsNombreValido(txtNombre.Text.Trim()) || !EsNombreValido(txtApellido.Text.Trim()))
            {
                lblMensaje.Text = "El nombre y el apellido solo pueden contener letras y espacios.";
                txtNombre.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtCedula.Mask) || !txtCedula.MaskCompleted)
            {
                lblMensaje.Text = "La cédula debe contener 11 números.";
                txtCedula.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtTelefono.Mask) || !txtTelefono.MaskCompleted)
            {
                lblMensaje.Text = "El teléfono debe contener 10 números.";
                txtTelefono.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEspecialidad.Text))
            {
                lblMensaje.Text = "La especialidad es obligatoria.";
                txtEspecialidad.Focus();
                return false;
            }

            string correo = txtCorreo.Text.Trim();
            if (!string.IsNullOrEmpty(correo))
            {
                int arroba = correo.IndexOf('@');
                int punto = correo.IndexOf('.', arroba + 1);
                if (arroba <= 0 || punto <= arroba + 1 || punto == correo.Length - 1)
                {
                    lblMensaje.Text = "El correo debe contener @ y un punto después del @.";
                    txtCorreo.Focus();
                    return false;
                }
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
                int idEntrenador = entrenadorEditar?.IdEntrenador ?? 0;
                string cedula = txtCedula.Text;
                if (await EntrenadorRepositorio.ExisteCedulaAsync(cedula, idEntrenador))
                {
                    lblMensaje.Text = "Ya existe un entrenador registrado con esa cédula.";
                    return;
                }

                Entrenador entrenador = new Entrenador
                {
                    IdEntrenador = idEntrenador,
                    Nombre = txtNombre.Text.Trim(),
                    Apellido = txtApellido.Text.Trim(),
                    Cedula = cedula,
                    Telefono = txtTelefono.Text,
                    Correo = txtCorreo.Text.Trim(),
                    Especialidad = txtEspecialidad.Text.Trim(),
                    FechaContratacion = dtpFechaContratacion.Value.Date,
                    Estado = chkEstado.Checked
                };

                if (entrenadorEditar is null)
                {
                    await EntrenadorRepositorio.GuardarAsync(entrenador);
                }
                else
                {
                    await EntrenadorRepositorio.ActualizarAsync(entrenador);
                }

                MessageBox.Show("Entrenador guardado correctamente.", "Entrenadores", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (PostgresException excepcion) when (excepcion.SqlState == PostgresErrorCodes.UniqueViolation)
            {
                lblMensaje.Text = "La cédula o el correo ya están registrados.";
            }
            catch (NpgsqlException)
            {
                lblMensaje.Text = "No fue posible realizar la operación en la base de datos.";
            }
            catch (Exception)
            {
                lblMensaje.Text = "Ocurrió un error al realizar la operación.";
            }
            finally
            {
                btnGuardar.Enabled = true;
            }
        }

        private static bool EsNombreValido(string texto)
        {
            foreach (char caracter in texto)
            {
                if (!char.IsLetter(caracter) && caracter != ' ')
                {
                    return false;
                }
            }

            return true;
        }

        private void txtNombreApellido_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ' && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtCedula_Enter(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCedula.Mask))
            {
                txtCedula.Mask = "000-0000000-0";
            }
        }

        private void txtCedula_Leave(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCedula.Text))
            {
                txtCedula.Mask = string.Empty;
            }
        }

        private void txtTelefono_Enter(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTelefono.Mask))
            {
                txtTelefono.Mask = "(000) 000-0000";
            }
        }

        private void txtTelefono_Leave(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTelefono.Text))
            {
                txtTelefono.Mask = string.Empty;
            }
        }
    }
}

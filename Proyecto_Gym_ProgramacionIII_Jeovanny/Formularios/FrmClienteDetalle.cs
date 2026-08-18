using Npgsql;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Datos;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    public partial class FrmClienteDetalle : Form
    {
        private readonly Cliente? clienteEditar;
        private string rutaFoto = string.Empty;

        public FrmClienteDetalle()
        {
            InitializeComponent();
            PrepararControlesOpcionales();
            PrepararNuevoCliente();
        }

        public FrmClienteDetalle(Cliente cliente)
        {
            InitializeComponent();
            PrepararControlesOpcionales();
            clienteEditar = cliente;
            CargarCliente();
        }

        private void PrepararControlesOpcionales()
        {
            dtpFechaNacimiento.ShowCheckBox = true;

            if (!cmbSexo.Items.Contains("OTRO"))
            {
                cmbSexo.Items.Add("OTRO");
            }
        }

        private void PrepararNuevoCliente()
        {
            lblTitulo.Text = "Nuevo cliente";
            txtCedula.Mask = string.Empty;
            txtTelefono.Mask = string.Empty;
            cmbSexo.SelectedIndex = -1;
            chkEstado.Checked = true;
            dtpFechaNacimiento.Checked = false;
        }

        private void CargarCliente()
        {
            if (clienteEditar is null)
            {
                return;
            }

            lblTitulo.Text = "Editar cliente";
            txtNombre.Text = clienteEditar.Nombre;
            txtApellido.Text = clienteEditar.Apellido;
            string cedula = ExtraerDigitos(clienteEditar.Cedula);
            string telefono = ExtraerDigitos(clienteEditar.Telefono);

            if (cedula.Length == 11)
            {
                txtCedula.Text = cedula;
            }

            if (telefono.Length == 10)
            {
                txtTelefono.Text = telefono;
            }

            txtCorreo.Text = clienteEditar.Correo;
            txtDireccion.Text = clienteEditar.Direccion;
            string sexoGuardado = clienteEditar.Sexo.Trim().ToUpperInvariant();
            cmbSexo.SelectedItem = sexoGuardado;
            chkEstado.Checked = clienteEditar.Estado;
            rutaFoto = clienteEditar.Foto;

            if (clienteEditar.FechaNacimiento.HasValue)
            {
                dtpFechaNacimiento.Value = clienteEditar.FechaNacimiento.Value;
                dtpFechaNacimiento.Checked = true;
            }
            else
            {
                dtpFechaNacimiento.Checked = false;
            }

            MostrarVistaPreviaFoto();
        }

        private bool ValidarCampos()
        {
            lblMensaje.Text = string.Empty;

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                lblMensaje.Text = "El nombre es obligatorio.";
                txtNombre.Focus();
                return false;
            }

            if (!EsNombreValido(txtNombre.Text.Trim()))
            {
                lblMensaje.Text = "El nombre solo puede contener letras y espacios.";
                txtNombre.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                lblMensaje.Text = "El apellido es obligatorio.";
                txtApellido.Focus();
                return false;
            }

            if (!EsNombreValido(txtApellido.Text.Trim()))
            {
                lblMensaje.Text = "El apellido solo puede contener letras y espacios.";
                txtApellido.Focus();
                return false;
            }

            string cedula = ExtraerDigitos(txtCedula.Text);

            if (!txtCedula.MaskCompleted || cedula.Length != 11)
            {
                lblMensaje.Text = "La cédula es obligatoria y debe tener exactamente 11 dígitos.";
                txtCedula.Focus();
                return false;
            }

            string telefono = ExtraerDigitos(txtTelefono.Text);

            if (!txtTelefono.MaskCompleted || telefono.Length != 10)
            {
                lblMensaje.Text = "El teléfono es obligatorio y debe tener exactamente 10 dígitos.";
                txtTelefono.Focus();
                return false;
            }

            string correo = txtCorreo.Text.Trim();

            if (!string.IsNullOrEmpty(correo))
            {
                int posicionArroba = correo.IndexOf('@');
                int posicionPunto = correo.IndexOf('.', posicionArroba + 1);

                if (posicionArroba <= 0 || posicionPunto <= posicionArroba + 1 || posicionPunto == correo.Length - 1)
                {
                    lblMensaje.Text = "El correo debe contener @ y un punto después del @.";
                    txtCorreo.Focus();
                    return false;
                }
            }

            if (cmbSexo.SelectedIndex < 0)
            {
                lblMensaje.Text = "El sexo es obligatorio.";
                cmbSexo.Focus();
                return false;
            }

            return true;
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
            {
                return;
            }

            btnGuardar.Enabled = false;
            bool cerrarFormulario = false;

            try
            {
                int idClienteIgnorar = clienteEditar?.IdCliente ?? 0;
                string cedula = ExtraerDigitos(txtCedula.Text);
                string telefono = ExtraerDigitos(txtTelefono.Text);
                bool existeCedula = await ClienteRepositorio.ExisteCedulaAsync(
                    cedula,
                    idClienteIgnorar);

                if (existeCedula)
                {
                    lblMensaje.Text = "Ya existe un cliente registrado con esa cédula.";
                    txtCedula.Focus();
                    return;
                }

                Cliente cliente = new Cliente
                {
                    IdCliente = idClienteIgnorar,
                    Nombre = txtNombre.Text.Trim(),
                    Apellido = txtApellido.Text.Trim(),
                    Cedula = cedula,
                    Telefono = telefono,
                    Correo = txtCorreo.Text.Trim(),
                    Direccion = txtDireccion.Text.Trim(),
                    FechaNacimiento = dtpFechaNacimiento.Checked
                        ? dtpFechaNacimiento.Value.Date
                        : null,
                    Sexo = cmbSexo.SelectedItem?.ToString() ?? string.Empty,
                    Foto = rutaFoto,
                    FechaRegistro = clienteEditar?.FechaRegistro ?? default,
                    Estado = chkEstado.Checked
                };

                if (clienteEditar is null)
                {
                    await ClienteRepositorio.GuardarAsync(cliente);
                }
                else
                {
                    await ClienteRepositorio.ActualizarAsync(cliente);
                }

                MessageBox.Show(
                    "Cliente guardado correctamente.",
                    "Clientes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                cerrarFormulario = true;
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

            if (cerrarFormulario)
            {
                Close();
            }
        }

        private void txtNombreApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ' && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtCedula_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCedula.Mask))
            {
                txtCedula.Mask = "000-0000000-0";
            }
        }

        private void txtCedula_Leave(object sender, EventArgs e)
        {
            if (ExtraerDigitos(txtCedula.Text).Length == 0)
            {
                txtCedula.Mask = string.Empty;
            }
        }

        private void txtTelefono_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTelefono.Mask))
            {
                txtTelefono.Mask = "(000) 000-0000";
            }
        }

        private void txtTelefono_Leave(object sender, EventArgs e)
        {
            if (ExtraerDigitos(txtTelefono.Text).Length == 0)
            {
                txtTelefono.Mask = string.Empty;
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

        private static string ExtraerDigitos(string texto)
        {
            string digitos = string.Empty;

            foreach (char caracter in texto)
            {
                if (char.IsDigit(caracter))
                {
                    digitos += caracter;
                }
            }

            return digitos;
        }

        private void btnSeleccionarFoto_Click(object sender, EventArgs e)
        {
            using OpenFileDialog selectorFoto = new OpenFileDialog
            {
                Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.bmp",
                CheckFileExists = true,
                Multiselect = false,
                Title = "Seleccionar fotografía del cliente"
            };

            if (selectorFoto.ShowDialog(this) == DialogResult.OK)
            {
                rutaFoto = selectorFoto.FileName;
                MostrarVistaPreviaFoto();
            }
        }

        private void btnQuitarFoto_Click(object sender, EventArgs e)
        {
            rutaFoto = string.Empty;
            LimpiarVistaPreviaFoto();
        }

        private void MostrarVistaPreviaFoto()
        {
            LimpiarVistaPreviaFoto();

            if (string.IsNullOrWhiteSpace(rutaFoto) || !File.Exists(rutaFoto))
            {
                return;
            }

            try
            {
                using Image imagen = Image.FromFile(rutaFoto);
                picFoto.Image = new Bitmap(imagen);
            }
            catch (Exception)
            {
                picFoto.Image = null;
            }
        }

        private void LimpiarVistaPreviaFoto()
        {
            picFoto.Image?.Dispose();
            picFoto.Image = null;
        }

        private void FrmClienteDetalle_FormClosed(object sender, FormClosedEventArgs e)
        {
            LimpiarVistaPreviaFoto();
        }

    }
}

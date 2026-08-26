using Npgsql;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Datos;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    public partial class FrmProveedorDetalle : Form
    {
        private readonly Proveedor? proveedorEditar;
        private bool actualizandoDocumento;

        public FrmProveedorDetalle()
        {
            InitializeComponent();
            ConfigurarCamposNumericos();
            txtTelefono.Mask = string.Empty;
        }

        public FrmProveedorDetalle(Proveedor proveedor)
        {
            proveedorEditar = proveedor;
            InitializeComponent();
            Text = "Editar proveedor";
            lblTitulo.Text = Text;
            ConfigurarCamposNumericos();
            txtNombre.Text = proveedor.Nombre;
            txtRncCedula.Text = proveedor.RncCedula;
            txtTelefono.Text = ExtraerDigitos(proveedor.Telefono);
            txtCorreo.Text = proveedor.Correo;
            txtDireccion.Text = proveedor.Direccion;
            chkEstado.Checked = proveedor.Estado;
        }

        private void ConfigurarCamposNumericos()
        {
            txtRncCedula.MaxLength = 13;
            txtRncCedula.TextChanged += txtRncCedula_TextChanged;
            txtTelefono.HidePromptOnLeave = true;
            txtTelefono.PromptChar = ' ';
            txtTelefono.ResetOnSpace = false;
            txtTelefono.Enter += txtTelefono_Enter;
            txtTelefono.Leave += txtTelefono_Leave;
        }

        private async void btnGuardar_Click(object? sender, EventArgs e)
        {
            lblMensaje.Text = string.Empty;
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtRncCedula.Text))
            {
                lblMensaje.Text = "El nombre y el RNC o cédula son obligatorios.";
                return;
            }
            string documento = ExtraerDigitos(txtRncCedula.Text);
            if (documento.Length != 9 && documento.Length != 11)
            {
                lblMensaje.Text = "El RNC debe tener 9 dígitos o la cédula debe tener 11 dígitos.";
                txtRncCedula.Focus();
                return;
            }

            string telefono = ExtraerDigitos(txtTelefono.Text);
            if (!txtTelefono.MaskCompleted || telefono.Length != 10)
            {
                lblMensaje.Text = "El teléfono debe contener 10 números.";
                txtTelefono.Focus();
                return;
            }
            string correo = txtCorreo.Text.Trim();
            if (!string.IsNullOrEmpty(correo))
            {
                int arroba = correo.IndexOf('@');
                int punto = correo.IndexOf('.', arroba + 1);
                if (arroba <= 0 || punto <= arroba + 1 || punto == correo.Length - 1)
                {
                    lblMensaje.Text = "El correo debe contener @ y un punto después del @.";
                    return;
                }
            }
            btnGuardar.Enabled = false;
            try
            {
                int id = proveedorEditar?.IdProveedor ?? 0;
                if (await ProveedorRepositorio.ExisteDocumentoAsync(documento, id))
                {
                    lblMensaje.Text = "Ya existe un proveedor con ese RNC o cédula.";
                    txtRncCedula.Focus();
                    return;
                }
                Proveedor proveedor = new Proveedor
                {
                    IdProveedor = id,
                    Nombre = txtNombre.Text.Trim(),
                    RncCedula = documento,
                    Telefono = telefono,
                    Correo = correo,
                    Direccion = txtDireccion.Text.Trim(),
                    Estado = chkEstado.Checked
                };
                if (proveedorEditar is null) await ProveedorRepositorio.GuardarAsync(proveedor);
                else await ProveedorRepositorio.ActualizarAsync(proveedor);
                MessageBox.Show("Proveedor guardado correctamente.", "Proveedores", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (PostgresException excepcion) when (excepcion.SqlState == PostgresErrorCodes.UniqueViolation)
            {
                lblMensaje.Text = "El RNC, la cédula o el correo ya están registrados.";
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

        private void txtRncCedula_TextChanged(object? sender, EventArgs e)
        {
            if (actualizandoDocumento)
            {
                return;
            }

            string digitos = ExtraerDigitos(txtRncCedula.Text);
            if (digitos.Length > 11)
            {
                digitos = digitos.Substring(0, 11);
            }

            string documentoFormateado = FormatearDocumento(digitos);

            if (txtRncCedula.Text == documentoFormateado)
            {
                return;
            }

            actualizandoDocumento = true;
            txtRncCedula.Text = documentoFormateado;
            txtRncCedula.SelectionStart = txtRncCedula.Text.Length;
            actualizandoDocumento = false;
        }

        private void txtRncCedula_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                return;
            }

            if (char.IsDigit(e.KeyChar)
                && ExtraerDigitos(txtRncCedula.Text).Length >= 11
                && txtRncCedula.SelectionLength == 0)
            {
                e.Handled = true;
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
            if (ExtraerDigitos(txtTelefono.Text).Length == 0)
            {
                txtTelefono.Mask = string.Empty;
            }
        }

        private static string FormatearDocumento(string digitos)
        {
            if (digitos.Length <= 3)
            {
                return digitos;
            }

            if (digitos.Length <= 10)
            {
                return digitos.Substring(0, 3) + "-" + digitos.Substring(3);
            }

            return digitos.Substring(0, 3)
                + "-"
                + digitos.Substring(3, 7)
                + "-"
                + digitos.Substring(10, 1);
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
    }
}

using Npgsql;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Datos;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    public partial class FrmClientes : Form
    {
        public FrmClientes()
        {
            InitializeComponent();
        }

        private async void FrmClientes_Load(object sender, EventArgs e)
        {
            await CargarClientesAsync();
        }

        private async Task CargarClientesAsync()
        {
            try
            {
                List<Cliente> clientes = await ClienteRepositorio.ListarAsync();
                dgvClientes.DataSource = null;
                dgvClientes.DataSource = clientes;
                ConfigurarColumnas();
                ActualizarTextoBotonEstado();
            }
            catch (NpgsqlException)
            {
                MessageBox.Show(
                    "No fue posible realizar la operación en la base de datos.",
                    "Clientes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (Exception)
            {
                MessageBox.Show(
                    "Ocurrió un error al realizar la operación.",
                    "Clientes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private async Task BuscarClientesAsync()
        {
            try
            {
                string texto = txtBuscar.Text.Trim();
                List<Cliente> clientes = await ClienteRepositorio.BuscarAsync(texto);
                dgvClientes.DataSource = null;
                dgvClientes.DataSource = clientes;
                ConfigurarColumnas();
                ActualizarTextoBotonEstado();
            }
            catch (NpgsqlException)
            {
                MessageBox.Show(
                    "No fue posible realizar la operación en la base de datos.",
                    "Clientes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (Exception)
            {
                MessageBox.Show(
                    "Ocurrió un error al realizar la operación.",
                    "Clientes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private Cliente? ObtenerClienteSeleccionado()
        {
            if (dgvClientes.CurrentRow?.DataBoundItem is Cliente cliente)
            {
                return cliente;
            }

            return null;
        }

        private void ActualizarTextoBotonEstado()
        {
            Cliente? cliente = ObtenerClienteSeleccionado();
            btnEditar.Enabled = cliente is not null;
        }

        private async void btnNuevo_Click(object sender, EventArgs e)
        {
            using FrmClienteDetalle formulario = new FrmClienteDetalle();

            if (formulario.ShowDialog(this) == DialogResult.OK)
            {
                await CargarClientesAsync();
            }
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            Cliente? clienteSeleccionado = ObtenerClienteSeleccionado();

            if (clienteSeleccionado is null)
            {
                MessageBox.Show(
                    "Selecciona un cliente para editar.",
                    "Clientes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            using FrmClienteDetalle formulario = new FrmClienteDetalle(clienteSeleccionado);

            if (formulario.ShowDialog(this) == DialogResult.OK)
            {
                await CargarClientesAsync();
            }
        }

        private async void btnActualizar_Click(object sender, EventArgs e)
        {
            await CargarClientesAsync();
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            await BuscarClientesAsync();
        }

        private async void btnLimpiarBusqueda_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            await CargarClientesAsync();
            txtBuscar.Focus();
        }

        private async void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                await BuscarClientesAsync();
            }
        }

        private void dgvClientes_SelectionChanged(object sender, EventArgs e)
        {
            ActualizarTextoBotonEstado();
        }

        private void dgvClientes_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.Value is null)
            {
                return;
            }

            DataGridViewColumn columna = dgvClientes.Columns[e.ColumnIndex];

            if (columna.DataPropertyName == nameof(Cliente.Cedula) && e.Value is string cedula)
            {
                e.Value = FormatearCedula(cedula);
                e.FormattingApplied = true;
                return;
            }

            if (columna.DataPropertyName == nameof(Cliente.Telefono) && e.Value is string telefono)
            {
                e.Value = FormatearTelefono(telefono);
                e.FormattingApplied = true;
                return;
            }

        }

        private void ConfigurarColumnas()
        {
            if (dgvClientes.Columns.Count == 0)
            {
                return;
            }

            dgvClientes.Columns[nameof(Cliente.IdCliente)]!.Visible = false;
            dgvClientes.Columns[nameof(Cliente.Nombre)]!.HeaderText = "Nombre";
            dgvClientes.Columns[nameof(Cliente.Apellido)]!.HeaderText = "Apellido";
            dgvClientes.Columns[nameof(Cliente.Cedula)]!.HeaderText = "Cédula";
            dgvClientes.Columns[nameof(Cliente.Telefono)]!.HeaderText = "Teléfono";
            dgvClientes.Columns[nameof(Cliente.Estado)]!.Visible = false;
            dgvClientes.Columns[nameof(Cliente.EstadoTexto)]!.HeaderText = "Estado";

            dgvClientes.Columns[nameof(Cliente.Correo)]!.Visible = false;
            dgvClientes.Columns[nameof(Cliente.Direccion)]!.Visible = false;
            dgvClientes.Columns[nameof(Cliente.FechaNacimiento)]!.Visible = false;
            dgvClientes.Columns[nameof(Cliente.Sexo)]!.HeaderText = "Sexo";
            dgvClientes.Columns[nameof(Cliente.Foto)]!.Visible = false;
            dgvClientes.Columns[nameof(Cliente.FechaRegistro)]!.Visible = false;
        }

        private static string FormatearCedula(string valor)
        {
            string digitos = ExtraerDigitos(valor);

            if (digitos.Length != 11)
            {
                return valor;
            }

            return $"{digitos.Substring(0, 3)}-{digitos.Substring(3, 7)}-{digitos.Substring(10, 1)}";
        }

        private static string FormatearTelefono(string valor)
        {
            string digitos = ExtraerDigitos(valor);

            if (digitos.Length != 10)
            {
                return valor;
            }

            return $"({digitos.Substring(0, 3)}) {digitos.Substring(3, 3)}-{digitos.Substring(6, 4)}";
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

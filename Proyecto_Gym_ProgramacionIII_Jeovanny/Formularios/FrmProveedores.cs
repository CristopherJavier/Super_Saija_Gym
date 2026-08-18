using Npgsql;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Datos;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    public partial class FrmProveedores : Form
    {
        public FrmProveedores()
        {
            InitializeComponent();
        }

        private async Task CargarAsync(string texto = "")
        {
            try
            {
                dgvProveedores.DataSource = await ProveedorRepositorio.ListarAsync(texto);
                if (dgvProveedores.Columns.Count > 0)
                {
                    dgvProveedores.Columns[nameof(Proveedor.IdProveedor)]!.Visible = false;
                    dgvProveedores.Columns[nameof(Proveedor.Nombre)]!.HeaderText = "Nombre";
                    dgvProveedores.Columns[nameof(Proveedor.RncCedula)]!.HeaderText = "RNC o cédula";
                    dgvProveedores.Columns[nameof(Proveedor.Telefono)]!.HeaderText = "Teléfono";
                    dgvProveedores.Columns[nameof(Proveedor.Correo)]!.HeaderText = "Correo";
                    dgvProveedores.Columns[nameof(Proveedor.Estado)]!.Visible = false;
                    dgvProveedores.Columns[nameof(Proveedor.EstadoTexto)]!.HeaderText = "Estado";
                    dgvProveedores.Columns[nameof(Proveedor.Direccion)]!.Visible = false;
                }
                ActualizarBotones();
            }
            catch (NpgsqlException)
            {
                MostrarError("No fue posible realizar la operación en la base de datos.");
            }
            catch (Exception)
            {
                MostrarError("Ocurrió un error al realizar la operación.");
            }
        }

        private Proveedor? ObtenerSeleccionado() => dgvProveedores.CurrentRow?.DataBoundItem as Proveedor;

        private void ActualizarBotones()
        {
            Proveedor? proveedor = ObtenerSeleccionado();
            btnEditar.Enabled = proveedor is not null;
        }

        private async void FrmProveedores_Load(object? sender, EventArgs e)
        {
            await CargarAsync();
        }

        private async void btnNuevo_Click(object? sender, EventArgs e)
        {
            using FrmProveedorDetalle formulario = new FrmProveedorDetalle();
            if (formulario.ShowDialog(this) == DialogResult.OK) await CargarAsync();
        }

        private async void btnBuscar_Click(object? sender, EventArgs e)
        {
            await CargarAsync(txtBuscar.Text.Trim());
        }

        private async void btnActualizar_Click(object? sender, EventArgs e)
        {
            await CargarAsync();
        }

        private void dgvProveedores_SelectionChanged(object? sender, EventArgs e)
        {
            ActualizarBotones();
        }

        private async void btnEditar_Click(object? sender, EventArgs e)
        {
            Proveedor? proveedor = ObtenerSeleccionado();
            if (proveedor is null) return;
            using FrmProveedorDetalle formulario = new FrmProveedorDetalle(proveedor);
            if (formulario.ShowDialog(this) == DialogResult.OK) await CargarAsync();
        }

        private async void btnLimpiar_Click(object? sender, EventArgs e)
        {
            txtBuscar.Clear();
            await CargarAsync();
            txtBuscar.Focus();
        }

        private async void txtBuscar_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                await CargarAsync(txtBuscar.Text.Trim());
            }
        }

        private static void MostrarError(string mensaje) => MessageBox.Show(mensaje, "Proveedores", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }
}

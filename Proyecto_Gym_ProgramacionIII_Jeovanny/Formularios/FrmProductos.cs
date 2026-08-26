using Npgsql;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Datos;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    public partial class FrmProductos : Form
    {
        public FrmProductos()
        {
            InitializeComponent();
        }

        private async Task CargarAsync(string texto = "")
        {
            try
            {
                dgvProductos.DataSource = await ProductoRepositorio.ListarAsync(texto);
                ConfigurarColumnas();
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

        private void ConfigurarColumnas()
        {
            if (dgvProductos.Columns.Count == 0) return;
            dgvProductos.Columns[nameof(Producto.IdProducto)]!.Visible = false;
            dgvProductos.Columns[nameof(Producto.Codigo)]!.HeaderText = "Código";
            dgvProductos.Columns[nameof(Producto.Nombre)]!.HeaderText = "Nombre";
            dgvProductos.Columns[nameof(Producto.NombreCategoria)]!.HeaderText = "Categoría";
            dgvProductos.Columns[nameof(Producto.NombreMarca)]!.HeaderText = "Marca";
            dgvProductos.Columns[nameof(Producto.PrecioVenta)]!.HeaderText = "Precio venta";
            dgvProductos.Columns[nameof(Producto.PrecioVenta)]!.DefaultCellStyle.Format = "N2";
            dgvProductos.Columns[nameof(Producto.Stock)]!.HeaderText = "Existencia";
            dgvProductos.Columns[nameof(Producto.Estado)]!.Visible = false;
            dgvProductos.Columns[nameof(Producto.EstadoTexto)]!.HeaderText = "Estado";
            dgvProductos.Columns[nameof(Producto.Descripcion)]!.Visible = false;
            dgvProductos.Columns[nameof(Producto.IdCategoria)]!.Visible = false;
            dgvProductos.Columns[nameof(Producto.IdMarca)]!.Visible = false;
            dgvProductos.Columns[nameof(Producto.PrecioCompra)]!.Visible = false;
            dgvProductos.Columns[nameof(Producto.StockMinimo)]!.Visible = false;
            dgvProductos.Columns[nameof(Producto.Imagen)]!.Visible = false;
        }

        private Producto? ObtenerSeleccionado() => dgvProductos.CurrentRow?.DataBoundItem as Producto;

        private void ActualizarBotones()
        {
            Producto? producto = ObtenerSeleccionado();
            btnEditar.Enabled = producto is not null;
        }

        private async void FrmProductos_Load(object? sender, EventArgs e)
        {
            await CargarAsync();
        }

        private async void btnNuevo_Click(object? sender, EventArgs e)
        {
            using FrmProductoDetalle formulario = new FrmProductoDetalle();
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

        private void dgvProductos_SelectionChanged(object? sender, EventArgs e)
        {
            ActualizarBotones();
        }

        private async void btnEditar_Click(object? sender, EventArgs e)
        {
            Producto? producto = ObtenerSeleccionado();
            if (producto is null) return;
            using FrmProductoDetalle formulario = new FrmProductoDetalle(producto);
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

        private static void MostrarError(string mensaje) => MessageBox.Show(mensaje, "Productos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }
}

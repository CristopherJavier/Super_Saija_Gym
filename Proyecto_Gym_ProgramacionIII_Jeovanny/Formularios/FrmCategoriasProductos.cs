using Npgsql;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Datos;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    public partial class FrmCategoriasProductos : Form
    {
        public FrmCategoriasProductos()
        {
            InitializeComponent();
        }

        private async Task CargarAsync(string texto = "")
        {
            try
            {
                dgvCategorias.DataSource = await CategoriaProductoRepositorio.ListarAsync(texto);
                if (dgvCategorias.Columns.Count > 0)
                {
                    dgvCategorias.Columns[nameof(CategoriaProducto.IdCategoria)]!.Visible = false;
                    dgvCategorias.Columns[nameof(CategoriaProducto.Nombre)]!.HeaderText = "Nombre";
                    dgvCategorias.Columns[nameof(CategoriaProducto.Descripcion)]!.HeaderText = "Descripción";
                    dgvCategorias.Columns[nameof(CategoriaProducto.Estado)]!.Visible = false;
                    dgvCategorias.Columns[nameof(CategoriaProducto.EstadoTexto)]!.HeaderText = "Estado";
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

        private CategoriaProducto? ObtenerSeleccionada()
        {
            return dgvCategorias.CurrentRow?.DataBoundItem as CategoriaProducto;
        }

        private void ActualizarBotones()
        {
            CategoriaProducto? categoria = ObtenerSeleccionada();
            btnEditar.Enabled = categoria is not null;
        }

        private async void FrmCategoriasProductos_Load(object? sender, EventArgs e)
        {
            await CargarAsync();
        }

        private async void btnNuevo_Click(object? sender, EventArgs e)
        {
            using FrmCategoriaProductoDetalle formulario = new FrmCategoriaProductoDetalle();
            if (formulario.ShowDialog(this) == DialogResult.OK)
            {
                await CargarAsync();
            }
        }

        private async void btnEditar_Click(object? sender, EventArgs e)
        {
            CategoriaProducto? categoria = ObtenerSeleccionada();
            if (categoria is null)
            {
                return;
            }

            using FrmCategoriaProductoDetalle formulario = new FrmCategoriaProductoDetalle(categoria);
            if (formulario.ShowDialog(this) == DialogResult.OK)
            {
                await CargarAsync();
            }
        }

        private async void btnActualizar_Click(object? sender, EventArgs e)
        {
            await CargarAsync();
        }

        private async void btnBuscar_Click(object? sender, EventArgs e)
        {
            await CargarAsync(txtBuscar.Text.Trim());
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

        private void dgvCategorias_SelectionChanged(object? sender, EventArgs e)
        {
            ActualizarBotones();
        }

        private static void MostrarError(string mensaje)
        {
            MessageBox.Show(mensaje, "Categorías de productos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}

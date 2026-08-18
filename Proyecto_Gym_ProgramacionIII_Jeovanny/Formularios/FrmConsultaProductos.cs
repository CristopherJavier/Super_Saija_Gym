using Npgsql;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Datos;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    public partial class FrmConsultaProductos : Form
    {
        public FrmConsultaProductos()
        {
            InitializeComponent();
        }

        private async void FrmConsultaProductos_Load(object? sender, EventArgs e)
        {
            await CargarProductosAsync();
        }

        private async Task CargarProductosAsync()
        {
            try
            {
                List<Producto> productos = await ProductoRepositorio.ListarAsync(txtBuscar.Text.Trim());
                dgvProductos.DataSource = null;
                dgvProductos.DataSource = productos;
            }
            catch (NpgsqlException)
            {
                MostrarError("No fue posible consultar los productos en la base de datos.");
            }
            catch (Exception)
            {
                MostrarError("Ocurrió un error al consultar los productos.");
            }
        }

        private async void btnBuscar_Click(object? sender, EventArgs e)
        {
            await CargarProductosAsync();
        }

        private async void btnLimpiar_Click(object? sender, EventArgs e)
        {
            txtBuscar.Clear();
            await CargarProductosAsync();
            txtBuscar.Focus();
        }

        private async void txtBuscar_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                await CargarProductosAsync();
            }
        }

        private static void MostrarError(string mensaje)
        {
            MessageBox.Show(mensaje, "Consulta de productos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}

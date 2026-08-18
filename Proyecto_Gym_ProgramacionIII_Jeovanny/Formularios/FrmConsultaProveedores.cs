using Npgsql;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Datos;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    public partial class FrmConsultaProveedores : Form
    {
        public FrmConsultaProveedores()
        {
            InitializeComponent();
        }

        private async void FrmConsultaProveedores_Load(object? sender, EventArgs e)
        {
            await CargarProveedoresAsync();
        }

        private async Task CargarProveedoresAsync()
        {
            try
            {
                List<Proveedor> proveedores = await ProveedorRepositorio.ListarAsync(txtBuscar.Text.Trim());
                dgvProveedores.DataSource = null;
                dgvProveedores.DataSource = proveedores;
            }
            catch (NpgsqlException)
            {
                MostrarError("No fue posible consultar los proveedores en la base de datos.");
            }
            catch (Exception)
            {
                MostrarError("Ocurrió un error al consultar los proveedores.");
            }
        }

        private async void btnBuscar_Click(object? sender, EventArgs e)
        {
            await CargarProveedoresAsync();
        }

        private async void btnLimpiar_Click(object? sender, EventArgs e)
        {
            txtBuscar.Clear();
            await CargarProveedoresAsync();
            txtBuscar.Focus();
        }

        private async void txtBuscar_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                await CargarProveedoresAsync();
            }
        }

        private static void MostrarError(string mensaje)
        {
            MessageBox.Show(mensaje, "Consulta de proveedores", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}

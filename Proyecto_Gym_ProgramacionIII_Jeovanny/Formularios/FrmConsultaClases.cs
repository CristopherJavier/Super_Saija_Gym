using Npgsql;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Datos;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    public partial class FrmConsultaClases : Form
    {
        public FrmConsultaClases()
        {
            InitializeComponent();
        }

        private async void FrmConsultaClases_Load(object? sender, EventArgs e)
        {
            await CargarClasesAsync();
        }

        private async Task CargarClasesAsync()
        {
            try
            {
                List<ClaseActividad> clases = await ClaseActividadRepositorio.ListarAsync(txtBuscar.Text.Trim());
                dgvClases.DataSource = null;
                dgvClases.DataSource = clases;
            }
            catch (NpgsqlException)
            {
                MostrarError("No fue posible consultar las clases en la base de datos.");
            }
            catch (Exception)
            {
                MostrarError("Ocurrió un error al consultar las clases.");
            }
        }

        private async void btnBuscar_Click(object? sender, EventArgs e)
        {
            await CargarClasesAsync();
        }

        private async void btnLimpiar_Click(object? sender, EventArgs e)
        {
            txtBuscar.Clear();
            await CargarClasesAsync();
            txtBuscar.Focus();
        }

        private async void txtBuscar_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                await CargarClasesAsync();
            }
        }

        private static void MostrarError(string mensaje)
        {
            MessageBox.Show(mensaje, "Consulta de clases", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}

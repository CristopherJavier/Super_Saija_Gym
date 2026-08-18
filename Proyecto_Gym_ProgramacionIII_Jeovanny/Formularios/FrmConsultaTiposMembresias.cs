using Npgsql;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Datos;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    public partial class FrmConsultaTiposMembresias : Form
    {
        public FrmConsultaTiposMembresias()
        {
            InitializeComponent();
        }

        private async void FrmConsultaTiposMembresias_Load(object? sender, EventArgs e)
        {
            await CargarTiposMembresiasAsync();
        }

        private async Task CargarTiposMembresiasAsync()
        {
            try
            {
                string texto = txtBuscar.Text.Trim();
                List<TipoMembresia> tipos = await TipoMembresiaRepositorio.ListarAsync(texto);
                dgvTiposMembresias.DataSource = null;
                dgvTiposMembresias.DataSource = tipos;
            }
            catch (NpgsqlException)
            {
                MostrarError("No fue posible consultar los tipos de membresías en la base de datos.");
            }
            catch (Exception)
            {
                MostrarError("Ocurrió un error al consultar los tipos de membresías.");
            }
        }

        private async void btnBuscar_Click(object? sender, EventArgs e)
        {
            await CargarTiposMembresiasAsync();
        }

        private async void btnLimpiar_Click(object? sender, EventArgs e)
        {
            txtBuscar.Clear();
            await CargarTiposMembresiasAsync();
            txtBuscar.Focus();
        }

        private async void txtBuscar_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                await CargarTiposMembresiasAsync();
            }
        }

        private static void MostrarError(string mensaje)
        {
            MessageBox.Show(
                mensaje,
                "Consulta de tipos de membresías",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }
}

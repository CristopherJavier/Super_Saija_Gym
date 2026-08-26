using Npgsql;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Datos;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    public partial class FrmConsultaCargos : Form
    {
        public FrmConsultaCargos()
        {
            InitializeComponent();
            colMonto.DefaultCellStyle.Format = "N2";
            colFechaVencimiento.DefaultCellStyle.Format = "dd/MM/yyyy";
            Load += FrmConsultaCargos_Load;
        }

        private async void FrmConsultaCargos_Load(object? sender, EventArgs e)
        {
            await CargarCargosAsync();
        }

        private async Task CargarCargosAsync()
        {
            try
            {
                List<Cargo> cargos = await CargoRepositorio.ListarAsync(
                    texto: txtBuscar.Text.Trim());

                dgvCargos.DataSource = cargos.Select(cargo => new
                {
                    Cliente = cargo.NombreCliente,
                    cargo.Concepto,
                    cargo.Monto,
                    cargo.FechaVencimiento,
                    Estado = cargo.EstadoTexto
                }).ToList();
            }
            catch (NpgsqlException)
            {
                MostrarError("No fue posible consultar los cargos en la base de datos.");
            }
            catch (Exception)
            {
                MostrarError("Ocurrió un error al consultar los cargos.");
            }
        }

        private async void btnBuscar_Click(object? sender, EventArgs e)
        {
            await CargarCargosAsync();
        }

        private async void btnLimpiar_Click(object? sender, EventArgs e)
        {
            txtBuscar.Clear();
            await CargarCargosAsync();
            txtBuscar.Focus();
        }

        private async void txtBuscar_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                await CargarCargosAsync();
            }
        }

        private static void MostrarError(string mensaje)
        {
            MessageBox.Show(mensaje, "Consulta de cargos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}

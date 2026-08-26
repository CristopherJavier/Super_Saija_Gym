using Npgsql;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Datos;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    public partial class FrmConsultaCobros : Form
    {
        public FrmConsultaCobros()
        {
            InitializeComponent();
            colFecha.DefaultCellStyle.Format = "dd/MM/yyyy hh:mm tt";
            colMonto.DefaultCellStyle.Format = "N2";
            Load += FrmConsultaCobros_Load;
        }

        private async void FrmConsultaCobros_Load(object? sender, EventArgs e)
        {
            await CargarCobrosAsync();
        }

        private async Task CargarCobrosAsync()
        {
            try
            {
                List<Cobro> cobros = await CobroRepositorio.ListarAsync(
                    texto: txtBuscar.Text.Trim());

                dgvCobros.DataSource = cobros.Select(cobro => new
                {
                    cobro.Fecha,
                    Cliente = cobro.NombreCliente,
                    cobro.Concepto,
                    MetodoPago = cobro.NombreMetodoPago,
                    Monto = cobro.Total,
                    Usuario = cobro.NombreUsuario
                }).ToList();
            }
            catch (NpgsqlException)
            {
                MostrarError("No fue posible consultar los cobros en la base de datos.");
            }
            catch (Exception)
            {
                MostrarError("Ocurrió un error al consultar los cobros.");
            }
        }

        private async void btnBuscar_Click(object? sender, EventArgs e)
        {
            await CargarCobrosAsync();
        }

        private async void btnLimpiar_Click(object? sender, EventArgs e)
        {
            txtBuscar.Clear();
            await CargarCobrosAsync();
            txtBuscar.Focus();
        }

        private async void txtBuscar_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                await CargarCobrosAsync();
            }
        }

        private static void MostrarError(string mensaje)
        {
            MessageBox.Show(mensaje, "Consulta de cobros", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}

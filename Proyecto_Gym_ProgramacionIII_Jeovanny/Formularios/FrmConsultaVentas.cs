using Npgsql;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Datos;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    public partial class FrmConsultaVentas : Form
    {
        public FrmConsultaVentas()
        {
            InitializeComponent();
            colFecha.DefaultCellStyle.Format = "dd/MM/yyyy hh:mm tt";
            colTotal.DefaultCellStyle.Format = "N2";
            Load += FrmConsultaVentas_Load;
        }

        private async void FrmConsultaVentas_Load(object? sender, EventArgs e)
        {
            await CargarVentasAsync();
        }

        private async Task CargarVentasAsync()
        {
            try
            {
                List<Venta> ventas = await VentaRepositorio.ListarAsync(
                    texto: txtBuscar.Text.Trim());

                dgvVentas.DataSource = ventas.Select(venta => new
                {
                    venta.Fecha,
                    Cliente = venta.NombreCliente,
                    Usuario = venta.NombreUsuario,
                    venta.TipoPago,
                    venta.Total,
                    Estado = venta.EstadoTexto
                }).ToList();
            }
            catch (NpgsqlException)
            {
                MostrarError("No fue posible consultar las ventas en la base de datos.");
            }
            catch (Exception)
            {
                MostrarError("Ocurrió un error al consultar las ventas.");
            }
        }

        private async void btnBuscar_Click(object? sender, EventArgs e)
        {
            await CargarVentasAsync();
        }

        private async void btnLimpiar_Click(object? sender, EventArgs e)
        {
            txtBuscar.Clear();
            await CargarVentasAsync();
            txtBuscar.Focus();
        }

        private async void txtBuscar_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                await CargarVentasAsync();
            }
        }

        private static void MostrarError(string mensaje)
        {
            MessageBox.Show(mensaje, "Consulta de ventas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}

using Npgsql;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Datos;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    public partial class FrmConsultaCompras : Form
    {
        public FrmConsultaCompras()
        {
            InitializeComponent();
            colFecha.DefaultCellStyle.Format = "dd/MM/yyyy hh:mm tt";
            colTotal.DefaultCellStyle.Format = "N2";
            Load += FrmConsultaCompras_Load;
        }

        private async void FrmConsultaCompras_Load(object? sender, EventArgs e)
        {
            await CargarComprasAsync();
        }

        private async Task CargarComprasAsync()
        {
            try
            {
                List<Compra> compras = await CompraRepositorio.ListarAsync(
                    texto: txtBuscar.Text.Trim());

                dgvCompras.DataSource = compras.Select(compra => new
                {
                    compra.Fecha,
                    Proveedor = compra.NombreProveedor,
                    compra.Total,
                    Estado = compra.EstadoTexto
                }).ToList();
            }
            catch (NpgsqlException)
            {
                MostrarError("No fue posible consultar las compras en la base de datos.");
            }
            catch (Exception)
            {
                MostrarError("Ocurrió un error al consultar las compras.");
            }
        }

        private async void btnBuscar_Click(object? sender, EventArgs e)
        {
            await CargarComprasAsync();
        }

        private async void btnLimpiar_Click(object? sender, EventArgs e)
        {
            txtBuscar.Clear();
            await CargarComprasAsync();
            txtBuscar.Focus();
        }

        private async void txtBuscar_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                await CargarComprasAsync();
            }
        }

        private static void MostrarError(string mensaje)
        {
            MessageBox.Show(mensaje, "Consulta de compras", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}

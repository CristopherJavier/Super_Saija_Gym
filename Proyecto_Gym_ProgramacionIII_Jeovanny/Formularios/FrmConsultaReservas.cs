using Npgsql;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Datos;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    public partial class FrmConsultaReservas : Form
    {
        public FrmConsultaReservas()
        {
            InitializeComponent();
            colFecha.DefaultCellStyle.Format = "dd/MM/yyyy";
            Load += FrmConsultaReservas_Load;
        }

        private async void FrmConsultaReservas_Load(object? sender, EventArgs e)
        {
            await CargarReservasAsync();
        }

        private async Task CargarReservasAsync()
        {
            try
            {
                List<ReservaClase> reservas = await ReservaClaseRepositorio.ListarAsync(
                    texto: txtBuscar.Text.Trim());

                dgvReservas.DataSource = reservas.Select(reserva => new
                {
                    Cliente = reserva.NombreCliente,
                    Clase = reserva.NombreClase,
                    Fecha = reserva.FechaClase,
                    Horario = reserva.HoraInicio.ToString(@"hh\:mm")
                        + " - "
                        + reserva.HoraFin.ToString(@"hh\:mm"),
                    Estado = reserva.EstadoTexto
                }).ToList();
            }
            catch (NpgsqlException)
            {
                MostrarError("No fue posible consultar las reservas en la base de datos.");
            }
            catch (Exception)
            {
                MostrarError("Ocurrió un error al consultar las reservas.");
            }
        }

        private async void btnBuscar_Click(object? sender, EventArgs e)
        {
            await CargarReservasAsync();
        }

        private async void btnLimpiar_Click(object? sender, EventArgs e)
        {
            txtBuscar.Clear();
            await CargarReservasAsync();
            txtBuscar.Focus();
        }

        private async void txtBuscar_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                await CargarReservasAsync();
            }
        }

        private static void MostrarError(string mensaje)
        {
            MessageBox.Show(mensaje, "Consulta de reservas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}

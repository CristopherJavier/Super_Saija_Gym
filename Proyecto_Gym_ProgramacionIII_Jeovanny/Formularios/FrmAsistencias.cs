using Proyecto_Gym_ProgramacionIII_Jeovanny.Datos;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    public class FrmAsistencias : FrmModuloBase
    {
        private readonly ComboBox cmbClientes;
        private readonly CheckBox chkUsarReserva;
        private readonly ComboBox cmbReservas;
        private readonly Button btnRegistrar;

        public FrmAsistencias()
            : base("Registro de asistencias", 82)
        {
            cmbClientes = CrearCombo();
            chkUsarReserva = new CheckBox
            {
                AutoSize = true,
                Text = "Vincular con reserva de hoy",
                TextAlign = ContentAlignment.MiddleLeft
            };
            cmbReservas = CrearCombo();
            cmbReservas.Enabled = false;
            cmbClientes.Format += cmbClientes_Format;
            cmbClientes.SelectedIndexChanged += cmbClientes_SelectedIndexChanged;
            cmbReservas.Format += cmbReservas_Format;
            chkUsarReserva.CheckedChanged += chkUsarReserva_CheckedChanged;
            AgregarCampo("Cliente", cmbClientes, 245);
            AgregarCampo("Tipo de asistencia", chkUsarReserva, 220);
            AgregarCampo("Reserva", cmbReservas, 250);
            btnRegistrar = AgregarBoton("REGISTRAR", btnRegistrar_Click);
            AgregarBoton("ACTUALIZAR", btnActualizar_Click, false);
            Load += FrmAsistencias_Load;
        }

        private async void FrmAsistencias_Load(object? sender, EventArgs e)
        {
            await CargarDatosAsync();
        }

        private async Task CargarDatosAsync()
        {
            try
            {
                LimpiarMensaje();
                List<Cliente> clientes = (await ClienteRepositorio.ListarAsync()).Where(x => x.Estado).ToList();
                cmbClientes.DataSource = clientes;
                dgvDatos.DataSource = await AsistenciaRepositorio.ListarAsync(DateTime.Today);
                ConfigurarTabla();
                btnRegistrar.Enabled = clientes.Count > 0;
                await CargarReservasAsync();
            }
            catch (Exception ex)
            {
                MostrarExcepcion(ex);
            }
        }

        private async Task CargarReservasAsync()
        {
            if (cmbClientes.SelectedItem is not Cliente cliente)
            {
                cmbReservas.DataSource = null;
                chkUsarReserva.Checked = false;
                chkUsarReserva.Enabled = false;
                return;
            }

            List<ReservaClase> reservas = await ReservaClaseRepositorio.ListarActivasDelClienteAsync(
                cliente.IdCliente,
                DateTime.Today);
            cmbReservas.DataSource = reservas;
            chkUsarReserva.Enabled = reservas.Count > 0;

            if (reservas.Count == 0)
            {
                chkUsarReserva.Checked = false;
            }
        }

        private async void btnRegistrar_Click(object? sender, EventArgs e)
        {
            LimpiarMensaje();

            if (cmbClientes.SelectedItem is not Cliente cliente)
            {
                lblMensaje.Text = "Debe seleccionar un cliente.";
                cmbClientes.Focus();
                return;
            }

            if (chkUsarReserva.Checked && cmbReservas.SelectedItem is not ReservaClase)
            {
                lblMensaje.Text = "Debe seleccionar una reserva.";
                cmbReservas.Focus();
                return;
            }

            ReservaClase? reserva = chkUsarReserva.Checked
                ? cmbReservas.SelectedItem as ReservaClase
                : null;
            btnRegistrar.Enabled = false;

            try
            {
                await AsistenciaRepositorio.RegistrarAsync(new Asistencia
                {
                    IdCliente = cliente.IdCliente,
                    IdReserva = reserva?.IdReserva,
                    IdUsuario = SesionActual.IdUsuario
                });
                MessageBox.Show(
                    "Asistencia registrada correctamente.",
                    "Registro de asistencias",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                await CargarDatosAsync();
            }
            catch (Exception ex)
            {
                MostrarExcepcion(ex);
            }
            finally
            {
                btnRegistrar.Enabled = true;
            }
        }

        private async void btnActualizar_Click(object? sender, EventArgs e)
        {
            await CargarDatosAsync();
        }

        private async void cmbClientes_SelectedIndexChanged(object? sender, EventArgs e)
        {
            try
            {
                await CargarReservasAsync();
            }
            catch (Exception ex)
            {
                MostrarExcepcion(ex);
            }
        }

        private void chkUsarReserva_CheckedChanged(object? sender, EventArgs e)
        {
            cmbReservas.Enabled = chkUsarReserva.Checked && chkUsarReserva.Enabled;
        }

        private void cmbClientes_Format(object? sender, ListControlConvertEventArgs e)
        {
            if (e.ListItem is Cliente cliente)
            {
                e.Value = $"{cliente.Nombre} {cliente.Apellido} - {cliente.Cedula}";
            }
        }

        private void cmbReservas_Format(object? sender, ListControlConvertEventArgs e)
        {
            if (e.ListItem is ReservaClase reserva)
            {
                e.Value = $"{reserva.NombreClase} - {reserva.NombreEntrenador}";
            }
        }

        private void ConfigurarTabla()
        {
            foreach (string nombre in new[]
            {
                nameof(Asistencia.IdCliente),
                nameof(Asistencia.IdUsuario)
            })
            {
                if (dgvDatos.Columns.Contains(nombre))
                {
                    dgvDatos.Columns[nombre]!.Visible = false;
                }
            }

            dgvDatos.Columns[nameof(Asistencia.IdAsistencia)]!.HeaderText = "Número";
            dgvDatos.Columns[nameof(Asistencia.NombreCliente)]!.HeaderText = "Cliente";
            dgvDatos.Columns[nameof(Asistencia.IdReserva)]!.HeaderText = "Reserva";
            dgvDatos.Columns[nameof(Asistencia.NombreClase)]!.HeaderText = "Clase";
            dgvDatos.Columns[nameof(Asistencia.FechaHora)]!.HeaderText = "Fecha y hora";
            dgvDatos.Columns[nameof(Asistencia.NombreUsuario)]!.HeaderText = "Usuario";
            dgvDatos.Columns[nameof(Asistencia.FechaHora)]!.DefaultCellStyle.Format = "dd/MM/yyyy hh:mm tt";
        }
    }
}

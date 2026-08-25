using Proyecto_Gym_ProgramacionIII_Jeovanny.Datos;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    public class FrmReservasClases : FrmModuloBase
    {
        private readonly ComboBox cmbClientes;
        private readonly ComboBox cmbHorarios;
        private readonly DateTimePicker dtpFechaClase;
        private readonly Button btnReservar;

        public FrmReservasClases()
            : base("Reserva de clases", 82)
        {
            cmbClientes = CrearCombo();
            cmbHorarios = CrearCombo();
            dtpFechaClase = CrearFecha();
            dtpFechaClase.MinDate = DateTime.Today;
            cmbClientes.Format += cmbClientes_Format;
            cmbHorarios.Format += cmbHorarios_Format;
            cmbHorarios.SelectedIndexChanged += cmbHorarios_SelectedIndexChanged;
            AgregarCampo("Cliente", cmbClientes, 245);
            AgregarCampo("Horario de clase", cmbHorarios, 300);
            AgregarCampo("Fecha de la clase", dtpFechaClase, 170);
            btnReservar = AgregarBoton("RESERVAR", btnReservar_Click);
            AgregarBoton("ACTUALIZAR", btnActualizar_Click, false);
            Load += FrmReservasClases_Load;
        }

        private async void FrmReservasClases_Load(object? sender, EventArgs e)
        {
            await CargarDatosAsync();
        }

        private async Task CargarDatosAsync()
        {
            try
            {
                LimpiarMensaje();
                List<Cliente> clientes = (await ClienteRepositorio.ListarAsync()).Where(x => x.Estado).ToList();
                List<HorarioClase> horarios = (await HorarioClaseRepositorio.ListarAsync()).Where(x => x.Estado).ToList();
                cmbClientes.DataSource = clientes;
                cmbHorarios.DataSource = horarios;
                dgvDatos.DataSource = await ReservaClaseRepositorio.ListarAsync(DateTime.Today);
                ConfigurarTabla();
                btnReservar.Enabled = clientes.Count > 0 && horarios.Count > 0;
                ActualizarFechaSugerida();
            }
            catch (Exception ex)
            {
                MostrarExcepcion(ex);
            }
        }

        private async void btnReservar_Click(object? sender, EventArgs e)
        {
            LimpiarMensaje();

            if (cmbClientes.SelectedItem is not Cliente cliente)
            {
                lblMensaje.Text = "Debe seleccionar un cliente.";
                cmbClientes.Focus();
                return;
            }

            if (cmbHorarios.SelectedItem is not HorarioClase horario)
            {
                lblMensaje.Text = "Debe seleccionar un horario.";
                cmbHorarios.Focus();
                return;
            }

            btnReservar.Enabled = false;

            try
            {
                int numero = await ReservaClaseRepositorio.GuardarAsync(new ReservaClase
                {
                    IdCliente = cliente.IdCliente,
                    IdHorario = horario.IdHorario,
                    FechaClase = dtpFechaClase.Value.Date,
                    Estado = true
                });
                MessageBox.Show(
                    $"Reserva número {numero} registrada correctamente.",
                    "Reserva de clases",
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
                btnReservar.Enabled = true;
            }
        }

        private async void btnActualizar_Click(object? sender, EventArgs e)
        {
            await CargarDatosAsync();
        }

        private void cmbClientes_Format(object? sender, ListControlConvertEventArgs e)
        {
            if (e.ListItem is Cliente cliente)
            {
                e.Value = $"{cliente.Nombre} {cliente.Apellido} - {cliente.Cedula}";
            }
        }

        private void cmbHorarios_Format(object? sender, ListControlConvertEventArgs e)
        {
            if (e.ListItem is HorarioClase horario)
            {
                e.Value = $"{horario.NombreClase} - {horario.DiaSemana} {DateTime.Today.Add(horario.HoraInicio):hh:mm tt}";
            }
        }

        private void cmbHorarios_SelectedIndexChanged(object? sender, EventArgs e)
        {
            ActualizarFechaSugerida();
        }

        private void ActualizarFechaSugerida()
        {
            if (cmbHorarios.SelectedItem is not HorarioClase horario)
            {
                lblResumen.Text = string.Empty;
                return;
            }

            DateTime fecha = DateTime.Today;

            while (ObtenerDiaSemana(fecha.DayOfWeek) != horario.DiaSemana)
            {
                fecha = fecha.AddDays(1);
            }

            dtpFechaClase.Value = fecha;
            lblResumen.Text = $"Entrenador: {horario.NombreEntrenador}   Horario: {horario.HoraInicio.ToString(@"hh\:mm")} - {horario.HoraFin.ToString(@"hh\:mm")}";
        }

        private void ConfigurarTabla()
        {
            foreach (string nombre in new[]
            {
                nameof(ReservaClase.IdCliente),
                nameof(ReservaClase.IdHorario),
                nameof(ReservaClase.Estado)
            })
            {
                if (dgvDatos.Columns.Contains(nombre))
                {
                    dgvDatos.Columns[nombre]!.Visible = false;
                }
            }

            dgvDatos.Columns[nameof(ReservaClase.IdReserva)]!.HeaderText = "Número";
            dgvDatos.Columns[nameof(ReservaClase.NombreCliente)]!.HeaderText = "Cliente";
            dgvDatos.Columns[nameof(ReservaClase.NombreClase)]!.HeaderText = "Clase";
            dgvDatos.Columns[nameof(ReservaClase.NombreEntrenador)]!.HeaderText = "Entrenador";
            dgvDatos.Columns[nameof(ReservaClase.FechaClase)]!.HeaderText = "Fecha de clase";
            dgvDatos.Columns[nameof(ReservaClase.FechaReserva)]!.HeaderText = "Fecha de reserva";
            dgvDatos.Columns[nameof(ReservaClase.EstadoTexto)]!.HeaderText = "Estado";
            ConfigurarColumnasFecha(dgvDatos, nameof(ReservaClase.FechaClase));
            dgvDatos.Columns[nameof(ReservaClase.FechaReserva)]!.DefaultCellStyle.Format = "dd/MM/yyyy hh:mm tt";
        }

        private static string ObtenerDiaSemana(DayOfWeek dia)
        {
            return dia switch
            {
                DayOfWeek.Monday => "LUNES",
                DayOfWeek.Tuesday => "MARTES",
                DayOfWeek.Wednesday => "MIERCOLES",
                DayOfWeek.Thursday => "JUEVES",
                DayOfWeek.Friday => "VIERNES",
                DayOfWeek.Saturday => "SABADO",
                _ => "DOMINGO"
            };
        }
    }
}

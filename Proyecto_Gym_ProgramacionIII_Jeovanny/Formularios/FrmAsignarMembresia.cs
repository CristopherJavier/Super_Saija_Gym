using Proyecto_Gym_ProgramacionIII_Jeovanny.Datos;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    public class FrmAsignarMembresia : FrmModuloBase
    {
        private readonly ComboBox cmbClientes;
        private readonly ComboBox cmbTiposMembresias;
        private readonly DateTimePicker dtpFechaInicio;
        private readonly Button btnGuardar;

        public FrmAsignarMembresia()
            : base("Asignación de membresía", 82)
        {
            cmbClientes = CrearCombo();
            cmbTiposMembresias = CrearCombo();
            dtpFechaInicio = CrearFecha();
            dtpFechaInicio.MinDate = DateTime.Today;

            cmbClientes.Format += cmbClientes_Format;
            cmbTiposMembresias.DisplayMember = nameof(TipoMembresia.Nombre);
            cmbTiposMembresias.SelectedIndexChanged += cmbTiposMembresias_SelectedIndexChanged;

            AgregarCampo("Cliente", cmbClientes, 245);
            AgregarCampo("Tipo de membresía", cmbTiposMembresias, 230);
            AgregarCampo("Fecha de inicio", dtpFechaInicio, 170);
            btnGuardar = AgregarBoton("ASIGNAR", btnGuardar_Click);
            AgregarBoton("ACTUALIZAR", btnActualizar_Click, false);
            Load += FrmAsignarMembresia_Load;
        }

        private async void FrmAsignarMembresia_Load(object? sender, EventArgs e)
        {
            await CargarDatosAsync();
        }

        private async Task CargarDatosAsync()
        {
            try
            {
                LimpiarMensaje();
                List<Cliente> clientes = (await ClienteRepositorio.ListarAsync())
                    .Where(x => x.Estado)
                    .ToList();
                List<TipoMembresia> tipos = (await TipoMembresiaRepositorio.ListarAsync())
                    .Where(x => x.Estado)
                    .ToList();
                cmbClientes.DataSource = clientes;
                cmbTiposMembresias.DataSource = tipos;
                dgvDatos.DataSource = await MembresiaClienteRepositorio.ListarAsync();
                ConfigurarTabla();
                btnGuardar.Enabled = clientes.Count > 0 && tipos.Count > 0;
                ActualizarResumen();
            }
            catch (Exception ex)
            {
                MostrarExcepcion(ex);
            }
        }

        private async void btnGuardar_Click(object? sender, EventArgs e)
        {
            LimpiarMensaje();

            if (cmbClientes.SelectedItem is not Cliente cliente)
            {
                lblMensaje.Text = "Debe seleccionar un cliente.";
                cmbClientes.Focus();
                return;
            }

            if (cmbTiposMembresias.SelectedItem is not TipoMembresia tipo)
            {
                lblMensaje.Text = "Debe seleccionar un tipo de membresía.";
                cmbTiposMembresias.Focus();
                return;
            }

            btnGuardar.Enabled = false;

            try
            {
                await MembresiaClienteRepositorio.AsignarAsync(
                    cliente.IdCliente,
                    tipo.IdTipoMembresia,
                    dtpFechaInicio.Value.Date);
                MessageBox.Show(
                    "Membresía asignada correctamente.",
                    "Asignación de membresía",
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
                btnGuardar.Enabled = true;
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

        private void cmbTiposMembresias_SelectedIndexChanged(object? sender, EventArgs e)
        {
            ActualizarResumen();
        }

        private void ActualizarResumen()
        {
            if (cmbTiposMembresias.SelectedItem is TipoMembresia tipo)
            {
                DateTime vencimiento = dtpFechaInicio.Value.Date.AddDays(tipo.DuracionDias - 1);
                lblResumen.Text = $"Duración: {tipo.DuracionDias} días   Precio: {tipo.Precio:N2}   Vence: {vencimiento:dd/MM/yyyy}";
            }
            else
            {
                lblResumen.Text = string.Empty;
            }
        }

        private void ConfigurarTabla()
        {
            Ocultar(nameof(MembresiaCliente.IdMembresiaCliente));
            Ocultar(nameof(MembresiaCliente.IdCliente));
            Ocultar(nameof(MembresiaCliente.IdTipoMembresia));
            Ocultar(nameof(MembresiaCliente.IdMembresiaAnterior));
            Ocultar(nameof(MembresiaCliente.Estado));
            Ocultar(nameof(MembresiaCliente.EstaVencida));
            dgvDatos.Columns[nameof(MembresiaCliente.NombreCliente)]!.HeaderText = "Cliente";
            dgvDatos.Columns[nameof(MembresiaCliente.NombreTipoMembresia)]!.HeaderText = "Membresía";
            dgvDatos.Columns[nameof(MembresiaCliente.FechaInicio)]!.HeaderText = "Inicio";
            dgvDatos.Columns[nameof(MembresiaCliente.FechaVencimiento)]!.HeaderText = "Vencimiento";
            dgvDatos.Columns[nameof(MembresiaCliente.PrecioAplicado)]!.HeaderText = "Precio";
            dgvDatos.Columns[nameof(MembresiaCliente.DiasRestantes)]!.HeaderText = "Días restantes";
            dgvDatos.Columns[nameof(MembresiaCliente.EstadoTexto)]!.HeaderText = "Estado";
            ConfigurarColumnasFecha(
                dgvDatos,
                nameof(MembresiaCliente.FechaInicio),
                nameof(MembresiaCliente.FechaVencimiento));
            ConfigurarColumnasMoneda(dgvDatos, nameof(MembresiaCliente.PrecioAplicado));
        }

        private void Ocultar(string nombre)
        {
            if (dgvDatos.Columns.Contains(nombre))
            {
                dgvDatos.Columns[nombre]!.Visible = false;
            }
        }
    }
}

using Proyecto_Gym_ProgramacionIII_Jeovanny.Datos;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    public class FrmRenovarMembresia : FrmModuloBase
    {
        private readonly ComboBox cmbMembresias;
        private readonly Button btnRenovar;

        public FrmRenovarMembresia()
            : base("Renovación de membresía", 82)
        {
            cmbMembresias = CrearCombo();
            cmbMembresias.Format += cmbMembresias_Format;
            cmbMembresias.SelectedIndexChanged += cmbMembresias_SelectedIndexChanged;
            AgregarCampo("Membresía del cliente", cmbMembresias, 430);
            btnRenovar = AgregarBoton("RENOVAR", btnRenovar_Click);
            AgregarBoton("ACTUALIZAR", btnActualizar_Click, false);
            Load += FrmRenovarMembresia_Load;
        }

        private async void FrmRenovarMembresia_Load(object? sender, EventArgs e)
        {
            await CargarDatosAsync();
        }

        private async Task CargarDatosAsync()
        {
            try
            {
                LimpiarMensaje();
                List<MembresiaCliente> renovables = await MembresiaClienteRepositorio.ListarRenovablesAsync();
                cmbMembresias.DataSource = renovables;
                dgvDatos.DataSource = await MembresiaClienteRepositorio.ListarAsync();
                ConfigurarTabla();
                btnRenovar.Enabled = renovables.Count > 0;
                ActualizarResumen();
            }
            catch (Exception ex)
            {
                MostrarExcepcion(ex);
            }
        }

        private async void btnRenovar_Click(object? sender, EventArgs e)
        {
            LimpiarMensaje();

            if (cmbMembresias.SelectedItem is not MembresiaCliente membresia)
            {
                lblMensaje.Text = "Debe seleccionar una membresía para renovar.";
                cmbMembresias.Focus();
                return;
            }

            btnRenovar.Enabled = false;

            try
            {
                MembresiaCliente renovada = await MembresiaClienteRepositorio.RenovarAsync(
                    membresia.IdMembresiaCliente);
                MessageBox.Show(
                    $"Membresía renovada hasta el {renovada.FechaVencimiento:dd/MM/yyyy}.",
                    "Renovación de membresía",
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
                btnRenovar.Enabled = true;
            }
        }

        private async void btnActualizar_Click(object? sender, EventArgs e)
        {
            await CargarDatosAsync();
        }

        private void cmbMembresias_Format(object? sender, ListControlConvertEventArgs e)
        {
            if (e.ListItem is MembresiaCliente membresia)
            {
                e.Value = $"{membresia.NombreCliente} - {membresia.NombreTipoMembresia} - vence {membresia.FechaVencimiento:dd/MM/yyyy}";
            }
        }

        private void cmbMembresias_SelectedIndexChanged(object? sender, EventArgs e)
        {
            ActualizarResumen();
        }

        private void ActualizarResumen()
        {
            if (cmbMembresias.SelectedItem is MembresiaCliente membresia)
            {
                DateTime nuevoInicio = membresia.FechaVencimiento.Date >= DateTime.Today
                    ? membresia.FechaVencimiento.Date.AddDays(1)
                    : DateTime.Today;
                lblResumen.Text = $"La renovación iniciará el {nuevoInicio:dd/MM/yyyy}.";
            }
            else
            {
                lblResumen.Text = string.Empty;
            }
        }

        private void ConfigurarTabla()
        {
            foreach (string nombre in new[]
            {
                nameof(MembresiaCliente.IdMembresiaCliente),
                nameof(MembresiaCliente.IdCliente),
                nameof(MembresiaCliente.IdTipoMembresia),
                nameof(MembresiaCliente.IdMembresiaAnterior),
                nameof(MembresiaCliente.Estado),
                nameof(MembresiaCliente.EstaVencida)
            })
            {
                if (dgvDatos.Columns.Contains(nombre))
                {
                    dgvDatos.Columns[nombre]!.Visible = false;
                }
            }

            dgvDatos.Columns[nameof(MembresiaCliente.NombreCliente)]!.HeaderText = "Cliente";
            dgvDatos.Columns[nameof(MembresiaCliente.NombreTipoMembresia)]!.HeaderText = "Membresía";
            dgvDatos.Columns[nameof(MembresiaCliente.FechaInicio)]!.HeaderText = "Inicio";
            dgvDatos.Columns[nameof(MembresiaCliente.FechaVencimiento)]!.HeaderText = "Vencimiento";
            dgvDatos.Columns[nameof(MembresiaCliente.PrecioAplicado)]!.HeaderText = "Precio";
            dgvDatos.Columns[nameof(MembresiaCliente.DiasRestantes)]!.HeaderText = "Días restantes";
            dgvDatos.Columns[nameof(MembresiaCliente.EstadoTexto)]!.HeaderText = "Estado";
            ConfigurarColumnasFecha(dgvDatos, nameof(MembresiaCliente.FechaInicio), nameof(MembresiaCliente.FechaVencimiento));
            ConfigurarColumnasMoneda(dgvDatos, nameof(MembresiaCliente.PrecioAplicado));
        }
    }
}

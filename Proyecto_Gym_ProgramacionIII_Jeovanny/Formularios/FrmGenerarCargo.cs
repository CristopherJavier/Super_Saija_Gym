using Proyecto_Gym_ProgramacionIII_Jeovanny.Datos;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    public class FrmGenerarCargo : FrmModuloBase
    {
        private readonly ComboBox cmbMembresias;
        private readonly TextBox txtConcepto;
        private readonly DateTimePicker dtpFechaVencimiento;
        private readonly Button btnGenerar;

        public FrmGenerarCargo()
            : base("Generación de cargos", 82)
        {
            cmbMembresias = CrearCombo();
            txtConcepto = CrearTexto(150);
            txtConcepto.ReadOnly = true;
            dtpFechaVencimiento = CrearFecha();
            cmbMembresias.Format += cmbMembresias_Format;
            cmbMembresias.SelectedIndexChanged += cmbMembresias_SelectedIndexChanged;
            AgregarCampo("Cliente y membresía", cmbMembresias, 300);
            AgregarCampo("Concepto", txtConcepto, 230);
            AgregarCampo("Fecha de vencimiento", dtpFechaVencimiento, 180);
            btnGenerar = AgregarBoton("GENERAR", btnGenerar_Click);
            AgregarBoton("ACTUALIZAR", btnActualizar_Click, false);
            Load += FrmGenerarCargo_Load;
        }

        private async void FrmGenerarCargo_Load(object? sender, EventArgs e)
        {
            await CargarDatosAsync();
        }

        private async Task CargarDatosAsync()
        {
            try
            {
                LimpiarMensaje();
                List<MembresiaCliente> membresias = await CargoRepositorio.ListarMembresiasSinCargoAsync();
                cmbMembresias.DataSource = membresias;
                dgvDatos.DataSource = await CargoRepositorio.ListarAsync();
                ConfigurarTabla();
                btnGenerar.Enabled = membresias.Count > 0;
                ActualizarMembresia();
            }
            catch (Exception ex)
            {
                MostrarExcepcion(ex);
            }
        }

        private async void btnGenerar_Click(object? sender, EventArgs e)
        {
            LimpiarMensaje();

            if (cmbMembresias.SelectedItem is not MembresiaCliente membresia)
            {
                lblMensaje.Text = "Debe seleccionar una membresía.";
                cmbMembresias.Focus();
                return;
            }

            btnGenerar.Enabled = false;

            try
            {
                int numero = await CargoRepositorio.GenerarAsync(
                    membresia.IdMembresiaCliente,
                    txtConcepto.Text,
                    dtpFechaVencimiento.Value.Date);
                MessageBox.Show(
                    $"Cargo número {numero} generado correctamente.",
                    "Generación de cargos",
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
                btnGenerar.Enabled = true;
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
                e.Value = $"{membresia.NombreCliente} - {membresia.NombreTipoMembresia}";
            }
        }

        private void cmbMembresias_SelectedIndexChanged(object? sender, EventArgs e)
        {
            ActualizarMembresia();
        }

        private void ActualizarMembresia()
        {
            if (cmbMembresias.SelectedItem is MembresiaCliente membresia)
            {
                txtConcepto.Text = $"Membresía {membresia.NombreTipoMembresia}";
                dtpFechaVencimiento.Value = membresia.FechaInicio.Date < dtpFechaVencimiento.MinDate
                    ? dtpFechaVencimiento.MinDate
                    : membresia.FechaInicio.Date;
                lblResumen.Text = $"Cliente: {membresia.NombreCliente}   Monto: {membresia.PrecioAplicado:N2}";
            }
            else
            {
                txtConcepto.Clear();
                lblResumen.Text = string.Empty;
            }
        }

        private void ConfigurarTabla()
        {
            foreach (string nombre in new[]
            {
                nameof(Cargo.IdCliente),
                nameof(Cargo.IdMembresiaCliente),
                nameof(Cargo.Estado),
                nameof(Cargo.EstaVencido)
            })
            {
                if (dgvDatos.Columns.Contains(nombre))
                {
                    dgvDatos.Columns[nombre]!.Visible = false;
                }
            }

            dgvDatos.Columns[nameof(Cargo.IdCargo)]!.HeaderText = "Número";
            dgvDatos.Columns[nameof(Cargo.NombreCliente)]!.HeaderText = "Cliente";
            dgvDatos.Columns[nameof(Cargo.Concepto)]!.HeaderText = "Concepto";
            dgvDatos.Columns[nameof(Cargo.FechaCargo)]!.HeaderText = "Fecha";
            dgvDatos.Columns[nameof(Cargo.FechaVencimiento)]!.HeaderText = "Vencimiento";
            dgvDatos.Columns[nameof(Cargo.Monto)]!.HeaderText = "Monto";
            dgvDatos.Columns[nameof(Cargo.Saldo)]!.HeaderText = "Saldo";
            dgvDatos.Columns[nameof(Cargo.EstadoTexto)]!.HeaderText = "Estado";
            ConfigurarColumnasFecha(dgvDatos, nameof(Cargo.FechaCargo), nameof(Cargo.FechaVencimiento));
            ConfigurarColumnasMoneda(dgvDatos, nameof(Cargo.Monto), nameof(Cargo.Saldo));
        }
    }
}

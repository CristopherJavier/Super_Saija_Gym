using Proyecto_Gym_ProgramacionIII_Jeovanny.Datos;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    public class FrmAbonos : FrmModuloBase
    {
        private readonly ComboBox cmbCuentas;
        private readonly ComboBox cmbMetodosPago;
        private readonly NumericUpDown nudMonto;
        private readonly Button btnRegistrar;

        public FrmAbonos()
            : base("Registro de abonos", 82)
        {
            cmbCuentas = CrearCombo();
            cmbMetodosPago = CrearCombo();
            nudMonto = CrearNumero(9999999999.99m, 2, 0.01m);
            cmbCuentas.Format += cmbCuentas_Format;
            cmbCuentas.SelectedIndexChanged += cmbCuentas_SelectedIndexChanged;
            cmbMetodosPago.DisplayMember = nameof(MetodoPago.Nombre);
            AgregarCampo("Cuenta por cobrar", cmbCuentas, 330);
            AgregarCampo("Método de pago", cmbMetodosPago, 200);
            AgregarCampo("Monto del abono", nudMonto, 170);
            btnRegistrar = AgregarBoton("REGISTRAR", btnRegistrar_Click);
            AgregarBoton("ACTUALIZAR", btnActualizar_Click, false);
            Load += FrmAbonos_Load;
        }

        private async void FrmAbonos_Load(object? sender, EventArgs e)
        {
            await CargarDatosAsync();
        }

        private async Task CargarDatosAsync()
        {
            try
            {
                LimpiarMensaje();
                List<CuentaCobrar> cuentas = await CuentaCobrarRepositorio.ListarAsync(true);
                List<MetodoPago> metodos = await MetodoPagoRepositorio.ListarAsync(soloActivos: true);
                cmbCuentas.DataSource = cuentas;
                cmbMetodosPago.DataSource = metodos;
                dgvDatos.DataSource = await CuentaCobrarRepositorio.ListarAbonosAsync();
                ConfigurarTabla();
                btnRegistrar.Enabled = cuentas.Count > 0 && metodos.Count > 0;
                ActualizarCuenta();
            }
            catch (Exception ex)
            {
                MostrarExcepcion(ex);
            }
        }

        private async void btnRegistrar_Click(object? sender, EventArgs e)
        {
            LimpiarMensaje();

            if (cmbCuentas.SelectedItem is not CuentaCobrar cuenta)
            {
                lblMensaje.Text = "Debe seleccionar una cuenta por cobrar.";
                cmbCuentas.Focus();
                return;
            }

            if (cmbMetodosPago.SelectedItem is not MetodoPago metodo)
            {
                lblMensaje.Text = "Debe seleccionar un método de pago.";
                cmbMetodosPago.Focus();
                return;
            }

            btnRegistrar.Enabled = false;

            try
            {
                await CuentaCobrarRepositorio.RegistrarAbonoAsync(new Abono
                {
                    IdCuenta = cuenta.IdCuenta,
                    Monto = nudMonto.Value,
                    IdMetodoPago = metodo.IdMetodoPago,
                    IdUsuario = SesionActual.IdUsuario
                });
                MessageBox.Show(
                    "Abono registrado correctamente.",
                    "Registro de abonos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                await CargarDatosAsync();
            }
            catch (Exception ex)
            {
                MostrarExcepcion(ex);
            }
        }

        private async void btnActualizar_Click(object? sender, EventArgs e)
        {
            await CargarDatosAsync();
        }

        private void cmbCuentas_Format(object? sender, ListControlConvertEventArgs e)
        {
            if (e.ListItem is CuentaCobrar cuenta)
            {
                e.Value = $"Venta {cuenta.IdVenta} - {cuenta.NombreCliente} - saldo {cuenta.Saldo:N2}";
            }
        }

        private void cmbCuentas_SelectedIndexChanged(object? sender, EventArgs e)
        {
            ActualizarCuenta();
        }

        private void ActualizarCuenta()
        {
            if (cmbCuentas.SelectedItem is CuentaCobrar cuenta)
            {
                nudMonto.Maximum = Math.Max(0.01m, cuenta.Saldo);
                nudMonto.Value = cuenta.Saldo;
                lblResumen.Text = $"Saldo actual: {cuenta.Saldo:N2}   Vencimiento: {cuenta.FechaVencimiento:dd/MM/yyyy}";
            }
            else
            {
                nudMonto.Value = nudMonto.Minimum;
                lblResumen.Text = string.Empty;
            }
        }

        private void ConfigurarTabla()
        {
            foreach (string nombre in new[]
            {
                nameof(Abono.IdMetodoPago),
                nameof(Abono.IdUsuario)
            })
            {
                if (dgvDatos.Columns.Contains(nombre))
                {
                    dgvDatos.Columns[nombre]!.Visible = false;
                }
            }

            dgvDatos.Columns[nameof(Abono.IdAbono)]!.HeaderText = "Número";
            dgvDatos.Columns[nameof(Abono.IdCuenta)]!.HeaderText = "Cuenta";
            dgvDatos.Columns[nameof(Abono.Fecha)]!.HeaderText = "Fecha";
            dgvDatos.Columns[nameof(Abono.Monto)]!.HeaderText = "Monto";
            dgvDatos.Columns[nameof(Abono.NombreMetodoPago)]!.HeaderText = "Método de pago";
            dgvDatos.Columns[nameof(Abono.NombreUsuario)]!.HeaderText = "Usuario";
            dgvDatos.Columns[nameof(Abono.Fecha)]!.DefaultCellStyle.Format = "dd/MM/yyyy hh:mm tt";
            ConfigurarColumnasMoneda(dgvDatos, nameof(Abono.Monto));
        }
    }
}

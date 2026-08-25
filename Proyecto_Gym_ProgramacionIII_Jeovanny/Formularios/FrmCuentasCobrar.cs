using Proyecto_Gym_ProgramacionIII_Jeovanny.Datos;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    public class FrmCuentasCobrar : FrmModuloBase
    {
        private readonly ComboBox cmbEstado;

        public FrmCuentasCobrar()
            : base("Cuentas por cobrar", 82)
        {
            cmbEstado = CrearCombo();
            cmbEstado.Items.AddRange(new object[] { "TODAS", "PENDIENTES" });
            cmbEstado.SelectedIndex = 1;
            AgregarCampo("Mostrar", cmbEstado, 190);
            AgregarBoton("CONSULTAR", btnConsultar_Click);
            AgregarBoton("ACTUALIZAR", btnActualizar_Click, false);
            Load += FrmCuentasCobrar_Load;
        }

        private async void FrmCuentasCobrar_Load(object? sender, EventArgs e)
        {
            await CargarCuentasAsync();
        }

        private async Task CargarCuentasAsync()
        {
            try
            {
                LimpiarMensaje();
                bool soloPendientes = cmbEstado.SelectedItem?.ToString() == "PENDIENTES";
                List<CuentaCobrar> cuentas = await CuentaCobrarRepositorio.ListarAsync(soloPendientes);
                dgvDatos.DataSource = cuentas;
                ConfigurarTabla();
                lblResumen.Text = $"Saldo pendiente total: {cuentas.Where(x => x.Estado).Sum(x => x.Saldo):N2}";
            }
            catch (Exception ex)
            {
                MostrarExcepcion(ex);
            }
        }

        private async void btnConsultar_Click(object? sender, EventArgs e)
        {
            await CargarCuentasAsync();
        }

        private async void btnActualizar_Click(object? sender, EventArgs e)
        {
            await CargarCuentasAsync();
        }

        private void ConfigurarTabla()
        {
            foreach (string nombre in new[]
            {
                nameof(CuentaCobrar.IdCliente),
                nameof(CuentaCobrar.Estado),
                nameof(CuentaCobrar.EstaVencida)
            })
            {
                if (dgvDatos.Columns.Contains(nombre))
                {
                    dgvDatos.Columns[nombre]!.Visible = false;
                }
            }

            dgvDatos.Columns[nameof(CuentaCobrar.IdCuenta)]!.HeaderText = "Cuenta";
            dgvDatos.Columns[nameof(CuentaCobrar.IdVenta)]!.HeaderText = "Venta";
            dgvDatos.Columns[nameof(CuentaCobrar.NombreCliente)]!.HeaderText = "Cliente";
            dgvDatos.Columns[nameof(CuentaCobrar.Saldo)]!.HeaderText = "Saldo";
            dgvDatos.Columns[nameof(CuentaCobrar.FechaVencimiento)]!.HeaderText = "Vencimiento";
            dgvDatos.Columns[nameof(CuentaCobrar.EstadoTexto)]!.HeaderText = "Estado";
            ConfigurarColumnasMoneda(dgvDatos, nameof(CuentaCobrar.Saldo));
            ConfigurarColumnasFecha(dgvDatos, nameof(CuentaCobrar.FechaVencimiento));
        }
    }
}

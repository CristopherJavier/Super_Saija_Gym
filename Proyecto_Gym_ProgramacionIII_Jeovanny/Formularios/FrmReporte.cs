using System.Data;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Datos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    public enum TipoReporte
    {
        BalanceClientes,
        Clientes,
        Membresias,
        Cobros,
        Ventas,
        Compras,
        Cargos
    }

    public class FrmReporte : FrmModuloBase
    {
        private readonly TipoReporte tipoReporte;
        private readonly MaskedTextBox? txtCedula;
        private readonly ComboBox? cmbEstado;
        private readonly DateTimePicker? dtpDesde;
        private readonly DateTimePicker? dtpHasta;

        public FrmReporte()
            : this(TipoReporte.BalanceClientes)
        {
        }

        public FrmReporte(TipoReporte tipo)
            : base(ObtenerTitulo(tipo), 82)
        {
            tipoReporte = tipo;

            if (tipo == TipoReporte.BalanceClientes)
            {
                txtCedula = new MaskedTextBox
                {
                    HidePromptOnLeave = true,
                    Mask = "000-0000000-0",
                    PromptChar = ' ',
                    ResetOnSpace = false,
                    TextMaskFormat = MaskFormat.ExcludePromptAndLiterals
                };
                AgregarCampo("Cédula", txtCedula, 210);
            }

            if (tipo == TipoReporte.Clientes
                || tipo == TipoReporte.Membresias
                || tipo == TipoReporte.Cargos)
            {
                cmbEstado = CrearCombo();
                CargarEstados();
                AgregarCampo("Estado", cmbEstado, 210);
            }

            if (tipo == TipoReporte.Cobros
                || tipo == TipoReporte.Ventas
                || tipo == TipoReporte.Compras)
            {
                dtpDesde = CrearFecha();
                dtpHasta = CrearFecha();
                dtpDesde.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                dtpHasta.Value = DateTime.Today;
                AgregarCampo("Desde", dtpDesde, 180);
                AgregarCampo("Hasta", dtpHasta, 180);
            }

            AgregarBoton("BUSCAR", btnBuscar_Click);
            AgregarBoton("LIMPIAR", btnLimpiar_Click, false);
            Load += FrmReporte_Load;
        }

        private async void FrmReporte_Load(object? sender, EventArgs e)
        {
            await BuscarAsync();
        }

        private async Task BuscarAsync()
        {
            try
            {
                LimpiarMensaje();

                if (txtCedula is not null
                    && txtCedula.Text.Length > 0
                    && !txtCedula.MaskCompleted)
                {
                    lblMensaje.Text = "La cédula debe contener exactamente 11 dígitos.";
                    txtCedula.Focus();
                    return;
                }

                DataTable datos = tipoReporte switch
                {
                    TipoReporte.BalanceClientes => await ReporteRepositorio.ObtenerBalanceClientesAsync(
                        txtCedula?.Text ?? string.Empty),
                    TipoReporte.Clientes => await ReporteRepositorio.ObtenerClientesAsync(
                        cmbEstado?.SelectedItem?.ToString() ?? "TODOS"),
                    TipoReporte.Membresias => await ReporteRepositorio.ObtenerMembresiasAsync(
                        cmbEstado?.SelectedItem?.ToString() ?? "TODAS"),
                    TipoReporte.Cobros => await ReporteRepositorio.ObtenerCobrosAsync(
                        dtpDesde!.Value.Date,
                        dtpHasta!.Value.Date),
                    TipoReporte.Ventas => await ReporteRepositorio.ObtenerVentasAsync(
                        dtpDesde!.Value.Date,
                        dtpHasta!.Value.Date),
                    TipoReporte.Compras => await ReporteRepositorio.ObtenerComprasAsync(
                        dtpDesde!.Value.Date,
                        dtpHasta!.Value.Date),
                    _ => await ReporteRepositorio.ObtenerCargosAsync(
                        cmbEstado?.SelectedItem?.ToString() ?? "TODOS")
                };

                dgvDatos.DataSource = datos;
                ConfigurarTabla(datos);
                ActualizarResumen(datos);
            }
            catch (Exception ex)
            {
                MostrarExcepcion(ex);
            }
        }

        private async void btnBuscar_Click(object? sender, EventArgs e)
        {
            await BuscarAsync();
        }

        private async void btnLimpiar_Click(object? sender, EventArgs e)
        {
            txtCedula?.Clear();

            if (cmbEstado is not null && cmbEstado.Items.Count > 0)
            {
                cmbEstado.SelectedIndex = 0;
            }

            if (dtpDesde is not null && dtpHasta is not null)
            {
                dtpDesde.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                dtpHasta.Value = DateTime.Today;
            }

            await BuscarAsync();
        }

        private void CargarEstados()
        {
            if (cmbEstado is null)
            {
                return;
            }

            if (tipoReporte == TipoReporte.Clientes)
            {
                cmbEstado.Items.AddRange(new object[] { "TODOS", "ACTIVOS", "INACTIVOS" });
            }
            else if (tipoReporte == TipoReporte.Membresias)
            {
                cmbEstado.Items.AddRange(new object[] { "TODAS", "ACTIVAS", "VENCIDAS" });
            }
            else
            {
                cmbEstado.Items.AddRange(new object[] { "TODOS", "PENDIENTES", "VENCIDOS" });
            }

            cmbEstado.SelectedIndex = 0;
        }

        private void ConfigurarTabla(DataTable datos)
        {
            foreach (DataColumn columnaDatos in datos.Columns)
            {
                if (!dgvDatos.Columns.Contains(columnaDatos.ColumnName))
                {
                    continue;
                }

                DataGridViewColumn columna = dgvDatos.Columns[columnaDatos.ColumnName]!;

                if (columnaDatos.DataType == typeof(decimal)
                    || columnaDatos.DataType == typeof(double)
                    || columnaDatos.DataType == typeof(float))
                {
                    columna.DefaultCellStyle.Format = "N2";
                    columna.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }

                if (columnaDatos.DataType == typeof(DateTime))
                {
                    columna.DefaultCellStyle.Format = columnaDatos.ColumnName == "Fecha"
                        ? "dd/MM/yyyy hh:mm tt"
                        : "dd/MM/yyyy";
                }
            }
        }

        private void ActualizarResumen(DataTable datos)
        {
            string? columnaTotal = tipoReporte switch
            {
                TipoReporte.BalanceClientes => "Balance pendiente",
                TipoReporte.Cobros => "Total",
                TipoReporte.Ventas => "Total",
                TipoReporte.Compras => "Total",
                TipoReporte.Cargos => "Saldo",
                _ => null
            };

            if (columnaTotal is null)
            {
                lblResumen.Text = $"Registros: {datos.Rows.Count}";
                return;
            }

            decimal total = 0;

            foreach (DataRow fila in datos.Rows)
            {
                if (fila[columnaTotal] is not DBNull)
                {
                    total += Convert.ToDecimal(fila[columnaTotal]);
                }
            }

            lblResumen.Text = $"Registros: {datos.Rows.Count}   Total: {total:N2}";
        }

        private static string ObtenerTitulo(TipoReporte tipo)
        {
            return tipo switch
            {
                TipoReporte.BalanceClientes => "Balance pendiente por cliente",
                TipoReporte.Clientes => "Reporte de clientes",
                TipoReporte.Membresias => "Reporte de membresías",
                TipoReporte.Cobros => "Cobros por fecha",
                TipoReporte.Ventas => "Ventas por fecha",
                TipoReporte.Compras => "Compras por fecha",
                _ => "Cargos pendientes y vencidos"
            };
        }
    }
}

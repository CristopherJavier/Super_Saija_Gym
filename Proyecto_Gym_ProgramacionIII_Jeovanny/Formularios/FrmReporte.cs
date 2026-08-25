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
        private readonly ComboBox? cmbEstado;
        private readonly DateTimePicker? dtpDesde;
        private readonly DateTimePicker? dtpHasta;

        public FrmReporte(TipoReporte tipo)
            : base(ObtenerTitulo(tipo), RequiereFiltros(tipo) ? 82 : 0)
        {
            tipoReporte = tipo;

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

            AgregarBoton("GENERAR", btnGenerar_Click);
            AgregarBoton("LIMPIAR", btnLimpiar_Click, false);
            Load += FrmReporte_Load;
        }

        private async void FrmReporte_Load(object? sender, EventArgs e)
        {
            await GenerarAsync();
        }

        private async Task GenerarAsync()
        {
            try
            {
                LimpiarMensaje();
                DataTable datos = tipoReporte switch
                {
                    TipoReporte.BalanceClientes => await ReporteRepositorio.ObtenerBalanceClientesAsync(),
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

        private async void btnGenerar_Click(object? sender, EventArgs e)
        {
            await GenerarAsync();
        }

        private async void btnLimpiar_Click(object? sender, EventArgs e)
        {
            if (cmbEstado is not null && cmbEstado.Items.Count > 0)
            {
                cmbEstado.SelectedIndex = 0;
            }

            if (dtpDesde is not null && dtpHasta is not null)
            {
                dtpDesde.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                dtpHasta.Value = DateTime.Today;
            }

            await GenerarAsync();
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

        private static bool RequiereFiltros(TipoReporte tipo)
        {
            return tipo != TipoReporte.BalanceClientes;
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

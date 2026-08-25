namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    partial class FrmPrincipal
    {
        private Button btnMovimientos = new Button();
        private FlowLayoutPanel pnlSubmenuMovimientos = new FlowLayoutPanel();
        private Button btnAsignarMembresia = new Button();
        private Button btnRenovarMembresia = new Button();
        private Button btnMovimientoCobros = new Button();
        private Button btnGenerarCargos = new Button();
        private Button btnMovimientoVentas = new Button();
        private Button btnMovimientoCompras = new Button();
        private Button btnReservarClases = new Button();
        private Button btnCuentasCobrar = new Button();
        private Button btnAbonos = new Button();
        private Button btnInventario = new Button();
        private Button btnAsistencias = new Button();
        private Button btnReportes = new Button();
        private FlowLayoutPanel pnlSubmenuReportes = new FlowLayoutPanel();
        private Button btnReporteBalanceClientes = new Button();
        private Button btnReporteClientes = new Button();
        private Button btnReporteMembresias = new Button();
        private Button btnReporteCobros = new Button();
        private Button btnReporteVentas = new Button();
        private Button btnReporteCompras = new Button();
        private Button btnReporteCargos = new Button();

        private void InicializarMenusMovimientosReportes()
        {
            ConfigurarBotonModulo(btnMovimientos, "btnMovimientos", ">  MOVIMIENTOS", 44, 21, true);
            btnMovimientos.Click += btnMovimientos_Click;
            ConfigurarPanelModulo(pnlSubmenuMovimientos, "pnlSubmenuMovimientos", 440);

            ConfigurarBotonModulo(btnAsignarMembresia, "btnAsignarMembresia", "ASIGNACIÓN DE MEMBRESÍA", 40, 36);
            ConfigurarBotonModulo(btnRenovarMembresia, "btnRenovarMembresia", "RENOVACIÓN DE MEMBRESÍA", 40, 36);
            ConfigurarBotonModulo(btnMovimientoCobros, "btnMovimientoCobros", "COBROS", 40, 36);
            ConfigurarBotonModulo(btnGenerarCargos, "btnGenerarCargos", "GENERACIÓN DE CARGOS", 40, 36);
            ConfigurarBotonModulo(btnMovimientoVentas, "btnMovimientoVentas", "VENTAS", 40, 36);
            ConfigurarBotonModulo(btnMovimientoCompras, "btnMovimientoCompras", "COMPRAS", 40, 36);
            ConfigurarBotonModulo(btnReservarClases, "btnReservarClases", "RESERVAS DE CLASES", 40, 36);
            ConfigurarBotonModulo(btnCuentasCobrar, "btnCuentasCobrar", "CUENTAS POR COBRAR", 40, 36);
            ConfigurarBotonModulo(btnAbonos, "btnAbonos", "ABONOS", 40, 36);
            ConfigurarBotonModulo(btnInventario, "btnInventario", "ENTRADA Y SALIDA DE INVENTARIO", 40, 36);
            ConfigurarBotonModulo(btnAsistencias, "btnAsistencias", "ASISTENCIAS", 40, 36);

            btnAsignarMembresia.Click += btnAsignarMembresia_Click;
            btnRenovarMembresia.Click += btnRenovarMembresia_Click;
            btnMovimientoCobros.Click += btnMovimientoCobros_Click;
            btnGenerarCargos.Click += btnGenerarCargos_Click;
            btnMovimientoVentas.Click += btnMovimientoVentas_Click;
            btnMovimientoCompras.Click += btnMovimientoCompras_Click;
            btnReservarClases.Click += btnReservarClases_Click;
            btnCuentasCobrar.Click += btnCuentasCobrar_Click;
            btnAbonos.Click += btnAbonos_Click;
            btnInventario.Click += btnInventario_Click;
            btnAsistencias.Click += btnAsistencias_Click;

            pnlSubmenuMovimientos.Controls.Add(btnAsignarMembresia);
            pnlSubmenuMovimientos.Controls.Add(btnRenovarMembresia);
            pnlSubmenuMovimientos.Controls.Add(btnMovimientoCobros);
            pnlSubmenuMovimientos.Controls.Add(btnGenerarCargos);
            pnlSubmenuMovimientos.Controls.Add(btnMovimientoVentas);
            pnlSubmenuMovimientos.Controls.Add(btnMovimientoCompras);
            pnlSubmenuMovimientos.Controls.Add(btnReservarClases);
            pnlSubmenuMovimientos.Controls.Add(btnCuentasCobrar);
            pnlSubmenuMovimientos.Controls.Add(btnAbonos);
            pnlSubmenuMovimientos.Controls.Add(btnInventario);
            pnlSubmenuMovimientos.Controls.Add(btnAsistencias);

            ConfigurarBotonModulo(btnReportes, "btnReportes", ">  REPORTES", 44, 21, true);
            btnReportes.Click += btnReportes_Click;
            ConfigurarPanelModulo(pnlSubmenuReportes, "pnlSubmenuReportes", 280);

            ConfigurarBotonModulo(btnReporteBalanceClientes, "btnReporteBalanceClientes", "BALANCE PENDIENTE POR CLIENTE", 40, 36);
            ConfigurarBotonModulo(btnReporteClientes, "btnReporteClientes", "CLIENTES", 40, 36);
            ConfigurarBotonModulo(btnReporteMembresias, "btnReporteMembresias", "MEMBRESÍAS", 40, 36);
            ConfigurarBotonModulo(btnReporteCobros, "btnReporteCobros", "COBROS POR FECHA", 40, 36);
            ConfigurarBotonModulo(btnReporteVentas, "btnReporteVentas", "VENTAS POR FECHA", 40, 36);
            ConfigurarBotonModulo(btnReporteCompras, "btnReporteCompras", "COMPRAS POR FECHA", 40, 36);
            ConfigurarBotonModulo(btnReporteCargos, "btnReporteCargos", "CARGOS PENDIENTES Y VENCIDOS", 40, 36);

            btnReporteBalanceClientes.Click += btnReporteBalanceClientes_Click;
            btnReporteClientes.Click += btnReporteClientes_Click;
            btnReporteMembresias.Click += btnReporteMembresias_Click;
            btnReporteCobros.Click += btnReporteCobros_Click;
            btnReporteVentas.Click += btnReporteVentas_Click;
            btnReporteCompras.Click += btnReporteCompras_Click;
            btnReporteCargos.Click += btnReporteCargos_Click;

            pnlSubmenuReportes.Controls.Add(btnReporteBalanceClientes);
            pnlSubmenuReportes.Controls.Add(btnReporteClientes);
            pnlSubmenuReportes.Controls.Add(btnReporteMembresias);
            pnlSubmenuReportes.Controls.Add(btnReporteCobros);
            pnlSubmenuReportes.Controls.Add(btnReporteVentas);
            pnlSubmenuReportes.Controls.Add(btnReporteCompras);
            pnlSubmenuReportes.Controls.Add(btnReporteCargos);

            pnlOpciones.Controls.Add(btnMovimientos);
            pnlOpciones.Controls.Add(pnlSubmenuMovimientos);
            pnlOpciones.Controls.Add(btnReportes);
            pnlOpciones.Controls.Add(pnlSubmenuReportes);
            pnlOpciones.Controls.SetChildIndex(btnMovimientos, 3);
            pnlOpciones.Controls.SetChildIndex(pnlSubmenuMovimientos, 4);
            pnlOpciones.Controls.SetChildIndex(btnReportes, 5);
            pnlOpciones.Controls.SetChildIndex(pnlSubmenuReportes, 6);
        }

        private static void ConfigurarPanelModulo(
            FlowLayoutPanel panel,
            string nombre,
            int alto)
        {
            panel.BackColor = Color.Black;
            panel.FlowDirection = FlowDirection.TopDown;
            panel.Margin = new Padding(0);
            panel.Name = nombre;
            panel.Size = new Size(263, alto);
            panel.Visible = false;
            panel.WrapContents = false;
        }

        private static void ConfigurarBotonModulo(
            Button boton,
            string nombre,
            string texto,
            int alto,
            int margenIzquierdo,
            bool encabezado = false)
        {
            boton.BackColor = Color.Black;
            boton.Cursor = Cursors.Hand;
            boton.FlatStyle = FlatStyle.Flat;
            boton.Font = new Font("Segoe UI", encabezado ? 9F : 8.5F, FontStyle.Bold);
            boton.ForeColor = Color.White;
            boton.Margin = new Padding(0);
            boton.Name = nombre;
            boton.Padding = new Padding(margenIzquierdo, 0, 0, 0);
            boton.Size = new Size(263, alto);
            boton.Text = texto;
            boton.TextAlign = ContentAlignment.MiddleLeft;
            boton.UseVisualStyleBackColor = false;
            boton.FlatAppearance.BorderSize = 0;
            boton.FlatAppearance.MouseDownBackColor = Color.FromArgb(100, 117, 136);
            boton.FlatAppearance.MouseOverBackColor = Color.FromArgb(124, 142, 163);
        }
    }
}

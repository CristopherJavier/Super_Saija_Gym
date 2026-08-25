using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Seguridad;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    public partial class FrmPrincipal : Form
    {
        private Form? formularioActivo;

        public bool CerrarSesionSolicitada { get; private set; } = false;

        public FrmPrincipal()
        {
            InitializeComponent();
            ConfigurarDesplazamientoMenu();
            InicializarMenusMovimientosReportes();
            InicializarMenuConfiguracion();
            pnlOpciones.SizeChanged += pnlOpciones_SizeChanged;
            AjustarAnchoMenu();
        }

        private void ConfigurarDesplazamientoMenu()
        {
            pnlOpciones.Dock = DockStyle.None;
            pnlOpciones.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            pnlOpciones.Width = pnlMenuLateral.ClientSize.Width
                + SystemInformation.VerticalScrollBarWidth;
            pnlOpciones.HorizontalScroll.Enabled = false;
            pnlOpciones.HorizontalScroll.Visible = false;
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            if (!SesionActual.HaySesion)
            {
                MessageBox.Show(
                    "No existe una sesión activa.",
                    "Sesión requerida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                Close();
                return;
            }

            lblBienvenida.Text = SesionActual.NombreCompleto;
            lblRol.Text = $"Rol: {SesionActual.NombreRol}";
            AplicarPermisos();
            SeleccionarBoton(btnInicio);
            AjustarAnchoMenu();
            MostrarInicio();
        }

        private void AbrirFormularioEnPanel(Form formulario)
        {
            LimpiarImagenInicio();

            if (formularioActivo is not null)
            {
                formularioActivo.Close();
            }

            formularioActivo = formulario;
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            pnlContenido.Controls.Clear();
            pnlContenido.BackColor = Color.White;
            pnlContenido.Padding = new Padding(0);
            pnlContenido.Controls.Add(formulario);
            formulario.Show();
            formulario.BringToFront();
        }

        private void MostrarInicio()
        {
            LimpiarImagenInicio();

            if (formularioActivo is not null)
            {
                formularioActivo.Close();
                formularioActivo = null;
            }

            pnlContenido.Controls.Clear();
            pnlContenido.BackColor = Color.Black;
            pnlContenido.Padding = new Padding(12);

            string rutaImagen = Path.Combine(
                AppContext.BaseDirectory,
                "Recursos",
                "inicio_goku.png");

            using Image imagen = Image.FromFile(rutaImagen);

            picInicio.Image = new Bitmap(imagen);
            pnlContenido.Controls.Add(picInicio);
        }

        private void LimpiarImagenInicio()
        {
            pnlContenido.Controls.Remove(picInicio);
            picInicio.Image?.Dispose();
            picInicio.Image = null;
        }

        private void SeleccionarBoton(Button botonSeleccionado)
        {
            Button[] botonesMenu =
            {
                btnInicio,
                btnClientes,
                btnEntrenadores,
                btnTiposMembresias,
                btnClasesActividades,
                btnHorariosClases,
                btnCategoriasProductos,
                btnProductos,
                btnProveedores,
                btnAsignarMembresia,
                btnRenovarMembresia,
                btnMovimientoCobros,
                btnGenerarCargos,
                btnMovimientoVentas,
                btnMovimientoCompras,
                btnReservarClases,
                btnCuentasCobrar,
                btnAbonos,
                btnInventario,
                btnAsistencias,
                btnReporteBalanceClientes,
                btnReporteClientes,
                btnReporteMembresias,
                btnReporteCobros,
                btnReporteVentas,
                btnReporteCompras,
                btnReporteCargos,
                btnConsultaEntrenadores,
                btnConsultaTiposMembresias,
                btnConsultaClases,
                btnConsultaReservas,
                btnConsultaCargos,
                btnConsultaProductos,
                btnConsultaVentas,
                btnConsultaCompras,
                btnConsultaProveedores,
                btnConsultaCobros,
                btnCambiarContrasena
            };

            foreach (Button boton in botonesMenu)
            {
                boton.BackColor = Color.Black;
            }

            botonSeleccionado.BackColor = Color.FromArgb(124, 142, 163);
        }

        private void btnMantenimientos_Click(object sender, EventArgs e)
        {
            pnlSubmenuMantenimientos.Visible = !pnlSubmenuMantenimientos.Visible;
            btnMantenimientos.Text = pnlSubmenuMantenimientos.Visible
                ? "v  MANTENIMIENTOS"
                : ">  MANTENIMIENTOS";
            AjustarAnchoMenu();
        }

        private void btnConsultas_Click(object sender, EventArgs e)
        {
            pnlSubmenuConsultas.Visible = !pnlSubmenuConsultas.Visible;
            btnConsultas.Text = pnlSubmenuConsultas.Visible
                ? "v  CONSULTAS"
                : ">  CONSULTAS";
            AjustarAnchoMenu();
        }

        private void btnMovimientos_Click(object? sender, EventArgs e)
        {
            pnlSubmenuMovimientos.Visible = !pnlSubmenuMovimientos.Visible;
            btnMovimientos.Text = pnlSubmenuMovimientos.Visible
                ? "v  MOVIMIENTOS"
                : ">  MOVIMIENTOS";
            AjustarAnchoMenu();
        }

        private void btnReportes_Click(object? sender, EventArgs e)
        {
            pnlSubmenuReportes.Visible = !pnlSubmenuReportes.Visible;
            btnReportes.Text = pnlSubmenuReportes.Visible
                ? "v  REPORTES"
                : ">  REPORTES";
            AjustarAnchoMenu();
        }

        private void btnConfiguracion_Click(object sender, EventArgs e)
        {
            pnlSubmenuConfiguracion.Visible = !pnlSubmenuConfiguracion.Visible;
            btnConfiguracion.Text = pnlSubmenuConfiguracion.Visible
                ? "v  CONFIGURACIÓN"
                : ">  CONFIGURACIÓN";
            AjustarAnchoMenu();
        }

        private void AjustarAnchoMenu()
        {
            if (btnConfiguracion is null || pnlSubmenuConfiguracion is null)
            {
                return;
            }

            int ancho = pnlMenuLateral.ClientSize.Width;

            btnInicio.Width = ancho;
            btnMantenimientos.Width = ancho;
            pnlSubmenuMantenimientos.Width = ancho;
            btnMovimientos.Width = ancho;
            pnlSubmenuMovimientos.Width = ancho;
            btnReportes.Width = ancho;
            pnlSubmenuReportes.Width = ancho;
            btnConsultas.Width = ancho;
            pnlSubmenuConsultas.Width = ancho;
            btnConfiguracion.Width = ancho;
            pnlSubmenuConfiguracion.Width = ancho;

            foreach (Control control in pnlSubmenuMantenimientos.Controls)
            {
                control.Width = ancho;
            }

            foreach (Control control in pnlSubmenuConsultas.Controls)
            {
                control.Width = ancho;
            }

            foreach (Control control in pnlSubmenuMovimientos.Controls)
            {
                control.Width = ancho;
            }

            foreach (Control control in pnlSubmenuReportes.Controls)
            {
                control.Width = ancho;
            }

            foreach (Control control in pnlSubmenuConfiguracion.Controls)
            {
                control.Width = ancho;
            }

            pnlOpciones.PerformLayout();
        }

        private void pnlOpciones_SizeChanged(object? sender, EventArgs e)
        {
            AjustarAnchoMenu();
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            SeleccionarBoton(btnInicio);
            MostrarInicio();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            SeleccionarBoton(btnClientes);
            AbrirFormularioEnPanel(new FrmClientes());
        }

        private void btnEntrenadores_Click(object sender, EventArgs e)
        {
            SeleccionarBoton(btnEntrenadores);
            AbrirFormularioEnPanel(new FrmEntrenadores());
        }

        private void btnTiposMembresias_Click(object sender, EventArgs e)
        {
            SeleccionarBoton(btnTiposMembresias);
            AbrirFormularioEnPanel(new FrmTiposMembresias());
        }

        private void btnClasesActividades_Click(object sender, EventArgs e)
        {
            SeleccionarBoton(btnClasesActividades);
            AbrirFormularioEnPanel(new FrmClasesActividades());
        }

        private void btnHorariosClases_Click(object sender, EventArgs e)
        {
            SeleccionarBoton(btnHorariosClases);
            AbrirFormularioEnPanel(new FrmHorariosClases());
        }

        private void btnCategoriasProductos_Click(object sender, EventArgs e)
        {
            SeleccionarBoton(btnCategoriasProductos);
            AbrirFormularioEnPanel(new FrmCategoriasProductos());
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            SeleccionarBoton(btnProductos);
            AbrirFormularioEnPanel(new FrmProductos());
        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            SeleccionarBoton(btnProveedores);
            AbrirFormularioEnPanel(new FrmProveedores());
        }

        private void btnAsignarMembresia_Click(object? sender, EventArgs e)
        {
            SeleccionarBoton(btnAsignarMembresia);
            AbrirFormularioEnPanel(new FrmAsignarMembresia());
        }

        private void btnRenovarMembresia_Click(object? sender, EventArgs e)
        {
            SeleccionarBoton(btnRenovarMembresia);
            AbrirFormularioEnPanel(new FrmRenovarMembresia());
        }

        private void btnMovimientoCobros_Click(object? sender, EventArgs e)
        {
            SeleccionarBoton(btnMovimientoCobros);
            AbrirFormularioEnPanel(new FrmCobros());
        }

        private void btnGenerarCargos_Click(object? sender, EventArgs e)
        {
            SeleccionarBoton(btnGenerarCargos);
            AbrirFormularioEnPanel(new FrmGenerarCargo());
        }

        private void btnMovimientoVentas_Click(object? sender, EventArgs e)
        {
            SeleccionarBoton(btnMovimientoVentas);
            AbrirFormularioEnPanel(new FrmVentas());
        }

        private void btnMovimientoCompras_Click(object? sender, EventArgs e)
        {
            SeleccionarBoton(btnMovimientoCompras);
            AbrirFormularioEnPanel(new FrmCompras());
        }

        private void btnReservarClases_Click(object? sender, EventArgs e)
        {
            SeleccionarBoton(btnReservarClases);
            AbrirFormularioEnPanel(new FrmReservasClases());
        }

        private void btnCuentasCobrar_Click(object? sender, EventArgs e)
        {
            SeleccionarBoton(btnCuentasCobrar);
            AbrirFormularioEnPanel(new FrmCuentasCobrar());
        }

        private void btnAbonos_Click(object? sender, EventArgs e)
        {
            SeleccionarBoton(btnAbonos);
            AbrirFormularioEnPanel(new FrmAbonos());
        }

        private void btnInventario_Click(object? sender, EventArgs e)
        {
            SeleccionarBoton(btnInventario);
            AbrirFormularioEnPanel(new FrmMovimientosInventario());
        }

        private void btnAsistencias_Click(object? sender, EventArgs e)
        {
            SeleccionarBoton(btnAsistencias);
            AbrirFormularioEnPanel(new FrmAsistencias());
        }

        private void btnReporteBalanceClientes_Click(object? sender, EventArgs e)
        {
            SeleccionarBoton(btnReporteBalanceClientes);
            AbrirFormularioEnPanel(new FrmReporte(TipoReporte.BalanceClientes));
        }

        private void btnReporteClientes_Click(object? sender, EventArgs e)
        {
            SeleccionarBoton(btnReporteClientes);
            AbrirFormularioEnPanel(new FrmReporte(TipoReporte.Clientes));
        }

        private void btnReporteMembresias_Click(object? sender, EventArgs e)
        {
            SeleccionarBoton(btnReporteMembresias);
            AbrirFormularioEnPanel(new FrmReporte(TipoReporte.Membresias));
        }

        private void btnReporteCobros_Click(object? sender, EventArgs e)
        {
            SeleccionarBoton(btnReporteCobros);
            AbrirFormularioEnPanel(new FrmReporte(TipoReporte.Cobros));
        }

        private void btnReporteVentas_Click(object? sender, EventArgs e)
        {
            SeleccionarBoton(btnReporteVentas);
            AbrirFormularioEnPanel(new FrmReporte(TipoReporte.Ventas));
        }

        private void btnReporteCompras_Click(object? sender, EventArgs e)
        {
            SeleccionarBoton(btnReporteCompras);
            AbrirFormularioEnPanel(new FrmReporte(TipoReporte.Compras));
        }

        private void btnReporteCargos_Click(object? sender, EventArgs e)
        {
            SeleccionarBoton(btnReporteCargos);
            AbrirFormularioEnPanel(new FrmReporte(TipoReporte.Cargos));
        }

        private void btnConsultaEntrenadores_Click(object sender, EventArgs e)
        {
            SeleccionarBoton(btnConsultaEntrenadores);
            AbrirFormularioEnPanel(new FrmConsultaEntrenadores());
        }

        private void btnConsultaTiposMembresias_Click(object sender, EventArgs e)
        {
            SeleccionarBoton(btnConsultaTiposMembresias);
            AbrirFormularioEnPanel(new FrmConsultaTiposMembresias());
        }

        private void btnConsultaClases_Click(object sender, EventArgs e)
        {
            SeleccionarBoton(btnConsultaClases);
            AbrirFormularioEnPanel(new FrmConsultaClases());
        }

        private void btnConsultaReservas_Click(object sender, EventArgs e)
        {
            SeleccionarBoton(btnConsultaReservas);
            AbrirFormularioEnPanel(new FrmConsultaReservas());
        }

        private void btnConsultaCargos_Click(object sender, EventArgs e)
        {
            SeleccionarBoton(btnConsultaCargos);
            AbrirFormularioEnPanel(new FrmConsultaCargos());
        }

        private void btnConsultaProductos_Click(object sender, EventArgs e)
        {
            SeleccionarBoton(btnConsultaProductos);
            AbrirFormularioEnPanel(new FrmConsultaProductos());
        }

        private void btnConsultaVentas_Click(object sender, EventArgs e)
        {
            SeleccionarBoton(btnConsultaVentas);
            AbrirFormularioEnPanel(new FrmConsultaVentas());
        }

        private void btnConsultaCompras_Click(object sender, EventArgs e)
        {
            SeleccionarBoton(btnConsultaCompras);
            AbrirFormularioEnPanel(new FrmConsultaCompras());
        }

        private void btnConsultaProveedores_Click(object sender, EventArgs e)
        {
            SeleccionarBoton(btnConsultaProveedores);
            AbrirFormularioEnPanel(new FrmConsultaProveedores());
        }

        private void btnConsultaCobros_Click(object sender, EventArgs e)
        {
            SeleccionarBoton(btnConsultaCobros);
            AbrirFormularioEnPanel(new FrmConsultaCobros());
        }

        private void btnCambiarContrasena_Click(object? sender, EventArgs e)
        {
            SeleccionarBoton(btnCambiarContrasena);
            using FrmCambiarContrasena formulario = new FrmCambiarContrasena();
            formulario.ShowDialog(this);
        }

        private void AplicarPermisos()
        {
            bool puedeClientes = SesionActual.TienePermiso(ClavesPermisos.MantenimientoClientes);
            bool puedeEntrenadores = SesionActual.TienePermiso(ClavesPermisos.MantenimientoEntrenadores);
            bool puedeTiposMembresias = SesionActual.TienePermiso(ClavesPermisos.MantenimientoTiposMembresias);
            bool puedeClases = SesionActual.TienePermiso(ClavesPermisos.MantenimientoClases);
            bool puedeHorarios = SesionActual.TienePermiso(ClavesPermisos.MantenimientoHorarios);
            bool puedeCategorias = SesionActual.TienePermiso(ClavesPermisos.MantenimientoCategorias);
            bool puedeProductos = SesionActual.TienePermiso(ClavesPermisos.MantenimientoProductos);
            bool puedeProveedores = SesionActual.TienePermiso(ClavesPermisos.MantenimientoProveedores);

            btnClientes.Visible = puedeClientes;
            btnEntrenadores.Visible = puedeEntrenadores;
            btnTiposMembresias.Visible = puedeTiposMembresias;
            btnClasesActividades.Visible = puedeClases;
            btnHorariosClases.Visible = puedeHorarios;
            btnCategoriasProductos.Visible = puedeCategorias;
            btnProductos.Visible = puedeProductos;
            btnProveedores.Visible = puedeProveedores;
            pnlSubmenuMantenimientos.Height = 40 * new[]
            {
                puedeClientes,
                puedeEntrenadores,
                puedeTiposMembresias,
                puedeClases,
                puedeHorarios,
                puedeCategorias,
                puedeProductos,
                puedeProveedores
            }.Count(permitido => permitido);
            btnMantenimientos.Visible = puedeClientes
                || puedeEntrenadores
                || puedeTiposMembresias
                || puedeClases
                || puedeHorarios
                || puedeCategorias
                || puedeProductos
                || puedeProveedores;

            bool puedeAsignarMembresia = SesionActual.TienePermiso(ClavesPermisos.MovimientoAsignarMembresia);
            bool puedeRenovarMembresia = SesionActual.TienePermiso(ClavesPermisos.MovimientoRenovarMembresia);
            bool puedeCobros = SesionActual.TienePermiso(ClavesPermisos.MovimientoCobros);
            bool puedeGenerarCargos = SesionActual.TienePermiso(ClavesPermisos.MovimientoGenerarCargos);
            bool puedeVentas = SesionActual.TienePermiso(ClavesPermisos.MovimientoVentas);
            bool puedeCompras = SesionActual.TienePermiso(ClavesPermisos.MovimientoCompras);
            bool puedeReservas = SesionActual.TienePermiso(ClavesPermisos.MovimientoReservas);
            bool puedeCuentasCobrar = SesionActual.TienePermiso(ClavesPermisos.MovimientoCuentasCobrar);
            bool puedeAbonos = SesionActual.TienePermiso(ClavesPermisos.MovimientoAbonos);
            bool puedeInventario = SesionActual.TienePermiso(ClavesPermisos.MovimientoInventario);
            bool puedeAsistencias = SesionActual.TienePermiso(ClavesPermisos.MovimientoAsistencias);

            btnAsignarMembresia.Visible = puedeAsignarMembresia;
            btnRenovarMembresia.Visible = puedeRenovarMembresia;
            btnMovimientoCobros.Visible = puedeCobros;
            btnGenerarCargos.Visible = puedeGenerarCargos;
            btnMovimientoVentas.Visible = puedeVentas;
            btnMovimientoCompras.Visible = puedeCompras;
            btnReservarClases.Visible = puedeReservas;
            btnCuentasCobrar.Visible = puedeCuentasCobrar;
            btnAbonos.Visible = puedeAbonos;
            btnInventario.Visible = puedeInventario;
            btnAsistencias.Visible = puedeAsistencias;
            pnlSubmenuMovimientos.Height = 40 * new[]
            {
                puedeAsignarMembresia,
                puedeRenovarMembresia,
                puedeCobros,
                puedeGenerarCargos,
                puedeVentas,
                puedeCompras,
                puedeReservas,
                puedeCuentasCobrar,
                puedeAbonos,
                puedeInventario,
                puedeAsistencias
            }.Count(permitido => permitido);
            btnMovimientos.Visible = puedeAsignarMembresia
                || puedeRenovarMembresia
                || puedeCobros
                || puedeGenerarCargos
                || puedeVentas
                || puedeCompras
                || puedeReservas
                || puedeCuentasCobrar
                || puedeAbonos
                || puedeInventario
                || puedeAsistencias;

            bool puedeReporteBalance = SesionActual.TienePermiso(ClavesPermisos.ReporteBalanceClientes);
            bool puedeReporteClientes = SesionActual.TienePermiso(ClavesPermisos.ReporteClientes);
            bool puedeReporteMembresias = SesionActual.TienePermiso(ClavesPermisos.ReporteMembresias);
            bool puedeReporteCobros = SesionActual.TienePermiso(ClavesPermisos.ReporteCobros);
            bool puedeReporteVentas = SesionActual.TienePermiso(ClavesPermisos.ReporteVentas);
            bool puedeReporteCompras = SesionActual.TienePermiso(ClavesPermisos.ReporteCompras);
            bool puedeReporteCargos = SesionActual.TienePermiso(ClavesPermisos.ReporteCargos);

            btnReporteBalanceClientes.Visible = puedeReporteBalance;
            btnReporteClientes.Visible = puedeReporteClientes;
            btnReporteMembresias.Visible = puedeReporteMembresias;
            btnReporteCobros.Visible = puedeReporteCobros;
            btnReporteVentas.Visible = puedeReporteVentas;
            btnReporteCompras.Visible = puedeReporteCompras;
            btnReporteCargos.Visible = puedeReporteCargos;
            pnlSubmenuReportes.Height = 40 * new[]
            {
                puedeReporteBalance,
                puedeReporteClientes,
                puedeReporteMembresias,
                puedeReporteCobros,
                puedeReporteVentas,
                puedeReporteCompras,
                puedeReporteCargos
            }.Count(permitido => permitido);
            btnReportes.Visible = puedeReporteBalance
                || puedeReporteClientes
                || puedeReporteMembresias
                || puedeReporteCobros
                || puedeReporteVentas
                || puedeReporteCompras
                || puedeReporteCargos;

            bool puedeConsultaEntrenadores = SesionActual.TienePermiso(ClavesPermisos.ConsultaEntrenadores);
            bool puedeConsultaMembresias = SesionActual.TienePermiso(ClavesPermisos.ConsultaMembresias);
            bool puedeConsultaClases = SesionActual.TienePermiso(ClavesPermisos.ConsultaClases);
            bool puedeConsultaReservas = SesionActual.TienePermiso(ClavesPermisos.ConsultaReservas);
            bool puedeConsultaCargos = SesionActual.TienePermiso(ClavesPermisos.ConsultaCargos);
            bool puedeConsultaProductos = SesionActual.TienePermiso(ClavesPermisos.ConsultaProductos);
            bool puedeConsultaVentas = SesionActual.TienePermiso(ClavesPermisos.ConsultaVentas);
            bool puedeConsultaCompras = SesionActual.TienePermiso(ClavesPermisos.ConsultaCompras);
            bool puedeConsultaProveedores = SesionActual.TienePermiso(ClavesPermisos.ConsultaProveedores);
            bool puedeConsultaCobros = SesionActual.TienePermiso(ClavesPermisos.ConsultaCobros);

            btnConsultaEntrenadores.Visible = puedeConsultaEntrenadores;
            btnConsultaTiposMembresias.Visible = puedeConsultaMembresias;
            btnConsultaClases.Visible = puedeConsultaClases;
            btnConsultaReservas.Visible = puedeConsultaReservas;
            btnConsultaCargos.Visible = puedeConsultaCargos;
            btnConsultaProductos.Visible = puedeConsultaProductos;
            btnConsultaVentas.Visible = puedeConsultaVentas;
            btnConsultaCompras.Visible = puedeConsultaCompras;
            btnConsultaProveedores.Visible = puedeConsultaProveedores;
            btnConsultaCobros.Visible = puedeConsultaCobros;
            pnlSubmenuConsultas.Height = 40 * new[]
            {
                puedeConsultaEntrenadores,
                puedeConsultaMembresias,
                puedeConsultaClases,
                puedeConsultaReservas,
                puedeConsultaCargos,
                puedeConsultaProductos,
                puedeConsultaVentas,
                puedeConsultaCompras,
                puedeConsultaProveedores,
                puedeConsultaCobros
            }.Count(permitido => permitido);
            btnConsultas.Visible = puedeConsultaEntrenadores
                || puedeConsultaMembresias
                || puedeConsultaClases
                || puedeConsultaReservas
                || puedeConsultaCargos
                || puedeConsultaProductos
                || puedeConsultaVentas
                || puedeConsultaCompras
                || puedeConsultaProveedores
                || puedeConsultaCobros;

            btnCambiarContrasena.Visible = true;
            pnlSubmenuConfiguracion.Height = 40;
            btnConfiguracion.Visible = true;

            if (!btnMantenimientos.Visible)
            {
                pnlSubmenuMantenimientos.Visible = false;
            }

            if (!btnConsultas.Visible)
            {
                pnlSubmenuConsultas.Visible = false;
            }

            if (!btnMovimientos.Visible)
            {
                pnlSubmenuMovimientos.Visible = false;
            }

            if (!btnReportes.Visible)
            {
                pnlSubmenuReportes.Visible = false;
            }

            if (!btnConfiguracion.Visible)
            {
                pnlSubmenuConfiguracion.Visible = false;
            }
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show(
                "¿Deseas cerrar la sesión actual?",
                "Confirmar cierre de sesión",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                CerrarSesionSolicitada = true;
                Close();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show(
                "¿Deseas salir de la aplicación?",
                "Confirmar salida",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                CerrarSesionSolicitada = false;
                Close();
            }
        }

    }
}

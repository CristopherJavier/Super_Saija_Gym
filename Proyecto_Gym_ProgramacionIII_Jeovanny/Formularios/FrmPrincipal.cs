using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    public partial class FrmPrincipal : Form
    {
        private Form? formularioActivo;

        public bool CerrarSesionSolicitada { get; private set; } = false;

        public FrmPrincipal()
        {
            InitializeComponent();
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
                btnConsultaEntrenadores,
                btnConsultaTiposMembresias,
                btnConsultaClases,
                btnConsultaReservas,
                btnConsultaCargos,
                btnConsultaProductos,
                btnConsultaVentas,
                btnConsultaCompras,
                btnConsultaProveedores,
                btnConsultaCobros
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

        private void AjustarAnchoMenu()
        {
            int altoContenido = btnInicio.Height + btnMantenimientos.Height + btnConsultas.Height;

            if (pnlSubmenuMantenimientos.Visible)
            {
                altoContenido += pnlSubmenuMantenimientos.Height;
            }

            if (pnlSubmenuConsultas.Visible)
            {
                altoContenido += pnlSubmenuConsultas.Height;
            }

            int ancho = pnlOpciones.ClientSize.Width;
            if (altoContenido > pnlOpciones.ClientSize.Height)
            {
                ancho -= SystemInformation.VerticalScrollBarWidth + 4;
            }

            ancho = Math.Max(0, ancho);

            btnInicio.Width = ancho;
            btnMantenimientos.Width = ancho;
            pnlSubmenuMantenimientos.Width = ancho;
            btnConsultas.Width = ancho;
            pnlSubmenuConsultas.Width = ancho;

            foreach (Control control in pnlSubmenuMantenimientos.Controls)
            {
                control.Width = ancho;
            }

            foreach (Control control in pnlSubmenuConsultas.Controls)
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

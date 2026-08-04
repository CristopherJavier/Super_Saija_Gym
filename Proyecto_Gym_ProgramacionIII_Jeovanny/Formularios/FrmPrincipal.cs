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
            MostrarInicio();
        }

        private void AbrirFormularioEnPanel(Form formulario)
        {
            if (formularioActivo is not null)
            {
                formularioActivo.Close();
            }

            formularioActivo = formulario;
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            pnlContenido.Controls.Clear();
            pnlContenido.Controls.Add(formulario);
            formulario.Show();
            formulario.BringToFront();
        }

        private void MostrarInicio()
        {
            if (formularioActivo is not null)
            {
                formularioActivo.Close();
                formularioActivo = null;
            }

            pnlContenido.Controls.Clear();
            lblTituloSeccion.Text = "Inicio";

            Label lblTitulo = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 28F, FontStyle.Bold),
                ForeColor = Color.Black,
                Location = new Point(40, 38),
                Text = "Panel principal"
            };

            Label lblTexto = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 12F),
                ForeColor = Color.FromArgb(124, 142, 163),
                Location = new Point(45, 100),
                Text = "Selecciona una opción del menú para comenzar."
            };

            pnlContenido.Controls.Add(lblTitulo);
            pnlContenido.Controls.Add(lblTexto);
        }

        private void MostrarModuloPendiente(string titulo)
        {
            if (formularioActivo is not null)
            {
                formularioActivo.Close();
                formularioActivo = null;
            }

            pnlContenido.Controls.Clear();
            lblTituloSeccion.Text = titulo;

            Label lblTitulo = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 28F, FontStyle.Bold),
                ForeColor = Color.Black,
                Location = new Point(40, 38),
                Text = titulo
            };

            Label lblTexto = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 12F),
                ForeColor = Color.FromArgb(124, 142, 163),
                Location = new Point(45, 100),
                Text = "Este mantenimiento se implementará en la siguiente fase."
            };

            pnlContenido.Controls.Add(lblTitulo);
            pnlContenido.Controls.Add(lblTexto);
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
                btnProveedores
            };

            foreach (Button boton in botonesMenu)
            {
                boton.BackColor = Color.Black;
            }

            botonSeleccionado.BackColor = Color.FromArgb(124, 142, 163);
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            SeleccionarBoton(btnInicio);
            MostrarInicio();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            SeleccionarBoton(btnClientes);
            MostrarModuloPendiente("Clientes");
        }

        private void btnEntrenadores_Click(object sender, EventArgs e)
        {
            SeleccionarBoton(btnEntrenadores);
            MostrarModuloPendiente("Entrenadores");
        }

        private void btnTiposMembresias_Click(object sender, EventArgs e)
        {
            SeleccionarBoton(btnTiposMembresias);
            MostrarModuloPendiente("Tipos de membresías");
        }

        private void btnClasesActividades_Click(object sender, EventArgs e)
        {
            SeleccionarBoton(btnClasesActividades);
            MostrarModuloPendiente("Clases y actividades");
        }

        private void btnHorariosClases_Click(object sender, EventArgs e)
        {
            SeleccionarBoton(btnHorariosClases);
            MostrarModuloPendiente("Horarios de clases");
        }

        private void btnCategoriasProductos_Click(object sender, EventArgs e)
        {
            SeleccionarBoton(btnCategoriasProductos);
            MostrarModuloPendiente("Categorías de productos");
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            SeleccionarBoton(btnProductos);
            MostrarModuloPendiente("Productos");
        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            SeleccionarBoton(btnProveedores);
            MostrarModuloPendiente("Proveedores");
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

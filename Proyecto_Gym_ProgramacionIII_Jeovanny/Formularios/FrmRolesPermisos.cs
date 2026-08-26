using Proyecto_Gym_ProgramacionIII_Jeovanny.Datos;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    public class FrmRolesPermisos : Form
    {
        private readonly ComboBox cmbRoles;
        private readonly TabControl tabModulos;
        private readonly CheckedListBox clbMantenimientos;
        private readonly CheckedListBox clbMovimientos;
        private readonly CheckedListBox clbReportes;
        private readonly CheckedListBox clbConsultas;
        private readonly CheckedListBox clbConfiguracion;
        private readonly List<CheckedListBox> listasPermisos;
        private readonly Button btnMarcarTodos;
        private readonly Button btnDesmarcarTodos;
        private readonly Button btnGuardar;
        private readonly Label lblMensaje;
        private readonly Label lblResumen;
        private bool cargando;
        private bool permiteEditarPermisos;
        private bool guardando;

        public FrmRolesPermisos()
        {
            BackColor = Color.White;
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.None;
            MinimumSize = new Size(686, 533);

            TableLayoutPanel tlpPrincipal = new TableLayoutPanel
            {
                BackColor = Color.White,
                ColumnCount = 1,
                Dock = DockStyle.Fill,
                Padding = new Padding(18, 12, 18, 18),
                RowCount = 7
            };
            tlpPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 68F));
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));

            Label lblTitulo = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                Text = "Roles y permisos"
            };
            Label lblInstruccion = new Label
            {
                Dock = DockStyle.Fill,
                ForeColor = Color.FromArgb(70, 70, 70),
                Text = "Elige un rol y marca únicamente las secciones que podrá utilizar.",
                TextAlign = ContentAlignment.MiddleLeft
            };
            Panel pnlRol = new Panel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0)
            };
            Label lblRol = new Label
            {
                Dock = DockStyle.Top,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Height = 25,
                Text = "Rol"
            };
            cmbRoles = new ComboBox
            {
                Dock = DockStyle.Bottom,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10F),
                Height = 35
            };
            pnlRol.Controls.Add(cmbRoles);
            pnlRol.Controls.Add(lblRol);

            FlowLayoutPanel pnlBotones = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                Margin = new Padding(0),
                Padding = new Padding(0, 4, 0, 4),
                WrapContents = false
            };
            btnMarcarTodos = CrearBoton("MARCAR MÓDULO", false);
            btnDesmarcarTodos = CrearBoton("DESMARCAR MÓDULO", false);
            btnGuardar = CrearBoton("GUARDAR CAMBIOS", true);
            pnlBotones.Controls.Add(btnMarcarTodos);
            pnlBotones.Controls.Add(btnDesmarcarTodos);
            pnlBotones.Controls.Add(btnGuardar);

            tabModulos = new TabControl
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Margin = new Padding(0),
                Multiline = false,
                SizeMode = TabSizeMode.FillToRight
            };
            clbMantenimientos = CrearListaPermisos();
            clbMovimientos = CrearListaPermisos();
            clbReportes = CrearListaPermisos();
            clbConsultas = CrearListaPermisos();
            clbConfiguracion = CrearListaPermisos();
            listasPermisos = new List<CheckedListBox>
            {
                clbMantenimientos,
                clbMovimientos,
                clbReportes,
                clbConsultas,
                clbConfiguracion
            };
            AgregarPestanaModulo("MANTENIMIENTOS", clbMantenimientos);
            AgregarPestanaModulo("MOVIMIENTOS", clbMovimientos);
            AgregarPestanaModulo("REPORTES", clbReportes);
            AgregarPestanaModulo("CONSULTAS", clbConsultas);
            AgregarPestanaModulo("CONFIGURACIÓN", clbConfiguracion);
            lblMensaje = new Label
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.Firebrick,
                TextAlign = ContentAlignment.MiddleLeft
            };
            lblResumen = new Label
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleRight
            };

            tlpPrincipal.Controls.Add(lblTitulo, 0, 0);
            tlpPrincipal.Controls.Add(lblInstruccion, 0, 1);
            tlpPrincipal.Controls.Add(pnlRol, 0, 2);
            tlpPrincipal.Controls.Add(tabModulos, 0, 3);
            tlpPrincipal.Controls.Add(pnlBotones, 0, 4);
            tlpPrincipal.Controls.Add(lblMensaje, 0, 5);
            tlpPrincipal.Controls.Add(lblResumen, 0, 6);
            Controls.Add(tlpPrincipal);

            cmbRoles.SelectedIndexChanged += cmbRoles_SelectedIndexChanged;
            foreach (CheckedListBox listaPermisos in listasPermisos)
            {
                listaPermisos.Format += clbPermisos_Format;
                listaPermisos.ItemCheck += clbPermisos_ItemCheck;
            }
            tabModulos.SelectedIndexChanged += tabModulos_SelectedIndexChanged;
            btnMarcarTodos.Click += btnMarcarTodos_Click;
            btnDesmarcarTodos.Click += btnDesmarcarTodos_Click;
            btnGuardar.Click += btnGuardar_Click;
            Load += FrmRolesPermisos_Load;
        }

        private static CheckedListBox CrearListaPermisos()
        {
            return new CheckedListBox
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                CheckOnClick = true,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10F),
                FormattingEnabled = true,
                IntegralHeight = false
            };
        }

        private void AgregarPestanaModulo(string titulo, CheckedListBox listaPermisos)
        {
            TabPage pestana = new TabPage
            {
                BackColor = Color.White,
                Padding = new Padding(8),
                Text = titulo
            };
            pestana.Controls.Add(listaPermisos);
            tabModulos.TabPages.Add(pestana);
        }

        private async void FrmRolesPermisos_Load(object? sender, EventArgs e)
        {
            await CargarDatosAsync();
        }

        private async Task CargarDatosAsync()
        {
            try
            {
                cargando = true;
                lblMensaje.Text = string.Empty;
                List<Rol> roles = await RolRepositorio.ListarAsync(soloActivos: true);
                List<Permiso> permisos = await PermisoRepositorio.ListarAsync(soloActivos: true);
                DistribuirPermisosPorModulo(permisos);
                cmbRoles.DataSource = roles;

                Rol? rolInicial = roles.FirstOrDefault(rol =>
                    !rol.Nombre.Equals("ADMIN", StringComparison.OrdinalIgnoreCase));

                if (rolInicial is not null)
                {
                    cmbRoles.SelectedItem = rolInicial;
                }

                cargando = false;
                await CargarPermisosDelRolAsync();
            }
            catch (Exception)
            {
                cargando = false;
                lblMensaje.Text = "No fue posible cargar los roles y permisos.";
            }
        }

        private void DistribuirPermisosPorModulo(List<Permiso> permisos)
        {
            List<Permiso> mantenimientos = new List<Permiso>();
            List<Permiso> movimientos = new List<Permiso>();
            List<Permiso> reportes = new List<Permiso>();
            List<Permiso> consultas = new List<Permiso>();
            List<Permiso> configuracion = new List<Permiso>();

            foreach (Permiso permiso in permisos)
            {
                if (permiso.Clave.StartsWith("MANT_", StringComparison.OrdinalIgnoreCase))
                {
                    mantenimientos.Add(permiso);
                }
                else if (permiso.Clave.StartsWith("MOV_", StringComparison.OrdinalIgnoreCase))
                {
                    movimientos.Add(permiso);
                }
                else if (permiso.Clave.StartsWith("REP_", StringComparison.OrdinalIgnoreCase))
                {
                    reportes.Add(permiso);
                }
                else if (permiso.Clave.StartsWith("CONS_", StringComparison.OrdinalIgnoreCase))
                {
                    consultas.Add(permiso);
                }
                else
                {
                    configuracion.Add(permiso);
                }
            }

            clbMantenimientos.DataSource = mantenimientos;
            clbMovimientos.DataSource = movimientos;
            clbReportes.DataSource = reportes;
            clbConsultas.DataSource = consultas;
            clbConfiguracion.DataSource = configuracion;
        }

        private async Task CargarPermisosDelRolAsync()
        {
            if (cargando || cmbRoles.SelectedItem is not Rol rol)
            {
                return;
            }

            try
            {
                cargando = true;
                lblMensaje.Text = string.Empty;
                bool esAdministrador = rol.Nombre.Equals("ADMIN", StringComparison.OrdinalIgnoreCase);
                List<int> idsAsignados = esAdministrador
                    ? new List<int>()
                    : await RolPermisoRepositorio.ListarIdsPermisosAsync(rol.IdRol);

                foreach (CheckedListBox listaPermisos in listasPermisos)
                {
                    for (int indice = 0; indice < listaPermisos.Items.Count; indice++)
                    {
                        Permiso permiso = (Permiso)listaPermisos.Items[indice];
                        listaPermisos.SetItemChecked(
                            indice,
                            esAdministrador || idsAsignados.Contains(permiso.IdPermiso));
                    }
                }

                permiteEditarPermisos = !esAdministrador;
                foreach (CheckedListBox listaPermisos in listasPermisos)
                {
                    listaPermisos.Enabled = permiteEditarPermisos;
                }
                Cursor cursorBotones = permiteEditarPermisos
                    ? Cursors.Hand
                    : Cursors.No;
                btnMarcarTodos.Cursor = cursorBotones;
                btnDesmarcarTodos.Cursor = cursorBotones;
                btnGuardar.Cursor = cursorBotones;
                lblMensaje.ForeColor = esAdministrador
                    ? Color.FromArgb(35, 110, 55)
                    : Color.Firebrick;
                lblMensaje.Text = esAdministrador
                    ? "ADMIN siempre tiene acceso completo."
                    : string.Empty;
                ActualizarResumen();
            }
            catch (Exception)
            {
                lblMensaje.ForeColor = Color.Firebrick;
                lblMensaje.Text = "No fue posible cargar los permisos del rol.";
            }
            finally
            {
                cargando = false;
            }
        }

        private async void cmbRoles_SelectedIndexChanged(object? sender, EventArgs e)
        {
            await CargarPermisosDelRolAsync();
        }

        private void tabModulos_SelectedIndexChanged(object? sender, EventArgs e)
        {
            ActualizarResumen();
        }

        private void clbPermisos_Format(object? sender, ListControlConvertEventArgs e)
        {
            if (e.ListItem is Permiso permiso)
            {
                e.Value = string.IsNullOrWhiteSpace(permiso.Descripcion)
                    ? permiso.Nombre
                    : $"{permiso.Nombre} — {permiso.Descripcion}";
            }
        }

        private void clbPermisos_ItemCheck(object? sender, ItemCheckEventArgs e)
        {
            if (cargando)
            {
                return;
            }

            BeginInvoke(ActualizarResumen);
        }

        private void btnMarcarTodos_Click(object? sender, EventArgs e)
        {
            if (!permiteEditarPermisos)
            {
                return;
            }

            CambiarSeleccionModulo(true);
        }

        private void btnDesmarcarTodos_Click(object? sender, EventArgs e)
        {
            if (!permiteEditarPermisos)
            {
                return;
            }

            CambiarSeleccionModulo(false);
        }

        private void CambiarSeleccionModulo(bool marcado)
        {
            CheckedListBox listaSeleccionada = ObtenerListaModuloSeleccionado();
            cargando = true;
            for (int indice = 0; indice < listaSeleccionada.Items.Count; indice++)
            {
                listaSeleccionada.SetItemChecked(indice, marcado);
            }
            cargando = false;
            ActualizarResumen();
        }

        private CheckedListBox ObtenerListaModuloSeleccionado()
        {
            if (tabModulos.SelectedIndex < 0)
            {
                return clbMantenimientos;
            }

            return listasPermisos[tabModulos.SelectedIndex];
        }

        private async void btnGuardar_Click(object? sender, EventArgs e)
        {
            if (!permiteEditarPermisos || guardando)
            {
                return;
            }

            if (cmbRoles.SelectedItem is not Rol rol)
            {
                lblMensaje.Text = "Selecciona un rol.";
                return;
            }

            List<int> idsPermisos = new List<int>();
            foreach (CheckedListBox listaPermisos in listasPermisos)
            {
                foreach (object elemento in listaPermisos.CheckedItems)
                {
                    if (elemento is Permiso permiso)
                    {
                        idsPermisos.Add(permiso.IdPermiso);
                    }
                }
            }

            guardando = true;

            try
            {
                await RolPermisoRepositorio.GuardarAsync(rol.IdRol, idsPermisos);
                lblMensaje.ForeColor = Color.FromArgb(35, 110, 55);
                lblMensaje.Text = "Permisos guardados correctamente.";
            }
            catch (InvalidOperationException ex)
            {
                lblMensaje.ForeColor = Color.Firebrick;
                lblMensaje.Text = ex.Message;
            }
            catch (Exception)
            {
                lblMensaje.ForeColor = Color.Firebrick;
                lblMensaje.Text = "No fue posible guardar los permisos.";
            }
            finally
            {
                guardando = false;
            }
        }

        private void ActualizarResumen()
        {
            CheckedListBox listaSeleccionada = ObtenerListaModuloSeleccionado();
            int seleccionados = listasPermisos.Sum(lista => lista.CheckedItems.Count);
            int total = listasPermisos.Sum(lista => lista.Items.Count);
            lblResumen.Text = $"Módulo: {listaSeleccionada.CheckedItems.Count} de {listaSeleccionada.Items.Count} | Total: {seleccionados} de {total}";
        }

        private static Button CrearBoton(string texto, bool principal)
        {
            Button boton = new Button
            {
                BackColor = principal ? Color.Black : Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                ForeColor = principal ? Color.White : Color.Black,
                Height = 45,
                Margin = new Padding(0, 0, 10, 0),
                Text = texto,
                UseVisualStyleBackColor = false,
                Width = 155
            };
            boton.FlatAppearance.BorderColor = Color.Black;
            boton.FlatAppearance.BorderSize = principal ? 0 : 1;
            return boton;
        }
    }
}

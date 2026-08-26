using Npgsql;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    public class FrmModuloBase : Form
    {
        protected readonly FlowLayoutPanel pnlCampos;
        protected readonly FlowLayoutPanel pnlBotones;
        protected readonly Label lblMensaje;
        protected readonly Label lblResumen;
        protected readonly DataGridView dgvDatos;

        public FrmModuloBase()
            : this("Módulo", 82)
        {
        }

        protected FrmModuloBase(string titulo, int altoCampos = 150)
        {
            BackColor = Color.White;
            Font = new Font("Segoe UI", 9F);
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            FormBorderStyle = FormBorderStyle.None;
            MinimumSize = new Size(686, 533);

            TableLayoutPanel tlpPrincipal = new TableLayoutPanel
            {
                BackColor = Color.White,
                ColumnCount = 1,
                Dock = DockStyle.Fill,
                Padding = new Padding(18, 12, 18, 18),
                RowCount = 8
            };
            tlpPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 13F));
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, altoCampos));
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 53F));
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 13F));
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));

            Label lblTitulo = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = Color.Black,
                Margin = new Padding(0),
                Text = titulo
            };

            pnlCampos = new FlowLayoutPanel
            {
                BackColor = Color.White,
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                Margin = new Padding(0),
                Padding = new Padding(0, 4, 0, 0),
                WrapContents = true
            };

            pnlBotones = new FlowLayoutPanel
            {
                BackColor = Color.White,
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                Margin = new Padding(0),
                Padding = new Padding(0, 4, 0, 4),
                WrapContents = false
            };

            lblMensaje = new Label
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.Firebrick,
                TextAlign = ContentAlignment.MiddleLeft
            };

            dgvDatos = CrearTabla();

            lblResumen = new Label
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.Black,
                TextAlign = ContentAlignment.MiddleRight
            };

            tlpPrincipal.Controls.Add(lblTitulo, 0, 0);
            tlpPrincipal.Controls.Add(pnlCampos, 0, 2);
            tlpPrincipal.Controls.Add(pnlBotones, 0, 3);
            tlpPrincipal.Controls.Add(lblMensaje, 0, 4);
            tlpPrincipal.Controls.Add(dgvDatos, 0, 6);
            tlpPrincipal.Controls.Add(lblResumen, 0, 7);
            Controls.Add(tlpPrincipal);
        }

        protected Panel AgregarCampo(string texto, Control control, int ancho = 210)
        {
            Panel panel = new Panel
            {
                BackColor = Color.White,
                Height = 68,
                Margin = new Padding(0, 0, 12, 6),
                Width = ancho
            };

            Label etiqueta = new Label
            {
                AutoSize = false,
                Dock = DockStyle.Top,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.Black,
                Height = 25,
                Text = texto,
                TextAlign = ContentAlignment.MiddleLeft
            };

            control.Dock = DockStyle.Bottom;
            control.Font = new Font("Segoe UI", 10F);
            control.Height = 35;
            panel.Controls.Add(control);
            panel.Controls.Add(etiqueta);
            pnlCampos.Controls.Add(panel);
            return panel;
        }

        protected Button AgregarBoton(
            string texto,
            EventHandler evento,
            bool principal = true,
            int ancho = 118)
        {
            Button boton = new Button
            {
                BackColor = principal ? Color.Black : Color.White,
                Cursor = Cursors.Hand,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                ForeColor = principal ? Color.White : Color.Black,
                Height = 45,
                Margin = new Padding(0, 0, 10, 0),
                Text = texto,
                UseVisualStyleBackColor = false,
                Width = ancho
            };
            boton.FlatAppearance.BorderColor = Color.Black;
            boton.FlatAppearance.BorderSize = principal ? 0 : 1;
            boton.Paint += (sender, e) => DibujarBotonDeshabilitado(boton, e, principal);
            boton.Click += evento;
            pnlBotones.Controls.Add(boton);
            return boton;
        }

        private static void DibujarBotonDeshabilitado(
            Button boton,
            PaintEventArgs e,
            bool principal)
        {
            if (boton.Enabled)
            {
                return;
            }

            Color colorFondo = principal ? Color.Black : Color.White;
            Color colorTexto = principal ? Color.White : Color.Black;
            using SolidBrush fondo = new SolidBrush(colorFondo);
            e.Graphics.FillRectangle(fondo, boton.ClientRectangle);

            if (!principal)
            {
                e.Graphics.DrawRectangle(
                    Pens.Black,
                    0,
                    0,
                    boton.ClientSize.Width - 1,
                    boton.ClientSize.Height - 1);
            }

            TextRenderer.DrawText(
                e.Graphics,
                boton.Text,
                boton.Font,
                boton.ClientRectangle,
                colorTexto,
                TextFormatFlags.HorizontalCenter
                | TextFormatFlags.VerticalCenter
                | TextFormatFlags.SingleLine);
        }

        protected static ComboBox CrearCombo()
        {
            return new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                FormattingEnabled = true,
                IntegralHeight = false,
                MaxDropDownItems = 12
            };
        }

        protected static DateTimePicker CrearFecha()
        {
            return new DateTimePicker
            {
                Format = DateTimePickerFormat.Short
            };
        }

        protected static NumericUpDown CrearNumero(
            decimal maximo,
            int decimales = 0,
            decimal minimo = 0)
        {
            return new NumericUpDown
            {
                DecimalPlaces = decimales,
                Maximum = maximo,
                Minimum = minimo,
                ThousandsSeparator = decimales > 0
            };
        }

        protected static TextBox CrearTexto(int limite)
        {
            return new TextBox
            {
                MaxLength = limite
            };
        }

        protected static DataGridView CrearTabla()
        {
            DataGridView tabla = new DataGridView
            {
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                AutoGenerateColumns = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.Fixed3D,
                Dock = DockStyle.Fill,
                Margin = new Padding(0),
                MultiSelect = false,
                ReadOnly = true,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            return tabla;
        }

        protected void MostrarExcepcion(Exception excepcion)
        {
            if (excepcion is InvalidOperationException)
            {
                lblMensaje.Text = excepcion.Message;
                return;
            }

            if (excepcion is NpgsqlException)
            {
                lblMensaje.Text = "No fue posible realizar la operación en la base de datos.";
                return;
            }

            lblMensaje.Text = "Ocurrió un error al realizar la operación.";
        }

        protected void MostrarConfirmacion(string mensaje)
        {
            lblMensaje.ForeColor = Color.FromArgb(35, 110, 55);
            lblMensaje.Text = mensaje;
        }

        protected void LimpiarMensaje()
        {
            lblMensaje.ForeColor = Color.Firebrick;
            lblMensaje.Text = string.Empty;
        }

        protected static void ConfigurarColumnasMoneda(DataGridView tabla, params string[] nombres)
        {
            foreach (string nombre in nombres)
            {
                if (tabla.Columns.Contains(nombre))
                {
                    tabla.Columns[nombre]!.DefaultCellStyle.Format = "N2";
                    tabla.Columns[nombre]!.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }
        }

        protected static void ConfigurarColumnasFecha(DataGridView tabla, params string[] nombres)
        {
            foreach (string nombre in nombres)
            {
                if (tabla.Columns.Contains(nombre))
                {
                    tabla.Columns[nombre]!.DefaultCellStyle.Format = "dd/MM/yyyy";
                }
            }
        }
    }
}

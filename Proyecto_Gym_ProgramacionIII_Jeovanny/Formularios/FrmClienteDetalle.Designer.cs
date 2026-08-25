namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    partial class FrmClienteDetalle
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            tlpPrincipal = new TableLayoutPanel();
            pnlFotografia = new Panel();
            tlpFotografia = new TableLayoutPanel();
            lblFoto = new Label();
            picFoto = new PictureBox();
            tlpBotonesFoto = new TableLayoutPanel();
            btnSeleccionarFoto = new Button();
            btnQuitarFoto = new Button();
            chkEstado = new CheckBox();
            pnlDatos = new Panel();
            tlpDatos = new TableLayoutPanel();
            lblSeccion = new Label();
            lblTitulo = new Label();
            pnlLineaTitulo = new Panel();
            tlpCampos = new TableLayoutPanel();
            lblNombre = new Label();
            lblApellido = new Label();
            txtNombre = new TextBox();
            txtApellido = new TextBox();
            lblCedula = new Label();
            lblTelefono = new Label();
            txtCedula = new MaskedTextBox();
            txtTelefono = new MaskedTextBox();
            lblCorreo = new Label();
            lblDireccion = new Label();
            txtCorreo = new TextBox();
            txtDireccion = new TextBox();
            lblFechaNacimiento = new Label();
            lblSexo = new Label();
            dtpFechaNacimiento = new DateTimePicker();
            cmbSexo = new ComboBox();
            lblMensaje = new Label();
            flpAcciones = new FlowLayoutPanel();
            btnCancelar = new Button();
            btnGuardar = new Button();
            tlpPrincipal.SuspendLayout();
            pnlFotografia.SuspendLayout();
            tlpFotografia.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picFoto).BeginInit();
            tlpBotonesFoto.SuspendLayout();
            pnlDatos.SuspendLayout();
            tlpDatos.SuspendLayout();
            tlpCampos.SuspendLayout();
            flpAcciones.SuspendLayout();
            SuspendLayout();
            // 
            // tlpPrincipal
            // 
            tlpPrincipal.BackColor = Color.White;
            tlpPrincipal.ColumnCount = 2;
            tlpPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 310F));
            tlpPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpPrincipal.Controls.Add(pnlFotografia, 0, 0);
            tlpPrincipal.Controls.Add(pnlDatos, 1, 0);
            tlpPrincipal.Dock = DockStyle.Fill;
            tlpPrincipal.Location = new Point(0, 0);
            tlpPrincipal.Margin = new Padding(0);
            tlpPrincipal.Name = "tlpPrincipal";
            tlpPrincipal.RowCount = 1;
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpPrincipal.Size = new Size(920, 610);
            tlpPrincipal.TabIndex = 0;
            // 
            // pnlFotografia
            // 
            pnlFotografia.BackColor = Color.Black;
            pnlFotografia.Controls.Add(tlpFotografia);
            pnlFotografia.Dock = DockStyle.Fill;
            pnlFotografia.Location = new Point(0, 0);
            pnlFotografia.Margin = new Padding(0);
            pnlFotografia.Name = "pnlFotografia";
            pnlFotografia.Padding = new Padding(34, 28, 34, 28);
            pnlFotografia.Size = new Size(310, 610);
            pnlFotografia.TabIndex = 1;
            // 
            // tlpFotografia
            // 
            tlpFotografia.BackColor = Color.Black;
            tlpFotografia.ColumnCount = 1;
            tlpFotografia.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpFotografia.Controls.Add(lblFoto, 0, 0);
            tlpFotografia.Controls.Add(picFoto, 0, 2);
            tlpFotografia.Controls.Add(tlpBotonesFoto, 0, 4);
            tlpFotografia.Controls.Add(chkEstado, 0, 6);
            tlpFotografia.Dock = DockStyle.Fill;
            tlpFotografia.Location = new Point(34, 28);
            tlpFotografia.Margin = new Padding(0);
            tlpFotografia.Name = "tlpFotografia";
            tlpFotografia.RowCount = 7;
            tlpFotografia.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tlpFotografia.RowStyles.Add(new RowStyle(SizeType.Absolute, 18F));
            tlpFotografia.RowStyles.Add(new RowStyle(SizeType.Absolute, 270F));
            tlpFotografia.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlpFotografia.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            tlpFotografia.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpFotografia.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            tlpFotografia.Size = new Size(242, 554);
            tlpFotografia.TabIndex = 0;
            // 
            // lblFoto
            // 
            lblFoto.Dock = DockStyle.Fill;
            lblFoto.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblFoto.ForeColor = Color.White;
            lblFoto.Location = new Point(0, 0);
            lblFoto.Margin = new Padding(0);
            lblFoto.Name = "lblFoto";
            lblFoto.Size = new Size(242, 40);
            lblFoto.TabIndex = 0;
            lblFoto.Text = "FOTOGRAFÍA";
            lblFoto.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // picFoto
            // 
            picFoto.BackColor = Color.Black;
            picFoto.Dock = DockStyle.Fill;
            picFoto.Location = new Point(0, 58);
            picFoto.Margin = new Padding(0);
            picFoto.Name = "picFoto";
            picFoto.Size = new Size(242, 270);
            picFoto.SizeMode = PictureBoxSizeMode.Zoom;
            picFoto.TabIndex = 1;
            picFoto.TabStop = false;
            // 
            // tlpBotonesFoto
            // 
            tlpBotonesFoto.ColumnCount = 2;
            tlpBotonesFoto.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpBotonesFoto.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpBotonesFoto.Controls.Add(btnSeleccionarFoto, 0, 0);
            tlpBotonesFoto.Controls.Add(btnQuitarFoto, 1, 0);
            tlpBotonesFoto.Dock = DockStyle.Fill;
            tlpBotonesFoto.Location = new Point(0, 348);
            tlpBotonesFoto.Margin = new Padding(0);
            tlpBotonesFoto.Name = "tlpBotonesFoto";
            tlpBotonesFoto.RowCount = 1;
            tlpBotonesFoto.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpBotonesFoto.Size = new Size(242, 44);
            tlpBotonesFoto.TabIndex = 2;
            // 
            // btnSeleccionarFoto
            // 
            btnSeleccionarFoto.BackColor = Color.FromArgb(124, 142, 163);
            btnSeleccionarFoto.Cursor = Cursors.Hand;
            btnSeleccionarFoto.Dock = DockStyle.Fill;
            btnSeleccionarFoto.FlatAppearance.BorderSize = 0;
            btnSeleccionarFoto.FlatStyle = FlatStyle.Flat;
            btnSeleccionarFoto.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            btnSeleccionarFoto.ForeColor = Color.White;
            btnSeleccionarFoto.Location = new Point(0, 0);
            btnSeleccionarFoto.Margin = new Padding(0, 0, 4, 0);
            btnSeleccionarFoto.Name = "btnSeleccionarFoto";
            btnSeleccionarFoto.Size = new Size(117, 44);
            btnSeleccionarFoto.TabIndex = 0;
            btnSeleccionarFoto.Text = "SELECCIONAR FOTO";
            btnSeleccionarFoto.UseVisualStyleBackColor = false;
            btnSeleccionarFoto.Click += btnSeleccionarFoto_Click;
            // 
            // btnQuitarFoto
            // 
            btnQuitarFoto.BackColor = Color.White;
            btnQuitarFoto.Cursor = Cursors.Hand;
            btnQuitarFoto.Dock = DockStyle.Fill;
            btnQuitarFoto.FlatAppearance.BorderColor = Color.Black;
            btnQuitarFoto.FlatStyle = FlatStyle.Flat;
            btnQuitarFoto.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            btnQuitarFoto.ForeColor = Color.Black;
            btnQuitarFoto.Location = new Point(125, 0);
            btnQuitarFoto.Margin = new Padding(4, 0, 0, 0);
            btnQuitarFoto.Name = "btnQuitarFoto";
            btnQuitarFoto.Size = new Size(117, 44);
            btnQuitarFoto.TabIndex = 1;
            btnQuitarFoto.Text = "QUITAR FOTO";
            btnQuitarFoto.UseVisualStyleBackColor = false;
            btnQuitarFoto.Click += btnQuitarFoto_Click;
            // 
            // chkEstado
            // 
            chkEstado.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            chkEstado.AutoSize = true;
            chkEstado.BackColor = Color.Black;
            chkEstado.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            chkEstado.ForeColor = Color.White;
            chkEstado.Location = new Point(0, 531);
            chkEstado.Margin = new Padding(0);
            chkEstado.Name = "chkEstado";
            chkEstado.Size = new Size(119, 23);
            chkEstado.TabIndex = 2;
            chkEstado.Text = "Cliente activo";
            chkEstado.UseVisualStyleBackColor = false;
            // 
            // pnlDatos
            // 
            pnlDatos.BackColor = Color.White;
            pnlDatos.Controls.Add(tlpDatos);
            pnlDatos.Dock = DockStyle.Fill;
            pnlDatos.Location = new Point(310, 0);
            pnlDatos.Margin = new Padding(0);
            pnlDatos.Name = "pnlDatos";
            pnlDatos.Padding = new Padding(38, 28, 38, 24);
            pnlDatos.Size = new Size(610, 610);
            pnlDatos.TabIndex = 0;
            // 
            // tlpDatos
            // 
            tlpDatos.BackColor = Color.White;
            tlpDatos.ColumnCount = 1;
            tlpDatos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpDatos.Controls.Add(lblSeccion, 0, 0);
            tlpDatos.Controls.Add(lblTitulo, 0, 1);
            tlpDatos.Controls.Add(pnlLineaTitulo, 0, 2);
            tlpDatos.Controls.Add(tlpCampos, 0, 4);
            tlpDatos.Controls.Add(lblMensaje, 0, 5);
            tlpDatos.Controls.Add(flpAcciones, 0, 6);
            tlpDatos.Dock = DockStyle.Fill;
            tlpDatos.Location = new Point(38, 28);
            tlpDatos.Margin = new Padding(0);
            tlpDatos.Name = "tlpDatos";
            tlpDatos.RowCount = 7;
            tlpDatos.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
            tlpDatos.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tlpDatos.RowStyles.Add(new RowStyle(SizeType.Absolute, 3F));
            tlpDatos.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
            tlpDatos.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpDatos.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            tlpDatos.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            tlpDatos.Size = new Size(534, 558);
            tlpDatos.TabIndex = 0;
            // 
            // lblSeccion
            // 
            lblSeccion.Dock = DockStyle.Fill;
            lblSeccion.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblSeccion.ForeColor = Color.FromArgb(124, 142, 163);
            lblSeccion.Location = new Point(0, 0);
            lblSeccion.Margin = new Padding(0);
            lblSeccion.Name = "lblSeccion";
            lblSeccion.Size = new Size(534, 22);
            lblSeccion.TabIndex = 0;
            lblSeccion.Text = "DATOS DEL CLIENTE";
            lblSeccion.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTitulo
            // 
            lblTitulo.Dock = DockStyle.Fill;
            lblTitulo.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.Black;
            lblTitulo.Location = new Point(0, 22);
            lblTitulo.Margin = new Padding(0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(534, 50);
            lblTitulo.TabIndex = 1;
            lblTitulo.Text = "Nuevo cliente";
            lblTitulo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlLineaTitulo
            // 
            pnlLineaTitulo.Anchor = AnchorStyles.Left;
            pnlLineaTitulo.BackColor = Color.FromArgb(124, 142, 163);
            pnlLineaTitulo.Location = new Point(0, 72);
            pnlLineaTitulo.Margin = new Padding(0);
            pnlLineaTitulo.Name = "pnlLineaTitulo";
            pnlLineaTitulo.Size = new Size(86, 3);
            pnlLineaTitulo.TabIndex = 2;
            // 
            // tlpCampos
            // 
            tlpCampos.ColumnCount = 2;
            tlpCampos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpCampos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpCampos.Controls.Add(lblNombre, 0, 0);
            tlpCampos.Controls.Add(lblApellido, 1, 0);
            tlpCampos.Controls.Add(txtNombre, 0, 1);
            tlpCampos.Controls.Add(txtApellido, 1, 1);
            tlpCampos.Controls.Add(lblCedula, 0, 3);
            tlpCampos.Controls.Add(lblTelefono, 1, 3);
            tlpCampos.Controls.Add(txtCedula, 0, 4);
            tlpCampos.Controls.Add(txtTelefono, 1, 4);
            tlpCampos.Controls.Add(lblCorreo, 0, 6);
            tlpCampos.Controls.Add(lblDireccion, 1, 6);
            tlpCampos.Controls.Add(txtCorreo, 0, 7);
            tlpCampos.Controls.Add(txtDireccion, 1, 7);
            tlpCampos.Controls.Add(lblFechaNacimiento, 0, 9);
            tlpCampos.Controls.Add(lblSexo, 1, 9);
            tlpCampos.Controls.Add(dtpFechaNacimiento, 0, 10);
            tlpCampos.Controls.Add(cmbSexo, 1, 10);
            tlpCampos.Dock = DockStyle.Fill;
            tlpCampos.Location = new Point(0, 97);
            tlpCampos.Margin = new Padding(0);
            tlpCampos.Name = "tlpCampos";
            tlpCampos.RowCount = 11;
            tlpCampos.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
            tlpCampos.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            tlpCampos.RowStyles.Add(new RowStyle(SizeType.Absolute, 14F));
            tlpCampos.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
            tlpCampos.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            tlpCampos.RowStyles.Add(new RowStyle(SizeType.Absolute, 14F));
            tlpCampos.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
            tlpCampos.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            tlpCampos.RowStyles.Add(new RowStyle(SizeType.Absolute, 14F));
            tlpCampos.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
            tlpCampos.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpCampos.Size = new Size(534, 365);
            tlpCampos.TabIndex = 0;
            // 
            // lblNombre
            // 
            lblNombre.Dock = DockStyle.Fill;
            lblNombre.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblNombre.ForeColor = Color.Black;
            lblNombre.Location = new Point(0, 0);
            lblNombre.Margin = new Padding(0, 0, 9, 0);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(258, 22);
            lblNombre.TabIndex = 0;
            lblNombre.Text = "Nombre";
            lblNombre.TextAlign = ContentAlignment.BottomLeft;
            // 
            // lblApellido
            // 
            lblApellido.Dock = DockStyle.Fill;
            lblApellido.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblApellido.ForeColor = Color.Black;
            lblApellido.Location = new Point(276, 0);
            lblApellido.Margin = new Padding(9, 0, 0, 0);
            lblApellido.Name = "lblApellido";
            lblApellido.Size = new Size(258, 22);
            lblApellido.TabIndex = 1;
            lblApellido.Text = "Apellido";
            lblApellido.TextAlign = ContentAlignment.BottomLeft;
            // 
            // txtNombre
            // 
            txtNombre.BorderStyle = BorderStyle.FixedSingle;
            txtNombre.Dock = DockStyle.Fill;
            txtNombre.Font = new Font("Segoe UI", 10F);
            txtNombre.Location = new Point(0, 22);
            txtNombre.Margin = new Padding(0, 0, 9, 0);
            txtNombre.MaxLength = 60;
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(258, 25);
            txtNombre.TabIndex = 0;
            txtNombre.KeyPress += txtNombreApellido_KeyPress;
            // 
            // txtApellido
            // 
            txtApellido.BorderStyle = BorderStyle.FixedSingle;
            txtApellido.Dock = DockStyle.Fill;
            txtApellido.Font = new Font("Segoe UI", 10F);
            txtApellido.Location = new Point(276, 22);
            txtApellido.Margin = new Padding(9, 0, 0, 0);
            txtApellido.MaxLength = 60;
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new Size(258, 25);
            txtApellido.TabIndex = 1;
            txtApellido.KeyPress += txtNombreApellido_KeyPress;
            // 
            // lblCedula
            // 
            lblCedula.Dock = DockStyle.Fill;
            lblCedula.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCedula.ForeColor = Color.Black;
            lblCedula.Location = new Point(0, 68);
            lblCedula.Margin = new Padding(0, 0, 9, 0);
            lblCedula.Name = "lblCedula";
            lblCedula.Size = new Size(258, 22);
            lblCedula.TabIndex = 4;
            lblCedula.Text = "Cédula";
            lblCedula.TextAlign = ContentAlignment.BottomLeft;
            // 
            // lblTelefono
            // 
            lblTelefono.Dock = DockStyle.Fill;
            lblTelefono.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTelefono.ForeColor = Color.Black;
            lblTelefono.Location = new Point(276, 68);
            lblTelefono.Margin = new Padding(9, 0, 0, 0);
            lblTelefono.Name = "lblTelefono";
            lblTelefono.Size = new Size(258, 22);
            lblTelefono.TabIndex = 5;
            lblTelefono.Text = "Teléfono";
            lblTelefono.TextAlign = ContentAlignment.BottomLeft;
            // 
            // txtCedula
            // 
            txtCedula.BorderStyle = BorderStyle.FixedSingle;
            txtCedula.Dock = DockStyle.Fill;
            txtCedula.Font = new Font("Segoe UI", 10F);
            txtCedula.HidePromptOnLeave = true;
            txtCedula.Location = new Point(0, 90);
            txtCedula.Margin = new Padding(0, 0, 9, 0);
            txtCedula.Mask = "000-0000000-0";
            txtCedula.Name = "txtCedula";
            txtCedula.PromptChar = ' ';
            txtCedula.ResetOnSpace = false;
            txtCedula.Size = new Size(258, 25);
            txtCedula.TabIndex = 2;
            txtCedula.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            txtCedula.Enter += txtCedula_Enter;
            txtCedula.Leave += txtCedula_Leave;
            // 
            // txtTelefono
            // 
            txtTelefono.BorderStyle = BorderStyle.FixedSingle;
            txtTelefono.Dock = DockStyle.Fill;
            txtTelefono.Font = new Font("Segoe UI", 10F);
            txtTelefono.HidePromptOnLeave = true;
            txtTelefono.Location = new Point(276, 90);
            txtTelefono.Margin = new Padding(9, 0, 0, 0);
            txtTelefono.Mask = "(000) 000-0000";
            txtTelefono.Name = "txtTelefono";
            txtTelefono.PromptChar = ' ';
            txtTelefono.ResetOnSpace = false;
            txtTelefono.Size = new Size(258, 25);
            txtTelefono.TabIndex = 3;
            txtTelefono.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            txtTelefono.Enter += txtTelefono_Enter;
            txtTelefono.Leave += txtTelefono_Leave;
            // 
            // lblCorreo
            // 
            lblCorreo.Dock = DockStyle.Fill;
            lblCorreo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCorreo.ForeColor = Color.Black;
            lblCorreo.Location = new Point(0, 136);
            lblCorreo.Margin = new Padding(0, 0, 9, 0);
            lblCorreo.Name = "lblCorreo";
            lblCorreo.Size = new Size(258, 22);
            lblCorreo.TabIndex = 8;
            lblCorreo.Text = "Correo (opcional)";
            lblCorreo.TextAlign = ContentAlignment.BottomLeft;
            // 
            // lblDireccion
            // 
            lblDireccion.Dock = DockStyle.Fill;
            lblDireccion.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDireccion.ForeColor = Color.Black;
            lblDireccion.Location = new Point(276, 136);
            lblDireccion.Margin = new Padding(9, 0, 0, 0);
            lblDireccion.Name = "lblDireccion";
            lblDireccion.Size = new Size(258, 22);
            lblDireccion.TabIndex = 9;
            lblDireccion.Text = "Dirección";
            lblDireccion.TextAlign = ContentAlignment.BottomLeft;
            // 
            // txtCorreo
            // 
            txtCorreo.BorderStyle = BorderStyle.FixedSingle;
            txtCorreo.Dock = DockStyle.Fill;
            txtCorreo.Font = new Font("Segoe UI", 10F);
            txtCorreo.Location = new Point(0, 158);
            txtCorreo.Margin = new Padding(0, 0, 9, 0);
            txtCorreo.MaxLength = 120;
            txtCorreo.Name = "txtCorreo";
            txtCorreo.Size = new Size(258, 25);
            txtCorreo.TabIndex = 4;
            // 
            // txtDireccion
            // 
            txtDireccion.BorderStyle = BorderStyle.FixedSingle;
            txtDireccion.Dock = DockStyle.Fill;
            txtDireccion.Font = new Font("Segoe UI", 10F);
            txtDireccion.Location = new Point(276, 158);
            txtDireccion.Margin = new Padding(9, 0, 0, 0);
            txtDireccion.MaxLength = 200;
            txtDireccion.Name = "txtDireccion";
            txtDireccion.Size = new Size(258, 25);
            txtDireccion.TabIndex = 5;
            // 
            // lblFechaNacimiento
            // 
            lblFechaNacimiento.Dock = DockStyle.Fill;
            lblFechaNacimiento.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblFechaNacimiento.ForeColor = Color.Black;
            lblFechaNacimiento.Location = new Point(0, 204);
            lblFechaNacimiento.Margin = new Padding(0, 0, 9, 0);
            lblFechaNacimiento.Name = "lblFechaNacimiento";
            lblFechaNacimiento.Size = new Size(258, 22);
            lblFechaNacimiento.TabIndex = 12;
            lblFechaNacimiento.Text = "Fecha de nacimiento";
            lblFechaNacimiento.TextAlign = ContentAlignment.BottomLeft;
            // 
            // lblSexo
            // 
            lblSexo.Dock = DockStyle.Fill;
            lblSexo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblSexo.ForeColor = Color.Black;
            lblSexo.Location = new Point(276, 204);
            lblSexo.Margin = new Padding(9, 0, 0, 0);
            lblSexo.Name = "lblSexo";
            lblSexo.Size = new Size(258, 22);
            lblSexo.TabIndex = 13;
            lblSexo.Text = "Sexo";
            lblSexo.TextAlign = ContentAlignment.BottomLeft;
            // 
            // dtpFechaNacimiento
            // 
            dtpFechaNacimiento.CalendarForeColor = Color.Black;
            dtpFechaNacimiento.CustomFormat = "dd/MM/yyyy";
            dtpFechaNacimiento.Dock = DockStyle.Top;
            dtpFechaNacimiento.Font = new Font("Segoe UI", 10F);
            dtpFechaNacimiento.Format = DateTimePickerFormat.Custom;
            dtpFechaNacimiento.Location = new Point(0, 226);
            dtpFechaNacimiento.Margin = new Padding(0, 0, 9, 0);
            dtpFechaNacimiento.Name = "dtpFechaNacimiento";
            dtpFechaNacimiento.Size = new Size(258, 25);
            dtpFechaNacimiento.TabIndex = 6;
            // 
            // cmbSexo
            // 
            cmbSexo.Dock = DockStyle.Top;
            cmbSexo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSexo.Font = new Font("Segoe UI", 10F);
            cmbSexo.ForeColor = Color.Black;
            cmbSexo.FormattingEnabled = true;
            cmbSexo.Items.AddRange(new object[] { "FEMENINO", "MASCULINO" });
            cmbSexo.Location = new Point(276, 226);
            cmbSexo.Margin = new Padding(9, 0, 0, 0);
            cmbSexo.Name = "cmbSexo";
            cmbSexo.Size = new Size(258, 25);
            cmbSexo.TabIndex = 7;
            // 
            // lblMensaje
            // 
            lblMensaje.Dock = DockStyle.Fill;
            lblMensaje.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblMensaje.ForeColor = Color.FromArgb(124, 142, 163);
            lblMensaje.Location = new Point(0, 462);
            lblMensaje.Margin = new Padding(0);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(534, 48);
            lblMensaje.TabIndex = 4;
            lblMensaje.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // flpAcciones
            // 
            flpAcciones.Controls.Add(btnCancelar);
            flpAcciones.Controls.Add(btnGuardar);
            flpAcciones.Dock = DockStyle.Fill;
            flpAcciones.FlowDirection = FlowDirection.RightToLeft;
            flpAcciones.Location = new Point(0, 510);
            flpAcciones.Margin = new Padding(0);
            flpAcciones.Name = "flpAcciones";
            flpAcciones.Padding = new Padding(0, 4, 0, 0);
            flpAcciones.Size = new Size(534, 48);
            flpAcciones.TabIndex = 1;
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.White;
            btnCancelar.Cursor = Cursors.Hand;
            btnCancelar.DialogResult = DialogResult.Cancel;
            btnCancelar.FlatAppearance.BorderColor = Color.Black;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCancelar.ForeColor = Color.Black;
            btnCancelar.Location = new Point(414, 4);
            btnCancelar.Margin = new Padding(8, 0, 0, 0);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(120, 40);
            btnCancelar.TabIndex = 1;
            btnCancelar.Text = "CANCELAR";
            btnCancelar.UseVisualStyleBackColor = false;
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.FromArgb(124, 142, 163);
            btnGuardar.Cursor = Cursors.Hand;
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Location = new Point(286, 4);
            btnGuardar.Margin = new Padding(0);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(120, 40);
            btnGuardar.TabIndex = 0;
            btnGuardar.Text = "GUARDAR";
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // FrmClienteDetalle
            // 
            AcceptButton = btnGuardar;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            CancelButton = btnCancelar;
            ClientSize = new Size(920, 610);
            Controls.Add(tlpPrincipal);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmClienteDetalle";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Cliente";
            FormClosed += FrmClienteDetalle_FormClosed;
            tlpPrincipal.ResumeLayout(false);
            pnlFotografia.ResumeLayout(false);
            tlpFotografia.ResumeLayout(false);
            tlpFotografia.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picFoto).EndInit();
            tlpBotonesFoto.ResumeLayout(false);
            pnlDatos.ResumeLayout(false);
            tlpDatos.ResumeLayout(false);
            tlpCampos.ResumeLayout(false);
            tlpCampos.PerformLayout();
            flpAcciones.ResumeLayout(false);
            ResumeLayout(false);
        }

        private TableLayoutPanel tlpPrincipal;
        private Panel pnlFotografia;
        private TableLayoutPanel tlpFotografia;
        private Label lblFoto;
        private PictureBox picFoto;
        private TableLayoutPanel tlpBotonesFoto;
        private Button btnSeleccionarFoto;
        private Button btnQuitarFoto;
        private CheckBox chkEstado;
        private Panel pnlDatos;
        private TableLayoutPanel tlpDatos;
        private Label lblSeccion;
        private Label lblTitulo;
        private Panel pnlLineaTitulo;
        private TableLayoutPanel tlpCampos;
        private Label lblNombre;
        private TextBox txtNombre;
        private Label lblApellido;
        private TextBox txtApellido;
        private Label lblCedula;
        private MaskedTextBox txtCedula;
        private Label lblTelefono;
        private MaskedTextBox txtTelefono;
        private Label lblCorreo;
        private TextBox txtCorreo;
        private Label lblDireccion;
        private TextBox txtDireccion;
        private Label lblFechaNacimiento;
        private DateTimePicker dtpFechaNacimiento;
        private Label lblSexo;
        private ComboBox cmbSexo;
        private Label lblMensaje;
        private FlowLayoutPanel flpAcciones;
        private Button btnGuardar;
        private Button btnCancelar;
    }
}

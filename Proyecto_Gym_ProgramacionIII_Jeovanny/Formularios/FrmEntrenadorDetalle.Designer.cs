namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    partial class FrmEntrenadorDetalle
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitulo;
        private Label lblNombre;
        private Label lblApellido;
        private Label lblCedula;
        private Label lblTelefono;
        private Label lblCorreo;
        private Label lblEspecialidad;
        private Label lblFechaContratacion;
        private TextBox txtNombre;
        private TextBox txtApellido;
        private MaskedTextBox txtCedula;
        private MaskedTextBox txtTelefono;
        private TextBox txtCorreo;
        private TextBox txtEspecialidad;
        private DateTimePicker dtpFechaContratacion;
        private CheckBox chkEstado;
        private Label lblMensaje;
        private Button btnGuardar;
        private Button btnCancelar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitulo = new Label();
            lblNombre = new Label();
            lblApellido = new Label();
            lblCedula = new Label();
            lblTelefono = new Label();
            lblCorreo = new Label();
            lblEspecialidad = new Label();
            lblFechaContratacion = new Label();
            txtNombre = new TextBox();
            txtApellido = new TextBox();
            txtCedula = new MaskedTextBox();
            txtTelefono = new MaskedTextBox();
            txtCorreo = new TextBox();
            txtEspecialidad = new TextBox();
            dtpFechaContratacion = new DateTimePicker();
            chkEstado = new CheckBox();
            lblMensaje = new Label();
            btnGuardar = new Button();
            btnCancelar = new Button();

            Text = "Nuevo entrenador";
            BackColor = Color.White;
            ClientSize = new Size(570, 420);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmEntrenadorDetalle";
            StartPosition = FormStartPosition.CenterParent;

            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitulo.Location = new Point(25, 18);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Text = "Nuevo entrenador";
            lblNombre.AutoSize = true;
            lblNombre.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblNombre.Location = new Point(25, 68);
            lblNombre.Name = "lblNombre";
            lblNombre.Text = "Nombre";
            lblApellido.AutoSize = true;
            lblApellido.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblApellido.Location = new Point(295, 68);
            lblApellido.Name = "lblApellido";
            lblApellido.Text = "Apellido";
            lblCedula.AutoSize = true;
            lblCedula.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCedula.Location = new Point(25, 130);
            lblCedula.Name = "lblCedula";
            lblCedula.Text = "Cédula";
            lblTelefono.AutoSize = true;
            lblTelefono.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTelefono.Location = new Point(295, 130);
            lblTelefono.Name = "lblTelefono";
            lblTelefono.Text = "Teléfono";
            lblCorreo.AutoSize = true;
            lblCorreo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCorreo.Location = new Point(25, 192);
            lblCorreo.Name = "lblCorreo";
            lblCorreo.Text = "Correo";
            lblEspecialidad.AutoSize = true;
            lblEspecialidad.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblEspecialidad.Location = new Point(295, 192);
            lblEspecialidad.Name = "lblEspecialidad";
            lblEspecialidad.Text = "Especialidad";
            lblFechaContratacion.AutoSize = true;
            lblFechaContratacion.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblFechaContratacion.Location = new Point(25, 254);
            lblFechaContratacion.Name = "lblFechaContratacion";
            lblFechaContratacion.Text = "Fecha de contratación";

            txtNombre.Font = new Font("Segoe UI", 10F);
            txtNombre.Location = new Point(25, 90);
            txtNombre.MaxLength = 60;
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(250, 25);
            txtApellido.Font = new Font("Segoe UI", 10F);
            txtApellido.Location = new Point(295, 90);
            txtApellido.MaxLength = 60;
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new Size(250, 25);
            txtNombre.KeyPress += txtNombreApellido_KeyPress;
            txtApellido.KeyPress += txtNombreApellido_KeyPress;

            txtCedula.Font = new Font("Segoe UI", 10F);
            txtCedula.HidePromptOnLeave = true;
            txtCedula.Location = new Point(25, 152);
            txtCedula.Mask = "000-0000000-0";
            txtCedula.Name = "txtCedula";
            txtCedula.PromptChar = ' ';
            txtCedula.ResetOnSpace = false;
            txtCedula.Size = new Size(250, 25);
            txtCedula.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            txtCedula.Enter += txtCedula_Enter;
            txtCedula.Leave += txtCedula_Leave;

            txtTelefono.Font = new Font("Segoe UI", 10F);
            txtTelefono.HidePromptOnLeave = true;
            txtTelefono.Location = new Point(295, 152);
            txtTelefono.Mask = "(000) 000-0000";
            txtTelefono.Name = "txtTelefono";
            txtTelefono.PromptChar = ' ';
            txtTelefono.ResetOnSpace = false;
            txtTelefono.Size = new Size(250, 25);
            txtTelefono.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            txtTelefono.Enter += txtTelefono_Enter;
            txtTelefono.Leave += txtTelefono_Leave;

            txtCorreo.Font = new Font("Segoe UI", 10F);
            txtCorreo.Location = new Point(25, 214);
            txtCorreo.MaxLength = 120;
            txtCorreo.Name = "txtCorreo";
            txtCorreo.Size = new Size(250, 25);
            txtEspecialidad.Font = new Font("Segoe UI", 10F);
            txtEspecialidad.Location = new Point(295, 214);
            txtEspecialidad.MaxLength = 100;
            txtEspecialidad.Name = "txtEspecialidad";
            txtEspecialidad.Size = new Size(250, 25);

            dtpFechaContratacion.CustomFormat = "dd/MM/yyyy";
            dtpFechaContratacion.Font = new Font("Segoe UI", 10F);
            dtpFechaContratacion.Format = DateTimePickerFormat.Custom;
            dtpFechaContratacion.Location = new Point(25, 276);
            dtpFechaContratacion.Name = "dtpFechaContratacion";
            dtpFechaContratacion.Size = new Size(250, 25);

            chkEstado.AutoSize = true;
            chkEstado.Checked = true;
            chkEstado.Font = new Font("Segoe UI", 10F);
            chkEstado.Location = new Point(295, 277);
            chkEstado.Name = "chkEstado";
            chkEstado.Text = "Activo";

            lblMensaje.AutoSize = false;
            lblMensaje.ForeColor = Color.Firebrick;
            lblMensaje.Location = new Point(25, 315);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(520, 24);

            btnGuardar.BackColor = Color.FromArgb(124, 142, 163);
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Location = new Point(295, 350);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(120, 38);
            btnGuardar.Text = "GUARDAR";
            btnGuardar.Click += btnGuardar_Click;

            btnCancelar.BackColor = Color.WhiteSmoke;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCancelar.ForeColor = Color.Black;
            btnCancelar.Location = new Point(425, 350);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(120, 38);
            btnCancelar.Text = "CANCELAR";
            btnCancelar.DialogResult = DialogResult.Cancel;

            Controls.Add(lblTitulo);
            Controls.Add(lblNombre);
            Controls.Add(lblApellido);
            Controls.Add(lblCedula);
            Controls.Add(lblTelefono);
            Controls.Add(lblCorreo);
            Controls.Add(lblEspecialidad);
            Controls.Add(lblFechaContratacion);
            Controls.Add(txtNombre);
            Controls.Add(txtApellido);
            Controls.Add(txtCedula);
            Controls.Add(txtTelefono);
            Controls.Add(txtCorreo);
            Controls.Add(txtEspecialidad);
            Controls.Add(dtpFechaContratacion);
            Controls.Add(chkEstado);
            Controls.Add(lblMensaje);
            Controls.Add(btnGuardar);
            Controls.Add(btnCancelar);
            AcceptButton = btnGuardar;
            CancelButton = btnCancelar;
        }
    }
}

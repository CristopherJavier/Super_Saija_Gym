namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    partial class FrmProveedorDetalle
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitulo;
        private Label lblNombre;
        private Label lblRncCedula;
        private Label lblTelefono;
        private Label lblCorreo;
        private Label lblDireccion;
        private TextBox txtNombre;
        private TextBox txtRncCedula;
        private MaskedTextBox txtTelefono;
        private TextBox txtCorreo;
        private TextBox txtDireccion;
        private CheckBox chkEstado;
        private Label lblMensaje;
        private Button btnGuardar;
        private Button btnCancelar;

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
            lblTitulo = new Label();
            lblNombre = new Label();
            lblRncCedula = new Label();
            lblTelefono = new Label();
            lblCorreo = new Label();
            lblDireccion = new Label();
            txtNombre = new TextBox();
            txtRncCedula = new TextBox();
            txtTelefono = new MaskedTextBox();
            txtCorreo = new TextBox();
            txtDireccion = new TextBox();
            chkEstado = new CheckBox();
            lblMensaje = new Label();
            btnGuardar = new Button();
            btnCancelar = new Button();
            SuspendLayout();
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitulo.Location = new Point(25, 18);
            lblTitulo.Text = "Nuevo proveedor";
            lblNombre.AutoSize = true;
            lblNombre.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblNombre.Location = new Point(25, 70);
            lblNombre.Text = "Nombre";
            lblRncCedula.AutoSize = true;
            lblRncCedula.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblRncCedula.Location = new Point(25, 130);
            lblRncCedula.Text = "RNC o cédula";
            lblTelefono.AutoSize = true;
            lblTelefono.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTelefono.Location = new Point(295, 130);
            lblTelefono.Text = "Teléfono";
            lblCorreo.AutoSize = true;
            lblCorreo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCorreo.Location = new Point(25, 192);
            lblCorreo.Text = "Correo";
            lblDireccion.AutoSize = true;
            lblDireccion.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDireccion.Location = new Point(25, 252);
            lblDireccion.Text = "Dirección";
            txtNombre.Font = new Font("Segoe UI", 10F);
            txtNombre.Location = new Point(25, 92);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(530, 25);
            txtNombre.MaxLength = 100;
            txtRncCedula.Font = new Font("Segoe UI", 10F);
            txtRncCedula.Location = new Point(25, 152);
            txtRncCedula.Name = "txtRncCedula";
            txtRncCedula.Size = new Size(250, 25);
            txtRncCedula.MaxLength = 20;
            txtRncCedula.KeyPress += txtRncCedula_KeyPress;
            txtTelefono.Font = new Font("Segoe UI", 10F);
            txtTelefono.Location = new Point(295, 152);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(260, 25);
            txtTelefono.Mask = "(000) 000-0000";
            txtTelefono.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            txtCorreo.Font = new Font("Segoe UI", 10F);
            txtCorreo.Location = new Point(25, 214);
            txtCorreo.Name = "txtCorreo";
            txtCorreo.Size = new Size(530, 25);
            txtCorreo.MaxLength = 120;
            txtDireccion.Font = new Font("Segoe UI", 10F);
            txtDireccion.Location = new Point(25, 274);
            txtDireccion.MaxLength = 200;
            txtDireccion.Multiline = true;
            txtDireccion.Name = "txtDireccion";
            txtDireccion.Size = new Size(530, 58);
            chkEstado.AutoSize = true;
            chkEstado.Checked = true;
            chkEstado.CheckState = CheckState.Checked;
            chkEstado.Font = new Font("Segoe UI", 10F);
            chkEstado.Location = new Point(25, 350);
            chkEstado.Name = "chkEstado";
            chkEstado.Text = "Activo";
            chkEstado.UseVisualStyleBackColor = true;
            lblMensaje.ForeColor = Color.Firebrick;
            lblMensaje.Location = new Point(25, 384);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(260, 40);
            btnGuardar.BackColor = Color.FromArgb(124, 142, 163);
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Location = new Point(315, 384);
            btnGuardar.Size = new Size(120, 38);
            btnGuardar.Text = "GUARDAR";
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Click += btnGuardar_Click;
            btnCancelar.BackColor = Color.WhiteSmoke;
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCancelar.ForeColor = Color.Black;
            btnCancelar.Location = new Point(445, 384);
            btnCancelar.Size = new Size(120, 38);
            btnCancelar.Text = "CANCELAR";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.DialogResult = DialogResult.Cancel;
            btnCancelar.Name = "btnCancelar";
            AcceptButton = btnGuardar;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            CancelButton = btnCancelar;
            ClientSize = new Size(580, 440);
            Controls.Add(lblTitulo);
            Controls.Add(lblNombre);
            Controls.Add(lblRncCedula);
            Controls.Add(lblTelefono);
            Controls.Add(lblCorreo);
            Controls.Add(lblDireccion);
            Controls.Add(txtNombre);
            Controls.Add(txtRncCedula);
            Controls.Add(txtTelefono);
            Controls.Add(txtCorreo);
            Controls.Add(txtDireccion);
            Controls.Add(chkEstado);
            Controls.Add(lblMensaje);
            Controls.Add(btnGuardar);
            Controls.Add(btnCancelar);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmProveedorDetalle";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Nuevo proveedor";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}

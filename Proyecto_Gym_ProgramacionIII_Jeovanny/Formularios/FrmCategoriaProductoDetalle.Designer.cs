namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    partial class FrmCategoriaProductoDetalle
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitulo;
        private Label lblNombre;
        private Label lblDescripcion;
        private TextBox txtNombre;
        private TextBox txtDescripcion;
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
            lblDescripcion = new Label();
            txtNombre = new TextBox();
            txtDescripcion = new TextBox();
            chkEstado = new CheckBox();
            lblMensaje = new Label();
            btnGuardar = new Button();
            btnCancelar = new Button();

            Text = "Nueva categoría";
            BackColor = Color.White;
            ClientSize = new Size(520, 335);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmCategoriaProductoDetalle";
            StartPosition = FormStartPosition.CenterParent;

            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitulo.Location = new Point(25, 18);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Text = "Nueva categoría";
            lblNombre.AutoSize = true;
            lblNombre.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblNombre.Location = new Point(25, 70);
            lblNombre.Name = "lblNombre";
            lblNombre.Text = "Nombre";
            lblDescripcion.AutoSize = true;
            lblDescripcion.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDescripcion.Location = new Point(25, 130);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Text = "Descripción";

            txtNombre.Font = new Font("Segoe UI", 10F);
            txtNombre.Location = new Point(25, 92);
            txtNombre.MaxLength = 80;
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(470, 25);

            txtDescripcion.Font = new Font("Segoe UI", 10F);
            txtDescripcion.Location = new Point(25, 152);
            txtDescripcion.MaxLength = 200;
            txtDescripcion.Multiline = true;
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(470, 65);

            chkEstado.AutoSize = true;
            chkEstado.Checked = true;
            chkEstado.Font = new Font("Segoe UI", 10F);
            chkEstado.Location = new Point(25, 235);
            chkEstado.Name = "chkEstado";
            chkEstado.Text = "Activo";

            lblMensaje.ForeColor = Color.Firebrick;
            lblMensaje.Location = new Point(25, 272);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(220, 40);

            btnGuardar.BackColor = Color.FromArgb(124, 142, 163);
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Location = new Point(265, 272);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(120, 38);
            btnGuardar.Text = "GUARDAR";
            btnGuardar.Click += btnGuardar_Click;

            btnCancelar.BackColor = Color.WhiteSmoke;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCancelar.ForeColor = Color.Black;
            btnCancelar.Location = new Point(395, 272);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(120, 38);
            btnCancelar.Text = "CANCELAR";
            btnCancelar.DialogResult = DialogResult.Cancel;

            Controls.Add(lblTitulo);
            Controls.Add(lblNombre);
            Controls.Add(lblDescripcion);
            Controls.Add(txtNombre);
            Controls.Add(txtDescripcion);
            Controls.Add(chkEstado);
            Controls.Add(lblMensaje);
            Controls.Add(btnGuardar);
            Controls.Add(btnCancelar);
            AcceptButton = btnGuardar;
            CancelButton = btnCancelar;
        }
    }
}

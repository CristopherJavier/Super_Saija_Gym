namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    partial class FrmTipoMembresiaDetalle
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitulo;
        private Label lblNombre;
        private Label lblDescripcion;
        private Label lblDuracionDias;
        private Label lblPrecio;
        private TextBox txtNombre;
        private TextBox txtDescripcion;
        private NumericUpDown nudDuracionDias;
        private NumericUpDown nudPrecio;
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
            lblDuracionDias = new Label();
            lblPrecio = new Label();
            txtNombre = new TextBox();
            txtDescripcion = new TextBox();
            nudDuracionDias = new NumericUpDown();
            nudPrecio = new NumericUpDown();
            chkEstado = new CheckBox();
            lblMensaje = new Label();
            btnGuardar = new Button();
            btnCancelar = new Button();

            Text = "Nuevo tipo de membresía";
            BackColor = Color.White;
            ClientSize = new Size(540, 390);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmTipoMembresiaDetalle";
            StartPosition = FormStartPosition.CenterParent;

            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitulo.Location = new Point(25, 18);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Text = "Nuevo tipo de membresía";
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
            lblDuracionDias.AutoSize = true;
            lblDuracionDias.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDuracionDias.Location = new Point(25, 222);
            lblDuracionDias.Name = "lblDuracionDias";
            lblDuracionDias.Text = "Duración en días";
            lblPrecio.AutoSize = true;
            lblPrecio.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPrecio.Location = new Point(285, 222);
            lblPrecio.Name = "lblPrecio";
            lblPrecio.Text = "Precio";

            txtNombre.Font = new Font("Segoe UI", 10F);
            txtNombre.Location = new Point(25, 92);
            txtNombre.MaxLength = 60;
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(490, 25);

            txtDescripcion.Font = new Font("Segoe UI", 10F);
            txtDescripcion.Location = new Point(25, 152);
            txtDescripcion.MaxLength = 200;
            txtDescripcion.Multiline = true;
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(490, 58);

            nudDuracionDias.Font = new Font("Segoe UI", 10F);
            nudDuracionDias.Location = new Point(25, 244);
            nudDuracionDias.Maximum = int.MaxValue;
            nudDuracionDias.Minimum = 1;
            nudDuracionDias.Name = "nudDuracionDias";
            nudDuracionDias.Size = new Size(230, 25);

            nudPrecio.DecimalPlaces = 2;
            nudPrecio.Font = new Font("Segoe UI", 10F);
            nudPrecio.Location = new Point(285, 244);
            nudPrecio.Maximum = 99999999.99M;
            nudPrecio.Name = "nudPrecio";
            nudPrecio.Size = new Size(230, 25);

            chkEstado.AutoSize = true;
            chkEstado.Checked = true;
            chkEstado.Font = new Font("Segoe UI", 10F);
            chkEstado.Location = new Point(25, 285);
            chkEstado.Name = "chkEstado";
            chkEstado.Text = "Activo";

            lblMensaje.ForeColor = Color.Firebrick;
            lblMensaje.Location = new Point(25, 315);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(250, 40);

            btnGuardar.BackColor = Color.FromArgb(124, 142, 163);
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Location = new Point(285, 315);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(120, 38);
            btnGuardar.Text = "GUARDAR";
            btnGuardar.Click += btnGuardar_Click;

            btnCancelar.BackColor = Color.WhiteSmoke;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCancelar.ForeColor = Color.Black;
            btnCancelar.Location = new Point(415, 315);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(120, 38);
            btnCancelar.Text = "CANCELAR";
            btnCancelar.DialogResult = DialogResult.Cancel;

            Controls.Add(lblTitulo);
            Controls.Add(lblNombre);
            Controls.Add(lblDescripcion);
            Controls.Add(lblDuracionDias);
            Controls.Add(lblPrecio);
            Controls.Add(txtNombre);
            Controls.Add(txtDescripcion);
            Controls.Add(nudDuracionDias);
            Controls.Add(nudPrecio);
            Controls.Add(chkEstado);
            Controls.Add(lblMensaje);
            Controls.Add(btnGuardar);
            Controls.Add(btnCancelar);
            AcceptButton = btnGuardar;
            CancelButton = btnCancelar;
        }
    }
}

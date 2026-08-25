namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    partial class FrmCambiarContrasena
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitulo;
        private Label lblContrasenaActual;
        private Label lblContrasenaNueva;
        private Label lblConfirmarContrasena;
        private TextBox txtContrasenaActual;
        private TextBox txtContrasenaNueva;
        private TextBox txtConfirmarContrasena;
        private CheckBox chkMostrarContrasena;
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
            lblContrasenaActual = new Label();
            lblContrasenaNueva = new Label();
            lblConfirmarContrasena = new Label();
            txtContrasenaActual = new TextBox();
            txtContrasenaNueva = new TextBox();
            txtConfirmarContrasena = new TextBox();
            chkMostrarContrasena = new CheckBox();
            lblMensaje = new Label();
            btnGuardar = new Button();
            btnCancelar = new Button();
            SuspendLayout();
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Text = "Cambiar contraseña";
            BackColor = Color.White;
            ClientSize = new Size(580, 365);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmCambiarContrasena";
            StartPosition = FormStartPosition.CenterParent;
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitulo.Location = new Point(25, 18);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Text = "Cambiar contraseña";
            lblContrasenaActual.AutoSize = true;
            lblContrasenaActual.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblContrasenaActual.Location = new Point(25, 70);
            lblContrasenaActual.Name = "lblContrasenaActual";
            lblContrasenaActual.Text = "Contraseña actual";
            txtContrasenaActual.Font = new Font("Segoe UI", 10F);
            txtContrasenaActual.Location = new Point(25, 92);
            txtContrasenaActual.MaxLength = 100;
            txtContrasenaActual.Name = "txtContrasenaActual";
            txtContrasenaActual.Size = new Size(530, 25);
            txtContrasenaActual.UseSystemPasswordChar = true;
            lblContrasenaNueva.AutoSize = true;
            lblContrasenaNueva.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblContrasenaNueva.Location = new Point(25, 130);
            lblContrasenaNueva.Name = "lblContrasenaNueva";
            lblContrasenaNueva.Text = "Contraseña nueva";
            txtContrasenaNueva.Font = new Font("Segoe UI", 10F);
            txtContrasenaNueva.Location = new Point(25, 152);
            txtContrasenaNueva.MaxLength = 100;
            txtContrasenaNueva.Name = "txtContrasenaNueva";
            txtContrasenaNueva.Size = new Size(530, 25);
            txtContrasenaNueva.UseSystemPasswordChar = true;
            lblConfirmarContrasena.AutoSize = true;
            lblConfirmarContrasena.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblConfirmarContrasena.Location = new Point(25, 190);
            lblConfirmarContrasena.Name = "lblConfirmarContrasena";
            lblConfirmarContrasena.Text = "Confirmar contraseña nueva";
            txtConfirmarContrasena.Font = new Font("Segoe UI", 10F);
            txtConfirmarContrasena.Location = new Point(25, 212);
            txtConfirmarContrasena.MaxLength = 100;
            txtConfirmarContrasena.Name = "txtConfirmarContrasena";
            txtConfirmarContrasena.Size = new Size(530, 25);
            txtConfirmarContrasena.UseSystemPasswordChar = true;
            chkMostrarContrasena.AutoSize = true;
            chkMostrarContrasena.Font = new Font("Segoe UI", 9F);
            chkMostrarContrasena.Location = new Point(25, 252);
            chkMostrarContrasena.Name = "chkMostrarContrasena";
            chkMostrarContrasena.Text = "Mostrar contraseñas";
            chkMostrarContrasena.CheckedChanged += chkMostrarContrasena_CheckedChanged;
            lblMensaje.ForeColor = Color.Firebrick;
            lblMensaje.Location = new Point(25, 307);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(270, 40);
            btnGuardar.BackColor = Color.FromArgb(124, 142, 163);
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Location = new Point(315, 307);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(120, 38);
            btnGuardar.Text = "GUARDAR";
            btnGuardar.Click += btnGuardar_Click;
            btnCancelar.BackColor = Color.WhiteSmoke;
            btnCancelar.DialogResult = DialogResult.Cancel;
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCancelar.ForeColor = Color.Black;
            btnCancelar.Location = new Point(445, 307);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(120, 38);
            btnCancelar.Text = "CANCELAR";
            Controls.Add(lblTitulo);
            Controls.Add(lblContrasenaActual);
            Controls.Add(txtContrasenaActual);
            Controls.Add(lblContrasenaNueva);
            Controls.Add(txtContrasenaNueva);
            Controls.Add(lblConfirmarContrasena);
            Controls.Add(txtConfirmarContrasena);
            Controls.Add(chkMostrarContrasena);
            Controls.Add(lblMensaje);
            Controls.Add(btnGuardar);
            Controls.Add(btnCancelar);
            AcceptButton = btnGuardar;
            CancelButton = btnCancelar;
            ResumeLayout(false);
            PerformLayout();
        }
    }
}

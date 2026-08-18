namespace Proyecto_Gym_ProgramacionIII_Jeovanny
{
    partial class FrmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
            pnlMarca = new Panel();
            picGimnasio = new PictureBox();
            pnlMarcaSuperior = new Panel();
            lblMarca = new Label();
            pnlLogin = new Panel();
            lblMensaje = new Label();
            btnSalir = new Button();
            btnIniciarSesion = new Button();
            chkMostrarContrasena = new CheckBox();
            txtContrasena = new TextBox();
            lblContrasena = new Label();
            txtUsuario = new TextBox();
            lblUsuario = new Label();
            lblBienvenida = new Label();
            pnlMarca.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picGimnasio).BeginInit();
            pnlMarcaSuperior.SuspendLayout();
            pnlLogin.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMarca
            // 
            pnlMarca.BackColor = Color.Black;
            pnlMarca.Controls.Add(picGimnasio);
            pnlMarca.Controls.Add(pnlMarcaSuperior);
            pnlMarca.Dock = DockStyle.Left;
            pnlMarca.Location = new Point(0, 0);
            pnlMarca.Margin = new Padding(3, 4, 3, 4);
            pnlMarca.Name = "pnlMarca";
            pnlMarca.Size = new Size(411, 720);
            pnlMarca.TabIndex = 0;
            // 
            // picGimnasio
            // 
            picGimnasio.BackColor = Color.Black;
            picGimnasio.Dock = DockStyle.Fill;
            picGimnasio.Image = (Image)resources.GetObject("picGimnasio.Image");
            picGimnasio.Location = new Point(0, 140);
            picGimnasio.Margin = new Padding(0);
            picGimnasio.Name = "picGimnasio";
            picGimnasio.Size = new Size(411, 580);
            picGimnasio.SizeMode = PictureBoxSizeMode.Zoom;
            picGimnasio.TabIndex = 1;
            picGimnasio.TabStop = false;
            // 
            // pnlMarcaSuperior
            // 
            pnlMarcaSuperior.BackColor = Color.Black;
            pnlMarcaSuperior.Controls.Add(lblMarca);
            pnlMarcaSuperior.Dock = DockStyle.Top;
            pnlMarcaSuperior.Location = new Point(0, 0);
            pnlMarcaSuperior.Margin = new Padding(3, 4, 3, 4);
            pnlMarcaSuperior.Name = "pnlMarcaSuperior";
            pnlMarcaSuperior.Size = new Size(411, 140);
            pnlMarcaSuperior.TabIndex = 0;
            // 
            // lblMarca
            // 
            lblMarca.Dock = DockStyle.Fill;
            lblMarca.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblMarca.ForeColor = Color.White;
            lblMarca.Location = new Point(0, 0);
            lblMarca.Name = "lblMarca";
            lblMarca.Size = new Size(411, 140);
            lblMarca.TabIndex = 0;
            lblMarca.Text = "Super Saija Gym";
            lblMarca.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlLogin
            // 
            pnlLogin.BackColor = Color.White;
            pnlLogin.Controls.Add(lblMensaje);
            pnlLogin.Controls.Add(btnSalir);
            pnlLogin.Controls.Add(btnIniciarSesion);
            pnlLogin.Controls.Add(chkMostrarContrasena);
            pnlLogin.Controls.Add(txtContrasena);
            pnlLogin.Controls.Add(lblContrasena);
            pnlLogin.Controls.Add(txtUsuario);
            pnlLogin.Controls.Add(lblUsuario);
            pnlLogin.Controls.Add(lblBienvenida);
            pnlLogin.Dock = DockStyle.Fill;
            pnlLogin.Location = new Point(411, 0);
            pnlLogin.Margin = new Padding(3, 4, 3, 4);
            pnlLogin.Name = "pnlLogin";
            pnlLogin.Size = new Size(640, 720);
            pnlLogin.TabIndex = 1;
            // 
            // lblMensaje
            // 
            lblMensaje.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblMensaje.ForeColor = Color.Black;
            lblMensaje.Location = new Point(78, 632);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(485, 59);
            lblMensaje.TabIndex = 9;
            lblMensaje.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnSalir
            // 
            btnSalir.BackColor = Color.White;
            btnSalir.Cursor = Cursors.Hand;
            btnSalir.FlatAppearance.BorderColor = Color.Black;
            btnSalir.FlatStyle = FlatStyle.Flat;
            btnSalir.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSalir.ForeColor = Color.Black;
            btnSalir.Location = new Point(78, 560);
            btnSalir.Margin = new Padding(3, 4, 3, 4);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(485, 53);
            btnSalir.TabIndex = 4;
            btnSalir.Text = "SALIR";
            btnSalir.UseVisualStyleBackColor = false;
            btnSalir.Click += btnSalir_Click;
            // 
            // btnIniciarSesion
            // 
            btnIniciarSesion.BackColor = Color.FromArgb(124, 142, 163);
            btnIniciarSesion.Cursor = Cursors.Hand;
            btnIniciarSesion.FlatAppearance.BorderSize = 0;
            btnIniciarSesion.FlatStyle = FlatStyle.Flat;
            btnIniciarSesion.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnIniciarSesion.ForeColor = Color.White;
            btnIniciarSesion.Location = new Point(78, 483);
            btnIniciarSesion.Margin = new Padding(3, 4, 3, 4);
            btnIniciarSesion.Name = "btnIniciarSesion";
            btnIniciarSesion.Size = new Size(485, 59);
            btnIniciarSesion.TabIndex = 3;
            btnIniciarSesion.Text = "INICIAR SESIÓN";
            btnIniciarSesion.UseVisualStyleBackColor = false;
            btnIniciarSesion.Click += btnIniciarSesion_Click;
            // 
            // chkMostrarContrasena
            // 
            chkMostrarContrasena.AutoSize = true;
            chkMostrarContrasena.BackColor = Color.White;
            chkMostrarContrasena.Cursor = Cursors.Hand;
            chkMostrarContrasena.Font = new Font("Segoe UI", 9.5F);
            chkMostrarContrasena.ForeColor = Color.Black;
            chkMostrarContrasena.Location = new Point(78, 427);
            chkMostrarContrasena.Margin = new Padding(3, 4, 3, 4);
            chkMostrarContrasena.Name = "chkMostrarContrasena";
            chkMostrarContrasena.Size = new Size(167, 25);
            chkMostrarContrasena.TabIndex = 2;
            chkMostrarContrasena.Text = "Mostrar contraseña";
            chkMostrarContrasena.UseVisualStyleBackColor = false;
            chkMostrarContrasena.CheckedChanged += chkMostrarContrasena_CheckedChanged;
            // 
            // txtContrasena
            // 
            txtContrasena.BackColor = Color.White;
            txtContrasena.BorderStyle = BorderStyle.FixedSingle;
            txtContrasena.Font = new Font("Segoe UI", 11F);
            txtContrasena.ForeColor = Color.Black;
            txtContrasena.Location = new Point(78, 365);
            txtContrasena.Margin = new Padding(3, 4, 3, 4);
            txtContrasena.MaxLength = 100;
            txtContrasena.Name = "txtContrasena";
            txtContrasena.Size = new Size(484, 32);
            txtContrasena.TabIndex = 1;
            txtContrasena.UseSystemPasswordChar = true;
            // 
            // lblContrasena
            // 
            lblContrasena.AutoSize = true;
            lblContrasena.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblContrasena.ForeColor = Color.Black;
            lblContrasena.Location = new Point(78, 331);
            lblContrasena.Name = "lblContrasena";
            lblContrasena.Size = new Size(96, 21);
            lblContrasena.TabIndex = 5;
            lblContrasena.Text = "Contraseña";
            // 
            // txtUsuario
            // 
            txtUsuario.BackColor = Color.White;
            txtUsuario.BorderStyle = BorderStyle.FixedSingle;
            txtUsuario.Font = new Font("Segoe UI", 11F);
            txtUsuario.ForeColor = Color.Black;
            txtUsuario.Location = new Point(78, 261);
            txtUsuario.Margin = new Padding(3, 4, 3, 4);
            txtUsuario.MaxLength = 50;
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(484, 32);
            txtUsuario.TabIndex = 0;
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblUsuario.ForeColor = Color.Black;
            lblUsuario.Location = new Point(78, 227);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(157, 21);
            lblUsuario.TabIndex = 3;
            lblUsuario.Text = "Nombre de usuario";
            // 
            // lblBienvenida
            // 
            lblBienvenida.AutoSize = true;
            lblBienvenida.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            lblBienvenida.ForeColor = Color.Black;
            lblBienvenida.Location = new Point(78, 121);
            lblBienvenida.Name = "lblBienvenida";
            lblBienvenida.Size = new Size(272, 62);
            lblBienvenida.TabIndex = 0;
            lblBienvenida.Text = "Bienvenido";
            // 
            // FrmLogin
            // 
            AcceptButton = btnIniciarSesion;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            CancelButton = btnSalir;
            ClientSize = new Size(1051, 720);
            Controls.Add(pnlLogin);
            Controls.Add(pnlMarca);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "FrmLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Inicio de sesión - Super Saija Gym";
            pnlMarca.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picGimnasio).EndInit();
            pnlMarcaSuperior.ResumeLayout(false);
            pnlLogin.ResumeLayout(false);
            pnlLogin.PerformLayout();
            ResumeLayout(false);
        }

        private Panel pnlMarca;
        private Panel pnlMarcaSuperior;
        private Label lblMarca;
        private PictureBox picGimnasio;
        private Panel pnlLogin;
        private Label lblBienvenida;
        private Label lblUsuario;
        private TextBox txtUsuario;
        private Label lblContrasena;
        private TextBox txtContrasena;
        private CheckBox chkMostrarContrasena;
        private Button btnIniciarSesion;
        private Button btnSalir;
        private Label lblMensaje;
    }
}

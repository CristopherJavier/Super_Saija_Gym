namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    partial class FrmConfiguracionInicial
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
            lblTitulo = new Label();
            lblInstruccion = new Label();
            lblNombreCompleto = new Label();
            txtNombreCompleto = new TextBox();
            lblNombreUsuario = new Label();
            txtNombreUsuario = new TextBox();
            lblContrasena = new Label();
            txtContrasena = new TextBox();
            lblConfirmarContrasena = new Label();
            txtConfirmarContrasena = new TextBox();
            chkMostrarContrasena = new CheckBox();
            btnCrearAdministrador = new Button();
            btnCancelar = new Button();
            lblMensaje = new Label();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.Font = new Font("Segoe UI", 26F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.Black;
            lblTitulo.Location = new Point(53, 47);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(500, 52);
            lblTitulo.TabIndex = 7;
            lblTitulo.Text = "Configuración inicial";
            lblTitulo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblInstruccion
            // 
            lblInstruccion.AutoSize = true;
            lblInstruccion.Font = new Font("Segoe UI", 11F);
            lblInstruccion.ForeColor = Color.FromArgb(124, 142, 163);
            lblInstruccion.Location = new Point(53, 79);
            lblInstruccion.Name = "lblInstruccion";
            lblInstruccion.Size = new Size(279, 20);
            lblInstruccion.TabIndex = 8;
            lblInstruccion.Text = "Crea el primer administrador del sistema";
            // 
            // lblNombreCompleto
            // 
            lblNombreCompleto.AutoSize = true;
            lblNombreCompleto.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblNombreCompleto.ForeColor = Color.Black;
            lblNombreCompleto.Location = new Point(53, 117);
            lblNombreCompleto.Name = "lblNombreCompleto";
            lblNombreCompleto.Size = new Size(120, 17);
            lblNombreCompleto.TabIndex = 9;
            lblNombreCompleto.Text = "Nombre completo";
            // 
            // txtNombreCompleto
            // 
            txtNombreCompleto.BorderStyle = BorderStyle.FixedSingle;
            txtNombreCompleto.Font = new Font("Segoe UI", 11F);
            txtNombreCompleto.Location = new Point(53, 140);
            txtNombreCompleto.MaxLength = 100;
            txtNombreCompleto.Name = "txtNombreCompleto";
            txtNombreCompleto.Size = new Size(494, 27);
            txtNombreCompleto.TabIndex = 0;
            // 
            // lblNombreUsuario
            // 
            lblNombreUsuario.AutoSize = true;
            lblNombreUsuario.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblNombreUsuario.ForeColor = Color.Black;
            lblNombreUsuario.Location = new Point(53, 182);
            lblNombreUsuario.Name = "lblNombreUsuario";
            lblNombreUsuario.Size = new Size(127, 17);
            lblNombreUsuario.TabIndex = 10;
            lblNombreUsuario.Text = "Nombre de usuario";
            // 
            // txtNombreUsuario
            // 
            txtNombreUsuario.BorderStyle = BorderStyle.FixedSingle;
            txtNombreUsuario.Font = new Font("Segoe UI", 11F);
            txtNombreUsuario.Location = new Point(53, 205);
            txtNombreUsuario.MaxLength = 50;
            txtNombreUsuario.Name = "txtNombreUsuario";
            txtNombreUsuario.Size = new Size(494, 27);
            txtNombreUsuario.TabIndex = 1;
            // 
            // lblContrasena
            // 
            lblContrasena.AutoSize = true;
            lblContrasena.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblContrasena.ForeColor = Color.Black;
            lblContrasena.Location = new Point(53, 247);
            lblContrasena.Name = "lblContrasena";
            lblContrasena.Size = new Size(77, 17);
            lblContrasena.TabIndex = 11;
            lblContrasena.Text = "Contraseña";
            // 
            // txtContrasena
            // 
            txtContrasena.BorderStyle = BorderStyle.FixedSingle;
            txtContrasena.Font = new Font("Segoe UI", 11F);
            txtContrasena.Location = new Point(53, 270);
            txtContrasena.MaxLength = 100;
            txtContrasena.Name = "txtContrasena";
            txtContrasena.Size = new Size(494, 27);
            txtContrasena.TabIndex = 2;
            txtContrasena.UseSystemPasswordChar = true;
            // 
            // lblConfirmarContrasena
            // 
            lblConfirmarContrasena.AutoSize = true;
            lblConfirmarContrasena.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblConfirmarContrasena.ForeColor = Color.Black;
            lblConfirmarContrasena.Location = new Point(53, 312);
            lblConfirmarContrasena.Name = "lblConfirmarContrasena";
            lblConfirmarContrasena.Size = new Size(141, 17);
            lblConfirmarContrasena.TabIndex = 12;
            lblConfirmarContrasena.Text = "Confirmar contraseña";
            // 
            // txtConfirmarContrasena
            // 
            txtConfirmarContrasena.BorderStyle = BorderStyle.FixedSingle;
            txtConfirmarContrasena.Font = new Font("Segoe UI", 11F);
            txtConfirmarContrasena.Location = new Point(53, 335);
            txtConfirmarContrasena.MaxLength = 100;
            txtConfirmarContrasena.Name = "txtConfirmarContrasena";
            txtConfirmarContrasena.Size = new Size(494, 27);
            txtConfirmarContrasena.TabIndex = 3;
            txtConfirmarContrasena.UseSystemPasswordChar = true;
            // 
            // chkMostrarContrasena
            // 
            chkMostrarContrasena.AutoSize = true;
            chkMostrarContrasena.Cursor = Cursors.Hand;
            chkMostrarContrasena.Font = new Font("Segoe UI", 9.5F);
            chkMostrarContrasena.ForeColor = Color.Black;
            chkMostrarContrasena.Location = new Point(53, 380);
            chkMostrarContrasena.Name = "chkMostrarContrasena";
            chkMostrarContrasena.Size = new Size(148, 21);
            chkMostrarContrasena.TabIndex = 4;
            chkMostrarContrasena.Text = "Mostrar contraseñas";
            chkMostrarContrasena.UseVisualStyleBackColor = true;
            chkMostrarContrasena.CheckedChanged += chkMostrarContrasena_CheckedChanged;
            // 
            // btnCrearAdministrador
            // 
            btnCrearAdministrador.BackColor = Color.FromArgb(124, 142, 163);
            btnCrearAdministrador.Cursor = Cursors.Hand;
            btnCrearAdministrador.FlatAppearance.BorderSize = 0;
            btnCrearAdministrador.FlatStyle = FlatStyle.Flat;
            btnCrearAdministrador.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCrearAdministrador.ForeColor = Color.White;
            btnCrearAdministrador.Location = new Point(53, 422);
            btnCrearAdministrador.Name = "btnCrearAdministrador";
            btnCrearAdministrador.Size = new Size(312, 44);
            btnCrearAdministrador.TabIndex = 5;
            btnCrearAdministrador.Text = "CREAR ADMINISTRADOR";
            btnCrearAdministrador.UseVisualStyleBackColor = false;
            btnCrearAdministrador.Click += btnCrearAdministrador_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.White;
            btnCancelar.Cursor = Cursors.Hand;
            btnCancelar.FlatAppearance.BorderColor = Color.Black;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCancelar.ForeColor = Color.Black;
            btnCancelar.Location = new Point(383, 422);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(164, 44);
            btnCancelar.TabIndex = 6;
            btnCancelar.Text = "CANCELAR";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // lblMensaje
            // 
            lblMensaje.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblMensaje.ForeColor = Color.Firebrick;
            lblMensaje.Location = new Point(53, 479);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(494, 56);
            lblMensaje.TabIndex = 13;
            lblMensaje.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // FrmConfiguracionInicial
            // 
            AcceptButton = btnCrearAdministrador;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            CancelButton = btnCancelar;
            ClientSize = new Size(600, 560);
            Controls.Add(lblMensaje);
            Controls.Add(btnCancelar);
            Controls.Add(btnCrearAdministrador);
            Controls.Add(chkMostrarContrasena);
            Controls.Add(txtConfirmarContrasena);
            Controls.Add(lblConfirmarContrasena);
            Controls.Add(txtContrasena);
            Controls.Add(lblContrasena);
            Controls.Add(txtNombreUsuario);
            Controls.Add(lblNombreUsuario);
            Controls.Add(txtNombreCompleto);
            Controls.Add(lblNombreCompleto);
            Controls.Add(lblInstruccion);
            Controls.Add(lblTitulo);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmConfiguracionInicial";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Configuración inicial - Super Saija Gym";
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblTitulo;
        private Label lblInstruccion;
        private Label lblNombreCompleto;
        private TextBox txtNombreCompleto;
        private Label lblNombreUsuario;
        private TextBox txtNombreUsuario;
        private Label lblContrasena;
        private TextBox txtContrasena;
        private Label lblConfirmarContrasena;
        private TextBox txtConfirmarContrasena;
        private CheckBox chkMostrarContrasena;
        private Button btnCrearAdministrador;
        private Button btnCancelar;
        private Label lblMensaje;
    }
}

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    partial class FrmPrincipal
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
            pnlEncabezado = new Panel();
            btnSalir = new Button();
            btnCerrarSesion = new Button();
            lblRol = new Label();
            lblBienvenida = new Label();
            lblNombreSistema = new Label();
            pnlContenido = new Panel();
            lblDescripcion = new Label();
            lblInicio = new Label();
            pnlEncabezado.SuspendLayout();
            pnlContenido.SuspendLayout();
            SuspendLayout();
            pnlEncabezado.BackColor = Color.Black;
            pnlEncabezado.Controls.Add(btnSalir);
            pnlEncabezado.Controls.Add(btnCerrarSesion);
            pnlEncabezado.Controls.Add(lblRol);
            pnlEncabezado.Controls.Add(lblBienvenida);
            pnlEncabezado.Controls.Add(lblNombreSistema);
            pnlEncabezado.Dock = DockStyle.Top;
            pnlEncabezado.Location = new Point(0, 0);
            pnlEncabezado.Name = "pnlEncabezado";
            pnlEncabezado.Size = new Size(1184, 92);
            pnlEncabezado.TabIndex = 0;
            btnSalir.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSalir.BackColor = Color.White;
            btnSalir.Cursor = Cursors.Hand;
            btnSalir.FlatAppearance.BorderColor = Color.White;
            btnSalir.FlatStyle = FlatStyle.Flat;
            btnSalir.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSalir.ForeColor = Color.Black;
            btnSalir.Location = new Point(1052, 26);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(100, 40);
            btnSalir.TabIndex = 1;
            btnSalir.Text = "SALIR";
            btnSalir.UseVisualStyleBackColor = false;
            btnSalir.Click += btnSalir_Click;
            btnCerrarSesion.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCerrarSesion.BackColor = Color.FromArgb(124, 142, 163);
            btnCerrarSesion.Cursor = Cursors.Hand;
            btnCerrarSesion.FlatAppearance.BorderSize = 0;
            btnCerrarSesion.FlatStyle = FlatStyle.Flat;
            btnCerrarSesion.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCerrarSesion.ForeColor = Color.White;
            btnCerrarSesion.Location = new Point(887, 26);
            btnCerrarSesion.Name = "btnCerrarSesion";
            btnCerrarSesion.Size = new Size(148, 40);
            btnCerrarSesion.TabIndex = 0;
            btnCerrarSesion.Text = "CERRAR SESIÓN";
            btnCerrarSesion.UseVisualStyleBackColor = false;
            btnCerrarSesion.Click += btnCerrarSesion_Click;
            lblRol.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblRol.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblRol.ForeColor = Color.FromArgb(124, 142, 163);
            lblRol.Location = new Point(550, 49);
            lblRol.Name = "lblRol";
            lblRol.Size = new Size(315, 24);
            lblRol.TabIndex = 4;
            lblRol.TextAlign = ContentAlignment.MiddleRight;
            lblBienvenida.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblBienvenida.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblBienvenida.ForeColor = Color.White;
            lblBienvenida.Location = new Point(450, 19);
            lblBienvenida.Name = "lblBienvenida";
            lblBienvenida.Size = new Size(415, 30);
            lblBienvenida.TabIndex = 3;
            lblBienvenida.TextAlign = ContentAlignment.MiddleRight;
            lblNombreSistema.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblNombreSistema.ForeColor = Color.White;
            lblNombreSistema.Location = new Point(28, 18);
            lblNombreSistema.Name = "lblNombreSistema";
            lblNombreSistema.Size = new Size(300, 52);
            lblNombreSistema.TabIndex = 2;
            lblNombreSistema.Text = "Super Saija Gym";
            lblNombreSistema.TextAlign = ContentAlignment.MiddleLeft;
            pnlContenido.BackColor = Color.White;
            pnlContenido.Controls.Add(lblDescripcion);
            pnlContenido.Controls.Add(lblInicio);
            pnlContenido.Dock = DockStyle.Fill;
            pnlContenido.Location = new Point(0, 92);
            pnlContenido.Name = "pnlContenido";
            pnlContenido.Size = new Size(1184, 589);
            pnlContenido.TabIndex = 1;
            lblDescripcion.AutoSize = true;
            lblDescripcion.Font = new Font("Segoe UI", 12F);
            lblDescripcion.ForeColor = Color.FromArgb(124, 142, 163);
            lblDescripcion.Location = new Point(50, 104);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(390, 21);
            lblDescripcion.TabIndex = 1;
            lblDescripcion.Text = "Selecciona una opción del sistema para comenzar.";
            lblInicio.AutoSize = true;
            lblInicio.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            lblInicio.ForeColor = Color.Black;
            lblInicio.Location = new Point(45, 42);
            lblInicio.Name = "lblInicio";
            lblInicio.Size = new Size(290, 51);
            lblInicio.TabIndex = 0;
            lblInicio.Text = "Panel principal";
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1184, 681);
            Controls.Add(pnlContenido);
            Controls.Add(pnlEncabezado);
            Font = new Font("Segoe UI", 9F);
            MinimumSize = new Size(900, 600);
            Name = "FrmPrincipal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Super Saija Gym";
            WindowState = FormWindowState.Maximized;
            Load += FrmPrincipal_Load;
            pnlEncabezado.ResumeLayout(false);
            pnlContenido.ResumeLayout(false);
            pnlContenido.PerformLayout();
            ResumeLayout(false);
        }

        private Panel pnlEncabezado;
        private Label lblNombreSistema;
        private Label lblBienvenida;
        private Label lblRol;
        private Panel pnlContenido;
        private Label lblInicio;
        private Label lblDescripcion;
        private Button btnCerrarSesion;
        private Button btnSalir;
    }
}

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    partial class FrmPrincipal
    {
        private Button btnConfiguracion = new Button();
        private FlowLayoutPanel pnlSubmenuConfiguracion = new FlowLayoutPanel();
        private Button btnCambiarContrasena = new Button();

        private void InicializarMenuConfiguracion()
        {
            ConfigurarBotonConfiguracion(
                btnConfiguracion,
                "btnConfiguracion",
                ">  CONFIGURACIÓN",
                44,
                21);
            btnConfiguracion.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnConfiguracion.Click += btnConfiguracion_Click;

            pnlSubmenuConfiguracion.BackColor = Color.Black;
            pnlSubmenuConfiguracion.FlowDirection = FlowDirection.TopDown;
            pnlSubmenuConfiguracion.Margin = new Padding(0);
            pnlSubmenuConfiguracion.Name = "pnlSubmenuConfiguracion";
            pnlSubmenuConfiguracion.Size = new Size(263, 40);
            pnlSubmenuConfiguracion.Visible = false;
            pnlSubmenuConfiguracion.WrapContents = false;

            ConfigurarBotonConfiguracion(btnCambiarContrasena, "btnCambiarContrasena", "CAMBIAR CONTRASEÑA", 40, 36);

            btnCambiarContrasena.Click += btnCambiarContrasena_Click;

            pnlSubmenuConfiguracion.Controls.Add(btnCambiarContrasena);
            pnlOpciones.Controls.Add(btnConfiguracion);
            pnlOpciones.Controls.Add(pnlSubmenuConfiguracion);
        }

        private static void ConfigurarBotonConfiguracion(
            Button boton,
            string nombre,
            string texto,
            int alto,
            int margenIzquierdo)
        {
            boton.BackColor = Color.Black;
            boton.Cursor = Cursors.Hand;
            boton.FlatStyle = FlatStyle.Flat;
            boton.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            boton.ForeColor = Color.White;
            boton.Margin = new Padding(0);
            boton.Name = nombre;
            boton.Padding = new Padding(margenIzquierdo, 0, 0, 0);
            boton.Size = new Size(263, alto);
            boton.Text = texto;
            boton.TextAlign = ContentAlignment.MiddleLeft;
            boton.UseVisualStyleBackColor = false;

            boton.FlatAppearance.BorderSize = 0;
            boton.FlatAppearance.MouseDownBackColor = Color.FromArgb(100, 117, 136);
            boton.FlatAppearance.MouseOverBackColor = Color.FromArgb(124, 142, 163);
        }
    }
}

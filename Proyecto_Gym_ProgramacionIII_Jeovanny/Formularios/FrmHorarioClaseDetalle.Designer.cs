namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    partial class FrmHorarioClaseDetalle
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitulo;
        private Label lblClase;
        private Label lblEntrenador;
        private Label lblDiaSemana;
        private Label lblHoraInicio;
        private Label lblHoraFin;
        private ComboBox cmbClase;
        private ComboBox cmbEntrenador;
        private ComboBox cmbDiaSemana;
        private DateTimePicker dtpHoraInicio;
        private DateTimePicker dtpHoraFin;
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
            lblClase = new Label();
            lblEntrenador = new Label();
            lblDiaSemana = new Label();
            lblHoraInicio = new Label();
            lblHoraFin = new Label();
            cmbClase = new ComboBox();
            cmbEntrenador = new ComboBox();
            cmbDiaSemana = new ComboBox();
            dtpHoraInicio = new DateTimePicker();
            dtpHoraFin = new DateTimePicker();
            chkEstado = new CheckBox();
            lblMensaje = new Label();
            btnGuardar = new Button();
            btnCancelar = new Button();

            Text = "Nuevo horario";
            BackColor = Color.White;
            ClientSize = new Size(560, 380);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmHorarioClaseDetalle";
            StartPosition = FormStartPosition.CenterParent;

            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitulo.Location = new Point(25, 18);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Text = "Nuevo horario";
            lblClase.AutoSize = true;
            lblClase.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblClase.Location = new Point(25, 72);
            lblClase.Name = "lblClase";
            lblClase.Text = "Clase o actividad";
            lblEntrenador.AutoSize = true;
            lblEntrenador.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblEntrenador.Location = new Point(290, 72);
            lblEntrenador.Name = "lblEntrenador";
            lblEntrenador.Text = "Entrenador";
            lblDiaSemana.AutoSize = true;
            lblDiaSemana.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDiaSemana.Location = new Point(25, 140);
            lblDiaSemana.Name = "lblDiaSemana";
            lblDiaSemana.Text = "Día de la semana";
            lblHoraInicio.AutoSize = true;
            lblHoraInicio.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblHoraInicio.Location = new Point(25, 208);
            lblHoraInicio.Name = "lblHoraInicio";
            lblHoraInicio.Text = "Hora de inicio";
            lblHoraFin.AutoSize = true;
            lblHoraFin.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblHoraFin.Location = new Point(290, 208);
            lblHoraFin.Name = "lblHoraFin";
            lblHoraFin.Text = "Hora de fin";

            cmbClase.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbClase.Font = new Font("Segoe UI", 10F);
            cmbClase.Location = new Point(25, 94);
            cmbClase.Name = "cmbClase";
            cmbClase.Size = new Size(245, 25);
            cmbEntrenador.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEntrenador.Font = new Font("Segoe UI", 10F);
            cmbEntrenador.Location = new Point(290, 94);
            cmbEntrenador.Name = "cmbEntrenador";
            cmbEntrenador.Size = new Size(245, 25);
            cmbDiaSemana.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDiaSemana.Font = new Font("Segoe UI", 10F);
            cmbDiaSemana.Location = new Point(25, 162);
            cmbDiaSemana.Name = "cmbDiaSemana";
            cmbDiaSemana.Size = new Size(245, 25);
            cmbDiaSemana.Items.AddRange(new object[] { "LUNES", "MARTES", "MIERCOLES", "JUEVES", "VIERNES", "SABADO", "DOMINGO" });

            dtpHoraInicio.CustomFormat = "hh:mm tt";
            dtpHoraInicio.Font = new Font("Segoe UI", 10F);
            dtpHoraInicio.Format = DateTimePickerFormat.Custom;
            dtpHoraInicio.Location = new Point(25, 230);
            dtpHoraInicio.Name = "dtpHoraInicio";
            dtpHoraInicio.ShowUpDown = true;
            dtpHoraInicio.Size = new Size(245, 25);
            dtpHoraFin.CustomFormat = "hh:mm tt";
            dtpHoraFin.Font = new Font("Segoe UI", 10F);
            dtpHoraFin.Format = DateTimePickerFormat.Custom;
            dtpHoraFin.Location = new Point(290, 230);
            dtpHoraFin.Name = "dtpHoraFin";
            dtpHoraFin.ShowUpDown = true;
            dtpHoraFin.Size = new Size(245, 25);

            chkEstado.AutoSize = true;
            chkEstado.Checked = true;
            chkEstado.Font = new Font("Segoe UI", 10F);
            chkEstado.Location = new Point(25, 276);
            chkEstado.Name = "chkEstado";
            chkEstado.Text = "Activo";

            lblMensaje.ForeColor = Color.Firebrick;
            lblMensaje.Location = new Point(25, 308);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(250, 45);

            btnGuardar.BackColor = Color.FromArgb(124, 142, 163);
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Location = new Point(285, 308);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(120, 38);
            btnGuardar.Text = "GUARDAR";
            btnGuardar.Click += btnGuardar_Click;

            btnCancelar.BackColor = Color.WhiteSmoke;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCancelar.ForeColor = Color.Black;
            btnCancelar.Location = new Point(415, 308);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(120, 38);
            btnCancelar.Text = "CANCELAR";
            btnCancelar.DialogResult = DialogResult.Cancel;

            Controls.Add(lblTitulo);
            Controls.Add(lblClase);
            Controls.Add(lblEntrenador);
            Controls.Add(lblDiaSemana);
            Controls.Add(lblHoraInicio);
            Controls.Add(lblHoraFin);
            Controls.Add(cmbClase);
            Controls.Add(cmbEntrenador);
            Controls.Add(cmbDiaSemana);
            Controls.Add(dtpHoraInicio);
            Controls.Add(dtpHoraFin);
            Controls.Add(chkEstado);
            Controls.Add(lblMensaje);
            Controls.Add(btnGuardar);
            Controls.Add(btnCancelar);
            AcceptButton = btnGuardar;
            CancelButton = btnCancelar;
            Load += FrmHorarioClaseDetalle_Load;
        }
    }
}

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    partial class FrmClasesActividades
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitulo;
        private Button btnNuevo;
        private TextBox txtBuscar;
        private Button btnBuscar;
        private Button btnLimpiar;
        private DataGridView dgvClases;
        private Button btnActualizar;
        private Button btnEditar;

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
            btnNuevo = new Button();
            txtBuscar = new TextBox();
            btnBuscar = new Button();
            btnLimpiar = new Button();
            dgvClases = new DataGridView();
            btnActualizar = new Button();
            btnEditar = new Button();

            Text = "Clases y actividades";
            BackColor = Color.White;
            ClientSize = new Size(690, 475);
            FormBorderStyle = FormBorderStyle.None;
            MinimumSize = new Size(600, 400);
            Name = "FrmClasesActividades";

            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.Black;
            lblTitulo.Location = new Point(18, 12);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Text = "Clases 12 actividades";

            btnNuevo.BackColor = Color.FromArgb(124, 142, 163);
            btnNuevo.FlatStyle = FlatStyle.Flat;
            btnNuevo.FlatAppearance.BorderSize = 0;
            btnNuevo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnNuevo.ForeColor = Color.White;
            btnNuevo.Location = new Point(527, 18);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(145, 36);
            btnNuevo.Text = "NUEVO";
            btnNuevo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnNuevo.Click += btnNuevo_Click;

            txtBuscar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtBuscar.Font = new Font("Segoe UI", 10F);
            txtBuscar.Location = new Point(18, 70);
            txtBuscar.MaxLength = 200;
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(430, 25);
            txtBuscar.KeyDown += txtBuscar_KeyDown;

            btnBuscar.BackColor = Color.Black;
            btnBuscar.FlatStyle = FlatStyle.Flat;
            btnBuscar.FlatAppearance.BorderSize = 0;
            btnBuscar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnBuscar.ForeColor = Color.White;
            btnBuscar.Location = new Point(458, 66);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(100, 36);
            btnBuscar.Text = "BUSCAR";
            btnBuscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnBuscar.Click += btnBuscar_Click;

            btnLimpiar.BackColor = Color.WhiteSmoke;
            btnLimpiar.FlatStyle = FlatStyle.Flat;
            btnLimpiar.FlatAppearance.BorderSize = 0;
            btnLimpiar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnLimpiar.ForeColor = Color.Black;
            btnLimpiar.Location = new Point(568, 66);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(104, 36);
            btnLimpiar.Text = "LIMPIAR";
            btnLimpiar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLimpiar.Click += btnLimpiar_Click;

            dgvClases.AllowUserToAddRows = false;
            dgvClases.AllowUserToDeleteRows = false;
            dgvClases.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvClases.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvClases.BackgroundColor = Color.White;
            dgvClases.Location = new Point(18, 113);
            dgvClases.MultiSelect = false;
            dgvClases.Name = "dgvClases";
            dgvClases.ReadOnly = true;
            dgvClases.RowHeadersVisible = false;
            dgvClases.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvClases.Size = new Size(654, 302);
            dgvClases.SelectionChanged += dgvClases_SelectionChanged;

            btnActualizar.BackColor = Color.Black;
            btnActualizar.FlatStyle = FlatStyle.Flat;
            btnActualizar.FlatAppearance.BorderSize = 0;
            btnActualizar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnActualizar.ForeColor = Color.White;
            btnActualizar.Location = new Point(422, 427);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(120, 36);
            btnActualizar.Text = "ACTUALIZAR";
            btnActualizar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnActualizar.Click += btnActualizar_Click;

            btnEditar.BackColor = Color.FromArgb(124, 142, 163);
            btnEditar.FlatStyle = FlatStyle.Flat;
            btnEditar.FlatAppearance.BorderSize = 0;
            btnEditar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnEditar.ForeColor = Color.White;
            btnEditar.Location = new Point(552, 427);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(120, 36);
            btnEditar.Text = "EDITAR";
            btnEditar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnEditar.Click += btnEditar_Click;

            Controls.Add(lblTitulo);
            Controls.Add(btnNuevo);
            Controls.Add(txtBuscar);
            Controls.Add(btnBuscar);
            Controls.Add(btnLimpiar);
            Controls.Add(dgvClases);
            Controls.Add(btnActualizar);
            Controls.Add(btnEditar);
            Load += FrmClasesActividades_Load;
        }
    }
}

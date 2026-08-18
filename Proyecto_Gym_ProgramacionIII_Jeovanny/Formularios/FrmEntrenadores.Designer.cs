namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    partial class FrmEntrenadores
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel tlpPrincipal;
        private TableLayoutPanel tlpEncabezado;
        private Panel pnlTitulos;
        private Label lblTitulo;
        private Button btnNuevo;
        private TableLayoutPanel tlpBusqueda;
        private TextBox txtBuscar;
        private Button btnBuscar;
        private Button btnLimpiar;
        private DataGridView dgvEntrenadores;
        private FlowLayoutPanel flpAcciones;
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
            tlpPrincipal = new TableLayoutPanel();
            tlpEncabezado = new TableLayoutPanel();
            pnlTitulos = new Panel();
            lblTitulo = new Label();
            btnNuevo = new Button();
            tlpBusqueda = new TableLayoutPanel();
            txtBuscar = new TextBox();
            btnBuscar = new Button();
            btnLimpiar = new Button();
            dgvEntrenadores = new DataGridView();
            flpAcciones = new FlowLayoutPanel();
            btnActualizar = new Button();
            btnEditar = new Button();

            Text = "Entrenadores";
            BackColor = Color.White;
            FormBorderStyle = FormBorderStyle.None;
            MinimumSize = new Size(600, 400);

            tlpPrincipal.BackColor = Color.White;
            tlpPrincipal.ColumnCount = 1;
            tlpPrincipal.Dock = DockStyle.Fill;
            tlpPrincipal.Name = "tlpPrincipal";
            tlpPrincipal.Padding = new Padding(18, 12, 18, 18);
            tlpPrincipal.RowCount = 7;
            tlpPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));

            tlpEncabezado.ColumnCount = 2;
            tlpEncabezado.Dock = DockStyle.Fill;
            tlpEncabezado.Margin = new Padding(0);
            tlpEncabezado.Name = "tlpEncabezado";
            tlpEncabezado.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpEncabezado.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 145F));

            pnlTitulos.Dock = DockStyle.Fill;
            pnlTitulos.Margin = new Padding(0);
            pnlTitulos.Name = "pnlTitulos";

            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.Black;
            lblTitulo.Location = new Point(0, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Text = "Entrenadores";

            btnNuevo.BackColor = Color.FromArgb(124, 142, 163);
            btnNuevo.Cursor = Cursors.Hand;
            btnNuevo.FlatStyle = FlatStyle.Flat;
            btnNuevo.FlatAppearance.BorderSize = 0;
            btnNuevo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnNuevo.ForeColor = Color.White;
            btnNuevo.Height = 36;
            btnNuevo.Margin = new Padding(6, 2, 0, 2);
            btnNuevo.Text = "NUEVO";
            btnNuevo.Width = 145;
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Click += btnNuevo_Click;

            tlpBusqueda.ColumnCount = 3;
            tlpBusqueda.Dock = DockStyle.Fill;
            tlpBusqueda.Margin = new Padding(0);
            tlpBusqueda.Name = "tlpBusqueda";
            tlpBusqueda.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpBusqueda.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tlpBusqueda.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));

            txtBuscar.Dock = DockStyle.Fill;
            txtBuscar.Font = new Font("Segoe UI", 10F);
            txtBuscar.Margin = new Padding(0, 5, 8, 5);
            txtBuscar.MaxLength = 120;
            txtBuscar.Name = "txtBuscar";
            txtBuscar.KeyDown += txtBuscar_KeyDown;

            btnBuscar.BackColor = Color.Black;
            btnBuscar.Cursor = Cursors.Hand;
            btnBuscar.FlatStyle = FlatStyle.Flat;
            btnBuscar.FlatAppearance.BorderSize = 0;
            btnBuscar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnBuscar.ForeColor = Color.White;
            btnBuscar.Height = 36;
            btnBuscar.Margin = new Padding(6, 2, 0, 2);
            btnBuscar.Text = "BUSCAR";
            btnBuscar.Width = 100;
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Click += btnBuscar_Click;

            btnLimpiar.BackColor = Color.WhiteSmoke;
            btnLimpiar.Cursor = Cursors.Hand;
            btnLimpiar.FlatStyle = FlatStyle.Flat;
            btnLimpiar.FlatAppearance.BorderSize = 0;
            btnLimpiar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnLimpiar.ForeColor = Color.Black;
            btnLimpiar.Height = 36;
            btnLimpiar.Margin = new Padding(6, 2, 0, 2);
            btnLimpiar.Text = "LIMPIAR";
            btnLimpiar.Width = 100;
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Click += btnLimpiar_Click;

            dgvEntrenadores.AllowUserToAddRows = false;
            dgvEntrenadores.AllowUserToDeleteRows = false;
            dgvEntrenadores.AllowUserToResizeRows = false;
            dgvEntrenadores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEntrenadores.BackgroundColor = Color.White;
            dgvEntrenadores.Dock = DockStyle.Fill;
            dgvEntrenadores.MultiSelect = false;
            dgvEntrenadores.Name = "dgvEntrenadores";
            dgvEntrenadores.ReadOnly = true;
            dgvEntrenadores.RowHeadersVisible = false;
            dgvEntrenadores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEntrenadores.SelectionChanged += dgvEntrenadores_SelectionChanged;

            flpAcciones.Dock = DockStyle.Fill;
            flpAcciones.FlowDirection = FlowDirection.RightToLeft;
            flpAcciones.Margin = new Padding(0);
            flpAcciones.Name = "flpAcciones";

            btnActualizar.BackColor = Color.Black;
            btnActualizar.Cursor = Cursors.Hand;
            btnActualizar.FlatStyle = FlatStyle.Flat;
            btnActualizar.FlatAppearance.BorderSize = 0;
            btnActualizar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnActualizar.ForeColor = Color.White;
            btnActualizar.Height = 36;
            btnActualizar.Margin = new Padding(6, 2, 0, 2);
            btnActualizar.Text = "ACTUALIZAR";
            btnActualizar.Width = 120;
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Click += btnActualizar_Click;

            btnEditar.BackColor = Color.FromArgb(124, 142, 163);
            btnEditar.Cursor = Cursors.Hand;
            btnEditar.FlatStyle = FlatStyle.Flat;
            btnEditar.FlatAppearance.BorderSize = 0;
            btnEditar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnEditar.ForeColor = Color.White;
            btnEditar.Height = 36;
            btnEditar.Margin = new Padding(6, 2, 0, 2);
            btnEditar.Text = "EDITAR";
            btnEditar.Width = 120;
            btnEditar.Name = "btnEditar";
            btnEditar.Click += btnEditar_Click;

            pnlTitulos.Controls.Add(lblTitulo);
            tlpEncabezado.Controls.Add(pnlTitulos, 0, 0);
            tlpEncabezado.Controls.Add(btnNuevo, 1, 0);
            tlpBusqueda.Controls.Add(txtBuscar, 0, 0);
            tlpBusqueda.Controls.Add(btnBuscar, 1, 0);
            tlpBusqueda.Controls.Add(btnLimpiar, 2, 0);
            flpAcciones.Controls.Add(btnEditar);
            flpAcciones.Controls.Add(btnActualizar);
            tlpPrincipal.Controls.Add(tlpEncabezado, 0, 0);
            tlpPrincipal.Controls.Add(tlpBusqueda, 0, 2);
            tlpPrincipal.Controls.Add(dgvEntrenadores, 0, 4);
            tlpPrincipal.Controls.Add(flpAcciones, 0, 6);
            Controls.Add(tlpPrincipal);
            Name = "FrmEntrenadores";
            Load += FrmEntrenadores_Load;
        }
    }
}

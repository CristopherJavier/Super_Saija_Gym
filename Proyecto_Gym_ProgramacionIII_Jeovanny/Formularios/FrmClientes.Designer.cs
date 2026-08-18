namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    partial class FrmClientes
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
            tlpPrincipal = new TableLayoutPanel();
            tlpEncabezado = new TableLayoutPanel();
            pnlTitulos = new Panel();
            lblTitulo = new Label();
            btnNuevo = new Button();
            tlpBusqueda = new TableLayoutPanel();
            txtBuscar = new TextBox();
            btnBuscar = new Button();
            btnLimpiarBusqueda = new Button();
            dgvClientes = new DataGridView();
            flpAcciones = new FlowLayoutPanel();
            btnEditar = new Button();
            btnActualizar = new Button();
            tlpPrincipal.SuspendLayout();
            tlpEncabezado.SuspendLayout();
            pnlTitulos.SuspendLayout();
            tlpBusqueda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvClientes).BeginInit();
            flpAcciones.SuspendLayout();
            SuspendLayout();
            // 
            // tlpPrincipal
            // 
            tlpPrincipal.BackColor = Color.White;
            tlpPrincipal.ColumnCount = 1;
            tlpPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpPrincipal.Controls.Add(tlpEncabezado, 0, 0);
            tlpPrincipal.Controls.Add(tlpBusqueda, 0, 2);
            tlpPrincipal.Controls.Add(dgvClientes, 0, 4);
            tlpPrincipal.Controls.Add(flpAcciones, 0, 6);
            tlpPrincipal.Dock = DockStyle.Fill;
            tlpPrincipal.Location = new Point(0, 0);
            tlpPrincipal.Name = "tlpPrincipal";
            tlpPrincipal.Padding = new Padding(18, 12, 18, 18);
            tlpPrincipal.RowCount = 7;
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tlpPrincipal.Size = new Size(690, 475);
            tlpPrincipal.TabIndex = 0;
            // 
            // tlpEncabezado
            // 
            tlpEncabezado.ColumnCount = 2;
            tlpEncabezado.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpEncabezado.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 145F));
            tlpEncabezado.Controls.Add(pnlTitulos, 0, 0);
            tlpEncabezado.Controls.Add(btnNuevo, 1, 0);
            tlpEncabezado.Dock = DockStyle.Fill;
            tlpEncabezado.Location = new Point(18, 12);
            tlpEncabezado.Margin = new Padding(0);
            tlpEncabezado.Name = "tlpEncabezado";
            tlpEncabezado.RowCount = 1;
            tlpEncabezado.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpEncabezado.Size = new Size(654, 48);
            tlpEncabezado.TabIndex = 0;
            // 
            // pnlTitulos
            // 
            pnlTitulos.Controls.Add(lblTitulo);
            pnlTitulos.Dock = DockStyle.Fill;
            pnlTitulos.Location = new Point(0, 0);
            pnlTitulos.Margin = new Padding(0);
            pnlTitulos.Name = "pnlTitulos";
            pnlTitulos.Size = new Size(509, 48);
            pnlTitulos.TabIndex = 0;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.Black;
            lblTitulo.Location = new Point(0, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(119, 37);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Clientes";
            // 
            // btnNuevo
            // 
            btnNuevo.BackColor = Color.FromArgb(124, 142, 163);
            btnNuevo.Cursor = Cursors.Hand;
            btnNuevo.Dock = DockStyle.Fill;
            btnNuevo.FlatAppearance.BorderSize = 0;
            btnNuevo.FlatStyle = FlatStyle.Flat;
            btnNuevo.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            btnNuevo.ForeColor = Color.White;
            btnNuevo.Location = new Point(519, 4);
            btnNuevo.Margin = new Padding(10, 4, 0, 4);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(135, 40);
            btnNuevo.TabIndex = 1;
            btnNuevo.Text = "NUEVO CLIENTE";
            btnNuevo.UseVisualStyleBackColor = false;
            btnNuevo.Click += btnNuevo_Click;
            // 
            // tlpBusqueda
            // 
            tlpBusqueda.ColumnCount = 3;
            tlpBusqueda.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpBusqueda.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 96F));
            tlpBusqueda.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tlpBusqueda.Controls.Add(txtBuscar, 0, 0);
            tlpBusqueda.Controls.Add(btnBuscar, 1, 0);
            tlpBusqueda.Controls.Add(btnLimpiarBusqueda, 2, 0);
            tlpBusqueda.Dock = DockStyle.Fill;
            tlpBusqueda.Location = new Point(18, 70);
            tlpBusqueda.Margin = new Padding(0);
            tlpBusqueda.Name = "tlpBusqueda";
            tlpBusqueda.RowCount = 1;
            tlpBusqueda.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpBusqueda.Size = new Size(654, 40);
            tlpBusqueda.TabIndex = 1;
            // 
            // txtBuscar
            // 
            txtBuscar.Dock = DockStyle.Fill;
            txtBuscar.Font = new Font("Segoe UI", 10F);
            txtBuscar.Location = new Point(0, 6);
            txtBuscar.Margin = new Padding(0, 6, 8, 6);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(450, 25);
            txtBuscar.TabIndex = 0;
            txtBuscar.KeyDown += txtBuscar_KeyDown;
            // 
            // btnBuscar
            // 
            btnBuscar.BackColor = Color.Black;
            btnBuscar.Cursor = Cursors.Hand;
            btnBuscar.Dock = DockStyle.Fill;
            btnBuscar.FlatAppearance.BorderSize = 0;
            btnBuscar.FlatStyle = FlatStyle.Flat;
            btnBuscar.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            btnBuscar.ForeColor = Color.White;
            btnBuscar.Location = new Point(458, 3);
            btnBuscar.Margin = new Padding(0, 3, 6, 3);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(90, 34);
            btnBuscar.TabIndex = 1;
            btnBuscar.Text = "BUSCAR";
            btnBuscar.UseVisualStyleBackColor = false;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // btnLimpiarBusqueda
            // 
            btnLimpiarBusqueda.BackColor = Color.White;
            btnLimpiarBusqueda.Cursor = Cursors.Hand;
            btnLimpiarBusqueda.Dock = DockStyle.Fill;
            btnLimpiarBusqueda.FlatAppearance.BorderColor = Color.Black;
            btnLimpiarBusqueda.FlatStyle = FlatStyle.Flat;
            btnLimpiarBusqueda.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            btnLimpiarBusqueda.ForeColor = Color.Black;
            btnLimpiarBusqueda.Location = new Point(554, 3);
            btnLimpiarBusqueda.Margin = new Padding(0, 3, 0, 3);
            btnLimpiarBusqueda.Name = "btnLimpiarBusqueda";
            btnLimpiarBusqueda.Size = new Size(100, 34);
            btnLimpiarBusqueda.TabIndex = 2;
            btnLimpiarBusqueda.Text = "LIMPIAR";
            btnLimpiarBusqueda.UseVisualStyleBackColor = false;
            btnLimpiarBusqueda.Click += btnLimpiarBusqueda_Click;
            // 
            // dgvClientes
            // 
            dgvClientes.AllowUserToAddRows = false;
            dgvClientes.AllowUserToDeleteRows = false;
            dgvClientes.AllowUserToResizeRows = false;
            dgvClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvClientes.BackgroundColor = Color.White;
            dgvClientes.BorderStyle = BorderStyle.Fixed3D;
            dgvClientes.Dock = DockStyle.Fill;
            dgvClientes.Location = new Point(18, 120);
            dgvClientes.Margin = new Padding(0);
            dgvClientes.MultiSelect = false;
            dgvClientes.Name = "dgvClientes";
            dgvClientes.ReadOnly = true;
            dgvClientes.RowHeadersVisible = false;
            dgvClientes.RowHeadersWidth = 51;
            dgvClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvClientes.Size = new Size(654, 287);
            dgvClientes.TabIndex = 2;
            dgvClientes.CellFormatting += dgvClientes_CellFormatting;
            dgvClientes.SelectionChanged += dgvClientes_SelectionChanged;
            // 
            // flpAcciones
            // 
            flpAcciones.Controls.Add(btnEditar);
            flpAcciones.Controls.Add(btnActualizar);
            flpAcciones.Dock = DockStyle.Fill;
            flpAcciones.FlowDirection = FlowDirection.RightToLeft;
            flpAcciones.Location = new Point(18, 417);
            flpAcciones.Margin = new Padding(0);
            flpAcciones.Name = "flpAcciones";
            flpAcciones.Size = new Size(654, 40);
            flpAcciones.TabIndex = 3;
            // 
            // btnEditar
            // 
            btnEditar.BackColor = Color.FromArgb(124, 142, 163);
            btnEditar.Cursor = Cursors.Hand;
            btnEditar.FlatAppearance.BorderSize = 0;
            btnEditar.FlatStyle = FlatStyle.Flat;
            btnEditar.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            btnEditar.ForeColor = Color.White;
            btnEditar.Location = new Point(531, 3);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(120, 34);
            btnEditar.TabIndex = 1;
            btnEditar.Text = "EDITAR";
            btnEditar.UseVisualStyleBackColor = false;
            btnEditar.Click += btnEditar_Click;
            // 
            // btnActualizar
            // 
            btnActualizar.BackColor = Color.Black;
            btnActualizar.Cursor = Cursors.Hand;
            btnActualizar.FlatAppearance.BorderSize = 0;
            btnActualizar.FlatStyle = FlatStyle.Flat;
            btnActualizar.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            btnActualizar.ForeColor = Color.White;
            btnActualizar.Location = new Point(405, 3);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(120, 34);
            btnActualizar.TabIndex = 0;
            btnActualizar.Text = "ACTUALIZAR";
            btnActualizar.UseVisualStyleBackColor = false;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // FrmClientes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(690, 475);
            Controls.Add(tlpPrincipal);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.None;
            MinimumSize = new Size(600, 400);
            Name = "FrmClientes";
            Text = "Clientes";
            Load += FrmClientes_Load;
            tlpPrincipal.ResumeLayout(false);
            tlpEncabezado.ResumeLayout(false);
            pnlTitulos.ResumeLayout(false);
            pnlTitulos.PerformLayout();
            tlpBusqueda.ResumeLayout(false);
            tlpBusqueda.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvClientes).EndInit();
            flpAcciones.ResumeLayout(false);
            ResumeLayout(false);
        }

        private TableLayoutPanel tlpPrincipal;
        private TableLayoutPanel tlpEncabezado;
        private Panel pnlTitulos;
        private Label lblTitulo;
        private Button btnNuevo;
        private TableLayoutPanel tlpBusqueda;
        private TextBox txtBuscar;
        private Button btnBuscar;
        private Button btnLimpiarBusqueda;
        private DataGridView dgvClientes;
        private FlowLayoutPanel flpAcciones;
        private Button btnEditar;
        private Button btnActualizar;
    }
}

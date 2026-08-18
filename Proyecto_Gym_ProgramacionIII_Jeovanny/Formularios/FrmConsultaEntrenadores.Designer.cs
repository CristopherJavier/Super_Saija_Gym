namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    partial class FrmConsultaEntrenadores
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel tlpPrincipal;
        private Panel pnlTitulos;
        private Label lblTitulo;
        private TableLayoutPanel tlpBusqueda;
        private TextBox txtBuscar;
        private Button btnBuscar;
        private Button btnLimpiar;
        private DataGridView dgvEntrenadores;

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
            pnlTitulos = new Panel();
            lblTitulo = new Label();
            tlpBusqueda = new TableLayoutPanel();
            txtBuscar = new TextBox();
            btnBuscar = new Button();
            btnLimpiar = new Button();
            dgvEntrenadores = new DataGridView();
            tlpPrincipal.SuspendLayout();
            pnlTitulos.SuspendLayout();
            tlpBusqueda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvEntrenadores).BeginInit();
            SuspendLayout();
            // 
            // tlpPrincipal
            // 
            tlpPrincipal.BackColor = Color.White;
            tlpPrincipal.ColumnCount = 1;
            tlpPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpPrincipal.Controls.Add(pnlTitulos, 0, 0);
            tlpPrincipal.Controls.Add(tlpBusqueda, 0, 2);
            tlpPrincipal.Controls.Add(dgvEntrenadores, 0, 4);
            tlpPrincipal.Dock = DockStyle.Fill;
            tlpPrincipal.Location = new Point(0, 0);
            tlpPrincipal.Name = "tlpPrincipal";
            tlpPrincipal.Padding = new Padding(18, 12, 18, 18);
            tlpPrincipal.RowCount = 5;
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 13F));
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 53F));
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 13F));
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpPrincipal.Size = new Size(686, 533);
            tlpPrincipal.TabIndex = 0;
            // 
            // pnlTitulos
            // 
            pnlTitulos.Controls.Add(lblTitulo);
            pnlTitulos.Dock = DockStyle.Fill;
            pnlTitulos.Location = new Point(18, 12);
            pnlTitulos.Margin = new Padding(0);
            pnlTitulos.Name = "pnlTitulos";
            pnlTitulos.Size = new Size(650, 48);
            pnlTitulos.TabIndex = 0;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.Black;
            lblTitulo.Location = new Point(0, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(346, 37);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Consulta de entrenadores";
            // 
            // tlpBusqueda
            // 
            tlpBusqueda.ColumnCount = 3;
            tlpBusqueda.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpBusqueda.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 118F));
            tlpBusqueda.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 118F));
            tlpBusqueda.Controls.Add(txtBuscar, 0, 0);
            tlpBusqueda.Controls.Add(btnBuscar, 1, 0);
            tlpBusqueda.Controls.Add(btnLimpiar, 2, 0);
            tlpBusqueda.Dock = DockStyle.Fill;
            tlpBusqueda.Location = new Point(18, 73);
            tlpBusqueda.Margin = new Padding(0);
            tlpBusqueda.Name = "tlpBusqueda";
            tlpBusqueda.RowCount = 1;
            tlpBusqueda.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpBusqueda.Size = new Size(650, 53);
            tlpBusqueda.TabIndex = 1;
            // 
            // txtBuscar
            // 
            txtBuscar.Dock = DockStyle.Fill;
            txtBuscar.Font = new Font("Segoe UI", 10F);
            txtBuscar.Location = new Point(0, 8);
            txtBuscar.Margin = new Padding(0, 8, 9, 8);
            txtBuscar.MaxLength = 120;
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(405, 25);
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
            btnBuscar.Location = new Point(414, 4);
            btnBuscar.Margin = new Padding(0, 4, 4, 4);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(114, 45);
            btnBuscar.TabIndex = 1;
            btnBuscar.Text = "BUSCAR";
            btnBuscar.UseVisualStyleBackColor = false;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.BackColor = Color.White;
            btnLimpiar.Cursor = Cursors.Hand;
            btnLimpiar.Dock = DockStyle.Fill;
            btnLimpiar.FlatAppearance.BorderColor = Color.Black;
            btnLimpiar.FlatStyle = FlatStyle.Flat;
            btnLimpiar.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            btnLimpiar.ForeColor = Color.Black;
            btnLimpiar.Location = new Point(536, 4);
            btnLimpiar.Margin = new Padding(4, 4, 0, 4);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(114, 45);
            btnLimpiar.TabIndex = 2;
            btnLimpiar.Text = "LIMPIAR";
            btnLimpiar.UseVisualStyleBackColor = false;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // dgvEntrenadores
            // 
            dgvEntrenadores.AllowUserToAddRows = false;
            dgvEntrenadores.AllowUserToDeleteRows = false;
            dgvEntrenadores.AllowUserToResizeRows = false;
            dgvEntrenadores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEntrenadores.BackgroundColor = Color.White;
            dgvEntrenadores.BorderStyle = BorderStyle.Fixed3D;
            dgvEntrenadores.Dock = DockStyle.Fill;
            dgvEntrenadores.Location = new Point(18, 139);
            dgvEntrenadores.Margin = new Padding(0);
            dgvEntrenadores.MultiSelect = false;
            dgvEntrenadores.Name = "dgvEntrenadores";
            dgvEntrenadores.ReadOnly = true;
            dgvEntrenadores.RowHeadersVisible = false;
            dgvEntrenadores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEntrenadores.Size = new Size(650, 376);
            dgvEntrenadores.TabIndex = 2;
            dgvEntrenadores.CellFormatting += dgvEntrenadores_CellFormatting;
            // 
            // FrmConsultaEntrenadores
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(686, 533);
            Controls.Add(tlpPrincipal);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.None;
            MinimumSize = new Size(686, 533);
            Name = "FrmConsultaEntrenadores";
            Text = "Consulta de entrenadores";
            Load += FrmConsultaEntrenadores_Load;
            tlpPrincipal.ResumeLayout(false);
            pnlTitulos.ResumeLayout(false);
            pnlTitulos.PerformLayout();
            tlpBusqueda.ResumeLayout(false);
            tlpBusqueda.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvEntrenadores).EndInit();
            ResumeLayout(false);
        }

    }
}

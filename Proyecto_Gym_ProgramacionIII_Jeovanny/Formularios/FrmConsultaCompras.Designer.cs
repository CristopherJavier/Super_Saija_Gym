namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    partial class FrmConsultaCompras
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel tlpPrincipal;
        private Panel pnlTitulos;
        private Label lblTitulo;
        private TableLayoutPanel tlpBusqueda;
        private TextBox txtBuscar;
        private Button btnBuscar;
        private Button btnLimpiar;
        private DataGridView dgvCompras;
        private DataGridViewTextBoxColumn colFecha;
        private DataGridViewTextBoxColumn colProveedor;
        private DataGridViewTextBoxColumn colTotal;
        private DataGridViewTextBoxColumn colEstado;

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
            pnlTitulos = new Panel();
            lblTitulo = new Label();
            tlpBusqueda = new TableLayoutPanel();
            txtBuscar = new TextBox();
            btnBuscar = new Button();
            btnLimpiar = new Button();
            dgvCompras = new DataGridView();
            colFecha = new DataGridViewTextBoxColumn();
            colProveedor = new DataGridViewTextBoxColumn();
            colTotal = new DataGridViewTextBoxColumn();
            colEstado = new DataGridViewTextBoxColumn();
            tlpPrincipal.SuspendLayout();
            pnlTitulos.SuspendLayout();
            tlpBusqueda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCompras).BeginInit();
            SuspendLayout();
            tlpPrincipal.BackColor = Color.White;
            tlpPrincipal.ColumnCount = 1;
            tlpPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpPrincipal.Controls.Add(pnlTitulos, 0, 0);
            tlpPrincipal.Controls.Add(tlpBusqueda, 0, 2);
            tlpPrincipal.Controls.Add(dgvCompras, 0, 4);
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
            pnlTitulos.Controls.Add(lblTitulo);
            pnlTitulos.Dock = DockStyle.Fill;
            pnlTitulos.Location = new Point(18, 12);
            pnlTitulos.Margin = new Padding(0);
            pnlTitulos.Name = "pnlTitulos";
            pnlTitulos.Size = new Size(650, 48);
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.Black;
            lblTitulo.Location = new Point(0, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Text = "Consulta de compras";
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
            txtBuscar.Dock = DockStyle.Fill;
            txtBuscar.Font = new Font("Segoe UI", 10F);
            txtBuscar.Location = new Point(0, 8);
            txtBuscar.Margin = new Padding(0, 8, 9, 8);
            txtBuscar.MaxLength = 150;
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(405, 25);
            txtBuscar.KeyDown += txtBuscar_KeyDown;
            btnBuscar.BackColor = Color.Black;
            btnBuscar.Cursor = Cursors.Hand;
            btnBuscar.Dock = DockStyle.Fill;
            btnBuscar.FlatAppearance.BorderSize = 0;
            btnBuscar.FlatStyle = FlatStyle.Flat;
            btnBuscar.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            btnBuscar.ForeColor = Color.White;
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Text = "BUSCAR";
            btnBuscar.UseVisualStyleBackColor = false;
            btnBuscar.Location = new Point(414, 4);
            btnBuscar.Margin = new Padding(0, 4, 4, 4);
            btnBuscar.Size = new Size(114, 45);
            btnBuscar.Click += btnBuscar_Click;
            btnLimpiar.BackColor = Color.White;
            btnLimpiar.Cursor = Cursors.Hand;
            btnLimpiar.Dock = DockStyle.Fill;
            btnLimpiar.FlatAppearance.BorderSize = 1;
            btnLimpiar.FlatStyle = FlatStyle.Flat;
            btnLimpiar.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            btnLimpiar.ForeColor = Color.Black;
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Text = "LIMPIAR";
            btnLimpiar.UseVisualStyleBackColor = false;
            btnLimpiar.FlatAppearance.BorderColor = Color.Black;
            btnLimpiar.Location = new Point(536, 4);
            btnLimpiar.Margin = new Padding(4, 4, 0, 4);
            btnLimpiar.Size = new Size(114, 45);
            btnLimpiar.Click += btnLimpiar_Click;
            dgvCompras.AllowUserToAddRows = false;
            dgvCompras.AllowUserToDeleteRows = false;
            dgvCompras.AllowUserToResizeRows = false;
            dgvCompras.AutoGenerateColumns = false;
            dgvCompras.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCompras.BackgroundColor = Color.White;
            dgvCompras.BorderStyle = BorderStyle.Fixed3D;
            dgvCompras.Columns.AddRange(new DataGridViewColumn[] { colFecha, colProveedor, colTotal, colEstado });
            dgvCompras.Dock = DockStyle.Fill;
            dgvCompras.Location = new Point(18, 139);
            dgvCompras.Margin = new Padding(0);
            dgvCompras.MultiSelect = false;
            dgvCompras.Name = "dgvCompras";
            dgvCompras.ReadOnly = true;
            dgvCompras.RowHeadersVisible = false;
            dgvCompras.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCompras.Size = new Size(650, 376);
            colFecha.DataPropertyName = "Fecha";
            colFecha.HeaderText = "Fecha";
            colFecha.Name = "colFecha";
            colFecha.ReadOnly = true;
            colProveedor.DataPropertyName = "Proveedor";
            colProveedor.HeaderText = "Proveedor";
            colProveedor.Name = "colProveedor";
            colProveedor.ReadOnly = true;
            colTotal.DataPropertyName = "Total";
            colTotal.HeaderText = "Total";
            colTotal.Name = "colTotal";
            colTotal.ReadOnly = true;
            colEstado.DataPropertyName = "Estado";
            colEstado.HeaderText = "Estado";
            colEstado.Name = "colEstado";
            colEstado.ReadOnly = true;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(686, 533);
            Controls.Add(tlpPrincipal);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.None;
            MinimumSize = new Size(686, 533);
            Name = "FrmConsultaCompras";
            Text = "Consulta de compras";
            tlpPrincipal.ResumeLayout(false);
            pnlTitulos.ResumeLayout(false);
            pnlTitulos.PerformLayout();
            tlpBusqueda.ResumeLayout(false);
            tlpBusqueda.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCompras).EndInit();
            ResumeLayout(false);
        }
    }
}

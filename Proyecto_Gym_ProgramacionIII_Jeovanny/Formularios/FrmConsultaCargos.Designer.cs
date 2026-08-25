namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    partial class FrmConsultaCargos
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel tlpPrincipal;
        private Panel pnlTitulos;
        private Label lblTitulo;
        private TableLayoutPanel tlpBusqueda;
        private TextBox txtBuscar;
        private Button btnBuscar;
        private Button btnLimpiar;
        private DataGridView dgvCargos;
        private DataGridViewTextBoxColumn colCliente;
        private DataGridViewTextBoxColumn colConcepto;
        private DataGridViewTextBoxColumn colMonto;
        private DataGridViewTextBoxColumn colFechaVencimiento;
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
            dgvCargos = new DataGridView();
            colCliente = new DataGridViewTextBoxColumn();
            colConcepto = new DataGridViewTextBoxColumn();
            colMonto = new DataGridViewTextBoxColumn();
            colFechaVencimiento = new DataGridViewTextBoxColumn();
            colEstado = new DataGridViewTextBoxColumn();
            tlpPrincipal.SuspendLayout();
            pnlTitulos.SuspendLayout();
            tlpBusqueda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCargos).BeginInit();
            SuspendLayout();
            tlpPrincipal.BackColor = Color.White;
            tlpPrincipal.ColumnCount = 1;
            tlpPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpPrincipal.Controls.Add(pnlTitulos, 0, 0);
            tlpPrincipal.Controls.Add(tlpBusqueda, 0, 2);
            tlpPrincipal.Controls.Add(dgvCargos, 0, 4);
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
            lblTitulo.Text = "Consulta de cargos";
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
            dgvCargos.AllowUserToAddRows = false;
            dgvCargos.AllowUserToDeleteRows = false;
            dgvCargos.AllowUserToResizeRows = false;
            dgvCargos.AutoGenerateColumns = false;
            dgvCargos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCargos.BackgroundColor = Color.White;
            dgvCargos.BorderStyle = BorderStyle.Fixed3D;
            dgvCargos.Columns.AddRange(new DataGridViewColumn[] { colCliente, colConcepto, colMonto, colFechaVencimiento, colEstado });
            dgvCargos.Dock = DockStyle.Fill;
            dgvCargos.Location = new Point(18, 139);
            dgvCargos.Margin = new Padding(0);
            dgvCargos.MultiSelect = false;
            dgvCargos.Name = "dgvCargos";
            dgvCargos.ReadOnly = true;
            dgvCargos.RowHeadersVisible = false;
            dgvCargos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCargos.Size = new Size(650, 376);
            colCliente.DataPropertyName = "Cliente";
            colCliente.HeaderText = "Cliente";
            colCliente.Name = "colCliente";
            colCliente.ReadOnly = true;
            colConcepto.DataPropertyName = "Concepto";
            colConcepto.HeaderText = "Membresía";
            colConcepto.Name = "colConcepto";
            colConcepto.ReadOnly = true;
            colMonto.DataPropertyName = "Monto";
            colMonto.HeaderText = "Monto";
            colMonto.Name = "colMonto";
            colMonto.ReadOnly = true;
            colFechaVencimiento.DataPropertyName = "FechaVencimiento";
            colFechaVencimiento.HeaderText = "Fecha de vencimiento";
            colFechaVencimiento.Name = "colFechaVencimiento";
            colFechaVencimiento.ReadOnly = true;
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
            Name = "FrmConsultaCargos";
            Text = "Consulta de cargos";
            tlpPrincipal.ResumeLayout(false);
            pnlTitulos.ResumeLayout(false);
            pnlTitulos.PerformLayout();
            tlpBusqueda.ResumeLayout(false);
            tlpBusqueda.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCargos).EndInit();
            ResumeLayout(false);
        }
    }
}

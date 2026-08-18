namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    partial class FrmConsultaClases
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel tlpPrincipal;
        private Panel pnlTitulos;
        private Label lblTitulo;
        private TableLayoutPanel tlpBusqueda;
        private TextBox txtBuscar;
        private Button btnBuscar;
        private Button btnLimpiar;
        private DataGridView dgvClases;
        private DataGridViewTextBoxColumn colNombre;
        private DataGridViewTextBoxColumn colDescripcion;
        private DataGridViewTextBoxColumn colCupoMaximo;
        private DataGridViewTextBoxColumn colEstadoTexto;

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
            dgvClases = new DataGridView();
            colNombre = new DataGridViewTextBoxColumn();
            colDescripcion = new DataGridViewTextBoxColumn();
            colCupoMaximo = new DataGridViewTextBoxColumn();
            colEstadoTexto = new DataGridViewTextBoxColumn();
            tlpPrincipal.SuspendLayout();
            pnlTitulos.SuspendLayout();
            tlpBusqueda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvClases).BeginInit();
            SuspendLayout();
            tlpPrincipal.BackColor = Color.White;
            tlpPrincipal.ColumnCount = 1;
            tlpPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpPrincipal.Controls.Add(pnlTitulos, 0, 0);
            tlpPrincipal.Controls.Add(tlpBusqueda, 0, 2);
            tlpPrincipal.Controls.Add(dgvClases, 0, 4);
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
            lblTitulo.Text = "Consulta de clases";
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
            txtBuscar.MaxLength = 200;
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
            dgvClases.AllowUserToAddRows = false;
            dgvClases.AllowUserToDeleteRows = false;
            dgvClases.AllowUserToResizeRows = false;
            dgvClases.AutoGenerateColumns = false;
            dgvClases.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvClases.BackgroundColor = Color.White;
            dgvClases.BorderStyle = BorderStyle.Fixed3D;
            dgvClases.Columns.AddRange(new DataGridViewColumn[] { colNombre, colDescripcion, colCupoMaximo, colEstadoTexto });
            dgvClases.Dock = DockStyle.Fill;
            dgvClases.Location = new Point(18, 139);
            dgvClases.Margin = new Padding(0);
            dgvClases.MultiSelect = false;
            dgvClases.Name = "dgvClases";
            dgvClases.ReadOnly = true;
            dgvClases.RowHeadersVisible = false;
            dgvClases.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvClases.Size = new Size(650, 376);
            colNombre.DataPropertyName = "Nombre";
            colNombre.FillWeight = 25F;
            colNombre.HeaderText = "Nombre";
            colNombre.Name = "colNombre";
            colNombre.ReadOnly = true;
            colNombre.SortMode = DataGridViewColumnSortMode.Automatic;
            colDescripcion.DataPropertyName = "Descripcion";
            colDescripcion.FillWeight = 45F;
            colDescripcion.HeaderText = "Descripción";
            colDescripcion.Name = "colDescripcion";
            colDescripcion.ReadOnly = true;
            colDescripcion.SortMode = DataGridViewColumnSortMode.Automatic;
            colCupoMaximo.DataPropertyName = "CupoMaximo";
            colCupoMaximo.FillWeight = 15F;
            colCupoMaximo.HeaderText = "Cupo máximo";
            colCupoMaximo.Name = "colCupoMaximo";
            colCupoMaximo.ReadOnly = true;
            colCupoMaximo.SortMode = DataGridViewColumnSortMode.Automatic;
            colEstadoTexto.DataPropertyName = "EstadoTexto";
            colEstadoTexto.FillWeight = 15F;
            colEstadoTexto.HeaderText = "Estado";
            colEstadoTexto.Name = "colEstadoTexto";
            colEstadoTexto.ReadOnly = true;
            colEstadoTexto.SortMode = DataGridViewColumnSortMode.Automatic;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(686, 533);
            Controls.Add(tlpPrincipal);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.None;
            MinimumSize = new Size(686, 533);
            Name = "FrmConsultaClases";
            Text = "Consulta de clases";
            Load += FrmConsultaClases_Load;
            tlpPrincipal.ResumeLayout(false);
            pnlTitulos.ResumeLayout(false);
            pnlTitulos.PerformLayout();
            tlpBusqueda.ResumeLayout(false);
            tlpBusqueda.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvClases).EndInit();
            ResumeLayout(false);
        }
    }
}

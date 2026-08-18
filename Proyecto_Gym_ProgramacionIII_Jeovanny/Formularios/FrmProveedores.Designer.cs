namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    partial class FrmProveedores
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitulo;
        private Button btnNuevo;
        private TextBox txtBuscar;
        private Button btnBuscar;
        private Button btnLimpiar;
        private DataGridView dgvProveedores;
        private Button btnActualizar;
        private Button btnEditar;

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
            lblTitulo = new Label();
            btnNuevo = new Button();
            txtBuscar = new TextBox();
            btnBuscar = new Button();
            btnLimpiar = new Button();
            dgvProveedores = new DataGridView();
            btnActualizar = new Button();
            btnEditar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvProveedores).BeginInit();
            SuspendLayout();
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitulo.Location = new Point(18, 12);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(185, 37);
            lblTitulo.Text = "Proveedores";
            btnNuevo.BackColor = Color.FromArgb(124, 142, 163);
            btnNuevo.FlatAppearance.BorderSize = 0;
            btnNuevo.FlatStyle = FlatStyle.Flat;
            btnNuevo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnNuevo.ForeColor = Color.White;
            btnNuevo.Location = new Point(527, 18);
            btnNuevo.Size = new Size(145, 36);
            btnNuevo.Text = "NUEVO";
            btnNuevo.UseVisualStyleBackColor = false;
            btnNuevo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Click += btnNuevo_Click;
            txtBuscar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtBuscar.Font = new Font("Segoe UI", 10F);
            txtBuscar.Location = new Point(18, 70);
            txtBuscar.MaxLength = 120;
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(430, 25);
            txtBuscar.KeyDown += txtBuscar_KeyDown;
            btnBuscar.BackColor = Color.Black;
            btnBuscar.FlatAppearance.BorderSize = 0;
            btnBuscar.FlatStyle = FlatStyle.Flat;
            btnBuscar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnBuscar.ForeColor = Color.White;
            btnBuscar.Location = new Point(458, 66);
            btnBuscar.Size = new Size(100, 36);
            btnBuscar.Text = "BUSCAR";
            btnBuscar.UseVisualStyleBackColor = false;
            btnBuscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Click += btnBuscar_Click;
            btnLimpiar.BackColor = Color.WhiteSmoke;
            btnLimpiar.FlatAppearance.BorderSize = 0;
            btnLimpiar.FlatStyle = FlatStyle.Flat;
            btnLimpiar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnLimpiar.ForeColor = Color.Black;
            btnLimpiar.Location = new Point(568, 66);
            btnLimpiar.Size = new Size(104, 36);
            btnLimpiar.Text = "LIMPIAR";
            btnLimpiar.UseVisualStyleBackColor = false;
            btnLimpiar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Click += btnLimpiar_Click;
            dgvProveedores.AllowUserToAddRows = false;
            dgvProveedores.AllowUserToDeleteRows = false;
            dgvProveedores.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvProveedores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProveedores.BackgroundColor = Color.White;
            dgvProveedores.Location = new Point(18, 113);
            dgvProveedores.MultiSelect = false;
            dgvProveedores.Name = "dgvProveedores";
            dgvProveedores.ReadOnly = true;
            dgvProveedores.RowHeadersVisible = false;
            dgvProveedores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProveedores.Size = new Size(654, 302);
            dgvProveedores.SelectionChanged += dgvProveedores_SelectionChanged;
            btnActualizar.BackColor = Color.Black;
            btnActualizar.FlatAppearance.BorderSize = 0;
            btnActualizar.FlatStyle = FlatStyle.Flat;
            btnActualizar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnActualizar.ForeColor = Color.White;
            btnActualizar.Location = new Point(422, 427);
            btnActualizar.Size = new Size(120, 36);
            btnActualizar.Text = "ACTUALIZAR";
            btnActualizar.UseVisualStyleBackColor = false;
            btnActualizar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Click += btnActualizar_Click;
            btnEditar.BackColor = Color.FromArgb(124, 142, 163);
            btnEditar.FlatAppearance.BorderSize = 0;
            btnEditar.FlatStyle = FlatStyle.Flat;
            btnEditar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnEditar.ForeColor = Color.White;
            btnEditar.Location = new Point(552, 427);
            btnEditar.Size = new Size(120, 36);
            btnEditar.Text = "EDITAR";
            btnEditar.UseVisualStyleBackColor = false;
            btnEditar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnEditar.Name = "btnEditar";
            btnEditar.Click += btnEditar_Click;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(690, 475);
            Controls.Add(lblTitulo);
            Controls.Add(btnNuevo);
            Controls.Add(txtBuscar);
            Controls.Add(btnBuscar);
            Controls.Add(btnLimpiar);
            Controls.Add(dgvProveedores);
            Controls.Add(btnActualizar);
            Controls.Add(btnEditar);
            FormBorderStyle = FormBorderStyle.None;
            MinimumSize = new Size(600, 400);
            Name = "FrmProveedores";
            Text = "Proveedores";
            Load += FrmProveedores_Load;
            ((System.ComponentModel.ISupportInitialize)dgvProveedores).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}

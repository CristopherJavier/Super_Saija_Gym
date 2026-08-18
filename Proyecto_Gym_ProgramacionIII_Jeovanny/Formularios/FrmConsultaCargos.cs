namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    public partial class FrmConsultaCargos : Form
    {
        public FrmConsultaCargos()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object? sender, EventArgs e)
        {
            dgvCargos.DataSource = null;
        }

        private void btnLimpiar_Click(object? sender, EventArgs e)
        {
            txtBuscar.Clear();
            dgvCargos.DataSource = null;
            txtBuscar.Focus();
        }

        private void txtBuscar_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                dgvCargos.DataSource = null;
            }
        }
    }
}

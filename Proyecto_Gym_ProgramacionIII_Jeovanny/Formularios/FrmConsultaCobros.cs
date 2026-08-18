namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    public partial class FrmConsultaCobros : Form
    {
        public FrmConsultaCobros()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object? sender, EventArgs e)
        {
            dgvCobros.DataSource = null;
        }

        private void btnLimpiar_Click(object? sender, EventArgs e)
        {
            txtBuscar.Clear();
            dgvCobros.DataSource = null;
            txtBuscar.Focus();
        }

        private void txtBuscar_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                dgvCobros.DataSource = null;
            }
        }
    }
}

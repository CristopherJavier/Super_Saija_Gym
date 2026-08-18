namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    public partial class FrmConsultaVentas : Form
    {
        public FrmConsultaVentas()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object? sender, EventArgs e)
        {
            dgvVentas.DataSource = null;
        }

        private void btnLimpiar_Click(object? sender, EventArgs e)
        {
            txtBuscar.Clear();
            dgvVentas.DataSource = null;
            txtBuscar.Focus();
        }

        private void txtBuscar_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                dgvVentas.DataSource = null;
            }
        }
    }
}

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    public partial class FrmConsultaCompras : Form
    {
        public FrmConsultaCompras()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object? sender, EventArgs e)
        {
            dgvCompras.DataSource = null;
        }

        private void btnLimpiar_Click(object? sender, EventArgs e)
        {
            txtBuscar.Clear();
            dgvCompras.DataSource = null;
            txtBuscar.Focus();
        }

        private void txtBuscar_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                dgvCompras.DataSource = null;
            }
        }
    }
}

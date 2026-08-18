using Npgsql;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Datos;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    public partial class FrmTiposMembresias : Form
    {
        public FrmTiposMembresias()
        {
            InitializeComponent();
        }

        private async void FrmTiposMembresias_Load(object? sender, EventArgs e)
        {
            await CargarAsync();
        }

        private async Task CargarAsync(string texto = "")
        {
            try
            {
                dgvTiposMembresias.DataSource = await TipoMembresiaRepositorio.ListarAsync(texto);
                ConfigurarColumnas();
                ActualizarBotones();
            }
            catch (NpgsqlException)
            {
                MostrarError("No fue posible realizar la operación en la base de datos.");
            }
            catch (Exception)
            {
                MostrarError("Ocurrió un error al realizar la operación.");
            }
        }

        private void ConfigurarColumnas()
        {
            if (dgvTiposMembresias.Columns.Count == 0)
            {
                return;
            }

            dgvTiposMembresias.Columns[nameof(TipoMembresia.IdTipoMembresia)]!.Visible = false;
            dgvTiposMembresias.Columns[nameof(TipoMembresia.Nombre)]!.HeaderText = "Nombre";
            dgvTiposMembresias.Columns[nameof(TipoMembresia.Descripcion)]!.HeaderText = "Descripción";
            dgvTiposMembresias.Columns[nameof(TipoMembresia.DuracionDias)]!.HeaderText = "Duración (días)";
            dgvTiposMembresias.Columns[nameof(TipoMembresia.Precio)]!.HeaderText = "Precio";
            dgvTiposMembresias.Columns[nameof(TipoMembresia.Precio)]!.DefaultCellStyle.Format = "N2";
            dgvTiposMembresias.Columns[nameof(TipoMembresia.Estado)]!.Visible = false;
            dgvTiposMembresias.Columns[nameof(TipoMembresia.EstadoTexto)]!.HeaderText = "Estado";
        }

        private TipoMembresia? ObtenerSeleccionado()
        {
            return dgvTiposMembresias.CurrentRow?.DataBoundItem as TipoMembresia;
        }

        private void ActualizarBotones()
        {
            TipoMembresia? tipo = ObtenerSeleccionado();
            btnEditar.Enabled = tipo is not null;
        }

        private async void btnNuevo_Click(object? sender, EventArgs e)
        {
            using FrmTipoMembresiaDetalle formulario = new FrmTipoMembresiaDetalle();
            if (formulario.ShowDialog(this) == DialogResult.OK)
            {
                await CargarAsync();
            }
        }

        private async void btnEditar_Click(object? sender, EventArgs e)
        {
            TipoMembresia? tipo = ObtenerSeleccionado();
            if (tipo is null)
            {
                return;
            }

            using FrmTipoMembresiaDetalle formulario = new FrmTipoMembresiaDetalle(tipo);
            if (formulario.ShowDialog(this) == DialogResult.OK)
            {
                await CargarAsync();
            }
        }

        private async void btnActualizar_Click(object? sender, EventArgs e)
        {
            await CargarAsync();
        }

        private async void btnBuscar_Click(object? sender, EventArgs e)
        {
            await CargarAsync(txtBuscar.Text.Trim());
        }

        private async void btnLimpiar_Click(object? sender, EventArgs e)
        {
            txtBuscar.Clear();
            await CargarAsync();
            txtBuscar.Focus();
        }

        private async void txtBuscar_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                await CargarAsync(txtBuscar.Text.Trim());
            }
        }

        private void dgvTiposMembresias_SelectionChanged(object? sender, EventArgs e)
        {
            ActualizarBotones();
        }

        private static void MostrarError(string mensaje)
        {
            MessageBox.Show(mensaje, "Tipos de membresías", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}

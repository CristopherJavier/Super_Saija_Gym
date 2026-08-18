using Npgsql;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Datos;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    public partial class FrmClasesActividades : Form
    {
        public FrmClasesActividades()
        {
            InitializeComponent();
        }

        private async Task CargarAsync(string texto = "")
        {
            try
            {
                dgvClases.DataSource = await ClaseActividadRepositorio.ListarAsync(texto);
                if (dgvClases.Columns.Count > 0)
                {
                    dgvClases.Columns[nameof(ClaseActividad.IdClase)]!.Visible = false;
                    dgvClases.Columns[nameof(ClaseActividad.Nombre)]!.HeaderText = "Nombre";
                    dgvClases.Columns[nameof(ClaseActividad.Descripcion)]!.HeaderText = "Descripción";
                    dgvClases.Columns[nameof(ClaseActividad.CupoMaximo)]!.HeaderText = "Cupo máximo";
                    dgvClases.Columns[nameof(ClaseActividad.Estado)]!.Visible = false;
                    dgvClases.Columns[nameof(ClaseActividad.EstadoTexto)]!.HeaderText = "Estado";
                }

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

        private ClaseActividad? ObtenerSeleccionada()
        {
            return dgvClases.CurrentRow?.DataBoundItem as ClaseActividad;
        }

        private void ActualizarBotones()
        {
            ClaseActividad? clase = ObtenerSeleccionada();
            btnEditar.Enabled = clase is not null;
        }

        private async void FrmClasesActividades_Load(object? sender, EventArgs e)
        {
            await CargarAsync();
        }

        private async void btnNuevo_Click(object? sender, EventArgs e)
        {
            using FrmClaseActividadDetalle formulario = new FrmClaseActividadDetalle();
            if (formulario.ShowDialog(this) == DialogResult.OK)
            {
                await CargarAsync();
            }
        }

        private async void btnEditar_Click(object? sender, EventArgs e)
        {
            ClaseActividad? clase = ObtenerSeleccionada();
            if (clase is null)
            {
                return;
            }

            using FrmClaseActividadDetalle formulario = new FrmClaseActividadDetalle(clase);
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

        private void dgvClases_SelectionChanged(object? sender, EventArgs e)
        {
            ActualizarBotones();
        }

        private static void MostrarError(string mensaje)
        {
            MessageBox.Show(mensaje, "Clases y actividades", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}

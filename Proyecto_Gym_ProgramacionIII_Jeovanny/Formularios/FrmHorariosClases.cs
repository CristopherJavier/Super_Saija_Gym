using Npgsql;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Datos;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    public partial class FrmHorariosClases : Form
    {
        public FrmHorariosClases()
        {
            InitializeComponent();
        }

        private async Task CargarAsync(string texto = "")
        {
            try
            {
                dgvHorarios.DataSource = await HorarioClaseRepositorio.ListarAsync(texto);
                if (dgvHorarios.Columns.Count > 0)
                {
                    dgvHorarios.Columns[nameof(HorarioClase.IdHorario)]!.Visible = false;
                    dgvHorarios.Columns[nameof(HorarioClase.NombreClase)]!.HeaderText = "Clase";
                    dgvHorarios.Columns[nameof(HorarioClase.NombreEntrenador)]!.HeaderText = "Entrenador";
                    dgvHorarios.Columns[nameof(HorarioClase.DiaSemana)]!.HeaderText = "Día";
                    dgvHorarios.Columns[nameof(HorarioClase.HoraInicio)]!.HeaderText = "Inicio";
                    dgvHorarios.Columns[nameof(HorarioClase.HoraFin)]!.HeaderText = "Fin";
                    dgvHorarios.Columns[nameof(HorarioClase.Estado)]!.Visible = false;
                    dgvHorarios.Columns[nameof(HorarioClase.EstadoTexto)]!.HeaderText = "Estado";
                    dgvHorarios.Columns[nameof(HorarioClase.IdClase)]!.Visible = false;
                    dgvHorarios.Columns[nameof(HorarioClase.IdEntrenador)]!.Visible = false;
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

        private HorarioClase? ObtenerSeleccionado()
        {
            return dgvHorarios.CurrentRow?.DataBoundItem as HorarioClase;
        }

        private void ActualizarBotones()
        {
            HorarioClase? horario = ObtenerSeleccionado();
            btnEditar.Enabled = horario is not null;
        }

        private async void FrmHorariosClases_Load(object? sender, EventArgs e)
        {
            await CargarAsync();
        }

        private async void btnNuevo_Click(object? sender, EventArgs e)
        {
            using FrmHorarioClaseDetalle formulario = new FrmHorarioClaseDetalle();
            if (formulario.ShowDialog(this) == DialogResult.OK)
            {
                await CargarAsync();
            }
        }

        private async void btnEditar_Click(object? sender, EventArgs e)
        {
            HorarioClase? horario = ObtenerSeleccionado();
            if (horario is null)
            {
                return;
            }

            using FrmHorarioClaseDetalle formulario = new FrmHorarioClaseDetalle(horario);
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

        private void dgvHorarios_SelectionChanged(object? sender, EventArgs e)
        {
            ActualizarBotones();
        }

        private void dgvHorarios_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.Value is null)
            {
                return;
            }

            string propiedad = dgvHorarios.Columns[e.ColumnIndex].DataPropertyName;
            if ((propiedad == nameof(HorarioClase.HoraInicio) || propiedad == nameof(HorarioClase.HoraFin)) && e.Value is TimeSpan hora)
            {
                e.Value = DateTime.Today.Add(hora).ToString("hh:mm tt");
                e.FormattingApplied = true;
            }
        }

        private static void MostrarError(string mensaje)
        {
            MessageBox.Show(mensaje, "Horarios de clases", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}

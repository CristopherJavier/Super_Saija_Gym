using Npgsql;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Datos;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    public partial class FrmHorarioClaseDetalle : Form
    {
        private readonly HorarioClase? horarioEditar;

        public FrmHorarioClaseDetalle()
        {
            InitializeComponent();
        }

        public FrmHorarioClaseDetalle(HorarioClase horario)
        {
            horarioEditar = horario;
            InitializeComponent();
            Text = "Editar horario";
            lblTitulo.Text = Text;
        }

        private async void FrmHorarioClaseDetalle_Load(object? sender, EventArgs e)
        {
            try
            {
                List<ClaseActividad> clases = await ClaseActividadRepositorio.ListarAsync();
                List<Entrenador> entrenadores = await EntrenadorRepositorio.ListarAsync();
                clases = clases.Where(x => x.Estado || x.IdClase == horarioEditar?.IdClase).ToList();
                entrenadores = entrenadores.Where(x => x.Estado || x.IdEntrenador == horarioEditar?.IdEntrenador).ToList();
                cmbClase.DataSource = clases;
                cmbEntrenador.DataSource = entrenadores;

                if (clases.Count == 0 || entrenadores.Count == 0)
                {
                    lblMensaje.Text = "Debes registrar una clase y un entrenador activos antes de crear un horario.";
                    btnGuardar.Enabled = false;
                    return;
                }

                if (horarioEditar is null)
                {
                    cmbDiaSemana.SelectedIndex = 0;
                    dtpHoraInicio.Value = DateTime.Today.AddHours(8);
                    dtpHoraFin.Value = DateTime.Today.AddHours(9);
                    return;
                }

                cmbClase.SelectedItem = clases.FirstOrDefault(x => x.IdClase == horarioEditar.IdClase);
                cmbEntrenador.SelectedItem = entrenadores.FirstOrDefault(x => x.IdEntrenador == horarioEditar.IdEntrenador);
                cmbDiaSemana.SelectedItem = horarioEditar.DiaSemana;
                dtpHoraInicio.Value = DateTime.Today.Add(horarioEditar.HoraInicio);
                dtpHoraFin.Value = DateTime.Today.Add(horarioEditar.HoraFin);
                chkEstado.Checked = horarioEditar.Estado;
            }
            catch (NpgsqlException)
            {
                lblMensaje.Text = "No fue posible cargar las clases y los entrenadores.";
                btnGuardar.Enabled = false;
            }
        }

        private async void btnGuardar_Click(object? sender, EventArgs e)
        {
            lblMensaje.Text = string.Empty;
            if (cmbClase.SelectedItem is not ClaseActividad clase || cmbEntrenador.SelectedItem is not Entrenador entrenador || cmbDiaSemana.SelectedItem is null)
            {
                lblMensaje.Text = "Selecciona la clase, el entrenador y el día.";
                return;
            }

            TimeSpan horaInicio = dtpHoraInicio.Value.TimeOfDay;
            TimeSpan horaFin = dtpHoraFin.Value.TimeOfDay;
            if (horaFin <= horaInicio)
            {
                lblMensaje.Text = "La hora de fin debe ser posterior a la hora de inicio.";
                return;
            }

            btnGuardar.Enabled = false;
            try
            {
                int id = horarioEditar?.IdHorario ?? 0;
                string dia = cmbDiaSemana.SelectedItem.ToString()!;
                if (await HorarioClaseRepositorio.ExisteAsync(clase.IdClase, dia, horaInicio, id))
                {
                    lblMensaje.Text = "Ya existe ese horario para la clase seleccionada.";
                    return;
                }

                HorarioClase horario = new HorarioClase
                {
                    IdHorario = id,
                    IdClase = clase.IdClase,
                    IdEntrenador = entrenador.IdEntrenador,
                    DiaSemana = dia,
                    HoraInicio = horaInicio,
                    HoraFin = horaFin,
                    Estado = chkEstado.Checked
                };

                if (horarioEditar is null)
                {
                    await HorarioClaseRepositorio.GuardarAsync(horario);
                }
                else
                {
                    await HorarioClaseRepositorio.ActualizarAsync(horario);
                }

                MessageBox.Show("Horario guardado correctamente.", "Horarios de clases", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (NpgsqlException)
            {
                lblMensaje.Text = "No fue posible realizar la operación en la base de datos.";
            }
            catch (Exception)
            {
                lblMensaje.Text = "Ocurrió un error al realizar la operación.";
            }
            finally
            {
                btnGuardar.Enabled = true;
            }
        }
    }
}

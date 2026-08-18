using Npgsql;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Datos;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    public partial class FrmEntrenadores : Form
    {
        public FrmEntrenadores()
        {
            InitializeComponent();
        }

        private async void FrmEntrenadores_Load(object? sender, EventArgs e)
        {
            await CargarAsync();
        }

        private async Task CargarAsync(string texto = "")
        {
            try
            {
                List<Entrenador> datos = string.IsNullOrWhiteSpace(texto)
                    ? await EntrenadorRepositorio.ListarAsync()
                    : await EntrenadorRepositorio.BuscarAsync(texto);
                dgvEntrenadores.DataSource = null;
                dgvEntrenadores.DataSource = datos;
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
            if (dgvEntrenadores.Columns.Count == 0)
            {
                return;
            }

            dgvEntrenadores.Columns[nameof(Entrenador.IdEntrenador)]!.Visible = false;
            dgvEntrenadores.Columns[nameof(Entrenador.Nombre)]!.HeaderText = "Nombre";
            dgvEntrenadores.Columns[nameof(Entrenador.Apellido)]!.HeaderText = "Apellido";
            dgvEntrenadores.Columns[nameof(Entrenador.Cedula)]!.HeaderText = "Cédula";
            dgvEntrenadores.Columns[nameof(Entrenador.Especialidad)]!.HeaderText = "Especialidad";
            dgvEntrenadores.Columns[nameof(Entrenador.Estado)]!.Visible = false;
            dgvEntrenadores.Columns[nameof(Entrenador.EstadoTexto)]!.HeaderText = "Estado";
            dgvEntrenadores.Columns[nameof(Entrenador.Telefono)]!.Visible = false;
            dgvEntrenadores.Columns[nameof(Entrenador.Correo)]!.Visible = false;
            dgvEntrenadores.Columns[nameof(Entrenador.FechaContratacion)]!.Visible = false;
        }

        private Entrenador? ObtenerSeleccionado()
        {
            return dgvEntrenadores.CurrentRow?.DataBoundItem as Entrenador;
        }

        private void ActualizarBotones()
        {
            Entrenador? entrenador = ObtenerSeleccionado();
            btnEditar.Enabled = entrenador is not null;
        }

        private async void btnNuevo_Click(object? sender, EventArgs e)
        {
            using FrmEntrenadorDetalle formulario = new FrmEntrenadorDetalle();
            if (formulario.ShowDialog(this) == DialogResult.OK)
            {
                await CargarAsync();
            }
        }

        private async void btnEditar_Click(object? sender, EventArgs e)
        {
            Entrenador? entrenador = ObtenerSeleccionado();
            if (entrenador is null)
            {
                return;
            }

            using FrmEntrenadorDetalle formulario = new FrmEntrenadorDetalle(entrenador);
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

        private void dgvEntrenadores_SelectionChanged(object? sender, EventArgs e)
        {
            ActualizarBotones();
        }

        private static void MostrarError(string mensaje)
        {
            MessageBox.Show(mensaje, "Entrenadores", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}

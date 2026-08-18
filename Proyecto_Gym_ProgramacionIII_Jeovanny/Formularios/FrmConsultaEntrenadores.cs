using Npgsql;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Datos;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    public partial class FrmConsultaEntrenadores : Form
    {
        public FrmConsultaEntrenadores()
        {
            InitializeComponent();
        }

        private async void FrmConsultaEntrenadores_Load(object? sender, EventArgs e)
        {
            await CargarEntrenadoresAsync();
        }

        private async Task CargarEntrenadoresAsync()
        {
            try
            {
                string texto = txtBuscar.Text.Trim();
                List<Entrenador> entrenadores = string.IsNullOrWhiteSpace(texto)
                    ? await EntrenadorRepositorio.ListarAsync()
                    : await EntrenadorRepositorio.BuscarAsync(texto);

                dgvEntrenadores.DataSource = null;
                dgvEntrenadores.DataSource = entrenadores;
                ConfigurarColumnas();
            }
            catch (NpgsqlException)
            {
                MostrarError("No fue posible consultar los entrenadores en la base de datos.");
            }
            catch (Exception)
            {
                MostrarError("Ocurrió un error al consultar los entrenadores.");
            }
        }

        private async void btnBuscar_Click(object? sender, EventArgs e)
        {
            await CargarEntrenadoresAsync();
        }

        private async void btnLimpiar_Click(object? sender, EventArgs e)
        {
            txtBuscar.Clear();
            await CargarEntrenadoresAsync();
            txtBuscar.Focus();
        }

        private void ConfigurarColumnas()
        {
            if (dgvEntrenadores.Columns.Count == 0)
            {
                return;
            }

            dgvEntrenadores.Columns[nameof(Entrenador.IdEntrenador)]!.Visible = false;
            dgvEntrenadores.Columns[nameof(Entrenador.Estado)]!.Visible = false;
            dgvEntrenadores.Columns[nameof(Entrenador.EstadoTexto)]!.HeaderText = "Estado";
        }

        private async void txtBuscar_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                await CargarEntrenadoresAsync();
            }
        }

        private void dgvEntrenadores_CellFormatting(
            object? sender,
            DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.Value is not string valor)
            {
                return;
            }

            string propiedad = dgvEntrenadores.Columns[e.ColumnIndex].DataPropertyName;

            if (propiedad == nameof(Entrenador.Cedula))
            {
                e.Value = FormatearCedula(valor);
                e.FormattingApplied = true;
            }
            else if (propiedad == nameof(Entrenador.Telefono))
            {
                e.Value = FormatearTelefono(valor);
                e.FormattingApplied = true;
            }
        }

        private static string FormatearCedula(string valor)
        {
            string digitos = ExtraerDigitos(valor);
            return digitos.Length == 11
                ? $"{digitos.Substring(0, 3)}-{digitos.Substring(3, 7)}-{digitos.Substring(10, 1)}"
                : valor;
        }

        private static string FormatearTelefono(string valor)
        {
            string digitos = ExtraerDigitos(valor);
            return digitos.Length == 10
                ? $"({digitos.Substring(0, 3)}) {digitos.Substring(3, 3)}-{digitos.Substring(6, 4)}"
                : valor;
        }

        private static string ExtraerDigitos(string texto)
        {
            string digitos = string.Empty;

            foreach (char caracter in texto)
            {
                if (char.IsDigit(caracter))
                {
                    digitos += caracter;
                }
            }

            return digitos;
        }

        private static void MostrarError(string mensaje)
        {
            MessageBox.Show(
                mensaje,
                "Consulta de entrenadores",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }
}

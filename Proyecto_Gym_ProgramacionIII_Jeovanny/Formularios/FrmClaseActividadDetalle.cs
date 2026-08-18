using Npgsql;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Datos;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    public partial class FrmClaseActividadDetalle : Form
    {
        private readonly ClaseActividad? claseEditar;

        public FrmClaseActividadDetalle()
        {
            InitializeComponent();
            txtNombre.KeyPress += txtNombre_KeyPress;
        }

        public FrmClaseActividadDetalle(ClaseActividad clase)
        {
            claseEditar = clase;
            InitializeComponent();
            Text = "Editar clase o actividad";
            lblTitulo.Text = Text;
            txtNombre.KeyPress += txtNombre_KeyPress;
            txtNombre.Text = clase.Nombre;
            txtDescripcion.Text = clase.Descripcion;
            nudCupoMaximo.Value = clase.CupoMaximo;
            chkEstado.Checked = clase.Estado;
        }

        private void txtNombre_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ' && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private async void btnGuardar_Click(object? sender, EventArgs e)
        {
            lblMensaje.Text = string.Empty;
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                lblMensaje.Text = "El nombre es obligatorio.";
                txtNombre.Focus();
                return;
            }

            btnGuardar.Enabled = false;
            try
            {
                int id = claseEditar?.IdClase ?? 0;
                if (await ClaseActividadRepositorio.ExisteNombreAsync(txtNombre.Text, id))
                {
                    lblMensaje.Text = "Ya existe una clase o actividad con ese nombre.";
                    return;
                }

                ClaseActividad clase = new ClaseActividad
                {
                    IdClase = id,
                    Nombre = txtNombre.Text.Trim(),
                    Descripcion = txtDescripcion.Text.Trim(),
                    CupoMaximo = Decimal.ToInt32(nudCupoMaximo.Value),
                    Estado = chkEstado.Checked
                };

                if (claseEditar is null)
                {
                    await ClaseActividadRepositorio.GuardarAsync(clase);
                }
                else
                {
                    await ClaseActividadRepositorio.ActualizarAsync(clase);
                }

                MessageBox.Show("Clase o actividad guardada correctamente.", "Clases y actividades", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

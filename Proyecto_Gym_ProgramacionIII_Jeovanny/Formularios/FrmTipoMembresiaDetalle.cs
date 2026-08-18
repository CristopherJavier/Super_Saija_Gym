using Npgsql;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Datos;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    public partial class FrmTipoMembresiaDetalle : Form
    {
        private readonly TipoMembresia? tipoEditar;

        public FrmTipoMembresiaDetalle()
        {
            InitializeComponent();
            txtNombre.KeyPress += txtNombre_KeyPress;
        }

        public FrmTipoMembresiaDetalle(TipoMembresia tipo)
        {
            tipoEditar = tipo;
            InitializeComponent();
            Text = "Editar tipo de membresía";
            lblTitulo.Text = Text;
            txtNombre.KeyPress += txtNombre_KeyPress;
            CargarTipo();
        }

        private void txtNombre_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ' && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void CargarTipo()
        {
            if (tipoEditar is null)
            {
                return;
            }

            txtNombre.Text = tipoEditar.Nombre;
            txtDescripcion.Text = tipoEditar.Descripcion;
            nudDuracionDias.Value = tipoEditar.DuracionDias;
            nudPrecio.Value = tipoEditar.Precio;
            chkEstado.Checked = tipoEditar.Estado;
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
                int id = tipoEditar?.IdTipoMembresia ?? 0;
                if (await TipoMembresiaRepositorio.ExisteNombreAsync(txtNombre.Text, id))
                {
                    lblMensaje.Text = "Ya existe un tipo de membresía con ese nombre.";
                    return;
                }

                TipoMembresia tipo = new TipoMembresia
                {
                    IdTipoMembresia = id,
                    Nombre = txtNombre.Text.Trim(),
                    Descripcion = txtDescripcion.Text.Trim(),
                    DuracionDias = Decimal.ToInt32(nudDuracionDias.Value),
                    Precio = nudPrecio.Value,
                    Estado = chkEstado.Checked
                };

                if (tipoEditar is null)
                {
                    await TipoMembresiaRepositorio.GuardarAsync(tipo);
                }
                else
                {
                    await TipoMembresiaRepositorio.ActualizarAsync(tipo);
                }

                MessageBox.Show("Tipo de membresía guardado correctamente.", "Tipos de membresías", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

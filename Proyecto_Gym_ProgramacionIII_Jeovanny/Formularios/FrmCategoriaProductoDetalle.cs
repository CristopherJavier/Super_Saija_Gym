using Npgsql;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Datos;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    public partial class FrmCategoriaProductoDetalle : Form
    {
        private readonly CategoriaProducto? categoriaEditar;

        public FrmCategoriaProductoDetalle()
        {
            InitializeComponent();
            txtNombre.KeyPress += txtNombre_KeyPress;
        }

        public FrmCategoriaProductoDetalle(CategoriaProducto categoria)
        {
            categoriaEditar = categoria;
            InitializeComponent();
            Text = "Editar categoría";
            lblTitulo.Text = Text;
            txtNombre.KeyPress += txtNombre_KeyPress;
            txtNombre.Text = categoria.Nombre;
            txtDescripcion.Text = categoria.Descripcion;
            chkEstado.Checked = categoria.Estado;
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
                int id = categoriaEditar?.IdCategoria ?? 0;
                if (await CategoriaProductoRepositorio.ExisteNombreAsync(txtNombre.Text, id))
                {
                    lblMensaje.Text = "Ya existe una categoría con ese nombre.";
                    return;
                }

                CategoriaProducto categoria = new CategoriaProducto
                {
                    IdCategoria = id,
                    Nombre = txtNombre.Text.Trim(),
                    Descripcion = txtDescripcion.Text.Trim(),
                    Estado = chkEstado.Checked
                };

                if (categoriaEditar is null)
                {
                    await CategoriaProductoRepositorio.GuardarAsync(categoria);
                }
                else
                {
                    await CategoriaProductoRepositorio.ActualizarAsync(categoria);
                }

                MessageBox.Show("Categoría guardada correctamente.", "Categorías de productos", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

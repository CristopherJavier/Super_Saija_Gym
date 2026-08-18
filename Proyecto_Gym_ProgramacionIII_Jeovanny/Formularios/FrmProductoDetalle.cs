using Npgsql;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Datos;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    public partial class FrmProductoDetalle : Form
    {
        private readonly Producto? productoEditar;
        private string rutaImagen = string.Empty;

        public FrmProductoDetalle()
        {
            InitializeComponent();
            txtNombre.KeyPress += txtNombre_KeyPress;
        }

        public FrmProductoDetalle(Producto producto)
        {
            productoEditar = producto;
            InitializeComponent();
            Text = "Editar producto";
            lblTitulo.Text = Text;
            rutaImagen = producto.Imagen;
            MostrarVistaPreviaImagen();
            txtNombre.KeyPress += txtNombre_KeyPress;
        }

        private void txtNombre_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ' && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private async void FrmProductoDetalle_Load(object? sender, EventArgs e)
        {
            try
            {
                List<CategoriaProducto> categorias = await CategoriaProductoRepositorio.ListarAsync();
                categorias = categorias.Where(x => x.Estado || x.IdCategoria == productoEditar?.IdCategoria).ToList();
                if (categorias.Count == 0)
                {
                    lblMensaje.Text = "Debes registrar una categoría activa antes de crear un producto.";
                    btnGuardar.Enabled = false;
                    return;
                }
                cmbCategoria.DataSource = categorias;

                if (productoEditar is null) return;
                txtCodigo.Text = productoEditar.Codigo;
                txtNombre.Text = productoEditar.Nombre;
                txtDescripcion.Text = productoEditar.Descripcion;
                cmbCategoria.SelectedItem = categorias.FirstOrDefault(x => x.IdCategoria == productoEditar.IdCategoria);
                nudPrecioCompra.Value = productoEditar.PrecioCompra;
                nudPrecioVenta.Value = productoEditar.PrecioVenta;
                nudStock.Value = productoEditar.Stock;
                nudStockMinimo.Value = productoEditar.StockMinimo;
                chkEstado.Checked = productoEditar.Estado;
            }
            catch (NpgsqlException)
            {
                lblMensaje.Text = "No fue posible cargar las categorías.";
                btnGuardar.Enabled = false;
            }
        }

        private void btnSeleccionarImagen_Click(object? sender, EventArgs e)
        {
            using OpenFileDialog selector = new OpenFileDialog
            {
                CheckFileExists = true,
                Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.bmp",
                Multiselect = false,
                Title = "Seleccionar imagen del producto"
            };
            if (selector.ShowDialog(this) == DialogResult.OK)
            {
                rutaImagen = selector.FileName;
                MostrarVistaPreviaImagen();
            }
        }

        private void btnQuitarImagen_Click(object? sender, EventArgs e)
        {
            rutaImagen = string.Empty;
            LimpiarVistaPreviaImagen();
        }

        private async void btnGuardar_Click(object? sender, EventArgs e)
        {
            lblMensaje.Text = string.Empty;
            if (string.IsNullOrWhiteSpace(txtCodigo.Text) || string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                lblMensaje.Text = "El código y el nombre son obligatorios.";
                return;
            }
            if (cmbCategoria.SelectedItem is not CategoriaProducto categoria)
            {
                lblMensaje.Text = "Selecciona una categoría.";
                return;
            }
            btnGuardar.Enabled = false;
            try
            {
                int id = productoEditar?.IdProducto ?? 0;
                if (await ProductoRepositorio.ExisteCodigoAsync(txtCodigo.Text, id))
                {
                    lblMensaje.Text = "Ya existe un producto con ese código.";
                    return;
                }
                Producto producto = new Producto
                {
                    IdProducto = id,
                    Codigo = txtCodigo.Text.Trim(),
                    Nombre = txtNombre.Text.Trim(),
                    Descripcion = txtDescripcion.Text.Trim(),
                    IdCategoria = categoria.IdCategoria,
                    PrecioCompra = nudPrecioCompra.Value,
                    PrecioVenta = nudPrecioVenta.Value,
                    Stock = Decimal.ToInt32(nudStock.Value),
                    StockMinimo = Decimal.ToInt32(nudStockMinimo.Value),
                    Imagen = rutaImagen,
                    Estado = chkEstado.Checked
                };
                if (productoEditar is null) await ProductoRepositorio.GuardarAsync(producto);
                else await ProductoRepositorio.ActualizarAsync(producto);
                MessageBox.Show("Producto guardado correctamente.", "Productos", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void MostrarVistaPreviaImagen()
        {
            LimpiarVistaPreviaImagen();

            if (string.IsNullOrWhiteSpace(rutaImagen) || !File.Exists(rutaImagen))
            {
                return;
            }

            try
            {
                using Image imagen = Image.FromFile(rutaImagen);
                picImagen.Image = new Bitmap(imagen);
            }
            catch (Exception)
            {
                picImagen.Image = null;
            }
        }

        private void LimpiarVistaPreviaImagen()
        {
            picImagen.Image?.Dispose();
            picImagen.Image = null;
        }

        private void FrmProductoDetalle_FormClosed(object? sender, FormClosedEventArgs e)
        {
            LimpiarVistaPreviaImagen();
        }
    }
}

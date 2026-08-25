using Proyecto_Gym_ProgramacionIII_Jeovanny.Datos;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    public class FrmCompras : FrmModuloBase
    {
        private readonly ComboBox cmbProveedores;
        private readonly ComboBox cmbMetodosPago;
        private readonly ComboBox cmbProductos;
        private readonly NumericUpDown nudCantidad;
        private readonly NumericUpDown nudPrecio;
        private readonly Button btnRegistrar;
        private readonly List<CompraDetalle> detalles = new List<CompraDetalle>();

        public FrmCompras()
            : base("Registro de compras al contado", 150)
        {
            cmbProveedores = CrearCombo();
            cmbMetodosPago = CrearCombo();
            cmbProductos = CrearCombo();
            nudCantidad = CrearNumero(int.MaxValue, 0, 1);
            nudPrecio = CrearNumero(9999999999.99m, 2);

            cmbProveedores.DisplayMember = nameof(Proveedor.Nombre);
            cmbMetodosPago.DisplayMember = nameof(MetodoPago.Nombre);
            cmbProductos.Format += cmbProductos_Format;
            cmbProductos.SelectedIndexChanged += cmbProductos_SelectedIndexChanged;

            AgregarCampo("Proveedor", cmbProveedores, 260);
            AgregarCampo("Método de pago", cmbMetodosPago, 200);
            AgregarCampo("Forma de compra", CrearCampoSoloLectura("CONTADO"), 150);
            AgregarCampo("Producto", cmbProductos, 270);
            AgregarCampo("Cantidad", nudCantidad, 130);
            AgregarCampo("Precio de compra", nudPrecio, 165);

            AgregarBoton("AGREGAR", btnAgregar_Click);
            AgregarBoton("QUITAR", btnQuitar_Click, false);
            btnRegistrar = AgregarBoton("REGISTRAR", btnRegistrar_Click);
            AgregarBoton("LIMPIAR", btnLimpiar_Click, false);
            Load += FrmCompras_Load;
            ActualizarCarrito();
        }

        private async void FrmCompras_Load(object? sender, EventArgs e)
        {
            await CargarListasAsync();
        }

        private async Task CargarListasAsync()
        {
            try
            {
                LimpiarMensaje();
                cmbProveedores.DataSource = (await ProveedorRepositorio.ListarAsync())
                    .Where(x => x.Estado)
                    .ToList();
                cmbMetodosPago.DataSource = await MetodoPagoRepositorio.ListarAsync(soloActivos: true);
                cmbProductos.DataSource = (await ProductoRepositorio.ListarAsync())
                    .Where(x => x.Estado)
                    .ToList();
                btnRegistrar.Enabled = detalles.Count > 0;
                ActualizarPrecio();
            }
            catch (Exception ex)
            {
                MostrarExcepcion(ex);
            }
        }

        private void btnAgregar_Click(object? sender, EventArgs e)
        {
            LimpiarMensaje();

            if (cmbProductos.SelectedItem is not Producto producto)
            {
                lblMensaje.Text = "Debe seleccionar un producto.";
                cmbProductos.Focus();
                return;
            }

            if (detalles.Any(x => x.IdProducto == producto.IdProducto))
            {
                lblMensaje.Text = "El producto seleccionado ya está agregado a la compra.";
                cmbProductos.Focus();
                return;
            }

            int cantidad = Convert.ToInt32(nudCantidad.Value);
            decimal precio = nudPrecio.Value;
            detalles.Add(new CompraDetalle
            {
                IdProducto = producto.IdProducto,
                NombreProducto = producto.Nombre,
                Cantidad = cantidad,
                Precio = precio,
                Subtotal = cantidad * precio
            });
            nudCantidad.Value = 1;
            ActualizarCarrito();
        }

        private void btnQuitar_Click(object? sender, EventArgs e)
        {
            if (dgvDatos.CurrentRow?.DataBoundItem is not CompraDetalle detalle)
            {
                lblMensaje.Text = "Debe seleccionar un producto del detalle.";
                return;
            }

            detalles.RemoveAll(x => x.IdProducto == detalle.IdProducto);
            LimpiarMensaje();
            ActualizarCarrito();
        }

        private async void btnRegistrar_Click(object? sender, EventArgs e)
        {
            LimpiarMensaje();

            if (cmbProveedores.SelectedItem is not Proveedor proveedor)
            {
                lblMensaje.Text = "Debe seleccionar un proveedor.";
                cmbProveedores.Focus();
                return;
            }

            if (cmbMetodosPago.SelectedItem is not MetodoPago metodo)
            {
                lblMensaje.Text = "Debe seleccionar un método de pago.";
                cmbMetodosPago.Focus();
                return;
            }

            if (detalles.Count == 0)
            {
                lblMensaje.Text = "Debe agregar al menos un producto a la compra.";
                return;
            }

            btnRegistrar.Enabled = false;

            try
            {
                Compra compra = new Compra
                {
                    IdProveedor = proveedor.IdProveedor,
                    IdUsuario = SesionActual.IdUsuario,
                    IdMetodoPago = metodo.IdMetodoPago
                };
                await CompraRepositorio.GuardarAsync(compra, detalles);
                MessageBox.Show(
                    $"Compra número {compra.IdCompra} registrada correctamente.",
                    "Registro de compras",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                detalles.Clear();
                ActualizarCarrito();
                await CargarListasAsync();
            }
            catch (Exception ex)
            {
                MostrarExcepcion(ex);
            }
            finally
            {
                btnRegistrar.Enabled = detalles.Count > 0;
            }
        }

        private void btnLimpiar_Click(object? sender, EventArgs e)
        {
            detalles.Clear();
            nudCantidad.Value = 1;
            LimpiarMensaje();
            ActualizarCarrito();
            ActualizarPrecio();
        }

        private void cmbProductos_SelectedIndexChanged(object? sender, EventArgs e)
        {
            ActualizarPrecio();
        }

        private void cmbProductos_Format(object? sender, ListControlConvertEventArgs e)
        {
            if (e.ListItem is Producto producto)
            {
                e.Value = $"{producto.Codigo} - {producto.Nombre}";
            }
        }

        private void ActualizarPrecio()
        {
            nudPrecio.Value = cmbProductos.SelectedItem is Producto producto
                ? Math.Min(nudPrecio.Maximum, producto.PrecioCompra)
                : 0;
        }

        private void ActualizarCarrito()
        {
            dgvDatos.DataSource = null;
            dgvDatos.DataSource = detalles.ToList();
            ConfigurarTabla();
            decimal total = detalles.Sum(x => x.Subtotal);
            lblResumen.Text = $"Subtotal: {total:N2}   Impuesto: 0.00   Total: {total:N2}";
            btnRegistrar.Enabled = detalles.Count > 0;
        }

        private void ConfigurarTabla()
        {
            foreach (string nombre in new[]
            {
                nameof(CompraDetalle.IdDetalleCompra),
                nameof(CompraDetalle.IdCompra),
                nameof(CompraDetalle.IdProducto)
            })
            {
                if (dgvDatos.Columns.Contains(nombre))
                {
                    dgvDatos.Columns[nombre]!.Visible = false;
                }
            }

            if (dgvDatos.Columns.Contains(nameof(CompraDetalle.NombreProducto)))
            {
                dgvDatos.Columns[nameof(CompraDetalle.NombreProducto)]!.HeaderText = "Producto";
                dgvDatos.Columns[nameof(CompraDetalle.Cantidad)]!.HeaderText = "Cantidad";
                dgvDatos.Columns[nameof(CompraDetalle.Precio)]!.HeaderText = "Precio";
                dgvDatos.Columns[nameof(CompraDetalle.Subtotal)]!.HeaderText = "Subtotal";
                ConfigurarColumnasMoneda(dgvDatos, nameof(CompraDetalle.Precio), nameof(CompraDetalle.Subtotal));
            }
        }

        private static TextBox CrearCampoSoloLectura(string texto)
        {
            return new TextBox
            {
                ReadOnly = true,
                Text = texto
            };
        }
    }
}

using Proyecto_Gym_ProgramacionIII_Jeovanny.Datos;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    public class FrmVentas : FrmModuloBase
    {
        private readonly ComboBox cmbTipoPago;
        private readonly CheckBox chkIdentificarCliente;
        private readonly ComboBox cmbClientes;
        private readonly DateTimePicker dtpFechaVencimiento;
        private readonly ComboBox cmbProductos;
        private readonly NumericUpDown nudCantidad;
        private readonly NumericUpDown nudDescuento;
        private readonly Button btnRegistrar;
        private readonly List<VentaDetalle> detalles = new List<VentaDetalle>();

        public FrmVentas()
            : base("Registro de ventas", 150)
        {
            cmbTipoPago = CrearCombo();
            cmbTipoPago.Items.AddRange(new object[] { "CONTADO", "CREDITO" });
            cmbTipoPago.SelectedIndex = 0;
            chkIdentificarCliente = new CheckBox
            {
                AutoSize = true,
                Text = "Identificar cliente"
            };
            cmbClientes = CrearCombo();
            dtpFechaVencimiento = CrearFecha();
            dtpFechaVencimiento.MinDate = DateTime.Today;
            cmbProductos = CrearCombo();
            nudCantidad = CrearNumero(int.MaxValue, 0, 1);
            nudDescuento = CrearNumero(100, 2);

            cmbTipoPago.SelectedIndexChanged += cmbTipoPago_SelectedIndexChanged;
            chkIdentificarCliente.CheckedChanged += chkIdentificarCliente_CheckedChanged;
            cmbClientes.Format += cmbClientes_Format;
            cmbProductos.Format += cmbProductos_Format;
            nudDescuento.ValueChanged += nudDescuento_ValueChanged;

            AgregarCampo("Tipo de venta", cmbTipoPago, 150);
            AgregarCampo("Cliente", chkIdentificarCliente, 140);
            AgregarCampo("Cliente seleccionado", cmbClientes, 230);
            AgregarCampo("Vencimiento del crédito", dtpFechaVencimiento, 170);
            AgregarCampo("Producto", cmbProductos, 250);
            AgregarCampo("Cantidad", nudCantidad, 110);
            AgregarCampo("Descuento (%)", nudDescuento, 140);

            AgregarBoton("AGREGAR", btnAgregar_Click);
            AgregarBoton("QUITAR", btnQuitar_Click, false);
            btnRegistrar = AgregarBoton("REGISTRAR", btnRegistrar_Click);
            AgregarBoton("LIMPIAR", btnLimpiar_Click, false);
            Load += FrmVentas_Load;
            ActualizarTipoPago();
            ActualizarCarrito();
        }

        private async void FrmVentas_Load(object? sender, EventArgs e)
        {
            await CargarListasAsync();
        }

        private async Task CargarListasAsync()
        {
            try
            {
                LimpiarMensaje();
                cmbClientes.DataSource = (await ClienteRepositorio.ListarAsync())
                    .Where(x => x.Estado)
                    .ToList();
                cmbProductos.DataSource = (await ProductoRepositorio.ListarAsync())
                    .Where(x => x.Estado && x.Stock > 0)
                    .ToList();
                btnRegistrar.Enabled = detalles.Count > 0;
                ActualizarTipoPago();
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

            int cantidad = Convert.ToInt32(nudCantidad.Value);

            if (cantidad > producto.Stock)
            {
                lblMensaje.Text = $"La cantidad supera el stock disponible de {producto.Stock} unidades.";
                nudCantidad.Focus();
                return;
            }

            if (detalles.Any(x => x.IdProducto == producto.IdProducto))
            {
                lblMensaje.Text = "El producto seleccionado ya está agregado a la venta.";
                cmbProductos.Focus();
                return;
            }

            detalles.Add(new VentaDetalle
            {
                IdProducto = producto.IdProducto,
                NombreProducto = producto.Nombre,
                Cantidad = cantidad,
                Precio = producto.PrecioVenta,
                Descuento = 0,
                Subtotal = producto.PrecioVenta * cantidad
            });
            nudCantidad.Value = 1;
            ActualizarCarrito();
        }

        private void btnQuitar_Click(object? sender, EventArgs e)
        {
            if (dgvDatos.CurrentRow?.DataBoundItem is not VentaDetalle detalle)
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
            bool credito = cmbTipoPago.SelectedItem?.ToString() == "CREDITO";
            Cliente? cliente = cmbClientes.SelectedItem as Cliente;

            if ((credito || chkIdentificarCliente.Checked) && cliente is null)
            {
                lblMensaje.Text = "Debe seleccionar un cliente.";
                cmbClientes.Focus();
                return;
            }

            if (detalles.Count == 0)
            {
                lblMensaje.Text = "Debe agregar al menos un producto a la venta.";
                return;
            }

            btnRegistrar.Enabled = false;

            try
            {
                Venta venta = new Venta
                {
                    IdCliente = credito || chkIdentificarCliente.Checked
                        ? cliente?.IdCliente
                        : null,
                    IdUsuario = SesionActual.IdUsuario,
                    TipoPago = cmbTipoPago.SelectedItem?.ToString() ?? string.Empty
                };
                await VentaRepositorio.GuardarAsync(
                    venta,
                    detalles,
                    nudDescuento.Value,
                    credito ? dtpFechaVencimiento.Value.Date : null);
                MessageBox.Show(
                    $"Venta número {venta.IdVenta} registrada correctamente.",
                    "Registro de ventas",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                detalles.Clear();
                nudDescuento.Value = 0;
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
            nudDescuento.Value = 0;
            LimpiarMensaje();
            ActualizarCarrito();
        }

        private void cmbTipoPago_SelectedIndexChanged(object? sender, EventArgs e)
        {
            ActualizarTipoPago();
        }

        private void chkIdentificarCliente_CheckedChanged(object? sender, EventArgs e)
        {
            cmbClientes.Enabled = chkIdentificarCliente.Checked;
        }

        private void nudDescuento_ValueChanged(object? sender, EventArgs e)
        {
            ActualizarResumen();
        }

        private void cmbClientes_Format(object? sender, ListControlConvertEventArgs e)
        {
            if (e.ListItem is Cliente cliente)
            {
                e.Value = $"{cliente.Nombre} {cliente.Apellido} - {cliente.Cedula}";
            }
        }

        private void cmbProductos_Format(object? sender, ListControlConvertEventArgs e)
        {
            if (e.ListItem is Producto producto)
            {
                e.Value = $"{producto.Codigo} - {producto.Nombre} - stock {producto.Stock}";
            }
        }

        private void ActualizarTipoPago()
        {
            bool credito = cmbTipoPago.SelectedItem?.ToString() == "CREDITO";

            if (credito)
            {
                chkIdentificarCliente.Checked = true;
                chkIdentificarCliente.Enabled = false;
            }
            else
            {
                chkIdentificarCliente.Enabled = true;
            }

            cmbClientes.Enabled = credito || chkIdentificarCliente.Checked;
            dtpFechaVencimiento.Enabled = credito;
        }

        private void ActualizarCarrito()
        {
            dgvDatos.DataSource = null;
            dgvDatos.DataSource = detalles.ToList();
            ConfigurarTabla();
            btnRegistrar.Enabled = detalles.Count > 0;
            ActualizarResumen();
        }

        private void ActualizarResumen()
        {
            decimal subtotal = detalles.Sum(x => x.Subtotal);
            decimal descuento = Math.Round(subtotal * nudDescuento.Value / 100m, 2);
            decimal total = subtotal - descuento;
            lblResumen.Text = $"Subtotal: {subtotal:N2}   Descuento: {descuento:N2}   Total: {total:N2}";
        }

        private void ConfigurarTabla()
        {
            foreach (string nombre in new[]
            {
                nameof(VentaDetalle.IdDetalleVenta),
                nameof(VentaDetalle.IdVenta),
                nameof(VentaDetalle.IdProducto),
                nameof(VentaDetalle.Descuento)
            })
            {
                if (dgvDatos.Columns.Contains(nombre))
                {
                    dgvDatos.Columns[nombre]!.Visible = false;
                }
            }

            if (dgvDatos.Columns.Contains(nameof(VentaDetalle.NombreProducto)))
            {
                dgvDatos.Columns[nameof(VentaDetalle.NombreProducto)]!.HeaderText = "Producto";
                dgvDatos.Columns[nameof(VentaDetalle.Cantidad)]!.HeaderText = "Cantidad";
                dgvDatos.Columns[nameof(VentaDetalle.Precio)]!.HeaderText = "Precio";
                dgvDatos.Columns[nameof(VentaDetalle.Subtotal)]!.HeaderText = "Subtotal";
                ConfigurarColumnasMoneda(dgvDatos, nameof(VentaDetalle.Precio), nameof(VentaDetalle.Subtotal));
            }
        }
    }
}

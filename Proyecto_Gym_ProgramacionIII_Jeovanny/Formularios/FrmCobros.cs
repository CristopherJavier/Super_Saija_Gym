using System.Drawing.Printing;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Datos;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    public class FrmCobros : FrmModuloBase
    {
        private readonly ComboBox cmbClientes;
        private readonly ComboBox cmbMetodosPago;
        private readonly ComboBox cmbTipoDetalle;
        private readonly ComboBox cmbElementos;
        private readonly NumericUpDown nudCantidad;
        private readonly NumericUpDown nudMonto;
        private readonly Button btnRegistrar;
        private readonly Button btnImprimir;
        private readonly List<CobroDetalle> detalles = new List<CobroDetalle>();
        private List<Producto> productos = new List<Producto>();
        private Cobro? ultimoCobro;
        private List<CobroDetalle> detallesUltimoCobro = new List<CobroDetalle>();
        private int indiceDetalleImpresion;
        private bool cargandoDatos;

        public FrmCobros()
            : base("Registro de cobros", 150)
        {
            cmbClientes = CrearCombo();
            cmbMetodosPago = CrearCombo();
            cmbTipoDetalle = CrearCombo();
            cmbElementos = CrearCombo();
            nudCantidad = CrearNumero(int.MaxValue, 0, 1);
            nudMonto = CrearNumero(9999999999.99m, 2);

            cmbClientes.Format += cmbClientes_Format;
            cmbClientes.SelectedIndexChanged += cmbClientes_SelectedIndexChanged;
            cmbMetodosPago.DisplayMember = nameof(MetodoPago.Nombre);
            cmbTipoDetalle.Items.AddRange(new object[] { "SERVICIO", "PRODUCTO" });
            cmbTipoDetalle.SelectedIndex = 0;
            cmbTipoDetalle.SelectedIndexChanged += cmbTipoDetalle_SelectedIndexChanged;
            cmbElementos.Format += cmbElementos_Format;
            cmbElementos.SelectedIndexChanged += cmbElementos_SelectedIndexChanged;

            AgregarCampo("Cliente", cmbClientes, 255);
            AgregarCampo("Método de pago", cmbMetodosPago, 190);
            AgregarCampo("Tipo de detalle", cmbTipoDetalle, 170);
            AgregarCampo("Servicio o producto", cmbElementos, 280);
            AgregarCampo("Cantidad", nudCantidad, 130);
            AgregarCampo("Monto o precio", nudMonto, 170);

            AgregarBoton("AGREGAR", btnAgregar_Click);
            AgregarBoton("QUITAR", btnQuitar_Click, false);
            btnRegistrar = AgregarBoton("REGISTRAR", btnRegistrar_Click);
            btnImprimir = AgregarBoton("IMPRIMIR RECIBO", btnImprimir_Click, false, 165);
            btnImprimir.Enabled = false;
            Load += FrmCobros_Load;
            ActualizarCarrito();
        }

        private async void FrmCobros_Load(object? sender, EventArgs e)
        {
            await CargarListasAsync();
        }

        private async Task CargarListasAsync()
        {
            try
            {
                cargandoDatos = true;
                LimpiarMensaje();
                cmbClientes.DataSource = (await ClienteRepositorio.ListarAsync())
                    .Where(x => x.Estado)
                    .ToList();
                cmbMetodosPago.DataSource = await MetodoPagoRepositorio.ListarAsync(soloActivos: true);
                productos = (await ProductoRepositorio.ListarAsync())
                    .Where(x => x.Estado && x.Stock > 0)
                    .ToList();
                cargandoDatos = false;
                await CargarElementosAsync();
            }
            catch (Exception ex)
            {
                cargandoDatos = false;
                MostrarExcepcion(ex);
            }
        }

        private async Task CargarElementosAsync()
        {
            if (cmbTipoDetalle.SelectedItem?.ToString() == "SERVICIO")
            {
                if (cmbClientes.SelectedItem is Cliente cliente)
                {
                    cmbElementos.DataSource = await CargoRepositorio.ListarAsync(cliente.IdCliente, true);
                }
                else
                {
                    cmbElementos.DataSource = null;
                }
            }
            else
            {
                cmbElementos.DataSource = productos;
            }

            ActualizarElemento();
        }

        private void btnAgregar_Click(object? sender, EventArgs e)
        {
            LimpiarMensaje();
            string tipo = cmbTipoDetalle.SelectedItem?.ToString() ?? string.Empty;

            if (tipo == "SERVICIO")
            {
                if (cmbElementos.SelectedItem is not Cargo cargo)
                {
                    lblMensaje.Text = "El cliente no tiene un cargo pendiente para agregar.";
                    cmbElementos.Focus();
                    return;
                }

                if (detalles.Any(x => x.IdCargo == cargo.IdCargo))
                {
                    lblMensaje.Text = "El cargo seleccionado ya está agregado al cobro.";
                    return;
                }

                if (nudMonto.Value > cargo.Saldo)
                {
                    lblMensaje.Text = "El monto no puede superar el saldo pendiente del cargo.";
                    nudMonto.Focus();
                    return;
                }

                detalles.Add(new CobroDetalle
                {
                    TipoDetalle = "SERVICIO",
                    IdCargo = cargo.IdCargo,
                    Descripcion = cargo.Concepto,
                    Cantidad = 1,
                    Precio = nudMonto.Value,
                    Subtotal = nudMonto.Value
                });
            }
            else
            {
                if (cmbElementos.SelectedItem is not Producto producto)
                {
                    lblMensaje.Text = "Debe seleccionar un producto.";
                    cmbElementos.Focus();
                    return;
                }

                if (detalles.Any(x => x.IdProducto == producto.IdProducto))
                {
                    lblMensaje.Text = "El producto seleccionado ya está agregado al cobro.";
                    return;
                }

                int cantidad = Convert.ToInt32(nudCantidad.Value);

                if (cantidad > producto.Stock)
                {
                    lblMensaje.Text = $"La cantidad supera el stock disponible de {producto.Stock} unidades.";
                    nudCantidad.Focus();
                    return;
                }

                detalles.Add(new CobroDetalle
                {
                    TipoDetalle = "PRODUCTO",
                    IdProducto = producto.IdProducto,
                    Descripcion = producto.Nombre,
                    Cantidad = cantidad,
                    Precio = producto.PrecioVenta,
                    Subtotal = producto.PrecioVenta * cantidad
                });
            }

            nudCantidad.Value = 1;
            ActualizarCarrito();
        }

        private void btnQuitar_Click(object? sender, EventArgs e)
        {
            if (dgvDatos.CurrentRow?.DataBoundItem is not CobroDetalle detalle)
            {
                lblMensaje.Text = "Debe seleccionar una línea del detalle.";
                return;
            }

            detalles.Remove(detalle);
            LimpiarMensaje();
            ActualizarCarrito();
        }

        private async void btnRegistrar_Click(object? sender, EventArgs e)
        {
            LimpiarMensaje();

            if (cmbClientes.SelectedItem is not Cliente cliente)
            {
                lblMensaje.Text = "Debe seleccionar un cliente.";
                cmbClientes.Focus();
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
                lblMensaje.Text = "Debe agregar al menos un servicio o producto al cobro.";
                return;
            }

            btnRegistrar.Enabled = false;

            try
            {
                Cobro cobro = new Cobro
                {
                    IdCliente = cliente.IdCliente,
                    IdUsuario = SesionActual.IdUsuario,
                    NombreUsuario = SesionActual.NombreCompleto,
                    IdMetodoPago = metodo.IdMetodoPago
                };
                await CobroRepositorio.GuardarAsync(cobro, detalles);
                ultimoCobro = cobro;
                detallesUltimoCobro = detalles.Select(CopiarDetalle).ToList();
                btnImprimir.Enabled = true;
                MessageBox.Show(
                    $"Cobro número {cobro.IdCobro} registrado correctamente.",
                    "Registro de cobros",
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

        private void btnImprimir_Click(object? sender, EventArgs e)
        {
            if (ultimoCobro is null || detallesUltimoCobro.Count == 0)
            {
                lblMensaje.Text = "No hay un recibo disponible para imprimir.";
                return;
            }

            try
            {
                using PrintDocument documento = new PrintDocument();
                documento.DocumentName = $"Recibo de cobro {ultimoCobro.IdCobro}";
                documento.BeginPrint += documento_BeginPrint;
                documento.PrintPage += documento_PrintPage;
                using PrintPreviewDialog vistaPrevia = new PrintPreviewDialog
                {
                    Document = documento,
                    StartPosition = FormStartPosition.CenterParent,
                    Width = 900,
                    Height = 700
                };
                vistaPrevia.ShowDialog(this);
            }
            catch (Exception)
            {
                lblMensaje.Text = "No fue posible preparar el recibo para impresión.";
            }
        }

        private async void cmbClientes_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cargandoDatos)
            {
                return;
            }

            try
            {
                await CargarElementosAsync();
            }
            catch (Exception ex)
            {
                MostrarExcepcion(ex);
            }
        }

        private async void cmbTipoDetalle_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cargandoDatos)
            {
                return;
            }

            try
            {
                await CargarElementosAsync();
            }
            catch (Exception ex)
            {
                MostrarExcepcion(ex);
            }
        }

        private void cmbElementos_SelectedIndexChanged(object? sender, EventArgs e)
        {
            ActualizarElemento();
        }

        private void cmbClientes_Format(object? sender, ListControlConvertEventArgs e)
        {
            if (e.ListItem is Cliente cliente)
            {
                e.Value = $"{cliente.Nombre} {cliente.Apellido} - {cliente.Cedula}";
            }
        }

        private void cmbElementos_Format(object? sender, ListControlConvertEventArgs e)
        {
            if (e.ListItem is Cargo cargo)
            {
                e.Value = $"Cargo {cargo.IdCargo} - {cargo.Concepto} - saldo {cargo.Saldo:N2}";
            }
            else if (e.ListItem is Producto producto)
            {
                e.Value = $"{producto.Codigo} - {producto.Nombre} - stock {producto.Stock}";
            }
        }

        private void ActualizarElemento()
        {
            if (cmbElementos.SelectedItem is Cargo cargo)
            {
                nudCantidad.Value = 1;
                nudCantidad.Enabled = false;
                nudMonto.ReadOnly = false;
                nudMonto.Minimum = 0.01m;
                nudMonto.Maximum = cargo.Saldo;
                nudMonto.Value = cargo.Saldo;
            }
            else if (cmbElementos.SelectedItem is Producto producto)
            {
                nudCantidad.Enabled = true;
                nudMonto.ReadOnly = true;
                nudMonto.Minimum = 0;
                nudMonto.Maximum = Math.Max(1, producto.PrecioVenta);
                nudMonto.Value = producto.PrecioVenta;
            }
            else
            {
                nudCantidad.Enabled = false;
                nudMonto.ReadOnly = true;
                nudMonto.Minimum = 0;
                nudMonto.Maximum = 1;
                nudMonto.Value = 0;
            }
        }

        private void ActualizarCarrito()
        {
            dgvDatos.DataSource = null;
            dgvDatos.DataSource = detalles.ToList();
            ConfigurarTabla();
            lblResumen.Text = $"Total del cobro: {detalles.Sum(x => x.Subtotal):N2}";
            btnRegistrar.Enabled = detalles.Count > 0;
        }

        private void ConfigurarTabla()
        {
            foreach (string nombre in new[]
            {
                nameof(CobroDetalle.IdDetalleCobro),
                nameof(CobroDetalle.IdCobro),
                nameof(CobroDetalle.IdCargo),
                nameof(CobroDetalle.IdProducto)
            })
            {
                if (dgvDatos.Columns.Contains(nombre))
                {
                    dgvDatos.Columns[nombre]!.Visible = false;
                }
            }

            if (dgvDatos.Columns.Contains(nameof(CobroDetalle.TipoDetalle)))
            {
                dgvDatos.Columns[nameof(CobroDetalle.TipoDetalle)]!.HeaderText = "Tipo";
                dgvDatos.Columns[nameof(CobroDetalle.Descripcion)]!.HeaderText = "Descripción";
                dgvDatos.Columns[nameof(CobroDetalle.Cantidad)]!.HeaderText = "Cantidad";
                dgvDatos.Columns[nameof(CobroDetalle.Precio)]!.HeaderText = "Precio";
                dgvDatos.Columns[nameof(CobroDetalle.Subtotal)]!.HeaderText = "Subtotal";
                ConfigurarColumnasMoneda(dgvDatos, nameof(CobroDetalle.Precio), nameof(CobroDetalle.Subtotal));
            }
        }

        private void documento_BeginPrint(object? sender, PrintEventArgs e)
        {
            indiceDetalleImpresion = 0;
        }

        private void documento_PrintPage(object? sender, PrintPageEventArgs e)
        {
            Graphics? graficos = e.Graphics;

            if (ultimoCobro is null || graficos is null)
            {
                e.HasMorePages = false;
                return;
            }

            using Font fuenteTitulo = new Font("Segoe UI", 18F, FontStyle.Bold);
            using Font fuenteSubtitulo = new Font("Segoe UI", 11F, FontStyle.Bold);
            using Font fuenteNormal = new Font("Segoe UI", 10F);
            using Pen linea = new Pen(Color.Black);
            float x = e.MarginBounds.Left;
            float y = e.MarginBounds.Top;
            float ancho = e.MarginBounds.Width;

            graficos.DrawString("SUPER SAIJA GYM", fuenteTitulo, Brushes.Black, x, y);
            y += 38;
            graficos.DrawString($"RECIBO DE COBRO N.º {ultimoCobro.IdCobro}", fuenteSubtitulo, Brushes.Black, x, y);
            y += 30;
            graficos.DrawString($"Fecha: {ultimoCobro.Fecha:dd/MM/yyyy hh:mm tt}", fuenteNormal, Brushes.Black, x, y);
            y += 24;
            graficos.DrawString($"Cliente: {ultimoCobro.NombreCliente}", fuenteNormal, Brushes.Black, x, y);
            y += 24;
            graficos.DrawString($"Método de pago: {ultimoCobro.NombreMetodoPago}", fuenteNormal, Brushes.Black, x, y);
            y += 24;
            graficos.DrawString($"Atendido por: {ultimoCobro.NombreUsuario}", fuenteNormal, Brushes.Black, x, y);
            y += 32;
            graficos.DrawLine(linea, x, y, x + ancho, y);
            y += 12;

            while (indiceDetalleImpresion < detallesUltimoCobro.Count)
            {
                CobroDetalle detalle = detallesUltimoCobro[indiceDetalleImpresion];

                if (y + 70 > e.MarginBounds.Bottom)
                {
                    e.HasMorePages = true;
                    return;
                }

                graficos.DrawString(detalle.Descripcion, fuenteNormal, Brushes.Black, x, y);
                y += 22;
                string valores = $"{detalle.Cantidad} x {detalle.Precio:N2}";
                graficos.DrawString(valores, fuenteNormal, Brushes.Black, x + 20, y);
                SizeF tamanoSubtotal = graficos.MeasureString(detalle.Subtotal.ToString("N2"), fuenteNormal);
                graficos.DrawString(
                    detalle.Subtotal.ToString("N2"),
                    fuenteNormal,
                    Brushes.Black,
                    x + ancho - tamanoSubtotal.Width,
                    y);
                y += 30;
                indiceDetalleImpresion++;
            }

            graficos.DrawLine(linea, x, y, x + ancho, y);
            y += 15;
            string total = $"TOTAL: {ultimoCobro.Total:N2}";
            SizeF tamanoTotal = graficos.MeasureString(total, fuenteSubtitulo);
            graficos.DrawString(total, fuenteSubtitulo, Brushes.Black, x + ancho - tamanoTotal.Width, y);
            e.HasMorePages = false;
        }

        private static CobroDetalle CopiarDetalle(CobroDetalle detalle)
        {
            return new CobroDetalle
            {
                IdDetalleCobro = detalle.IdDetalleCobro,
                IdCobro = detalle.IdCobro,
                TipoDetalle = detalle.TipoDetalle,
                IdCargo = detalle.IdCargo,
                IdProducto = detalle.IdProducto,
                Descripcion = detalle.Descripcion,
                Cantidad = detalle.Cantidad,
                Precio = detalle.Precio,
                Subtotal = detalle.Subtotal
            };
        }
    }
}

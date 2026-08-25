using Proyecto_Gym_ProgramacionIII_Jeovanny.Datos;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    public class FrmMovimientosInventario : FrmModuloBase
    {
        private readonly ComboBox cmbProductos;
        private readonly ComboBox cmbTipoMovimiento;
        private readonly NumericUpDown nudCantidad;
        private readonly Button btnRegistrar;

        public FrmMovimientosInventario()
            : base("Entrada y salida de inventario", 82)
        {
            cmbProductos = CrearCombo();
            cmbTipoMovimiento = CrearCombo();
            nudCantidad = CrearNumero(int.MaxValue, 0, 1);
            cmbProductos.Format += cmbProductos_Format;
            cmbProductos.SelectedIndexChanged += cmbProductos_SelectedIndexChanged;
            cmbTipoMovimiento.Items.AddRange(new object[] { "ENTRADA", "SALIDA" });
            cmbTipoMovimiento.SelectedIndex = 0;
            AgregarCampo("Producto", cmbProductos, 310);
            AgregarCampo("Tipo de movimiento", cmbTipoMovimiento, 200);
            AgregarCampo("Cantidad", nudCantidad, 150);
            btnRegistrar = AgregarBoton("REGISTRAR", btnRegistrar_Click);
            AgregarBoton("ACTUALIZAR", btnActualizar_Click, false);
            Load += FrmMovimientosInventario_Load;
        }

        private async void FrmMovimientosInventario_Load(object? sender, EventArgs e)
        {
            await CargarDatosAsync();
        }

        private async Task CargarDatosAsync()
        {
            try
            {
                LimpiarMensaje();
                List<Producto> productos = (await ProductoRepositorio.ListarAsync()).Where(x => x.Estado).ToList();
                cmbProductos.DataSource = productos;
                dgvDatos.DataSource = await MovimientoInventarioRepositorio.ListarAsync();
                ConfigurarTabla();
                btnRegistrar.Enabled = productos.Count > 0;
                ActualizarResumen();
            }
            catch (Exception ex)
            {
                MostrarExcepcion(ex);
            }
        }

        private async void btnRegistrar_Click(object? sender, EventArgs e)
        {
            LimpiarMensaje();

            if (cmbProductos.SelectedItem is not Producto producto)
            {
                lblMensaje.Text = "Debe seleccionar un producto.";
                cmbProductos.Focus();
                return;
            }

            btnRegistrar.Enabled = false;

            try
            {
                await MovimientoInventarioRepositorio.RegistrarAsync(new MovimientoInventario
                {
                    IdProducto = producto.IdProducto,
                    TipoMovimiento = cmbTipoMovimiento.SelectedItem?.ToString() ?? string.Empty,
                    Cantidad = Convert.ToInt32(nudCantidad.Value),
                    IdUsuario = SesionActual.IdUsuario
                });
                MessageBox.Show(
                    "Movimiento de inventario registrado correctamente.",
                    "Inventario",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                nudCantidad.Value = 1;
                await CargarDatosAsync();
            }
            catch (Exception ex)
            {
                MostrarExcepcion(ex);
            }
            finally
            {
                btnRegistrar.Enabled = true;
            }
        }

        private async void btnActualizar_Click(object? sender, EventArgs e)
        {
            await CargarDatosAsync();
        }

        private void cmbProductos_Format(object? sender, ListControlConvertEventArgs e)
        {
            if (e.ListItem is Producto producto)
            {
                e.Value = $"{producto.Codigo} - {producto.Nombre}";
            }
        }

        private void cmbProductos_SelectedIndexChanged(object? sender, EventArgs e)
        {
            ActualizarResumen();
        }

        private void ActualizarResumen()
        {
            lblResumen.Text = cmbProductos.SelectedItem is Producto producto
                ? $"Stock actual: {producto.Stock} unidades"
                : string.Empty;
        }

        private void ConfigurarTabla()
        {
            foreach (string nombre in new[]
            {
                nameof(MovimientoInventario.IdProducto),
                nameof(MovimientoInventario.IdUsuario)
            })
            {
                if (dgvDatos.Columns.Contains(nombre))
                {
                    dgvDatos.Columns[nombre]!.Visible = false;
                }
            }

            dgvDatos.Columns[nameof(MovimientoInventario.IdMovimiento)]!.HeaderText = "Número";
            dgvDatos.Columns[nameof(MovimientoInventario.NombreProducto)]!.HeaderText = "Producto";
            dgvDatos.Columns[nameof(MovimientoInventario.TipoMovimiento)]!.HeaderText = "Tipo";
            dgvDatos.Columns[nameof(MovimientoInventario.Cantidad)]!.HeaderText = "Cantidad";
            dgvDatos.Columns[nameof(MovimientoInventario.Fecha)]!.HeaderText = "Fecha";
            dgvDatos.Columns[nameof(MovimientoInventario.NombreUsuario)]!.HeaderText = "Usuario";
            dgvDatos.Columns[nameof(MovimientoInventario.IdVenta)]!.HeaderText = "Venta";
            dgvDatos.Columns[nameof(MovimientoInventario.IdCompra)]!.HeaderText = "Compra";
            dgvDatos.Columns[nameof(MovimientoInventario.Fecha)]!.DefaultCellStyle.Format = "dd/MM/yyyy hh:mm tt";
        }
    }
}

using Proyecto_Gym_ProgramacionIII_Jeovanny.Datos;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    public class FrmMetodosPago : FrmModuloBase
    {
        private readonly TextBox txtBuscar;
        private readonly Button btnEditar;

        public FrmMetodosPago()
            : base("Métodos de pago", 82)
        {
            txtBuscar = CrearTexto(150);
            AgregarCampo("Buscar por nombre", txtBuscar, 300);
            AgregarBoton("NUEVO", btnNuevo_Click);
            btnEditar = AgregarBoton("EDITAR", btnEditar_Click, false);
            AgregarBoton("ACTUALIZAR", btnActualizar_Click, false);
            btnEditar.Enabled = false;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            dgvDatos.SelectionChanged += dgvDatos_SelectionChanged;
            dgvDatos.CellDoubleClick += dgvDatos_CellDoubleClick;
            Load += FrmMetodosPago_Load;
        }

        private async void FrmMetodosPago_Load(object? sender, EventArgs e)
        {
            await CargarAsync();
        }

        private async Task CargarAsync()
        {
            try
            {
                LimpiarMensaje();
                List<MetodoPago> metodos = await MetodoPagoRepositorio.ListarAsync(txtBuscar.Text);
                dgvDatos.DataSource = metodos;
                ConfigurarTabla();
                btnEditar.Enabled = ObtenerSeleccionado() is not null;
                lblResumen.Text = $"Métodos registrados: {metodos.Count}";
            }
            catch (Exception ex)
            {
                MostrarExcepcion(ex);
            }
        }

        private MetodoPago? ObtenerSeleccionado()
        {
            return dgvDatos.CurrentRow?.DataBoundItem as MetodoPago;
        }

        private async void btnNuevo_Click(object? sender, EventArgs e)
        {
            using FrmMetodoPagoDetalle formulario = new FrmMetodoPagoDetalle();
            if (formulario.ShowDialog(this) == DialogResult.OK)
            {
                await CargarAsync();
            }
        }

        private async void btnEditar_Click(object? sender, EventArgs e)
        {
            MetodoPago? metodo = ObtenerSeleccionado();
            if (metodo is null)
            {
                lblMensaje.Text = "Selecciona un método de pago de la lista.";
                return;
            }

            using FrmMetodoPagoDetalle formulario = new FrmMetodoPagoDetalle(metodo);
            if (formulario.ShowDialog(this) == DialogResult.OK)
            {
                await CargarAsync();
            }
        }

        private async void btnActualizar_Click(object? sender, EventArgs e)
        {
            await CargarAsync();
        }

        private async void txtBuscar_TextChanged(object? sender, EventArgs e)
        {
            await CargarAsync();
        }

        private void dgvDatos_SelectionChanged(object? sender, EventArgs e)
        {
            btnEditar.Enabled = ObtenerSeleccionado() is not null;
        }

        private void dgvDatos_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnEditar_Click(sender, EventArgs.Empty);
            }
        }

        private void ConfigurarTabla()
        {
            if (dgvDatos.Columns.Count == 0)
            {
                return;
            }

            dgvDatos.Columns[nameof(MetodoPago.IdMetodoPago)]!.Visible = false;
            dgvDatos.Columns[nameof(MetodoPago.Nombre)]!.HeaderText = "Nombre";
            dgvDatos.Columns[nameof(MetodoPago.Descripcion)]!.HeaderText = "Descripción";
            dgvDatos.Columns[nameof(MetodoPago.Estado)]!.Visible = false;
            dgvDatos.Columns[nameof(MetodoPago.EstadoTexto)]!.HeaderText = "Estado";
        }
    }
}

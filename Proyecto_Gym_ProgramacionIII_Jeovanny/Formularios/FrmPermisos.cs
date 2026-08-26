using Proyecto_Gym_ProgramacionIII_Jeovanny.Datos;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    public class FrmPermisos : FrmModuloBase
    {
        private readonly TextBox txtBuscar;
        private readonly Button btnEditar;

        public FrmPermisos()
            : base("Permisos", 82)
        {
            txtBuscar = CrearTexto(150);
            AgregarCampo("Buscar permiso", txtBuscar, 300);
            AgregarBoton("NUEVO", btnNuevo_Click);
            btnEditar = AgregarBoton("EDITAR", btnEditar_Click, false);
            AgregarBoton("ACTUALIZAR", btnActualizar_Click, false);
            btnEditar.Enabled = false;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            dgvDatos.SelectionChanged += dgvDatos_SelectionChanged;
            dgvDatos.CellDoubleClick += dgvDatos_CellDoubleClick;
            Load += FrmPermisos_Load;
        }

        private async void FrmPermisos_Load(object? sender, EventArgs e)
        {
            await CargarAsync();
        }

        private async Task CargarAsync()
        {
            try
            {
                LimpiarMensaje();
                List<Permiso> permisos = await PermisoRepositorio.ListarAsync(txtBuscar.Text);
                dgvDatos.DataSource = permisos;
                ConfigurarTabla();
                btnEditar.Enabled = ObtenerSeleccionado() is not null;
                lblResumen.Text = $"Permisos registrados: {permisos.Count}";
            }
            catch (Exception ex)
            {
                MostrarExcepcion(ex);
            }
        }

        private Permiso? ObtenerSeleccionado()
        {
            return dgvDatos.CurrentRow?.DataBoundItem as Permiso;
        }

        private async void btnNuevo_Click(object? sender, EventArgs e)
        {
            using FrmPermisoDetalle formulario = new FrmPermisoDetalle();
            if (formulario.ShowDialog(this) == DialogResult.OK)
            {
                await CargarAsync();
            }
        }

        private async void btnEditar_Click(object? sender, EventArgs e)
        {
            Permiso? permiso = ObtenerSeleccionado();
            if (permiso is null)
            {
                lblMensaje.Text = "Selecciona un permiso de la lista.";
                return;
            }

            using FrmPermisoDetalle formulario = new FrmPermisoDetalle(permiso);
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

            dgvDatos.Columns[nameof(Permiso.IdPermiso)]!.Visible = false;
            dgvDatos.Columns[nameof(Permiso.Clave)]!.HeaderText = "Código";
            dgvDatos.Columns[nameof(Permiso.Nombre)]!.HeaderText = "Permiso";
            dgvDatos.Columns[nameof(Permiso.Descripcion)]!.HeaderText = "Descripción";
            dgvDatos.Columns[nameof(Permiso.Estado)]!.Visible = false;
            dgvDatos.Columns[nameof(Permiso.EstadoTexto)]!.HeaderText = "Estado";
        }
    }
}

using Proyecto_Gym_ProgramacionIII_Jeovanny.Datos;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    public class FrmRoles : FrmModuloBase
    {
        private readonly TextBox txtBuscar;
        private readonly Button btnEditar;

        public FrmRoles()
            : base("Roles", 82)
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
            Load += FrmRoles_Load;
        }

        private async void FrmRoles_Load(object? sender, EventArgs e)
        {
            await CargarAsync();
        }

        private async Task CargarAsync()
        {
            try
            {
                LimpiarMensaje();
                List<Rol> roles = await RolRepositorio.ListarAsync(txtBuscar.Text);
                dgvDatos.DataSource = roles;
                ConfigurarTabla();
                btnEditar.Enabled = ObtenerSeleccionado() is not null;
                lblResumen.Text = $"Roles registrados: {roles.Count}";
            }
            catch (Exception ex)
            {
                MostrarExcepcion(ex);
            }
        }

        private Rol? ObtenerSeleccionado()
        {
            return dgvDatos.CurrentRow?.DataBoundItem as Rol;
        }

        private async void btnNuevo_Click(object? sender, EventArgs e)
        {
            using FrmRolDetalle formulario = new FrmRolDetalle();
            if (formulario.ShowDialog(this) == DialogResult.OK)
            {
                await CargarAsync();
            }
        }

        private async void btnEditar_Click(object? sender, EventArgs e)
        {
            Rol? rol = ObtenerSeleccionado();
            if (rol is null)
            {
                lblMensaje.Text = "Selecciona un rol de la lista.";
                return;
            }

            using FrmRolDetalle formulario = new FrmRolDetalle(rol);
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

            dgvDatos.Columns[nameof(Rol.IdRol)]!.Visible = false;
            dgvDatos.Columns[nameof(Rol.Nombre)]!.HeaderText = "Nombre";
            dgvDatos.Columns[nameof(Rol.Descripcion)]!.HeaderText = "Descripción";
            dgvDatos.Columns[nameof(Rol.Estado)]!.Visible = false;
            dgvDatos.Columns[nameof(Rol.EstadoTexto)]!.HeaderText = "Estado";
        }
    }
}

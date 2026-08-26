using Proyecto_Gym_ProgramacionIII_Jeovanny.Datos;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    public class FrmUsuarios : FrmModuloBase
    {
        private readonly TextBox txtBuscar;
        private readonly Button btnEditar;

        public FrmUsuarios()
            : base("Usuarios", 82)
        {
            txtBuscar = CrearTexto(150);
            AgregarCampo("Buscar usuario", txtBuscar, 300);
            AgregarBoton("NUEVO", btnNuevo_Click);
            btnEditar = AgregarBoton("EDITAR", btnEditar_Click, false);
            AgregarBoton("ACTUALIZAR", btnActualizar_Click, false);
            btnEditar.Enabled = false;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            dgvDatos.SelectionChanged += dgvDatos_SelectionChanged;
            dgvDatos.CellDoubleClick += dgvDatos_CellDoubleClick;
            Load += FrmUsuarios_Load;
        }

        private async void FrmUsuarios_Load(object? sender, EventArgs e)
        {
            await CargarAsync();
        }

        private async Task CargarAsync()
        {
            try
            {
                LimpiarMensaje();
                List<Usuario> usuarios = await UsuarioRepositorio.ListarAsync(txtBuscar.Text);
                dgvDatos.DataSource = usuarios;
                ConfigurarTabla();
                btnEditar.Enabled = ObtenerSeleccionado() is not null;
                lblResumen.Text = $"Usuarios registrados: {usuarios.Count}";
            }
            catch (Exception ex)
            {
                MostrarExcepcion(ex);
            }
        }

        private Usuario? ObtenerSeleccionado()
        {
            return dgvDatos.CurrentRow?.DataBoundItem as Usuario;
        }

        private async void btnNuevo_Click(object? sender, EventArgs e)
        {
            using FrmUsuarioDetalle formulario = new FrmUsuarioDetalle();
            if (formulario.ShowDialog(this) == DialogResult.OK)
            {
                await CargarAsync();
            }
        }

        private async void btnEditar_Click(object? sender, EventArgs e)
        {
            Usuario? usuario = ObtenerSeleccionado();
            if (usuario is null)
            {
                lblMensaje.Text = "Selecciona un usuario de la lista.";
                return;
            }

            using FrmUsuarioDetalle formulario = new FrmUsuarioDetalle(usuario);
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

            foreach (string nombre in new[]
            {
                nameof(Usuario.IdUsuario),
                nameof(Usuario.ContrasenaHash),
                nameof(Usuario.ContrasenaSalt),
                nameof(Usuario.IdRol),
                nameof(Usuario.RolActivo),
                nameof(Usuario.Activo)
            })
            {
                dgvDatos.Columns[nombre]!.Visible = false;
            }

            dgvDatos.Columns[nameof(Usuario.NombreUsuario)]!.HeaderText = "Usuario";
            dgvDatos.Columns[nameof(Usuario.NombreCompleto)]!.HeaderText = "Nombre completo";
            dgvDatos.Columns[nameof(Usuario.NombreRol)]!.HeaderText = "Rol";
            dgvDatos.Columns[nameof(Usuario.EstadoTexto)]!.HeaderText = "Estado";
            dgvDatos.Columns[nameof(Usuario.FechaCreacion)]!.HeaderText = "Fecha de creación";
            dgvDatos.Columns[nameof(Usuario.FechaCreacion)]!.DefaultCellStyle.Format = "dd/MM/yyyy";
        }
    }
}

using Proyecto_Gym_ProgramacionIII_Jeovanny.Datos;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    public class FrmRolDetalle : Form
    {
        private readonly Rol? rolEditar;
        private readonly TextBox txtNombre;
        private readonly TextBox txtDescripcion;
        private readonly CheckBox chkEstado;
        private readonly Label lblMensaje;
        private readonly Button btnGuardar;

        public FrmRolDetalle(Rol? rol = null)
        {
            rolEditar = rol;
            BackColor = Color.White;
            ClientSize = new Size(540, 390);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = rol is null ? "Nuevo rol" : "Editar rol";

            Label lblTitulo = CrearEtiqueta(Text, 25, 20, 490, 42, 18F, true);
            Label lblNombre = CrearEtiqueta("Nombre del rol", 25, 78, 490, 22, 9F, true);
            txtNombre = new TextBox
            {
                CharacterCasing = CharacterCasing.Upper,
                Font = new Font("Segoe UI", 10F),
                Location = new Point(25, 103),
                MaxLength = 30,
                Size = new Size(490, 30)
            };
            Label lblDescripcion = CrearEtiqueta("Descripción", 25, 148, 490, 22, 9F, true);
            txtDescripcion = new TextBox
            {
                Font = new Font("Segoe UI", 10F),
                Location = new Point(25, 173),
                MaxLength = 150,
                Multiline = true,
                Size = new Size(490, 70)
            };
            chkEstado = new CheckBox
            {
                AutoSize = true,
                Checked = true,
                Font = new Font("Segoe UI", 10F),
                Location = new Point(25, 258),
                Text = "Rol activo"
            };
            lblMensaje = CrearEtiqueta(string.Empty, 25, 295, 230, 60, 9F, false);
            lblMensaje.ForeColor = Color.Firebrick;
            btnGuardar = CrearBoton("GUARDAR", 265, 307, true);
            Button btnCancelar = CrearBoton("CANCELAR", 395, 307, false);
            btnGuardar.Click += btnGuardar_Click;
            btnCancelar.DialogResult = DialogResult.Cancel;
            txtNombre.KeyPress += txtNombre_KeyPress;

            Controls.AddRange(new Control[]
            {
                lblTitulo,
                lblNombre,
                txtNombre,
                lblDescripcion,
                txtDescripcion,
                chkEstado,
                lblMensaje,
                btnGuardar,
                btnCancelar
            });

            AcceptButton = btnGuardar;
            CancelButton = btnCancelar;
            CargarRol();
        }

        private void CargarRol()
        {
            if (rolEditar is null)
            {
                return;
            }

            txtNombre.Text = rolEditar.Nombre;
            txtDescripcion.Text = rolEditar.Descripcion;
            chkEstado.Checked = rolEditar.Estado;

            if (rolEditar.Nombre.Equals("ADMIN", StringComparison.OrdinalIgnoreCase))
            {
                txtNombre.ReadOnly = true;
                chkEstado.Enabled = false;
            }
        }

        private async void btnGuardar_Click(object? sender, EventArgs e)
        {
            lblMensaje.Text = string.Empty;
            string nombre = txtNombre.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                lblMensaje.Text = "Escribe el nombre del rol.";
                txtNombre.Focus();
                return;
            }

            btnGuardar.Enabled = false;

            try
            {
                int idRol = rolEditar?.IdRol ?? 0;
                if (await RolRepositorio.ExisteNombreAsync(nombre, idRol))
                {
                    lblMensaje.Text = "Ya existe un rol con ese nombre.";
                    txtNombre.Focus();
                    return;
                }

                if (rolEditar is not null
                    && rolEditar.Estado
                    && !chkEstado.Checked
                    && await RolRepositorio.TieneUsuariosActivosAsync(idRol))
                {
                    lblMensaje.Text = "Este rol tiene usuarios activos y no puede desactivarse.";
                    return;
                }

                Rol rol = new Rol
                {
                    IdRol = idRol,
                    Nombre = nombre,
                    Descripcion = txtDescripcion.Text.Trim(),
                    Estado = chkEstado.Checked
                };

                if (rolEditar is null)
                {
                    await RolRepositorio.GuardarAsync(rol);
                }
                else
                {
                    await RolRepositorio.ActualizarAsync(rol);
                }

                MessageBox.Show(
                    "Rol guardado correctamente.",
                    "Roles",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception)
            {
                lblMensaje.Text = "No fue posible guardar el rol.";
            }
            finally
            {
                btnGuardar.Enabled = true;
            }
        }

        private void txtNombre_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ' && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private static Label CrearEtiqueta(
            string texto,
            int x,
            int y,
            int ancho,
            int alto,
            float tamano,
            bool negrita)
        {
            return new Label
            {
                Font = new Font("Segoe UI", tamano, negrita ? FontStyle.Bold : FontStyle.Regular),
                Location = new Point(x, y),
                Size = new Size(ancho, alto),
                Text = texto
            };
        }

        private static Button CrearBoton(string texto, int x, int y, bool principal)
        {
            Button boton = new Button
            {
                BackColor = principal ? Color.Black : Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = principal ? Color.White : Color.Black,
                Location = new Point(x, y),
                Size = new Size(120, 42),
                Text = texto
            };
            boton.FlatAppearance.BorderColor = Color.Black;
            boton.FlatAppearance.BorderSize = principal ? 0 : 1;
            return boton;
        }
    }
}

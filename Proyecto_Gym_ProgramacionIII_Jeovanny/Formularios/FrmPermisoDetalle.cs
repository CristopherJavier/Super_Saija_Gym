using Proyecto_Gym_ProgramacionIII_Jeovanny.Datos;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    public class FrmPermisoDetalle : Form
    {
        private readonly Permiso? permisoEditar;
        private readonly TextBox txtClave;
        private readonly TextBox txtNombre;
        private readonly TextBox txtDescripcion;
        private readonly CheckBox chkEstado;
        private readonly Label lblMensaje;
        private readonly Button btnGuardar;

        public FrmPermisoDetalle(Permiso? permiso = null)
        {
            permisoEditar = permiso;
            BackColor = Color.White;
            ClientSize = new Size(570, 475);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = permiso is null ? "Nuevo permiso" : "Editar permiso";

            Label lblTitulo = CrearEtiqueta(Text, 25, 20, 520, 42, 18F, true);
            Label lblClave = CrearEtiqueta("Código interno", 25, 76, 520, 22, 9F, true);
            txtClave = new TextBox
            {
                CharacterCasing = CharacterCasing.Upper,
                Font = new Font("Segoe UI", 10F),
                Location = new Point(25, 101),
                MaxLength = 60,
                Size = new Size(520, 30)
            };
            Label lblNombre = CrearEtiqueta("Nombre que verá el usuario", 25, 145, 520, 22, 9F, true);
            txtNombre = new TextBox
            {
                Font = new Font("Segoe UI", 10F),
                Location = new Point(25, 170),
                MaxLength = 80,
                Size = new Size(520, 30)
            };
            Label lblDescripcion = CrearEtiqueta("Descripción", 25, 214, 520, 22, 9F, true);
            txtDescripcion = new TextBox
            {
                Font = new Font("Segoe UI", 10F),
                Location = new Point(25, 239),
                MaxLength = 150,
                Multiline = true,
                Size = new Size(520, 70)
            };
            chkEstado = new CheckBox
            {
                AutoSize = true,
                Checked = true,
                Font = new Font("Segoe UI", 10F),
                Location = new Point(25, 325),
                Text = "Permiso activo"
            };
            lblMensaje = CrearEtiqueta(string.Empty, 25, 365, 260, 65, 9F, false);
            lblMensaje.ForeColor = Color.Firebrick;
            btnGuardar = CrearBoton("GUARDAR", 295, 380, true);
            Button btnCancelar = CrearBoton("CANCELAR", 425, 380, false);
            btnGuardar.Click += btnGuardar_Click;
            btnCancelar.DialogResult = DialogResult.Cancel;
            txtClave.KeyPress += txtClave_KeyPress;

            Controls.AddRange(new Control[]
            {
                lblTitulo,
                lblClave,
                txtClave,
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
            CargarPermiso();
        }

        private void CargarPermiso()
        {
            if (permisoEditar is null)
            {
                return;
            }

            txtClave.Text = permisoEditar.Clave;
            txtClave.ReadOnly = true;
            txtNombre.Text = permisoEditar.Nombre;
            txtDescripcion.Text = permisoEditar.Descripcion;
            chkEstado.Checked = permisoEditar.Estado;
        }

        private async void btnGuardar_Click(object? sender, EventArgs e)
        {
            lblMensaje.Text = string.Empty;
            string clave = txtClave.Text.Trim();
            string nombre = txtNombre.Text.Trim();

            if (string.IsNullOrWhiteSpace(clave))
            {
                lblMensaje.Text = "Escribe el código interno.";
                txtClave.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(nombre))
            {
                lblMensaje.Text = "Escribe el nombre del permiso.";
                txtNombre.Focus();
                return;
            }

            btnGuardar.Enabled = false;

            try
            {
                int idPermiso = permisoEditar?.IdPermiso ?? 0;
                if (await PermisoRepositorio.ExisteClaveAsync(clave, idPermiso))
                {
                    lblMensaje.Text = "Ya existe un permiso con ese código.";
                    txtClave.Focus();
                    return;
                }

                if (await PermisoRepositorio.ExisteNombreAsync(nombre, idPermiso))
                {
                    lblMensaje.Text = "Ya existe un permiso con ese nombre.";
                    txtNombre.Focus();
                    return;
                }

                Permiso permiso = new Permiso
                {
                    IdPermiso = idPermiso,
                    Clave = clave,
                    Nombre = nombre,
                    Descripcion = txtDescripcion.Text.Trim(),
                    Estado = chkEstado.Checked
                };

                if (permisoEditar is null)
                {
                    await PermisoRepositorio.GuardarAsync(permiso);
                }
                else
                {
                    await PermisoRepositorio.ActualizarAsync(permiso);
                }

                MessageBox.Show(
                    "Permiso guardado correctamente.",
                    "Permisos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception)
            {
                lblMensaje.Text = "No fue posible guardar el permiso.";
            }
            finally
            {
                btnGuardar.Enabled = true;
            }
        }

        private void txtClave_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar)
                && e.KeyChar != '_'
                && !char.IsControl(e.KeyChar))
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

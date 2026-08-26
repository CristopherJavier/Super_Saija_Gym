using Proyecto_Gym_ProgramacionIII_Jeovanny.Datos;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Seguridad;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    public class FrmUsuarioDetalle : Form
    {
        private readonly Usuario? usuarioEditar;
        private readonly TextBox txtNombreCompleto;
        private readonly TextBox txtNombreUsuario;
        private readonly ComboBox cmbRol;
        private readonly TextBox txtContrasena;
        private readonly TextBox txtConfirmarContrasena;
        private readonly CheckBox chkMostrarContrasena;
        private readonly CheckBox chkActivo;
        private readonly Label lblMensaje;
        private readonly Button btnGuardar;

        public FrmUsuarioDetalle(Usuario? usuario = null)
        {
            usuarioEditar = usuario;
            BackColor = Color.White;
            ClientSize = new Size(620, 650);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = usuario is null ? "Nuevo usuario" : "Editar usuario";

            Label lblTitulo = CrearEtiqueta(Text, 25, 18, 570, 42, 18F, true);
            Label lblNombreCompleto = CrearEtiqueta("Nombre completo", 25, 72, 570, 22, 9F, true);
            txtNombreCompleto = CrearTexto(25, 97, 570, 100);
            Label lblNombreUsuario = CrearEtiqueta("Nombre para iniciar sesión", 25, 143, 570, 22, 9F, true);
            txtNombreUsuario = CrearTexto(25, 168, 570, 50);
            Label lblRol = CrearEtiqueta("Rol", 25, 214, 570, 22, 9F, true);
            cmbRol = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10F),
                FormattingEnabled = true,
                Location = new Point(25, 239),
                Size = new Size(570, 30)
            };
            string textoContrasena = usuario is null
                ? "Contraseña"
                : "Nueva contraseña (déjala vacía para conservar la actual)";
            Label lblContrasena = CrearEtiqueta(textoContrasena, 25, 285, 570, 22, 9F, true);
            txtContrasena = CrearTexto(25, 310, 570, 100);
            txtContrasena.UseSystemPasswordChar = true;
            Label lblConfirmar = CrearEtiqueta("Confirmar contraseña", 25, 356, 570, 22, 9F, true);
            txtConfirmarContrasena = CrearTexto(25, 381, 570, 100);
            txtConfirmarContrasena.UseSystemPasswordChar = true;
            chkMostrarContrasena = new CheckBox
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 9F),
                Location = new Point(25, 427),
                Text = "Mostrar contraseña"
            };
            chkActivo = new CheckBox
            {
                AutoSize = true,
                Checked = true,
                Font = new Font("Segoe UI", 10F),
                Location = new Point(25, 463),
                Text = "Usuario activo"
            };
            Label lblAviso = CrearEtiqueta(
                "Los cambios de rol y permisos se aplican al próximo inicio de sesión.",
                25,
                500,
                570,
                28,
                9F,
                false);
            lblAviso.ForeColor = Color.FromArgb(80, 80, 80);
            lblMensaje = CrearEtiqueta(string.Empty, 25, 542, 290, 65, 9F, false);
            lblMensaje.ForeColor = Color.Firebrick;
            btnGuardar = CrearBoton("GUARDAR", 335, 555, true);
            Button btnCancelar = CrearBoton("CANCELAR", 475, 555, false);
            btnGuardar.Enabled = false;
            btnGuardar.Click += btnGuardar_Click;
            btnCancelar.DialogResult = DialogResult.Cancel;
            chkMostrarContrasena.CheckedChanged += chkMostrarContrasena_CheckedChanged;
            txtNombreCompleto.KeyPress += txtNombreCompleto_KeyPress;
            Load += FrmUsuarioDetalle_Load;

            Controls.AddRange(new Control[]
            {
                lblTitulo,
                lblNombreCompleto,
                txtNombreCompleto,
                lblNombreUsuario,
                txtNombreUsuario,
                lblRol,
                cmbRol,
                lblContrasena,
                txtContrasena,
                lblConfirmar,
                txtConfirmarContrasena,
                chkMostrarContrasena,
                chkActivo,
                lblAviso,
                lblMensaje,
                btnGuardar,
                btnCancelar
            });

            AcceptButton = btnGuardar;
            CancelButton = btnCancelar;
        }

        private async void FrmUsuarioDetalle_Load(object? sender, EventArgs e)
        {
            try
            {
                List<Rol> roles = await RolRepositorio.ListarAsync(soloActivos: true);
                cmbRol.DataSource = roles;
                cmbRol.DisplayMember = nameof(Rol.Nombre);

                if (usuarioEditar is not null)
                {
                    txtNombreCompleto.Text = usuarioEditar.NombreCompleto;
                    txtNombreUsuario.Text = usuarioEditar.NombreUsuario;
                    chkActivo.Checked = usuarioEditar.Activo;
                    cmbRol.SelectedItem = roles.FirstOrDefault(rol => rol.IdRol == usuarioEditar.IdRol);
                }

                btnGuardar.Enabled = roles.Count > 0;

                if (roles.Count == 0)
                {
                    lblMensaje.Text = "No existen roles activos para asignar.";
                }
            }
            catch (Exception)
            {
                lblMensaje.Text = "No fue posible cargar los roles.";
            }
        }

        private async void btnGuardar_Click(object? sender, EventArgs e)
        {
            lblMensaje.Text = string.Empty;
            string nombreCompleto = txtNombreCompleto.Text.Trim();
            string nombreUsuario = txtNombreUsuario.Text.Trim();
            string contrasena = txtContrasena.Text;
            string confirmacion = txtConfirmarContrasena.Text;

            if (string.IsNullOrWhiteSpace(nombreCompleto))
            {
                lblMensaje.Text = "Escribe el nombre completo.";
                txtNombreCompleto.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(nombreUsuario))
            {
                lblMensaje.Text = "Escribe el nombre para iniciar sesión.";
                txtNombreUsuario.Focus();
                return;
            }

            if (cmbRol.SelectedItem is not Rol rol)
            {
                lblMensaje.Text = "Selecciona un rol.";
                cmbRol.Focus();
                return;
            }

            if (usuarioEditar is null && string.IsNullOrWhiteSpace(contrasena))
            {
                lblMensaje.Text = "Escribe una contraseña para el usuario.";
                txtContrasena.Focus();
                return;
            }

            if (!string.IsNullOrEmpty(contrasena) || !string.IsNullOrEmpty(confirmacion))
            {
                if (string.IsNullOrWhiteSpace(contrasena))
                {
                    lblMensaje.Text = "Escribe la nueva contraseña.";
                    txtContrasena.Focus();
                    return;
                }

                if (contrasena.Length < 4)
                {
                    lblMensaje.Text = "La contraseña debe tener al menos 4 caracteres.";
                    txtContrasena.Focus();
                    return;
                }

                if (contrasena != confirmacion)
                {
                    lblMensaje.Text = "Las contraseñas no coinciden.";
                    txtConfirmarContrasena.Focus();
                    return;
                }
            }

            btnGuardar.Enabled = false;

            try
            {
                int idUsuario = usuarioEditar?.IdUsuario ?? 0;
                if (await UsuarioRepositorio.ExisteNombreUsuarioAsync(nombreUsuario, idUsuario))
                {
                    lblMensaje.Text = "Ese nombre de usuario ya está registrado.";
                    txtNombreUsuario.Focus();
                    return;
                }

                if (usuarioEditar is not null
                    && await UsuarioRepositorio.EsUltimoAdministradorActivoAsync(idUsuario)
                    && (!chkActivo.Checked || !rol.Nombre.Equals("ADMIN", StringComparison.OrdinalIgnoreCase)))
                {
                    lblMensaje.Text = "Debe permanecer al menos un administrador activo.";
                    return;
                }

                Usuario usuario = new Usuario
                {
                    IdUsuario = idUsuario,
                    NombreUsuario = nombreUsuario,
                    NombreCompleto = nombreCompleto,
                    IdRol = rol.IdRol,
                    NombreRol = rol.Nombre,
                    Activo = chkActivo.Checked
                };

                if (usuarioEditar is null)
                {
                    PasswordHelper.CrearHash(contrasena, out string hash, out string salt);
                    await UsuarioRepositorio.GuardarAsync(usuario, hash, salt);
                }
                else
                {
                    await UsuarioRepositorio.ActualizarAsync(usuario);

                    if (!string.IsNullOrWhiteSpace(contrasena))
                    {
                        PasswordHelper.CrearHash(contrasena, out string hash, out string salt);
                        await UsuarioRepositorio.ActualizarContrasenaAsync(idUsuario, hash, salt);
                    }
                }

                MessageBox.Show(
                    "Usuario guardado correctamente.",
                    "Usuarios",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception)
            {
                lblMensaje.Text = "No fue posible guardar el usuario.";
            }
            finally
            {
                btnGuardar.Enabled = true;
            }
        }

        private void chkMostrarContrasena_CheckedChanged(object? sender, EventArgs e)
        {
            bool ocultar = !chkMostrarContrasena.Checked;
            txtContrasena.UseSystemPasswordChar = ocultar;
            txtConfirmarContrasena.UseSystemPasswordChar = ocultar;
        }

        private void txtNombreCompleto_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ' && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private static TextBox CrearTexto(int x, int y, int ancho, int limite)
        {
            return new TextBox
            {
                Font = new Font("Segoe UI", 10F),
                Location = new Point(x, y),
                MaxLength = limite,
                Size = new Size(ancho, 30)
            };
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

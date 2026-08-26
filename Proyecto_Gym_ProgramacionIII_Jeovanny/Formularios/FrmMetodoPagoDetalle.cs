using Proyecto_Gym_ProgramacionIII_Jeovanny.Datos;
using Proyecto_Gym_ProgramacionIII_Jeovanny.Modelos;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Formularios
{
    public class FrmMetodoPagoDetalle : Form
    {
        private readonly MetodoPago? metodoEditar;
        private readonly Label lblTitulo;
        private readonly TextBox txtNombre;
        private readonly TextBox txtDescripcion;
        private readonly CheckBox chkEstado;
        private readonly Label lblMensaje;
        private readonly Button btnGuardar;

        public FrmMetodoPagoDetalle(MetodoPago? metodo = null)
        {
            metodoEditar = metodo;
            BackColor = Color.White;
            ClientSize = new Size(540, 390);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = metodo is null ? "Nuevo método de pago" : "Editar método de pago";

            lblTitulo = CrearEtiqueta(Text, 25, 20, 490, 42, 18F, true);
            Label lblNombre = CrearEtiqueta("Nombre", 25, 78, 490, 22, 9F, true);
            txtNombre = new TextBox
            {
                Font = new Font("Segoe UI", 10F),
                Location = new Point(25, 103),
                MaxLength = 50,
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
                Text = "Disponible para usar"
            };
            lblMensaje = CrearEtiqueta(string.Empty, 25, 295, 230, 60, 9F, false);
            lblMensaje.ForeColor = Color.Firebrick;
            btnGuardar = CrearBoton("GUARDAR", 265, 307, true);
            Button btnCancelar = CrearBoton("CANCELAR", 395, 307, false);
            btnGuardar.Click += btnGuardar_Click;
            btnCancelar.DialogResult = DialogResult.Cancel;

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
            CargarMetodo();
        }

        private void CargarMetodo()
        {
            if (metodoEditar is null)
            {
                return;
            }

            txtNombre.Text = metodoEditar.Nombre;
            txtDescripcion.Text = metodoEditar.Descripcion;
            chkEstado.Checked = metodoEditar.Estado;
        }

        private async void btnGuardar_Click(object? sender, EventArgs e)
        {
            lblMensaje.Text = string.Empty;
            string nombre = txtNombre.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                lblMensaje.Text = "Escribe el nombre del método.";
                txtNombre.Focus();
                return;
            }

            btnGuardar.Enabled = false;

            try
            {
                int idMetodo = metodoEditar?.IdMetodoPago ?? 0;
                if (await MetodoPagoRepositorio.ExisteNombreAsync(nombre, idMetodo))
                {
                    lblMensaje.Text = "Ya existe un método con ese nombre.";
                    txtNombre.Focus();
                    return;
                }

                MetodoPago metodo = new MetodoPago
                {
                    IdMetodoPago = idMetodo,
                    Nombre = nombre,
                    Descripcion = txtDescripcion.Text.Trim(),
                    Estado = chkEstado.Checked
                };

                if (metodoEditar is null)
                {
                    await MetodoPagoRepositorio.GuardarAsync(metodo);
                }
                else
                {
                    await MetodoPagoRepositorio.ActualizarAsync(metodo);
                }

                MessageBox.Show(
                    "Método de pago guardado correctamente.",
                    "Métodos de pago",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception)
            {
                lblMensaje.Text = "No fue posible guardar el método de pago.";
            }
            finally
            {
                btnGuardar.Enabled = true;
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
                DialogResult = DialogResult.None,
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

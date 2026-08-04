# Guía para explicar el formulario de inicio de sesión

## 1. ¿Qué es Windows Forms?

Windows Forms es una tecnología de .NET que permite crear aplicaciones de escritorio para Windows. La interfaz se construye usando ventanas y controles como etiquetas, cajas de texto y botones.

## 2. ¿Qué representa un Form?

Un `Form` representa una ventana de la aplicación. En este proyecto, `FrmLogin` es la ventana de inicio de sesión. El formulario sirve como contenedor principal de todos los controles que aparecen dentro de esa ventana.

## 3. ¿Qué es un control?

Un control es un elemento visual que se coloca dentro de un formulario. Puede mostrar información, recibir datos del usuario o ejecutar una acción. Cada control tiene propiedades como nombre, texto, tamaño, color y posición.

## 4. Controles utilizados

- `Label`: muestra un texto informativo que normalmente no edita el usuario.
- `TextBox`: permite que el usuario escriba información.
- `Button`: permite ejecutar una acción cuando se presiona.
- `CheckBox`: permite activar o desactivar una opción.
- `Panel`: agrupa y organiza otros controles dentro del formulario.

## 5. Diferencia entre Name y Text

`Name` es el nombre que utiliza el programador para identificar un control dentro del código. Por ejemplo, `btnSalir`.

`Text` es el contenido visible para el usuario. El control llamado `btnSalir` muestra el texto `SALIR`.

Cambiar `Text` no cambia el nombre utilizado en el código, y cambiar `Name` no cambia automáticamente el texto visible.

## 6. Convenciones de nombres

Las primeras letras indican el tipo de control:

- `txtUsuario`: `txt` indica que es un `TextBox` y `Usuario` explica para qué se utiliza.
- `btnSalir`: `btn` indica que es un `Button` y `Salir` explica su acción.
- `lblMensaje`: `lbl` indica que es un `Label` y `Mensaje` explica que muestra información al usuario.

Esta convención ayuda a reconocer rápidamente los controles cuando se lee el código.

## 7. ¿Qué es un evento?

Un evento es una acción que ocurre durante el uso del programa. Por ejemplo, el usuario puede presionar un botón, marcar una casilla o escribir en una caja de texto. El programa puede ejecutar un método cuando ocurre uno de estos eventos.

## 8. ¿Qué hace el evento Click?

El evento `Click` ocurre cuando el usuario presiona un botón. En este formulario se utiliza para validar los campos al presionar `INICIAR SESIÓN` y para pedir confirmación al presionar `SALIR`.

## 9. ¿Qué hace CheckedChanged?

El evento `CheckedChanged` ocurre cuando cambia el estado marcado o desmarcado de un `CheckBox`. En `chkMostrarContrasena` permite mostrar la contraseña cuando está marcado y volver a ocultarla cuando no está marcado.

## 10. ¿Para qué sirve InitializeComponent()?

`InitializeComponent()` es el método que crea y configura los controles del formulario. Establece sus textos, colores, tamaños, posiciones y eventos. Visual Studio genera este método dentro de `FrmLogin.Designer.cs` y el constructor de `FrmLogin` lo ejecuta cuando se crea la ventana.

## 11. División de FrmLogin

- `FrmLogin.cs`: contiene el constructor, los eventos y la lógica escrita por el programador.
- `FrmLogin.Designer.cs`: contiene la creación y configuración visual de los controles.
- `FrmLogin.resx`: almacena recursos que puede utilizar el formulario, como iconos, imágenes o textos localizados.

Los dos archivos `.cs` utilizan la palabra `partial`. Esto permite dividir la misma clase `FrmLogin` entre varios archivos sin crear clases diferentes.

## 12. Funcionamiento de ValidarCampos()

El método `ValidarCampos()` trabaja de la siguiente manera:

1. Obtiene el usuario con `txtUsuario.Text.Trim()`.
2. Obtiene la contraseña con `txtContrasena.Text`.
3. Limpia cualquier mensaje mostrado anteriormente.
4. Prepara el color rojo suave que se utilizará para los errores.
5. Comprueba si el usuario está vacío. Si está vacío, muestra un mensaje, coloca el cursor en `txtUsuario` y devuelve `false`.
6. Comprueba si la contraseña está vacía. Si está vacía, muestra un mensaje, coloca el cursor en `txtContrasena` y devuelve `false`.
7. Si ambos campos tienen contenido, devuelve `true`.

Cuando el método devuelve `true`, el evento del botón muestra un mensaje con el color de acento azul grisáceo para indicar que los datos están completos.

## 13. ¿Por qué se usa Trim() solamente en el usuario?

`Trim()` elimina los espacios que existan al principio y al final de un texto. Se utiliza en el usuario para evitar que un valor compuesto solamente por espacios sea aceptado.

No se utiliza en la contraseña porque los espacios pueden formar parte intencional de una contraseña. Modificarlos cambiaría el valor que escribió el usuario.

## 14. ¿Qué hace Focus()?

`Focus()` coloca el cursor en un control. Si falta el usuario, `txtUsuario.Focus()` permite escribirlo inmediatamente. Si falta la contraseña, `txtContrasena.Focus()` coloca el cursor en el campo de contraseña.

## 15. ¿Qué hace return dentro de la validación?

`return` termina la ejecución del método y devuelve un resultado. `return false` detiene la validación cuando falta un campo. Esto evita continuar como si todos los datos estuvieran completos. `return true` indica que los dos campos contienen información.

## 16. Estado actual de la autenticación

Este formulario todavía no comprueba usuarios ni contraseñas reales. Solo verifica que los dos campos tengan contenido. La autenticación real se agregará en otra fase mediante PostgreSQL y consultas parametrizadas, después de recibir autorización.

## 17. ¿Qué es un mantenimiento?

Un mantenimiento es una parte del sistema que permite administrar datos básicos que otros módulos necesitarán. Clientes, entrenadores, tipos de membresías, clases, horarios, categorías, productos y proveedores son mantenimientos porque sus registros se crean y organizan antes de utilizarlos en procesos como ventas o reservas.

## 18. ¿Qué significa CRUD?

CRUD reúne las cuatro operaciones básicas de un mantenimiento:

- `Create`: crear un registro.
- `Read`: leer o mostrar registros.
- `Update`: actualizar un registro existente.
- `Delete`: eliminar un registro o, cuando convenga conservarlo, desactivarlo mediante su estado.

En esta fase se prepara la base de datos que permitirá implementar esas operaciones, pero todavía no se crean los formularios.

## 19. Relaciones 1:1, 1:N y N:M

Una relación 1:1 significa que un registro puede estar relacionado con uno solo de la otra tabla. El ejemplo es `usuarios` con `usuarios_perfiles`, porque cada usuario puede tener un único perfil.

Una relación 1:N significa que un registro principal puede relacionarse con muchos registros. Por ejemplo, una categoría puede tener muchos productos, pero cada producto pertenece a una categoría.

Una relación N:M significa que varios registros de una tabla pueden relacionarse con varios de otra. Productos y proveedores tienen esta relación: un producto puede tener varios proveedores y un proveedor puede ofrecer varios productos. La tabla intermedia `productos_proveedores` guarda las combinaciones.

## 20. Claves primarias y foráneas

La clave primaria identifica cada registro de forma única. Por ejemplo, `id_producto` permite distinguir un producto de todos los demás.

La clave foránea guarda la referencia hacia otra tabla y asegura que el registro relacionado exista. Por ejemplo, `productos.id_categoria` apunta a `categorias_productos.id_categoria` e impide asignar una categoría inexistente.

## 21. ¿Por qué se diseña la base antes de los formularios?

La base define qué datos se guardarán, cuáles son obligatorios, qué valores son válidos y cómo se relacionan las tablas. Revisar primero esta estructura evita crear controles para datos incorrectos o tener que rehacer formularios cuando aparezcan relaciones que no se habían considerado.

Después de aprobar el diseño, cada formulario podrá construirse de acuerdo con campos y reglas ya definidos.

## 22. Tabla de mantenimiento y tabla de proceso

Una tabla de mantenimiento guarda datos relativamente estables que sirven como catálogo o referencia, como clientes, productos, categorías o proveedores.

Una tabla de proceso registra operaciones realizadas en una fecha y con participantes específicos, como una venta, una compra, un cobro o una reserva. Normalmente utiliza datos de varios mantenimientos y puede necesitar encabezados, detalles, totales y estados propios. Las tablas de proceso se definirán en fases posteriores porque no pertenecen al alcance actual.

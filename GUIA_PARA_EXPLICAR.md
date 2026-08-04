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

`FrmLogin` comprueba los campos, busca el usuario mediante una consulta parametrizada y verifica la contraseña con `PasswordHelper`. Cuando el acceso es correcto crea la sesión y abre `FrmPrincipal`. Los permisos de ventanas y los mantenimientos todavía pertenecen a fases posteriores.

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

## 23. Creación segura del primer administrador

### ¿Por qué no se guarda la contraseña original?

La contraseña original es un dato secreto que solo debe conocer la persona que la escribe. Guardarla directamente permitiría que alguien pudiera leerla si obtiene acceso a la base de datos. El programa la utiliza temporalmente para calcular un hash y no la envía al repositorio ni la almacena.

### ¿Qué es un hash?

Un hash es un resultado calculado a partir de la contraseña. Sirve para comprobar posteriormente si una contraseña escrita produce el mismo resultado, pero no está diseñado para recuperar la contraseña original.

### ¿Qué es una sal?

La sal es un conjunto de bytes aleatorios que se genera para cada contraseña. Se combina con la contraseña antes de calcular el hash. Esto hace que dos personas con la misma contraseña obtengan resultados almacenados diferentes.

### ¿Qué hace PBKDF2?

PBKDF2 aplica repetidamente una función criptográfica a la contraseña y la sal. Las iteraciones hacen que comprobar una contraseña válida siga siendo práctico, pero aumentan el trabajo necesario para intentar adivinar muchas contraseñas.

En el proyecto se usa PBKDF2 con SHA256, una sal aleatoria y una cantidad fija de iteraciones. El hash y la sal se convierten a Base64 para poder guardarlos como texto en PostgreSQL.

### ¿Por qué se usa FixedTimeEquals?

`FixedTimeEquals` compara el hash calculado con el hash almacenado procurando que el tiempo de comparación no revele en qué posición apareció una diferencia. Es una comparación apropiada para valores relacionados con seguridad.

### ¿Qué hace PasswordHelper?

`PasswordHelper` concentra las operaciones de seguridad de contraseñas. `CrearHash` genera una sal aleatoria, calcula el hash y devuelve ambos valores en Base64. `VerificarContrasena` vuelve a calcular el hash con la sal guardada y compara los resultados. Esta clase no conoce formularios ni accede a PostgreSQL.

### ¿Qué hace UsuarioRepositorio?

`UsuarioRepositorio` contiene las operaciones de la tabla `usuarios` necesarias para esta fase. Comprueba si ya existen usuarios, verifica si un nombre de usuario está ocupado y crea el primer administrador usando el identificador del rol `ADMIN` almacenado en PostgreSQL.

El repositorio recibe el hash y la sal, no la contraseña original. También devuelve el identificador generado por PostgreSQL cuando el administrador se crea correctamente.

### ¿Qué significa una consulta parametrizada?

Una consulta parametrizada mantiene el texto SQL separado de los valores escritos por el usuario. En lugar de concatenar esos valores dentro de la consulta se utilizan parámetros como `@nombreUsuario`. Esto ayuda a evitar inyección SQL y permite que Npgsql envíe cada valor con su tipo correspondiente.

### ¿Por qué el formulario inicial aparece una sola vez?

Al iniciar, el programa consulta con `EXISTS` si la tabla `usuarios` contiene al menos un registro. Si está vacía, muestra `FrmConfiguracionInicial`. Después de crear el administrador, la tabla deja de estar vacía y las siguientes ejecuciones abren directamente `FrmLogin`.

El formulario inicial no contiene datos predeterminados. El nombre completo, el usuario y la contraseña deben ser escritos manualmente por la persona que realiza la configuración.

### Flujo de Program.cs

1. Inicializa Windows Forms.
2. Consulta si existen usuarios mediante `UsuarioRepositorio.HayUsuariosAsync()`.
3. Si existen usuarios, ejecuta `FrmLogin`.
4. Si no existen usuarios, muestra `FrmConfiguracionInicial` como diálogo.
5. Si el administrador se crea correctamente, abre `FrmLogin`.
6. Si se cancela la configuración o falla la conexión inicial, termina la aplicación.

El inicio de sesión conserva la validación de campos y ahora también compara las credenciales usando PostgreSQL y `PasswordHelper`.

### Contraseña original, hash y salt

- Contraseña original: texto secreto escrito por el usuario y utilizado únicamente durante el cálculo.
- Hash: resultado derivado de la contraseña mediante PBKDF2 y SHA256.
- Salt o sal: valor aleatorio que se combina con la contraseña para producir un resultado diferente para cada registro.

En PostgreSQL solo se guardan el hash y la sal en Base64. No se guarda la contraseña original.

### Cómo explicarlo al profesor

Se puede explicar que el programa primero comprueba si la instalación ya tiene usuarios. Cuando la tabla está vacía presenta un formulario especial para crear el administrador. El formulario valida los datos, transforma la contraseña con PBKDF2 y entrega al repositorio solamente el hash y la sal. El repositorio busca el rol `ADMIN`, ejecuta una inserción parametrizada y devuelve el identificador creado.

Esta separación permite explicar tres responsabilidades sencillas: el formulario recoge y valida datos, `PasswordHelper` protege la contraseña y `UsuarioRepositorio` se comunica con PostgreSQL.

### Cinco preguntas posibles del profesor

#### 1. ¿Por qué no se puede recuperar la contraseña desde el hash?

Porque el hash se usa para comparar resultados, no para cifrar y descifrar información. Para validar una contraseña se calcula un hash nuevo y se compara con el almacenado.

#### 2. ¿Por qué se necesita una sal si ya existe un hash?

Porque la sal hace que contraseñas iguales produzcan hashes almacenados diferentes y dificulta el uso de resultados calculados previamente.

#### 3. ¿Cómo se evita la inyección SQL al crear el usuario?

Los datos se envían mediante parámetros de Npgsql. No se concatenan el nombre, el usuario, el hash ni la sal dentro del texto SQL.

#### 4. ¿Cómo sabe el programa cuándo debe mostrar la configuración inicial?

Ejecuta una consulta con `EXISTS` sobre `usuarios`. Si no hay registros muestra la configuración; si hay al menos uno abre el login.

#### 5. ¿Qué se agregó después de crear el primer administrador?

Se conectó `FrmLogin` con PostgreSQL, se utilizó `PasswordHelper.VerificarContrasena`, se creó `SesionActual` y se agregó una pantalla principal básica.

## 24. Autenticación y sesión

### Cómo recibe los datos FrmLogin

`FrmLogin` obtiene el nombre de usuario desde `txtUsuario` y elimina solamente los espacios del principio y del final. La contraseña se obtiene desde `txtContrasena` sin aplicar `Trim()`, porque un espacio puede formar parte de la contraseña elegida por el usuario.

Antes de consultar PostgreSQL, `ValidarCampos()` comprueba que ambos campos tengan contenido. La lógica de autenticación solo continúa cuando esta validación devuelve `true`.

### Cómo busca al usuario UsuarioRepositorio

`UsuarioRepositorio.ObtenerPorNombreUsuarioAsync()` abre una conexión, relaciona `usuarios` con `roles` mediante `id_rol` y busca el nombre sin diferenciar mayúsculas de minúsculas. La consulta devuelve los datos del usuario y el nombre de su rol. Si no encuentra el registro, devuelve `null`.

El repositorio no verifica la contraseña ni muestra mensajes. Su responsabilidad es obtener y convertir los datos de PostgreSQL en un objeto `Usuario`.

### Por qué la consulta usa un parámetro

El nombre escrito se envía mediante `@nombreUsuario`. El parámetro mantiene el valor separado del texto SQL, evita concatenaciones y ayuda a prevenir inyección SQL. Npgsql se encarga de enviar el valor correctamente a PostgreSQL.

### Mensaje para usuario o contraseña incorrectos

Un usuario inexistente y una contraseña incorrecta muestran el mismo mensaje: `Usuario o contraseña incorrectos.` Esto evita confirmar a una persona externa si un nombre de usuario específico está registrado.

### Cómo PasswordHelper verifica el hash

Después de obtener el usuario, `PasswordHelper.VerificarContrasena()` convierte desde Base64 el hash y la sal almacenados. Luego calcula un hash nuevo usando la contraseña escrita, la misma sal, PBKDF2 y SHA256. Finalmente compara los dos hashes mediante `FixedTimeEquals`.

La contraseña escrita no se guarda en la sesión ni se envía nuevamente a PostgreSQL.

### Qué contiene SesionActual

`SesionActual` guarda únicamente:

- Identificador del usuario.
- Nombre de usuario.
- Nombre completo.
- Identificador del rol.
- Nombre del rol.
- Indicador de que existe una sesión.

Estos datos permiten identificar al usuario autenticado durante el uso de la aplicación.

### Por qué la sesión no guarda datos de contraseña

La sesión no necesita la contraseña, el hash ni la sal después de completar la autenticación. Conservar esos datos aumentaría innecesariamente la cantidad de información sensible disponible en memoria. `SesionActual.Iniciar()` copia solamente los datos de identidad y rol.

### Cómo obtiene FrmPrincipal el nombre y el rol

Cuando se carga, `FrmPrincipal` comprueba `SesionActual.HaySesion`. Si existe sesión, forma el saludo con `SesionActual.NombreCompleto` y muestra el rol usando `SesionActual.NombreRol`. El formulario no contiene nombres o roles predeterminados.

### Diferencia entre cerrar sesión y salir

Cerrar sesión establece `CerrarSesionSolicitada` en `true` y cierra `FrmPrincipal`. Entonces `FrmLogin` limpia `SesionActual`, borra los campos y vuelve a mostrarse para permitir otro acceso.

Salir mantiene `CerrarSesionSolicitada` en `false`. `FrmLogin` limpia la sesión, se cierra y la aplicación termina normalmente. Cerrar `FrmPrincipal` mediante la X produce el mismo resultado que salir.

### Por qué FrmPrincipal no consulta PostgreSQL

Los datos necesarios ya fueron obtenidos durante la autenticación y copiados a `SesionActual`. Volver a consultar la base para mostrar el saludo y el rol sería trabajo repetido. También mezclaría acceso a datos con la responsabilidad visual del formulario.

### Qué hacen async y await en el botón

El evento `btnIniciarSesion_Click` es asíncrono porque abrir la conexión y consultar PostgreSQL puede tomar tiempo. `await` espera el resultado sin bloquear innecesariamente la interfaz. Cuando la consulta termina, el método continúa con la verificación de la contraseña.

### Por qué se desactiva el botón

El botón se desactiva mientras se consulta y se verifica el acceso para evitar varios clics y solicitudes repetidas. El bloque `finally` vuelve a activarlo siempre que `FrmLogin` continúe abierto.

### Flujo completo

1. `FrmLogin` valida que los campos tengan contenido.
2. `UsuarioRepositorio` busca al usuario y obtiene su rol desde PostgreSQL.
3. `PasswordHelper` verifica la contraseña contra el hash y la sal.
4. `SesionActual` conserva los datos no sensibles del usuario autenticado.
5. `FrmPrincipal` muestra el nombre completo y el rol de la sesión.

El flujo puede resumirse así:

`FrmLogin` → `UsuarioRepositorio` → `PasswordHelper` → `SesionActual` → `FrmPrincipal`

### Cómo explicarlo al profesor

Se puede explicar que cada parte tiene una responsabilidad sencilla. El formulario recibe datos y muestra resultados; el repositorio consulta PostgreSQL; el ayudante de contraseñas realiza la comparación segura; la sesión conserva la identidad durante el uso; y el formulario principal presenta los datos autenticados.

No se creó un servicio adicional porque estas cinco partes ya separan claramente las responsabilidades necesarias para esta etapa.

### Cómo realizar una modificación en vivo

Para cambiar un mensaje se edita el texto asignado a `lblMensaje` dentro de `FrmLogin.cs`. Para agregar o modificar una validación sencilla se trabaja dentro de `ValidarCampos()`. Para cambiar el comportamiento de cierre de sesión se revisa la condición `CerrarSesionSolicitada` después de cerrar `FrmPrincipal`.

Después de cada modificación se debe compilar y probar el caso afectado.

### Cinco preguntas posibles del profesor

#### 1. ¿Por qué el repositorio no comprueba la contraseña?

Porque el repositorio se encarga de obtener datos. La verificación criptográfica pertenece a `PasswordHelper`, lo que mantiene cada responsabilidad separada y fácil de explicar.

#### 2. ¿Qué ocurre si el usuario está inactivo?

El login detiene el acceso antes de verificar la contraseña, muestra que el usuario está inactivo y no inicia una sesión.

#### 3. ¿Cómo llegan el nombre completo y el rol al formulario principal?

PostgreSQL los devuelve al repositorio, el repositorio crea un objeto `Usuario`, el login copia sus datos a `SesionActual` y `FrmPrincipal` los lee desde esa sesión.

#### 4. ¿Por qué cerrar sesión no reinicia la aplicación?

Porque `FrmPrincipal` se abre como diálogo desde la única instancia de `FrmLogin`. Al solicitar cerrar sesión, el formulario principal termina y el login oculto vuelve a mostrarse limpio.

#### 5. ¿Qué evita que se envíen varias consultas al presionar varias veces?

El botón de inicio de sesión se desactiva antes de consultar y se vuelve a activar en `finally` cuando corresponde.

## 25. Navegación con menú lateral y panel contenedor

### Menú lateral

`FrmPrincipal` mantiene `pnlMenuLateral` en el lado izquierdo. Este panel contiene el nombre del sistema, las opciones disponibles y los datos de la sesión. Como utiliza `DockStyle.Left`, conserva su ancho cuando la ventana cambia de tamaño.

Los botones todavía no abren mantenimientos reales. En esta fase solamente cambian el título y el mensaje temporal de `pnlContenido`.

### Panel contenedor

`pnlContenido` representa el área cambiante de la ventana principal. El menú y el encabezado permanecen visibles mientras este panel muestra la portada, un aviso temporal o, en fases posteriores, un formulario de mantenimiento.

Este enfoque se parece a una página con menú lateral: la ventana principal permanece abierta y solo cambia su zona central.

### Qué hace Dock

La propiedad `Dock` permite que un control ocupe un borde o todo el espacio disponible de su contenedor. En este diseño:

- `pnlMenuLateral` usa `DockStyle.Left`.
- `pnlEncabezado` usa `DockStyle.Top`.
- `pnlContenido` usa `DockStyle.Fill`.
- `pnlSesion` usa `DockStyle.Bottom`.
- `pnlOpciones` usa `DockStyle.Fill` dentro del menú.

Gracias a estas propiedades no se necesitan cálculos manuales cuando el usuario maximiza, restaura o cambia el tamaño de la ventana.

### Ventana independiente y formulario incrustado

Una ventana independiente aparece separada y tiene su propio borde, título y posición en el escritorio. Un formulario incrustado se coloca dentro de `pnlContenido` y se comporta como parte de `FrmPrincipal`.

Los futuros mantenimientos principales se incrustarán para conservar el menú y el encabezado. Los formularios pequeños para agregar o editar registros podrán ser diálogos independientes cuando esa fase sea autorizada.

### Qué hace TopLevel = false

Un formulario normalmente es una ventana de nivel superior. Al establecer `TopLevel = false`, Windows Forms permite agregarlo como control dentro de otro contenedor, en este caso `pnlContenido`.

### Qué hace FormBorderStyle.None

`FormBorderStyle.None` elimina el borde y la barra de título del formulario incrustado. Así no parece una segunda ventana colocada dentro de la principal.

### Qué hace Dock.Fill

Después de incrustar el formulario, `DockStyle.Fill` hace que ocupe todo el espacio disponible en `pnlContenido` y se adapte cuando cambia el tamaño de `FrmPrincipal`.

### Qué hace AbrirFormularioEnPanel()

`AbrirFormularioEnPanel()` cierra el formulario interno anterior, configura el nuevo para que pueda incrustarse, limpia el panel, agrega el formulario y lo muestra al frente. El método queda preparado, pero todavía no se utiliza porque no existen formularios de mantenimiento autorizados.

Cuando exista un formulario como `FrmClientes`, su botón podrá reemplazar el aviso temporal por una llamada sencilla a este método.

### Contenido temporal

`MostrarInicio()` limpia el área central y presenta la portada. `MostrarModuloPendiente()` recibe el nombre de una opción y muestra que ese mantenimiento se implementará en la siguiente fase. Ninguno de estos métodos consulta PostgreSQL ni realiza operaciones CRUD.

### Esto no es arquitectura por capas

Incrustar formularios es una decisión de navegación y presentación visual. No define por sí mismo una arquitectura por capas ni introduce servicios, interfaces o patrones adicionales.

El proyecto conserva una separación sencilla por carpetas:

- `Formularios` contiene la interfaz y sus eventos.
- `Datos` contiene las consultas y conexiones.
- `Modelos` contiene las clases que representan información y sesión.
- `Seguridad` contiene la creación y verificación de hashes.

### Presentación experimental de la imagen del login

El `PictureBox` original ahora ocupa el espacio disponible debajo del nombre del sistema. Utiliza `Zoom`, fondo negro, ningún borde y ningún margen. De esta forma conserva la proporción de la imagen sin usar `StretchImage`, recortes o código de dibujo.

Este cambio es visual y experimental. No modifica el archivo, el recurso ni la lógica de autenticación.

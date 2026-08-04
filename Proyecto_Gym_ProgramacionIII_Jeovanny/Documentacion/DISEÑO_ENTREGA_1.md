# Diseño de base de datos de la primera entrega

## 1. Alcance de la entrega actual

Esta entrega prepara la estructura de PostgreSQL necesaria para el inicio de sesión, los roles, los permisos y los ocho mantenimientos solicitados. Incluye las tablas, claves, relaciones, restricciones e índices que servirán como base para los formularios de una fase posterior.

Los ocho mantenimientos preparados son:

1. Clientes.
2. Entrenadores.
3. Tipos de membresías.
4. Clases o actividades del gimnasio.
5. Horarios de clases o actividades.
6. Categorías de productos.
7. Productos.
8. Proveedores.

En esta entrega no se crean formularios, no se conecta el inicio de sesión, no se crean usuarios y no se ejecutan los scripts en PostgreSQL.

## 2. Módulos para entregas posteriores

Quedan fuera de esta entrega la asignación y renovación de membresías, cobros, cargos, ventas, compras, reservas, asistencia, inventario, cuentas por cobrar, abonos, consultas, reportes, dashboard y POS. También quedan pendientes las interfaces de los mantenimientos y la autenticación real contra PostgreSQL.

Estas funciones corresponden a procesos que dependen de los datos básicos. Por ejemplo, una venta necesita que los productos existan y una reserva necesita que las clases y sus horarios estén definidos.

## 3. Tablas de seguridad e inicio de sesión

### usuarios

- `id_usuario`: identificador numérico generado automáticamente y clave primaria.
- `nombre_usuario`: nombre único utilizado para iniciar sesión.
- `nombre_completo`: nombre que identifica a la persona dentro del sistema.
- `contrasena_hash`: resultado seguro del procesamiento de la contraseña.
- `contrasena_salt`: valor adicional usado para proteger el hash.
- `id_rol`: clave foránea obligatoria que indica el rol del usuario.
- `activo`: permite habilitar o deshabilitar el acceso sin borrar el registro.
- `fecha_creacion`: fecha y hora de creación del usuario.

### roles

- `id_rol`: identificador generado automáticamente y clave primaria.
- `nombre`: nombre único del rol, como `ADMIN` o `EMPLEADO`.
- `descripcion`: explicación breve del propósito del rol.
- `estado`: indica si el rol está disponible.

### permisos

- `id_permiso`: identificador generado automáticamente y clave primaria.
- `nombre`: nombre único del permiso.
- `descripcion`: explicación breve del permiso.
- `estado`: indica si el permiso está disponible.

En esta fase la tabla se crea vacía porque los permisos exactos se definirán cuando existan las ventanas que deben protegerse.

### roles_permisos

- `id_rol`: clave foránea hacia `roles`.
- `id_permiso`: clave foránea hacia `permisos`.

Los dos campos forman la clave primaria compuesta. La tabla permite asignar varios permisos a cada rol y el mismo permiso a varios roles.

### usuarios_perfiles

- `id_usuario`: clave primaria y, al mismo tiempo, clave foránea hacia `usuarios`.
- `telefono`: teléfono opcional del usuario.
- `correo`: correo opcional y único.
- `foto`: referencia de texto para la fotografía.

Esta separación evita mezclar los datos de autenticación con información opcional del perfil.

## 4. Tablas de mantenimiento y sus campos

### clientes

- `id_cliente`: identificador generado automáticamente y clave primaria.
- `nombre` y `apellido`: datos obligatorios para identificar al cliente.
- `cedula`: documento obligatorio y único.
- `telefono`: número de contacto obligatorio.
- `correo`: correo opcional y único.
- `direccion`: dirección opcional.
- `fecha_nacimiento`: fecha opcional para conocer la edad.
- `sexo`: valor opcional limitado a `MASCULINO`, `FEMENINO` u `OTRO`.
- `foto`: referencia de texto para la fotografía.
- `fecha_registro`: fecha y hora automática de creación.
- `estado`: indica si el cliente está activo.

### entrenadores

- `id_entrenador`: identificador generado automáticamente y clave primaria.
- `nombre` y `apellido`: datos personales obligatorios.
- `cedula`: documento obligatorio y único.
- `telefono`: número de contacto obligatorio.
- `correo`: correo opcional y único.
- `especialidad`: área profesional del entrenador.
- `fecha_contratacion`: fecha obligatoria que usa la fecha actual de forma predeterminada.
- `estado`: indica si el entrenador está activo.

### tipos_membresias

- `id_tipo_membresia`: identificador generado automáticamente y clave primaria.
- `nombre`: nombre único del tipo de membresía.
- `descripcion`: explicación opcional.
- `duracion_dias`: cantidad de días, obligatoriamente mayor que cero.
- `precio`: importe obligatorio igual o mayor que cero.
- `estado`: indica si el tipo está disponible.

### clases_actividades

- `id_clase`: identificador generado automáticamente y clave primaria.
- `nombre`: nombre único de la clase o actividad.
- `descripcion`: explicación opcional.
- `cupo_maximo`: cantidad máxima de participantes, mayor que cero.
- `estado`: indica si la clase está disponible.

### horarios_clases

- `id_horario`: identificador generado automáticamente y clave primaria.
- `id_clase`: clave foránea hacia `clases_actividades`.
- `id_entrenador`: clave foránea hacia `entrenadores`.
- `dia_semana`: día limitado a los siete valores permitidos.
- `hora_inicio` y `hora_fin`: período de la clase; la hora final debe ser posterior a la inicial.
- `estado`: indica si el horario está disponible.

La combinación de clase, día y hora de inicio es única para impedir la repetición exacta de un horario.

### categorias_productos

- `id_categoria`: identificador generado automáticamente y clave primaria.
- `nombre`: nombre único de la categoría.
- `descripcion`: explicación opcional.
- `estado`: indica si la categoría está disponible.

### marcas

- `id_marca`: identificador generado automáticamente y clave primaria.
- `nombre`: nombre único de la marca.
- `estado`: indica si la marca está disponible.

### productos

- `id_producto`: identificador generado automáticamente y clave primaria.
- `codigo`: código obligatorio y único.
- `nombre`: nombre obligatorio del producto.
- `descripcion`: explicación opcional.
- `id_categoria`: clave foránea obligatoria hacia `categorias_productos`.
- `id_marca`: clave foránea opcional hacia `marcas`.
- `precio_compra` y `precio_venta`: importes obligatorios iguales o mayores que cero.
- `stock` y `stock_minimo`: cantidades enteras iguales o mayores que cero.
- `imagen`: referencia de texto para la imagen.
- `estado`: indica si el producto está disponible.

### proveedores

- `id_proveedor`: identificador generado automáticamente y clave primaria.
- `nombre`: nombre obligatorio del proveedor.
- `rnc_cedula`: documento fiscal o personal obligatorio y único.
- `telefono`: número de contacto obligatorio.
- `correo`: correo opcional y único.
- `direccion`: dirección opcional.
- `estado`: indica si el proveedor está activo.

### productos_proveedores

- `id_producto`: clave foránea hacia `productos`.
- `id_proveedor`: clave foránea hacia `proveedores`.
- `costo_referencia`: costo opcional que, cuando existe, debe ser igual o mayor que cero.
- `estado`: indica si la relación comercial está activa.

Los identificadores de producto y proveedor forman la clave primaria compuesta.

## 5. Razón de las decisiones de campos

Se utilizaron identificadores enteros generados automáticamente para que cada registro tenga una referencia sencilla y estable. Los tamaños de los textos limitan los datos a longitudes razonables: los nombres son más cortos que las descripciones, mientras que las rutas o referencias de imágenes usan `TEXT` porque su longitud puede variar.

Los correos se dejaron opcionales porque no todas las personas o empresas necesariamente proporcionarán uno, pero se definieron como únicos cuando tengan valor. Los campos `estado` permiten desactivar datos sin borrarlos y conservar futuras relaciones históricas. Las fechas predeterminadas registran de forma consistente el momento de creación o contratación. Los importes usan `NUMERIC` para conservar exactamente dos posiciones decimales.

Los campos de fotografía e imagen se definen como texto porque todavía debe decidirse si almacenarán una ruta local, un nombre de archivo o una dirección externa. No se guardan archivos binarios en esta fase.

## 6. Marcas como tabla de apoyo

`marcas` se incluye porque `productos` necesita `id_marca`, aunque no forma parte de los ocho mantenimientos solicitados. Separarla evita repetir el nombre de una marca en muchos productos y mantiene una única escritura válida para cada marca. En esta entrega no se crea una ventana para administrarla.

## 7. Relaciones entre tablas

- Un rol puede pertenecer a muchos usuarios; cada usuario tiene un rol.
- Roles y permisos se relacionan mediante `roles_permisos`.
- Un usuario puede tener como máximo un perfil en `usuarios_perfiles`.
- Una categoría puede agrupar muchos productos.
- Una marca puede identificar muchos productos.
- Una clase puede aparecer en muchos horarios.
- Un entrenador puede impartir muchos horarios.
- Productos y proveedores se relacionan mediante `productos_proveedores`.

Las tablas intermedias usan `ON DELETE CASCADE` para eliminar automáticamente las asignaciones que ya no pueden existir cuando se elimina uno de sus registros principales. `usuarios_perfiles` también lo usa porque un perfil no tiene sentido sin su usuario.

## 8. Ejemplo de relación 1:1

`usuarios` y `usuarios_perfiles` representan una relación uno a uno. `usuarios_perfiles.id_usuario` es clave primaria, por lo que un usuario no puede aparecer dos veces, y también es clave foránea hacia `usuarios`. Un usuario puede tener un solo perfil y cada perfil pertenece a un único usuario.

## 9. Ejemplos de relación 1:N

- `roles` y `usuarios`: un rol puede estar asignado a muchos usuarios, pero cada usuario tiene un solo rol.
- `categorias_productos` y `productos`: una categoría puede contener muchos productos, pero cada producto tiene una categoría obligatoria.
- `clases_actividades` y `horarios_clases`: una clase puede programarse en muchos horarios, pero cada horario pertenece a una clase.

## 10. Ejemplo de relación N:M

Un producto puede ser ofrecido por varios proveedores y un proveedor puede ofrecer varios productos. La tabla `productos_proveedores` transforma esta relación muchos a muchos en dos relaciones uno a muchos y permite guardar el costo de referencia específico de cada combinación.

## 11. Clave primaria

Una clave primaria identifica de manera única cada fila de una tabla. No admite valores repetidos ni nulos. Puede estar formada por un solo campo, como `clientes.id_cliente`, o por varios campos, como la combinación usada en `productos_proveedores`.

## 12. Clave foránea

Una clave foránea conecta una tabla con la clave primaria de otra. PostgreSQL impide guardar una referencia hacia un registro inexistente. Por ejemplo, `productos.id_categoria` garantiza que la categoría elegida exista en `categorias_productos`.

## 13. Restricción CHECK

Una restricción `CHECK` valida una regla antes de aceptar un dato. En este diseño impide precios negativos, duraciones iguales o menores que cero, horas finales anteriores a las iniciales y valores no permitidos para sexo o día de la semana.

## 14. Normalización hasta Tercera Forma Normal

La normalización organiza los datos para reducir repeticiones y evitar inconsistencias. En Primera Forma Normal cada campo contiene un valor simple. En Segunda Forma Normal cada dato depende de toda la clave primaria. En Tercera Forma Normal los campos que no son clave dependen directamente de la clave y no de otros campos secundarios.

Este diseño llega de manera práctica a Tercera Forma Normal al separar roles, categorías, marcas y proveedores en sus propias tablas y utilizar claves foráneas para relacionarlos.

## 15. Por qué no se repiten nombres relacionados

No se guarda el nombre de la categoría o la marca directamente en cada producto, ni el nombre del rol en cada usuario. Si un nombre se repitiera en muchas filas, un cambio obligaría a actualizar varios registros y podría dejar escrituras diferentes. La clave foránea guarda una sola referencia y el nombre se administra en su tabla correspondiente.

## 16. Por qué todavía no existen tablas de procesos

Ventas, cobros, compras, reservas y otros procesos necesitan reglas que todavía no forman parte del alcance autorizado. Diseñarlos ahora obligaría a inventar estados, documentos, cálculos y relaciones no confirmadas. Primero se preparan los datos maestros; después se definirán los procesos con los requisitos completos.

## 17. Cómo explicar la estructura frente al profesor

La explicación puede comenzar indicando que la base se divide en seguridad, mantenimientos y futuras operaciones. Luego se muestra que cada tabla tiene una clave primaria, que las claves foráneas representan relaciones reales y que las restricciones evitan datos inválidos desde PostgreSQL.

Como ejemplos concretos pueden presentarse `usuarios_perfiles` para la relación 1:1, `categorias_productos` con `productos` para la relación 1:N y `productos_proveedores` para la relación N:M. Finalmente, se explica que la normalización evita repetir nombres y que los formularios se construirán después sobre una estructura ya revisada.

## Índices para búsquedas frecuentes

Se agregan índices compuestos para buscar clientes y entrenadores por nombre y apellido, un índice para productos por nombre, otro para horarios por clase y día, y uno para localizar las relaciones de un proveedor. No se duplican índices sobre claves primarias o columnas `UNIQUE`, porque PostgreSQL ya crea el soporte necesario para esas restricciones.

## Decisiones que deben confirmarse más adelante

- Los permisos exactos que tendrá el rol `EMPLEADO`.
- Si `Marcas` tendrá una ventana propia en una entrega posterior.
- Cómo se guardarán definitivamente las fotografías e imágenes.
- Si el profesor requiere campos adicionales.
- Cuáles tres funcionalidades adicionales implementará el equipo.

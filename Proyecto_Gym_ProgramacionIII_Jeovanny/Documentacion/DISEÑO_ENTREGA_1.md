# Diseño de la primera entrega

## 1. Alcance

La primera entrega incluye el inicio de sesión con roles y estos ocho mantenimientos:

1. Clientes.
2. Entrenadores.
3. Tipos de membresías.
4. Clases o actividades.
5. Horarios de clases o actividades.
6. Categorías de productos.
7. Productos.
8. Proveedores.

Cada mantenimiento permite listar, buscar, crear, editar y activar o desactivar registros. La desactivación conserva la información y evita borrar datos que podrían relacionarse con operaciones futuras.

## 2. Seguridad

### roles

- `id_rol`: clave primaria generada por PostgreSQL.
- `nombre`: nombre único del rol.
- `descripcion`: propósito del rol.
- `estado`: indica si está disponible.

La entrega registra los roles `ADMIN` y `EMPLEADO`.

### usuarios

- `id_usuario`: clave primaria.
- `nombre_usuario`: valor único para iniciar sesión.
- `nombre_completo`: persona propietaria de la cuenta.
- `contrasena_hash`: resultado protegido de la contraseña.
- `contrasena_salt`: valor aleatorio usado al generar el hash.
- `id_rol`: clave foránea hacia `roles`.
- `activo`: habilita o impide el acceso.
- `fecha_creacion`: fecha y hora del registro.

`FrmConfiguracionInicial` aparece solamente cuando no existen usuarios y crea el primer `ADMIN`. `FrmLogin` verifica las credenciales y `SesionActual` conserva únicamente la identidad y el rol, nunca datos de contraseña.

## 3. Tablas de mantenimiento

### clientes

Guarda nombre, apellido, cédula, teléfono, correo, dirección, fecha de nacimiento, sexo, foto, fecha de registro y estado.

### entrenadores

Guarda nombre, apellido, cédula, teléfono, correo, especialidad, fecha de contratación y estado.

### tipos_membresias

Guarda nombre, descripción, duración en días, precio y estado.

### clases_actividades

Guarda nombre, descripción, cupo máximo y estado.

### horarios_clases

Relaciona una clase con un entrenador y guarda día de la semana, hora de inicio, hora de fin y estado.

### categorias_productos

Guarda nombre, descripción y estado.

### productos

Guarda código, nombre, descripción, categoría, precios de compra y venta, existencias, existencia mínima, imagen y estado.

### proveedores

Guarda nombre, RNC o cédula, teléfono, correo, dirección y estado.

## 4. Relaciones

- Un rol puede estar asignado a muchos usuarios, pero cada usuario tiene un rol.
- Una clase puede tener varios horarios, pero cada horario pertenece a una clase.
- Un entrenador puede aparecer en varios horarios, pero cada horario tiene un entrenador.
- Una categoría puede contener varios productos, pero cada producto tiene una categoría.

Estas relaciones se representan con claves foráneas y evitan repetir nombres relacionados en cada fila.

## 5. Validaciones y restricciones

- Los campos obligatorios se validan antes de guardar.
- Los nombres y apellidos personales impiden números.
- Las cédulas, teléfonos y RNC identificados como numéricos impiden letras.
- Los controles respetan los límites `VARCHAR` definidos en PostgreSQL.
- Los precios y existencias no aceptan valores negativos.
- La hora final de una clase debe ser posterior a la hora inicial.
- Las cédulas, códigos, correos y nombres definidos como únicos se comprueban antes de guardar y también están protegidos por PostgreSQL.

## 6. Organización del código

- `Modelos`: representa los datos de cada tabla.
- `Datos`: contiene conexiones y consultas parametrizadas con Npgsql.
- `Formularios`: contiene las ventanas, eventos y validaciones.
- `Seguridad`: genera y verifica hashes de contraseñas.
- `ScriptsBD`: crea la base y las tablas necesarias.

No se utiliza Entity Framework. La separación permite explicar que el formulario obtiene los datos, el repositorio ejecuta SQL y el modelo transporta la información.

## 7. Elementos fuera de esta entrega

No se incluyen membresías asignadas, renovaciones, cargos, cobros, ventas, compras, reservas, cuentas por cobrar, abonos, inventario, consultas, reportes, dashboard, POS ni configuración de usuarios y permisos. Esos elementos pertenecen a entregas posteriores según el mandato.

## 8. Explicación para el profesor

El programa inicia comprobando si existe un usuario. Si la instalación está vacía, permite crear el primer administrador sin guardar contraseñas en el código. El login consulta PostgreSQL, verifica el hash y abre el menú con la identidad y el rol de la sesión.

Los ocho mantenimientos comparten un flujo sencillo: muestran registros en un `DataGridView`, permiten buscar, abren un formulario para crear o editar y cambian el estado sin eliminar físicamente la fila. Las consultas reciben parámetros para que los valores escritos por el usuario no se unan directamente al texto SQL.

# Configuración y seguridad

## Alcance implementado

La opción Configuración permite administrar métodos de pago, usuarios, roles, permisos y la relación entre roles y permisos.

El rol `ADMIN` conserva acceso completo. Los demás roles solo ven las opciones que tienen asignadas. Un usuario inactivo o asociado con un rol inactivo no puede iniciar sesión.

## Uso de las pantallas

- Las listas pueden filtrarse escribiendo en el campo de búsqueda.
- `NUEVO` abre una ventana con los campos necesarios para crear un registro.
- `EDITAR` modifica la fila seleccionada. También puede hacerse doble clic sobre la fila.
- `ACTUALIZAR` vuelve a consultar la información guardada.
- El estado indica si un método, usuario, rol o permiso está disponible para utilizarse.
- Al editar un usuario se puede establecer una contraseña nueva sin mostrar la contraseña anterior.
- En Roles y permisos se elige un rol y después se utiliza una de las cinco pestañas: Mantenimientos, Movimientos, Reportes, Consultas o Configuración.
- Cada pestaña muestra únicamente las opciones de su módulo. `MARCAR MÓDULO` y `DESMARCAR MÓDULO` afectan solo la pestaña visible.
- `GUARDAR CAMBIOS` guarda juntas las selecciones realizadas en los cinco módulos.
- En Roles y permisos, los botones blancos utilizan letras negras y el botón negro utiliza letras blancas, incluso cuando el rol `ADMIN` está protegido contra cambios.

Los cambios de rol y permisos se reflejan la próxima vez que el usuario inicia sesión. El rol `ADMIN` siempre conserva acceso completo y no permite desactivar su estado desde la aplicación.

## Límites y validaciones

| Campo | Límite | Contenido permitido |
|---|---:|---|
| Método de pago | 50 | Letras y espacios |
| Descripción de método | 150 | Texto |
| Nombre de usuario | 50 | Texto |
| Nombre completo | 100 | Letras y espacios |
| Nombre de rol | 30 | Letras y espacios |
| Descripción de rol | 150 | Texto |
| Código de permiso | 60 | Letras, números y guion bajo |
| Nombre de permiso | 80 | Texto |
| Descripción de permiso | 150 | Texto |
| Contraseña nueva | 100 | Texto protegido en pantalla |
| Confirmación | 100 | Texto protegido en pantalla |

Los límites de entrada respetan los tamaños definidos en PostgreSQL. Las validaciones se realizan también después de pegar texto para impedir que un valor inválido llegue al repositorio.

## Protección de contraseñas

Las contraseñas no se guardan como texto normal. `PasswordHelper` genera una sal aleatoria y un hash PBKDF2 con SHA-256. Una persona con acceso a la administración de usuarios puede establecer una contraseña nueva al editar un usuario, pero nunca puede consultar la contraseña anterior.

## Estructura interna necesaria

Las tablas `usuarios`, `roles`, `permisos` y `roles_permisos` controlan el acceso al sistema. La tabla `metodos_pago` contiene las opciones utilizadas por cobros, compras y abonos.

No se elimina información desde estas pantallas. Los registros que ya no deben utilizarse se marcan como inactivos para conservar el historial relacionado.

## Explicación para el profesor

La seguridad se divide en tres partes sencillas. El usuario identifica a la persona que entra al programa, el rol agrupa sus responsabilidades y los permisos determinan cuáles secciones puede ver. La pantalla separa esos permisos por los cinco módulos principales para facilitar su revisión, pero los guarda juntos para el rol seleccionado. Las contraseñas se almacenan mediante un hash y una sal, nunca como texto normal. El administrador conserva acceso completo para evitar que el sistema quede sin una cuenta capaz de configurar la seguridad.

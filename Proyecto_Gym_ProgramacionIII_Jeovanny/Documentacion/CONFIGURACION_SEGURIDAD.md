# Seguridad y cambio de contraseña

## Alcance implementado

La opción Configuración de la aplicación contiene únicamente el cambio de contraseña del usuario conectado. El login conserva la validación de usuarios, roles y permisos porque son necesarios para controlar el acceso al sistema.

El rol `ADMIN` conserva acceso completo. Los demás roles solo ven las opciones que tienen asignadas. Un usuario inactivo o asociado con un rol inactivo no puede iniciar sesión.

## Límites y validaciones

| Campo | Límite | Contenido permitido |
|---|---:|---|
| Contraseña actual | 100 | Texto protegido en pantalla |
| Contraseña nueva | 100 | Texto protegido en pantalla |
| Confirmación | 100 | Texto protegido en pantalla |

Los límites coinciden con los tamaños definidos en PostgreSQL. Las validaciones se realizan también después de pegar texto para impedir que un valor inválido llegue al repositorio.

## Protección de contraseñas

Las contraseñas no se guardan como texto normal. `PasswordHelper` genera una sal aleatoria y un hash PBKDF2 con SHA-256. Para cambiar la contraseña, el usuario debe escribir correctamente su contraseña actual.

## Estructura interna necesaria

Las tablas `usuarios`, `roles`, `permisos` y `roles_permisos` permanecen en PostgreSQL porque el login y la visibilidad del menú dependen de ellas. No aparecen como mantenimientos dentro de Configuración.

## Explicación para el profesor

La seguridad valida al usuario y su rol al iniciar sesión. La contraseña se almacena mediante un hash y una sal, nunca como texto normal. La única opción visible dentro de Configuración permite cambiar la contraseña después de confirmar correctamente la actual.

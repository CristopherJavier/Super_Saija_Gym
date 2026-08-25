# Configuración de PostgreSQL para Super Saija Gym

## 1. Crear la base de datos

1. Abre pgAdmin 4.
2. Selecciona la base `postgres` y abre `Query Tool`.
3. Ejecuta `ScriptsBD/01_crear_base_datos.sql`.
4. Actualiza la lista de bases de datos.
5. Abre `Query Tool` sobre `super_saija_gym_db`.

## 2. Crear las tablas

Dentro de `super_saija_gym_db`, ejecuta los archivos una sola vez y en este orden:

1. `ScriptsBD/02_usuarios_roles.sql`.
2. `ScriptsBD/03_mantenimientos.sql`.
3. `ScriptsBD/04_configuracion_procesos.sql`.

Los scripts utilizan `IF NOT EXISTS`, por lo que también pueden ejecutarse nuevamente para comprobar que las tablas existan. No eliminan información guardada.

## 3. Tablas de seguridad

Los scripts de seguridad y configuración crean:

- `roles`, con los roles `ADMIN` y `EMPLEADO`.
- `usuarios`, relacionada con `roles` mediante `id_rol`.
- `permisos`, con las opciones controladas por nivel de acceso.
- `roles_permisos`, que relaciona cada rol con sus permisos.
- `metodos_pago`, para las formas de pago utilizadas por los procesos.

La tabla `usuarios` guarda el hash y la sal de la contraseña. La contraseña original nunca se almacena.

## 4. Tablas de mantenimientos

El tercer script crea las tablas necesarias para la primera entrega:

- `clientes`.
- `entrenadores`.
- `tipos_membresias`.
- `clases_actividades`.
- `horarios_clases`.
- `categorias_productos`.
- `productos`.
- `proveedores`.

## 5. Tablas de procesos

El cuarto script crea la estructura necesaria para:

- Marcas de productos.
- Membresías asignadas a clientes.
- Cargos y cobros.
- Ventas, compras y sus detalles.
- Cuentas por cobrar y abonos.
- Reservas y asistencias.
- Movimientos de inventario.

## 6. Variables de entorno

La aplicación necesita estas variables:

```text
SUPER_SAIJA_DB_HOST
SUPER_SAIJA_DB_PORT
SUPER_SAIJA_DB_NAME
SUPER_SAIJA_DB_USER
SUPER_SAIJA_DB_PASSWORD
```

Ejemplo de nombres y valores locales:

```text
SUPER_SAIJA_DB_HOST = localhost
SUPER_SAIJA_DB_PORT = 5432
SUPER_SAIJA_DB_NAME = super_saija_gym_db
SUPER_SAIJA_DB_USER = postgres
SUPER_SAIJA_DB_PASSWORD = TU_CONTRASEÑA_DE_POSTGRESQL
```

El valor de la contraseña debe configurarse solamente en las variables de entorno de Windows. No debe escribirse en el código ni guardarse en Git.

## 7. Crear las variables en Windows

1. Abre el menú Inicio.
2. Busca `Variables de entorno`.
3. Selecciona `Editar las variables de entorno para tu cuenta`.
4. En `Variables de usuario`, presiona `Nueva`.
5. Crea las cinco variables indicadas.
6. Cierra y vuelve a abrir Visual Studio para que lea los valores.

## 8. Primer administrador

Cuando la tabla `usuarios` está vacía, el programa abre `FrmConfiguracionInicial`. Esta ventana solicita el nombre, el usuario y la contraseña del primer administrador. Después de guardarlo, las siguientes ejecuciones abren directamente `FrmLogin`.

Este formulario es necesario porque evita incluir una contraseña predeterminada dentro del proyecto.

## 9. Comprobación

1. Ejecuta la aplicación.
2. Si no existen usuarios, crea el administrador inicial.
3. Inicia sesión con ese usuario.
4. Comprueba que el formulario principal muestre el nombre y el rol.
5. Abre los mantenimientos autorizados desde el menú lateral.
6. En `CONFIGURACIÓN`, comprueba métodos de pago, usuarios, roles, permisos y asignación de permisos.
7. Cierra la sesión e inicia con un usuario que no sea administrador para confirmar que solo vea sus opciones autorizadas.

La aplicación usa consultas SQL parametrizadas mediante Npgsql y no utiliza Entity Framework.

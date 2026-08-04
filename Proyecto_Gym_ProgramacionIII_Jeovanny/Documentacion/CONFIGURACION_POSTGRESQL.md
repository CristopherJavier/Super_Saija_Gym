# Configuración de PostgreSQL para Super Saija Gym

## 1. ¿Qué es PostgreSQL?

PostgreSQL es un sistema gestor de bases de datos. Su trabajo es almacenar, organizar y proteger la información que utilizará una aplicación.

La base de datos de este proyecto se llamará `super_saija_gym_db`.

## 2. ¿Qué es pgAdmin 4?

pgAdmin 4 es una aplicación con interfaz gráfica que permite administrar servidores PostgreSQL, crear bases de datos, ejecutar instrucciones SQL y revisar tablas.

## 3. Diferencia entre PostgreSQL y pgAdmin 4

PostgreSQL es el servidor que guarda y procesa los datos. pgAdmin 4 es una herramienta visual que se conecta al servidor para administrarlo.

Cerrar pgAdmin no detiene necesariamente PostgreSQL. El servidor puede continuar ejecutándose como un servicio de Windows.

## 4. Abrir pgAdmin 4

1. Abre el menú Inicio de Windows.
2. Escribe `pgAdmin 4`.
3. Abre la aplicación.
4. Si pgAdmin solicita su contraseña maestra, escribe la que configuraste para pgAdmin.

La contraseña maestra de pgAdmin y la contraseña del usuario `postgres` pueden ser diferentes.

## 5. Localizar el servidor PostgreSQL

1. En el panel izquierdo busca `Servers`.
2. Expande `Servers` usando la flecha.
3. Busca el servidor registrado, que normalmente tiene un nombre parecido a `PostgreSQL` seguido de su versión.
4. Si aparece desconectado, haz doble clic sobre él.
5. Cuando lo solicite, escribe la contraseña que configuraste para el usuario de PostgreSQL.

## 6. Abrir Query Tool sobre la base postgres

1. Expande el servidor.
2. Expande `Databases`.
3. Selecciona la base de datos `postgres`.
4. Haz clic derecho sobre `postgres`.
5. Selecciona `Query Tool`.

La base `postgres` ya existe normalmente y permite ejecutar la instrucción que crea la base del proyecto.

## 7. Ejecutar 01_crear_base_datos.sql

1. Dentro de Query Tool, presiona el botón para abrir un archivo.
2. Busca el proyecto en tu computadora.
3. Abre `Proyecto_Gym_ProgramacionIII_Jeovanny/ScriptsBD/01_crear_base_datos.sql`.
4. Comprueba que Query Tool continúa conectado a la base `postgres`.
5. Presiona el botón Ejecutar o la tecla `F5` dentro de Query Tool.
6. Revisa el panel de mensajes para confirmar que la instrucción terminó correctamente.

Este script crea únicamente `super_saija_gym_db`. No elimina bases existentes.

## 8. Actualizar la lista de Databases

1. En el panel izquierdo, haz clic derecho sobre `Databases`.
2. Selecciona `Refresh`.
3. Expande nuevamente `Databases` si fuera necesario.

## 9. Encontrar super_saija_gym_db

Después de actualizar, debe aparecer una base llamada:

```text
super_saija_gym_db
```

Si no aparece, revisa los mensajes de Query Tool y confirma que el primer script fue ejecutado sobre la base `postgres`.

## 10. Abrir Query Tool dentro de super_saija_gym_db

1. Selecciona `super_saija_gym_db` en la lista de bases.
2. Haz clic derecho sobre ella.
3. Selecciona `Query Tool`.
4. Verifica en la parte superior que la conexión corresponde a `super_saija_gym_db`.

## 11. Ejecutar 02_crear_tabla_usuarios.sql

1. En el nuevo Query Tool, abre el archivo `Proyecto_Gym_ProgramacionIII_Jeovanny/ScriptsBD/02_crear_tabla_usuarios.sql`.
2. Confirma que estás conectado a `super_saija_gym_db`.
3. Presiona Ejecutar o `F5` dentro de Query Tool.
4. Revisa el panel de mensajes para confirmar que la tabla fue creada.

El script crea la tabla vacía. No crea usuarios, contraseñas ni datos de prueba.

## 12. Localizar la tabla usuarios

Dentro de `super_saija_gym_db`, expande en este orden:

1. `Schemas`
2. `public`
3. `Tables`
4. `usuarios`

Si no aparece, haz clic derecho sobre `Tables` y selecciona `Refresh`.

## 13. Comprobar las columnas

1. Expande la tabla `usuarios`.
2. Expande `Columns`.
3. Comprueba que aparezcan estas columnas:

```text
id_usuario
nombre_usuario
nombre_completo
contrasena_hash
contrasena_salt
rol
activo
fecha_creacion
```

También puedes hacer clic derecho sobre `usuarios`, seleccionar `Properties` y revisar sus columnas y restricciones.

## 14. Variables de entorno necesarias

La aplicación leerá estas variables de entorno:

```text
SUPER_SAIJA_DB_HOST
SUPER_SAIJA_DB_PORT
SUPER_SAIJA_DB_NAME
SUPER_SAIJA_DB_USER
SUPER_SAIJA_DB_PASSWORD
```

Valores de ejemplo:

```text
SUPER_SAIJA_DB_HOST = localhost
SUPER_SAIJA_DB_PORT = 5432
SUPER_SAIJA_DB_NAME = super_saija_gym_db
SUPER_SAIJA_DB_USER = postgres
SUPER_SAIJA_DB_PASSWORD = TU_CONTRASEÑA_DE_POSTGRESQL
```

`TU_CONTRASEÑA_DE_POSTGRESQL` es solamente un texto de ejemplo. Debes sustituirlo por tu contraseña real únicamente en las variables de entorno de tu computadora. Nunca escribas la contraseña real en el proyecto ni la subas a GitHub.

## 15. Crear las variables mediante la interfaz de Windows

1. Abre el menú Inicio.
2. Busca `Variables de entorno`.
3. Selecciona `Editar las variables de entorno para tu cuenta`.
4. En la sección `Variables de usuario`, presiona `Nueva`.
5. Escribe el nombre de la primera variable y su valor.
6. Repite el proceso para las cinco variables.
7. Presiona `Aceptar` para guardar las ventanas.
8. Cierra y vuelve a abrir Visual Studio para que pueda leer las variables nuevas.

Guarda la contraseña solamente como valor de `SUPER_SAIJA_DB_PASSWORD` en tu computadora.

## 16. Opción con PowerShell

Puedes crear las variables desde PowerShell con estos comandos:

```powershell
setx SUPER_SAIJA_DB_HOST "localhost"
setx SUPER_SAIJA_DB_PORT "5432"
setx SUPER_SAIJA_DB_NAME "super_saija_gym_db"
setx SUPER_SAIJA_DB_USER "postgres"
setx SUPER_SAIJA_DB_PASSWORD "TU_CONTRASEÑA_DE_POSTGRESQL"
```

No ejecutes literalmente el último comando sin sustituir el texto de ejemplo. No compartas el comando con tu contraseña ni lo guardes en un archivo del proyecto.

`setx` guarda las variables para procesos futuros. Después de utilizarlo debes cerrar y volver a abrir Visual Studio y PowerShell.

## 17. Estado actual de esta fase

En esta fase solamente se prepara la base, la tabla vacía y la clase de conexión. `FrmLogin` todavía no utiliza PostgreSQL y no existe ningún usuario creado automáticamente.

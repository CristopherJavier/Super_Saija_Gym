# Datos de demostración

El archivo `ScriptsBD/05_datos_demostracion.sql` carga información ficticia para revisar visualmente el programa y probar sus procesos.

Antes de ejecutarlo deben estar creadas todas las tablas y debe existir al menos un usuario del sistema. El script utiliza ese usuario para relacionar ventas, compras, cobros, abonos, asistencias y movimientos de inventario. No crea usuarios ni contraseñas.

La carga incluye:

- 30 clientes.
- 20 entrenadores.
- 6 tipos de membresía.
- 10 clases o actividades.
- 20 horarios correspondientes al día de ejecución.
- 6 categorías y 6 marcas.
- 20 productos.
- 20 proveedores.
- 25 membresías asignadas: 20 con cargo y 5 pendientes de generar cargo.
- 20 cargos pendientes y 20 cargos pagados.
- 25 ventas, de las cuales 20 son a crédito.
- 20 cuentas por cobrar y 20 abonos.
- 20 compras.
- 20 cobros con sus detalles.
- 25 reservas para el día de ejecución: 20 con asistencia y 5 pendientes.
- 20 asistencias.
- Movimientos de entrada y salida de inventario.

Los registros usan cédulas, códigos y correos reservados para demostración. El archivo puede ejecutarse nuevamente sin repetir esos registros principales.

Cinco clientes de demostración quedan sin membresía para probar la asignación. Otros cinco tienen una membresía sin cargo para probar la generación de cargos. Esos últimos también conservan una reserva sin asistencia para poder probar el registro vinculado a una reserva.

Los catálogos se mantienen en una cantidad razonable. No se crean veinte métodos de pago, categorías o tipos de membresía porque eso produciría opciones artificiales y dificultaría la demostración.

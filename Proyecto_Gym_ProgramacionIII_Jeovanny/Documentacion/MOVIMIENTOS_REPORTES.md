# Movimientos y reportes

## Organización del menú

La barra lateral presenta los módulos en este orden:

1. Inicio.
2. Mantenimientos.
3. Movimientos.
4. Reportes.
5. Consultas.
6. Configuración.

Configuración conserva únicamente la opción para cambiar la contraseña.

## Movimientos implementados

El mandato enumera diez procesos y también exige el registro de asistencias en sus requerimientos funcionales. Por esa razón, Asistencias se incluye como el movimiento número once.

1. Asignación de membresía: relaciona un cliente activo con un tipo de membresía activo y calcula el vencimiento según la duración del plan.
2. Renovación de membresía: conserva la membresía anterior como historial y crea el nuevo período a partir del día siguiente al vencimiento, o desde la fecha actual si ya estaba vencida.
3. Cobros: permite combinar cargos de servicios y productos, usar un método de pago y generar un recibo imprimible.
4. Generación de cargos: crea el cargo pendiente de una membresía antes de cobrarlo. El número lo genera PostgreSQL automáticamente.
5. Ventas: utiliza un detalle de productos, permite contado o crédito y aplica un descuento porcentual entre cero y cien.
6. Compras: registra únicamente compras al contado y aumenta el inventario de cada producto comprado.
7. Reservas de clases: valida que la fecha coincida con el día del horario y que todavía exista cupo.
8. Cuentas por cobrar: muestra las deudas creadas automáticamente por las ventas a crédito.
9. Abonos: registra pagos parciales o totales sin permitir que el monto supere el saldo pendiente.
10. Entrada y salida de inventario: actualiza el stock y no permite una salida mayor que la existencia disponible.
11. Asistencias: registra la entrada de un cliente y permite vincularla con una reserva correspondiente al día actual.

Los pagos adelantados de una membresía se manejan como un cobro de servicio realizado antes de la fecha de vencimiento del cargo. No se creó una regla adicional de saldo a favor porque el mandato indica que el cargo debe generarse antes del cobro.

## Reglas confirmadas

- Métodos de pago: Efectivo, Tarjeta y Transferencia.
- Impuestos: no se calculan por ahora y se almacenan en cero.
- Descuento de ventas: porcentaje entre cero y cien; PostgreSQL almacena el monto calculado.
- Impresión: solamente los recibos de cobro tienen impresión.
- Compras: siempre al contado.
- Ventas a crédito: requieren un cliente y una fecha de vencimiento.

## Reportes implementados

1. Balance pendiente por cliente.
2. Clientes activos e inactivos.
3. Membresías activas y vencidas, con días restantes y cliente.
4. Cobros por rango de fechas.
5. Ventas por rango de fechas.
6. Compras por rango de fechas.
7. Cargos pendientes y vencidos.

Los reportes se muestran dentro de la aplicación y no incluyen impresión, según la decisión tomada para esta entrega.

## Integridad de los datos

Las operaciones que modifican varias tablas utilizan transacciones de PostgreSQL. Si una parte falla, se deshace toda la operación. Por ejemplo, una venta guarda su encabezado y detalle, descuenta inventario y, cuando es a crédito, crea la cuenta por cobrar dentro de la misma transacción.

Todas las consultas utilizan parámetros. Los límites numéricos y de texto de las ventanas respetan los tipos y tamaños definidos en PostgreSQL.

## Explicación para el profesor

Los mantenimientos administran datos básicos, mientras que los movimientos registran hechos del negocio. Una venta, un cobro o una reserva son movimientos porque cambian el estado del sistema. Los reportes no modifican datos; organizan la información ya registrada para analizar clientes, dinero, membresías y cargos.

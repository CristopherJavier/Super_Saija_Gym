CREATE TABLE IF NOT EXISTS metodos_pago (
    id_metodo_pago INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL UNIQUE,
    descripcion VARCHAR(150),
    estado BOOLEAN NOT NULL DEFAULT TRUE
);

INSERT INTO metodos_pago (nombre, descripcion, estado)
VALUES
    ('EFECTIVO', 'Pago realizado en efectivo', TRUE),
    ('TARJETA', 'Pago realizado con tarjeta', TRUE),
    ('TRANSFERENCIA', 'Pago realizado por transferencia', TRUE)
ON CONFLICT (nombre) DO UPDATE
SET descripcion = EXCLUDED.descripcion,
    estado = TRUE;

CREATE TABLE IF NOT EXISTS permisos (
    id_permiso INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    clave VARCHAR(60) NOT NULL UNIQUE,
    nombre VARCHAR(80) NOT NULL UNIQUE,
    descripcion VARCHAR(150),
    estado BOOLEAN NOT NULL DEFAULT TRUE
);

CREATE TABLE IF NOT EXISTS roles_permisos (
    id_rol INTEGER NOT NULL,
    id_permiso INTEGER NOT NULL,
    PRIMARY KEY (id_rol, id_permiso),
    FOREIGN KEY (id_rol) REFERENCES roles (id_rol),
    FOREIGN KEY (id_permiso) REFERENCES permisos (id_permiso)
);

INSERT INTO permisos (clave, nombre, descripcion)
VALUES
    ('MANT_CLIENTES', 'Mantenimiento de clientes', 'Permite administrar clientes'),
    ('MANT_ENTRENADORES', 'Mantenimiento de entrenadores', 'Permite administrar entrenadores'),
    ('MANT_TIPOS_MEMBRESIAS', 'Mantenimiento de tipos de membresías', 'Permite administrar tipos de membresías'),
    ('MANT_CLASES', 'Mantenimiento de clases', 'Permite administrar clases y actividades'),
    ('MANT_HORARIOS', 'Mantenimiento de horarios', 'Permite administrar horarios de clases'),
    ('MANT_CATEGORIAS', 'Mantenimiento de categorías', 'Permite administrar categorías de productos'),
    ('MANT_PRODUCTOS', 'Mantenimiento de productos', 'Permite administrar productos'),
    ('MANT_PROVEEDORES', 'Mantenimiento de proveedores', 'Permite administrar proveedores'),
    ('CONS_ENTRENADORES', 'Consulta de entrenadores', 'Permite consultar entrenadores'),
    ('CONS_MEMBRESIAS', 'Consulta de membresías', 'Permite consultar membresías'),
    ('CONS_CLASES', 'Consulta de clases', 'Permite consultar clases'),
    ('CONS_RESERVAS', 'Consulta de reservas', 'Permite consultar reservas'),
    ('CONS_CARGOS', 'Consulta de cargos', 'Permite consultar cargos'),
    ('CONS_PRODUCTOS', 'Consulta de productos', 'Permite consultar productos'),
    ('CONS_VENTAS', 'Consulta de ventas', 'Permite consultar ventas'),
    ('CONS_COMPRAS', 'Consulta de compras', 'Permite consultar compras'),
    ('CONS_PROVEEDORES', 'Consulta de proveedores', 'Permite consultar proveedores'),
    ('CONS_COBROS', 'Consulta de cobros', 'Permite consultar cobros'),
    ('MOV_ASIGNAR_MEMBRESIA', 'Asignación de membresías', 'Permite asignar membresías a clientes'),
    ('MOV_RENOVAR_MEMBRESIA', 'Renovación de membresías', 'Permite renovar membresías de clientes'),
    ('MOV_COBROS', 'Registro de cobros', 'Permite registrar cobros'),
    ('MOV_GENERAR_CARGOS', 'Generación de cargos', 'Permite generar cargos a clientes'),
    ('MOV_VENTAS', 'Registro de ventas', 'Permite registrar ventas'),
    ('MOV_COMPRAS', 'Registro de compras', 'Permite registrar compras al contado'),
    ('MOV_RESERVAS', 'Reserva de clases', 'Permite reservar clases'),
    ('MOV_CUENTAS_COBRAR', 'Cuentas por cobrar', 'Permite revisar cuentas por cobrar'),
    ('MOV_ABONOS', 'Registro de abonos', 'Permite registrar abonos'),
    ('MOV_INVENTARIO', 'Movimientos de inventario', 'Permite registrar entradas y salidas de inventario'),
    ('MOV_ASISTENCIAS', 'Registro de asistencias', 'Permite registrar asistencias'),
    ('REP_BALANCE_CLIENTES', 'Reporte de balance por cliente', 'Permite consultar balances pendientes'),
    ('REP_CLIENTES', 'Reporte de clientes', 'Permite consultar clientes activos e inactivos'),
    ('REP_MEMBRESIAS', 'Reporte de membresías', 'Permite consultar membresías activas y vencidas'),
    ('REP_COBROS', 'Reporte de cobros', 'Permite consultar cobros por fecha'),
    ('REP_VENTAS', 'Reporte de ventas', 'Permite consultar ventas por fecha'),
    ('REP_COMPRAS', 'Reporte de compras', 'Permite consultar compras por fecha'),
    ('REP_CARGOS', 'Reporte de cargos', 'Permite consultar cargos pendientes y vencidos'),
    ('CONFIG_METODOS_PAGO', 'Configuración de métodos de pago', 'Permite administrar los métodos de pago'),
    ('CONFIG_USUARIOS', 'Configuración de usuarios', 'Permite administrar los usuarios del sistema'),
    ('CONFIG_ROLES', 'Configuración de roles', 'Permite administrar los roles del sistema'),
    ('CONFIG_PERMISOS', 'Configuración de permisos', 'Permite administrar los permisos del sistema'),
    ('CONFIG_ROLES_PERMISOS', 'Asignación de permisos', 'Permite asignar permisos a cada rol')
ON CONFLICT (clave) DO NOTHING;

INSERT INTO roles_permisos (id_rol, id_permiso)
SELECT r.id_rol, p.id_permiso
FROM roles r
CROSS JOIN permisos p
WHERE r.nombre = 'ADMIN'
ON CONFLICT DO NOTHING;

CREATE TABLE IF NOT EXISTS marcas (
    id_marca INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    nombre VARCHAR(80) NOT NULL UNIQUE,
    estado BOOLEAN NOT NULL DEFAULT TRUE
);

ALTER TABLE productos
ADD COLUMN IF NOT EXISTS id_marca INTEGER;

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1
        FROM pg_constraint
        WHERE conname = 'fk_productos_marcas'
    ) THEN
        ALTER TABLE productos
        ADD CONSTRAINT fk_productos_marcas
        FOREIGN KEY (id_marca) REFERENCES marcas (id_marca);
    END IF;
END $$;

CREATE TABLE IF NOT EXISTS membresias_clientes (
    id_membresia_cliente INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    id_cliente INTEGER NOT NULL,
    id_tipo_membresia INTEGER NOT NULL,
    id_membresia_anterior INTEGER,
    fecha_inicio DATE NOT NULL,
    fecha_vencimiento DATE NOT NULL,
    precio_aplicado NUMERIC(10,2) NOT NULL,
    estado BOOLEAN NOT NULL DEFAULT TRUE,
    FOREIGN KEY (id_cliente) REFERENCES clientes (id_cliente),
    FOREIGN KEY (id_tipo_membresia) REFERENCES tipos_membresias (id_tipo_membresia),
    FOREIGN KEY (id_membresia_anterior) REFERENCES membresias_clientes (id_membresia_cliente),
    CHECK (fecha_vencimiento >= fecha_inicio),
    CHECK (precio_aplicado >= 0)
);

CREATE TABLE IF NOT EXISTS cargos (
    id_cargo INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    id_cliente INTEGER NOT NULL,
    id_membresia_cliente INTEGER,
    concepto VARCHAR(150) NOT NULL,
    fecha_cargo DATE NOT NULL DEFAULT CURRENT_DATE,
    fecha_vencimiento DATE NOT NULL,
    monto NUMERIC(12,2) NOT NULL,
    saldo NUMERIC(12,2) NOT NULL,
    estado BOOLEAN NOT NULL DEFAULT TRUE,
    FOREIGN KEY (id_cliente) REFERENCES clientes (id_cliente),
    FOREIGN KEY (id_membresia_cliente) REFERENCES membresias_clientes (id_membresia_cliente),
    CHECK (monto > 0),
    CHECK (saldo >= 0),
    CHECK (saldo <= monto)
);

CREATE TABLE IF NOT EXISTS ventas (
    id_venta INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    fecha TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    id_cliente INTEGER,
    id_usuario INTEGER NOT NULL,
    tipo_pago VARCHAR(10) NOT NULL,
    subtotal NUMERIC(12,2) NOT NULL,
    descuento NUMERIC(12,2) NOT NULL DEFAULT 0,
    impuesto NUMERIC(12,2) NOT NULL DEFAULT 0,
    total NUMERIC(12,2) NOT NULL,
    estado BOOLEAN NOT NULL DEFAULT TRUE,
    FOREIGN KEY (id_cliente) REFERENCES clientes (id_cliente),
    FOREIGN KEY (id_usuario) REFERENCES usuarios (id_usuario),
    CHECK (tipo_pago IN ('CONTADO', 'CREDITO')),
    CHECK (subtotal >= 0),
    CHECK (descuento >= 0),
    CHECK (impuesto >= 0),
    CHECK (total >= 0),
    CHECK (descuento <= subtotal)
);

CREATE TABLE IF NOT EXISTS ventas_detalle (
    id_detalle_venta INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    id_venta INTEGER NOT NULL,
    id_producto INTEGER NOT NULL,
    cantidad INTEGER NOT NULL,
    precio NUMERIC(12,2) NOT NULL,
    descuento NUMERIC(12,2) NOT NULL DEFAULT 0,
    subtotal NUMERIC(12,2) NOT NULL,
    FOREIGN KEY (id_venta) REFERENCES ventas (id_venta),
    FOREIGN KEY (id_producto) REFERENCES productos (id_producto),
    CHECK (cantidad > 0),
    CHECK (precio >= 0),
    CHECK (descuento >= 0),
    CHECK (subtotal >= 0)
);

CREATE TABLE IF NOT EXISTS cuentas_cobrar (
    id_cuenta INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    id_venta INTEGER NOT NULL UNIQUE,
    id_cliente INTEGER NOT NULL,
    saldo NUMERIC(12,2) NOT NULL,
    fecha_vencimiento DATE NOT NULL,
    estado BOOLEAN NOT NULL DEFAULT TRUE,
    FOREIGN KEY (id_venta) REFERENCES ventas (id_venta),
    FOREIGN KEY (id_cliente) REFERENCES clientes (id_cliente),
    CHECK (saldo >= 0)
);

CREATE TABLE IF NOT EXISTS abonos (
    id_abono INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    id_cuenta INTEGER NOT NULL,
    fecha TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    monto NUMERIC(12,2) NOT NULL,
    id_metodo_pago INTEGER NOT NULL,
    id_usuario INTEGER NOT NULL,
    FOREIGN KEY (id_cuenta) REFERENCES cuentas_cobrar (id_cuenta),
    FOREIGN KEY (id_metodo_pago) REFERENCES metodos_pago (id_metodo_pago),
    FOREIGN KEY (id_usuario) REFERENCES usuarios (id_usuario),
    CHECK (monto > 0)
);

CREATE TABLE IF NOT EXISTS compras (
    id_compra INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    fecha TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    id_proveedor INTEGER NOT NULL,
    id_usuario INTEGER NOT NULL,
    id_metodo_pago INTEGER NOT NULL,
    subtotal NUMERIC(12,2) NOT NULL,
    impuesto NUMERIC(12,2) NOT NULL DEFAULT 0,
    total NUMERIC(12,2) NOT NULL,
    estado BOOLEAN NOT NULL DEFAULT TRUE,
    FOREIGN KEY (id_proveedor) REFERENCES proveedores (id_proveedor),
    FOREIGN KEY (id_usuario) REFERENCES usuarios (id_usuario),
    FOREIGN KEY (id_metodo_pago) REFERENCES metodos_pago (id_metodo_pago),
    CHECK (subtotal >= 0),
    CHECK (impuesto >= 0),
    CHECK (total >= 0)
);

CREATE TABLE IF NOT EXISTS compras_detalle (
    id_detalle_compra INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    id_compra INTEGER NOT NULL,
    id_producto INTEGER NOT NULL,
    cantidad INTEGER NOT NULL,
    precio NUMERIC(12,2) NOT NULL,
    subtotal NUMERIC(12,2) NOT NULL,
    FOREIGN KEY (id_compra) REFERENCES compras (id_compra),
    FOREIGN KEY (id_producto) REFERENCES productos (id_producto),
    CHECK (cantidad > 0),
    CHECK (precio >= 0),
    CHECK (subtotal >= 0)
);

CREATE TABLE IF NOT EXISTS reservas_clases (
    id_reserva INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    id_cliente INTEGER NOT NULL,
    id_horario INTEGER NOT NULL,
    fecha_clase DATE NOT NULL,
    fecha_reserva TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    estado BOOLEAN NOT NULL DEFAULT TRUE,
    FOREIGN KEY (id_cliente) REFERENCES clientes (id_cliente),
    FOREIGN KEY (id_horario) REFERENCES horarios_clases (id_horario),
    UNIQUE (id_cliente, id_horario, fecha_clase)
);

CREATE TABLE IF NOT EXISTS asistencias (
    id_asistencia INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    id_cliente INTEGER NOT NULL,
    id_reserva INTEGER UNIQUE,
    fecha_hora TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    id_usuario INTEGER NOT NULL,
    FOREIGN KEY (id_cliente) REFERENCES clientes (id_cliente),
    FOREIGN KEY (id_reserva) REFERENCES reservas_clases (id_reserva),
    FOREIGN KEY (id_usuario) REFERENCES usuarios (id_usuario)
);

CREATE TABLE IF NOT EXISTS cobros (
    id_cobro INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    fecha TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    id_cliente INTEGER NOT NULL,
    id_usuario INTEGER NOT NULL,
    id_metodo_pago INTEGER NOT NULL,
    total NUMERIC(12,2) NOT NULL,
    estado BOOLEAN NOT NULL DEFAULT TRUE,
    FOREIGN KEY (id_cliente) REFERENCES clientes (id_cliente),
    FOREIGN KEY (id_usuario) REFERENCES usuarios (id_usuario),
    FOREIGN KEY (id_metodo_pago) REFERENCES metodos_pago (id_metodo_pago),
    CHECK (total > 0)
);

CREATE TABLE IF NOT EXISTS cobros_detalle (
    id_detalle_cobro INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    id_cobro INTEGER NOT NULL,
    tipo_detalle VARCHAR(10) NOT NULL,
    id_cargo INTEGER,
    id_producto INTEGER,
    descripcion VARCHAR(150) NOT NULL,
    cantidad INTEGER NOT NULL DEFAULT 1,
    precio NUMERIC(12,2) NOT NULL,
    subtotal NUMERIC(12,2) NOT NULL,
    FOREIGN KEY (id_cobro) REFERENCES cobros (id_cobro),
    FOREIGN KEY (id_cargo) REFERENCES cargos (id_cargo),
    FOREIGN KEY (id_producto) REFERENCES productos (id_producto),
    CHECK (tipo_detalle IN ('SERVICIO', 'PRODUCTO', 'ADELANTO')),
    CHECK (cantidad > 0),
    CHECK (precio >= 0),
    CHECK (subtotal >= 0),
    CHECK (
        (tipo_detalle = 'SERVICIO' AND id_cargo IS NOT NULL AND id_producto IS NULL)
        OR (tipo_detalle = 'PRODUCTO' AND id_cargo IS NULL AND id_producto IS NOT NULL)
        OR (tipo_detalle = 'ADELANTO' AND id_cargo IS NULL AND id_producto IS NULL)
    )
);

CREATE TABLE IF NOT EXISTS movimientos_inventario (
    id_movimiento INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    id_producto INTEGER NOT NULL,
    tipo_movimiento VARCHAR(7) NOT NULL,
    cantidad INTEGER NOT NULL,
    fecha TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    id_usuario INTEGER NOT NULL,
    id_venta INTEGER,
    id_compra INTEGER,
    FOREIGN KEY (id_producto) REFERENCES productos (id_producto),
    FOREIGN KEY (id_usuario) REFERENCES usuarios (id_usuario),
    FOREIGN KEY (id_venta) REFERENCES ventas (id_venta),
    FOREIGN KEY (id_compra) REFERENCES compras (id_compra),
    CHECK (tipo_movimiento IN ('ENTRADA', 'SALIDA')),
    CHECK (cantidad > 0)
);

CREATE INDEX IF NOT EXISTS idx_roles_permisos_permiso
ON roles_permisos (id_permiso);

CREATE INDEX IF NOT EXISTS idx_productos_marca
ON productos (id_marca);

CREATE INDEX IF NOT EXISTS idx_membresias_clientes_cliente
ON membresias_clientes (id_cliente, fecha_vencimiento);

CREATE INDEX IF NOT EXISTS idx_cargos_cliente
ON cargos (id_cliente, fecha_vencimiento);

CREATE INDEX IF NOT EXISTS idx_ventas_fecha
ON ventas (fecha);

CREATE INDEX IF NOT EXISTS idx_ventas_cliente
ON ventas (id_cliente);

CREATE INDEX IF NOT EXISTS idx_ventas_detalle_venta
ON ventas_detalle (id_venta);

CREATE INDEX IF NOT EXISTS idx_cuentas_cobrar_cliente
ON cuentas_cobrar (id_cliente, fecha_vencimiento);

CREATE INDEX IF NOT EXISTS idx_abonos_cuenta
ON abonos (id_cuenta, fecha);

CREATE INDEX IF NOT EXISTS idx_compras_fecha
ON compras (fecha);

CREATE INDEX IF NOT EXISTS idx_compras_proveedor
ON compras (id_proveedor);

CREATE INDEX IF NOT EXISTS idx_compras_detalle_compra
ON compras_detalle (id_compra);

CREATE INDEX IF NOT EXISTS idx_reservas_fecha
ON reservas_clases (fecha_clase, id_horario);

CREATE INDEX IF NOT EXISTS idx_asistencias_fecha
ON asistencias (fecha_hora);

CREATE INDEX IF NOT EXISTS idx_cobros_fecha
ON cobros (fecha);

CREATE INDEX IF NOT EXISTS idx_cobros_cliente
ON cobros (id_cliente);

CREATE INDEX IF NOT EXISTS idx_cobros_detalle_cobro
ON cobros_detalle (id_cobro);

CREATE INDEX IF NOT EXISTS idx_movimientos_producto_fecha
ON movimientos_inventario (id_producto, fecha);

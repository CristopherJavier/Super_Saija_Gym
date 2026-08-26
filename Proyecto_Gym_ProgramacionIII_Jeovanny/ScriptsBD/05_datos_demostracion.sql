BEGIN;

DO $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM usuarios) THEN
        RAISE EXCEPTION 'Debe existir al menos un usuario antes de cargar los datos de demostración.';
    END IF;
END $$;

INSERT INTO metodos_pago (nombre, descripcion, estado)
VALUES
    ('EFECTIVO', 'Pago realizado en efectivo', TRUE),
    ('TARJETA', 'Pago realizado con tarjeta', TRUE),
    ('TRANSFERENCIA', 'Pago realizado por transferencia', TRUE)
ON CONFLICT (nombre) DO UPDATE
SET descripcion = EXCLUDED.descripcion,
    estado = TRUE;

WITH datos AS (
    SELECT g,
           (ARRAY['Alejandro', 'Beatriz', 'Carlos', 'Daniela', 'Eduardo', 'Fernanda', 'Gabriel', 'Helena', 'Isaac', 'Juliana', 'Kevin', 'Laura', 'Marcos', 'Natalia', 'Oscar', 'Patricia', 'Rafael', 'Sofía', 'Tomás', 'Valentina', 'Wendy', 'Wilmer', 'Xavier', 'Xiomara', 'Yadira', 'Yolanda', 'Zacarías', 'Zaida', 'Zoilo', 'Zuleika'])[g] AS nombre,
           (ARRAY['Ramírez', 'Santos', 'Méndez', 'Castillo', 'Reyes', 'Paredes', 'Vargas', 'Herrera', 'Peña', 'Morales', 'Guerrero', 'Díaz', 'Núñez', 'Cruz', 'Jiménez', 'Rosario', 'Ortiz', 'Suárez', 'Medina', 'Acosta', 'Benítez', 'Campos', 'Domínguez', 'Estévez', 'Flores', 'Garrido', 'Hidalgo', 'Linares', 'Mora', 'Pichardo'])[g] AS apellido
    FROM generate_series(1, 30) AS g
)
INSERT INTO clientes (
    nombre,
    apellido,
    cedula,
    telefono,
    correo,
    direccion,
    fecha_nacimiento,
    sexo,
    fecha_registro,
    estado
)
SELECT nombre,
       apellido,
       '9000000' || LPAD(g::TEXT, 4, '0'),
       '809555' || LPAD(g::TEXT, 4, '0'),
       'cliente' || LPAD(g::TEXT, 2, '0') || '@supersaijagym.demo',
       'Calle Deportiva ' || g || ', Santo Domingo',
       CURRENT_DATE - MAKE_INTERVAL(years => 20 + g),
       CASE WHEN g % 2 = 0 THEN 'FEMENINO' ELSE 'MASCULINO' END,
       CURRENT_TIMESTAMP - MAKE_INTERVAL(days => 30 - g),
       TRUE
FROM datos
ON CONFLICT (cedula) DO UPDATE
SET nombre = EXCLUDED.nombre,
    apellido = EXCLUDED.apellido,
    telefono = EXCLUDED.telefono,
    correo = EXCLUDED.correo,
    direccion = EXCLUDED.direccion,
    fecha_nacimiento = EXCLUDED.fecha_nacimiento,
    sexo = EXCLUDED.sexo,
    estado = TRUE;

WITH datos AS (
    SELECT g,
           (ARRAY['Andrés', 'Bianca', 'Cristian', 'Diana', 'Emilio', 'Fabiola', 'Gustavo', 'Hilda', 'Iván', 'Josefina', 'Leonardo', 'Mariela', 'Nicolás', 'Olivia', 'Pedro', 'Raquel', 'Samuel', 'Teresa', 'Víctor', 'Wendy'])[g] AS nombre,
           (ARRAY['Almonte', 'Báez', 'Cabral', 'Delgado', 'Espinal', 'Franco', 'Guzmán', 'Hernández', 'Iglesias', 'Lora', 'Matos', 'Navarro', 'Orozco', 'Pérez', 'Quezada', 'Rivas', 'Salcedo', 'Tejada', 'Ureña', 'Valdez'])[g] AS apellido,
           (ARRAY['Pesas', 'Yoga', 'Cardio', 'Spinning', 'CrossFit', 'Pilates', 'Funcional', 'Boxeo', 'Zumba', 'Calistenia', 'Natación', 'Rehabilitación', 'Nutrición deportiva', 'Atletismo', 'Artes marciales', 'Aeróbicos', 'TRX', 'Entrenamiento personal', 'Resistencia', 'Movilidad'])[g] AS especialidad
    FROM generate_series(1, 20) AS g
)
INSERT INTO entrenadores (
    nombre,
    apellido,
    cedula,
    telefono,
    correo,
    especialidad,
    fecha_contratacion,
    estado
)
SELECT nombre,
       apellido,
       '8000000' || LPAD(g::TEXT, 4, '0'),
       '829555' || LPAD(g::TEXT, 4, '0'),
       'entrenador' || LPAD(g::TEXT, 2, '0') || '@supersaijagym.demo',
       especialidad,
       CURRENT_DATE - MAKE_INTERVAL(months => 6 + g),
       TRUE
FROM datos
ON CONFLICT (cedula) DO UPDATE
SET nombre = EXCLUDED.nombre,
    apellido = EXCLUDED.apellido,
    telefono = EXCLUDED.telefono,
    correo = EXCLUDED.correo,
    especialidad = EXCLUDED.especialidad,
    estado = TRUE;

INSERT INTO tipos_membresias (nombre, descripcion, duracion_dias, precio, estado)
VALUES
    ('Visita diaria', 'Acceso al gimnasio durante un día', 1, 300.00, TRUE),
    ('Semanal', 'Acceso completo durante siete días', 7, 900.00, TRUE),
    ('Quincenal', 'Acceso completo durante quince días', 15, 1500.00, TRUE),
    ('Mensual', 'Acceso completo durante treinta días', 30, 2500.00, TRUE),
    ('Trimestral', 'Acceso completo durante noventa días', 90, 6500.00, TRUE),
    ('Anual', 'Acceso completo durante un año', 365, 22000.00, TRUE)
ON CONFLICT (nombre) DO UPDATE
SET descripcion = EXCLUDED.descripcion,
    duracion_dias = EXCLUDED.duracion_dias,
    precio = EXCLUDED.precio,
    estado = TRUE;

INSERT INTO clases_actividades (nombre, descripcion, cupo_maximo, estado)
VALUES
    ('Yoga inicial', 'Sesión de flexibilidad y respiración', 25, TRUE),
    ('Spinning', 'Entrenamiento cardiovascular en bicicleta', 20, TRUE),
    ('Zumba', 'Baile y ejercicio cardiovascular', 30, TRUE),
    ('Entrenamiento funcional', 'Circuito de fuerza y coordinación', 20, TRUE),
    ('Boxeo recreativo', 'Técnica básica y acondicionamiento', 18, TRUE),
    ('Pilates', 'Control corporal y fortalecimiento central', 20, TRUE),
    ('Cross training', 'Circuito de fuerza y resistencia', 16, TRUE),
    ('Calistenia', 'Ejercicios con el peso corporal', 20, TRUE),
    ('Movilidad', 'Trabajo de movilidad articular', 25, TRUE),
    ('Cardio intenso', 'Entrenamiento cardiovascular avanzado', 22, TRUE)
ON CONFLICT (nombre) DO UPDATE
SET descripcion = EXCLUDED.descripcion,
    cupo_maximo = EXCLUDED.cupo_maximo,
    estado = TRUE;

INSERT INTO categorias_productos (nombre, descripcion, estado)
VALUES
    ('Bebidas', 'Bebidas hidratantes y energéticas', TRUE),
    ('Suplementos', 'Suplementos para entrenamiento', TRUE),
    ('Accesorios', 'Accesorios personales para ejercicios', TRUE),
    ('Ropa deportiva', 'Ropa para entrenamiento', TRUE),
    ('Equipos ligeros', 'Equipos pequeños de gimnasio', TRUE),
    ('Cuidado personal', 'Artículos de higiene y cuidado', TRUE)
ON CONFLICT (nombre) DO UPDATE
SET descripcion = EXCLUDED.descripcion,
    estado = TRUE;

INSERT INTO marcas (nombre, estado)
VALUES
    ('PowerFit', TRUE),
    ('NutriMax', TRUE),
    ('HydraSport', TRUE),
    ('ActiveWear', TRUE),
    ('GymPro', TRUE),
    ('VitalCare', TRUE)
ON CONFLICT (nombre) DO UPDATE
SET estado = TRUE;

WITH datos AS (
    SELECT g,
           (ARRAY['Agua mineral', 'Bebida isotónica', 'Proteína de vainilla', 'Proteína de chocolate', 'Creatina', 'Barra energética', 'Guantes de entrenamiento', 'Cinturón de pesas', 'Botella deportiva', 'Toalla deportiva', 'Camiseta técnica', 'Pantalón deportivo', 'Banda elástica', 'Cuerda para saltar', 'Rodillo de masaje', 'Colchoneta', 'Muñequeras', 'Protector de rodilla', 'Gel refrescante', 'Desodorante deportivo'])[g] AS nombre,
           (ARRAY['Bebidas', 'Bebidas', 'Suplementos', 'Suplementos', 'Suplementos', 'Suplementos', 'Accesorios', 'Accesorios', 'Accesorios', 'Accesorios', 'Ropa deportiva', 'Ropa deportiva', 'Equipos ligeros', 'Equipos ligeros', 'Equipos ligeros', 'Equipos ligeros', 'Accesorios', 'Accesorios', 'Cuidado personal', 'Cuidado personal'])[g] AS categoria,
           (ARRAY['HydraSport', 'HydraSport', 'NutriMax', 'NutriMax', 'NutriMax', 'NutriMax', 'PowerFit', 'PowerFit', 'GymPro', 'GymPro', 'ActiveWear', 'ActiveWear', 'GymPro', 'GymPro', 'VitalCare', 'GymPro', 'PowerFit', 'PowerFit', 'VitalCare', 'VitalCare'])[g] AS marca
    FROM generate_series(1, 20) AS g
)
INSERT INTO productos (
    codigo,
    nombre,
    descripcion,
    id_categoria,
    id_marca,
    precio_compra,
    precio_venta,
    stock,
    stock_minimo,
    estado
)
SELECT 'DEMO-' || LPAD(g::TEXT, 3, '0'),
       datos.nombre,
       'Producto de demostración para ' || LOWER(datos.categoria),
       cp.id_categoria,
       m.id_marca,
       100.00 + g * 25.00,
       150.00 + g * 40.00,
       40 + g,
       10,
       TRUE
FROM datos
INNER JOIN categorias_productos cp ON cp.nombre = datos.categoria
INNER JOIN marcas m ON m.nombre = datos.marca
ON CONFLICT (codigo) DO UPDATE
SET nombre = EXCLUDED.nombre,
    descripcion = EXCLUDED.descripcion,
    id_categoria = EXCLUDED.id_categoria,
    id_marca = EXCLUDED.id_marca,
    precio_compra = EXCLUDED.precio_compra,
    precio_venta = EXCLUDED.precio_venta,
    stock = EXCLUDED.stock,
    stock_minimo = EXCLUDED.stock_minimo,
    estado = TRUE;

WITH datos AS (
    SELECT g,
           (ARRAY['Suplidora Atlas', 'Caribe Fitness', 'Nutrición Total', 'Equipos del Este', 'Deportes Nacionales', 'Comercial Hércules', 'Importadora Activa', 'Soluciones Fitness', 'Distribuidora Olimpo', 'Grupo Fortaleza', 'Accesorios Elite', 'Bebidas del Caribe', 'Suplementos Premium', 'Textiles Deportivos', 'Equipos Titanes', 'Salud y Movimiento', 'Provisión Atlética', 'Mundo Fitness', 'Comercial Energía', 'Distribuidora Vital'])[g] AS nombre
    FROM generate_series(1, 20) AS g
)
INSERT INTO proveedores (
    nombre,
    rnc_cedula,
    telefono,
    correo,
    direccion,
    estado
)
SELECT nombre,
       '7000000' || LPAD(g::TEXT, 4, '0'),
       '849555' || LPAD(g::TEXT, 4, '0'),
       'proveedor' || LPAD(g::TEXT, 2, '0') || '@supersaijagym.demo',
       'Avenida Comercial ' || g || ', Santo Domingo',
       TRUE
FROM datos
ON CONFLICT (rnc_cedula) DO UPDATE
SET nombre = EXCLUDED.nombre,
    telefono = EXCLUDED.telefono,
    correo = EXCLUDED.correo,
    direccion = EXCLUDED.direccion,
    estado = TRUE;

WITH datos AS (
    SELECT g,
           (ARRAY['Yoga inicial', 'Spinning', 'Zumba', 'Entrenamiento funcional', 'Boxeo recreativo', 'Pilates', 'Cross training', 'Calistenia', 'Movilidad', 'Cardio intenso'])[((g - 1) % 10) + 1] AS clase,
           CASE EXTRACT(DOW FROM CURRENT_DATE)::INTEGER
               WHEN 0 THEN 'DOMINGO'
               WHEN 1 THEN 'LUNES'
               WHEN 2 THEN 'MARTES'
               WHEN 3 THEN 'MIERCOLES'
               WHEN 4 THEN 'JUEVES'
               WHEN 5 THEN 'VIERNES'
               ELSE 'SABADO'
           END AS dia_semana,
           CASE
               WHEN g <= 10 THEN TIME '06:00' + ((g - 1) % 4) * INTERVAL '1 hour'
               ELSE TIME '17:00' + ((g - 11) % 4) * INTERVAL '1 hour'
           END AS hora_inicio
    FROM generate_series(1, 20) AS g
), entrenadores_demo AS (
    SELECT id_entrenador,
           ROW_NUMBER() OVER (ORDER BY cedula) AS numero
    FROM entrenadores
    WHERE cedula BETWEEN '80000000001' AND '80000000020'
)
INSERT INTO horarios_clases (
    id_clase,
    id_entrenador,
    dia_semana,
    hora_inicio,
    hora_fin,
    estado
)
SELECT ca.id_clase,
       e.id_entrenador,
       datos.dia_semana,
       datos.hora_inicio,
       datos.hora_inicio + INTERVAL '1 hour',
       TRUE
FROM datos
INNER JOIN clases_actividades ca ON ca.nombre = datos.clase
INNER JOIN entrenadores_demo e ON e.numero = datos.g
ON CONFLICT (id_clase, dia_semana, hora_inicio) DO UPDATE
SET id_entrenador = EXCLUDED.id_entrenador,
    hora_fin = EXCLUDED.hora_fin,
    estado = TRUE;

WITH clientes_demo AS (
    SELECT id_cliente,
           ROW_NUMBER() OVER (ORDER BY cedula) AS numero
    FROM clientes
    WHERE cedula BETWEEN '90000000001' AND '90000000025'
), tipos_demo AS (
    SELECT id_tipo_membresia,
           precio,
           ROW_NUMBER() OVER (ORDER BY id_tipo_membresia) AS numero
    FROM tipos_membresias
    WHERE nombre IN ('Visita diaria', 'Semanal', 'Quincenal', 'Mensual', 'Trimestral', 'Anual')
)
INSERT INTO membresias_clientes (
    id_cliente,
    id_tipo_membresia,
    fecha_inicio,
    fecha_vencimiento,
    precio_aplicado,
    estado
)
SELECT c.id_cliente,
       t.id_tipo_membresia,
       CASE WHEN c.numero <= 10 THEN CURRENT_DATE - 15 ELSE CURRENT_DATE - 75 END,
       CASE WHEN c.numero <= 10 THEN CURRENT_DATE + 14 ELSE CURRENT_DATE - 46 END,
       t.precio,
       TRUE
FROM clientes_demo c
INNER JOIN tipos_demo t ON t.numero = ((c.numero - 1) % 6) + 1
WHERE NOT EXISTS (
    SELECT 1
    FROM membresias_clientes mc
    WHERE mc.id_cliente = c.id_cliente
);

WITH clientes_demo AS (
    SELECT id_cliente,
           ROW_NUMBER() OVER (ORDER BY cedula) AS numero
    FROM clientes
    WHERE cedula BETWEEN '90000000001' AND '90000000020'
), membresias_demo AS (
    SELECT DISTINCT ON (id_cliente)
           id_cliente,
           id_membresia_cliente,
           precio_aplicado
    FROM membresias_clientes
    WHERE id_cliente IN (SELECT id_cliente FROM clientes_demo)
    ORDER BY id_cliente, id_membresia_cliente DESC
)
INSERT INTO cargos (
    id_cliente,
    id_membresia_cliente,
    concepto,
    fecha_cargo,
    fecha_vencimiento,
    monto,
    saldo,
    estado
)
SELECT c.id_cliente,
       m.id_membresia_cliente,
       'Balance pendiente de demostración',
       CURRENT_DATE - 20,
       CASE WHEN c.numero <= 10 THEN CURRENT_DATE + 10 ELSE CURRENT_DATE - 10 END,
       m.precio_aplicado,
       m.precio_aplicado,
       TRUE
FROM clientes_demo c
INNER JOIN membresias_demo m ON m.id_cliente = c.id_cliente
WHERE NOT EXISTS (
    SELECT 1
    FROM cargos ca
    WHERE ca.id_cliente = c.id_cliente
      AND ca.concepto = 'Balance pendiente de demostración'
);

WITH clientes_demo AS (
    SELECT id_cliente,
           ROW_NUMBER() OVER (ORDER BY cedula) AS numero
    FROM clientes
    WHERE cedula BETWEEN '90000000001' AND '90000000020'
)
INSERT INTO cargos (
    id_cliente,
    concepto,
    fecha_cargo,
    fecha_vencimiento,
    monto,
    saldo,
    estado
)
SELECT id_cliente,
       'Cargo pagado de demostración',
       CURRENT_DATE - 5,
       CURRENT_DATE + 5,
       700.00 + numero * 10.00,
       0,
       FALSE
FROM clientes_demo c
WHERE NOT EXISTS (
    SELECT 1
    FROM cargos ca
    WHERE ca.id_cliente = c.id_cliente
      AND ca.concepto = 'Cargo pagado de demostración'
);

WITH clientes_demo AS (
    SELECT id_cliente,
           ROW_NUMBER() OVER (ORDER BY cedula) AS numero
    FROM clientes
    WHERE cedula BETWEEN '90000000001' AND '90000000020'
), usuario_demo AS (
    SELECT MIN(id_usuario) AS id_usuario
    FROM usuarios
)
INSERT INTO ventas (
    fecha,
    id_cliente,
    id_usuario,
    tipo_pago,
    subtotal,
    descuento,
    impuesto,
    total,
    estado
)
SELECT CURRENT_DATE - LEAST(c.numero - 1, EXTRACT(DAY FROM CURRENT_DATE)::INTEGER - 1)::INTEGER + TIME '09:00',
       c.id_cliente,
       u.id_usuario,
       'CREDITO',
       1200.00 + c.numero * 20.00,
       0,
       0,
       1200.00 + c.numero * 20.00,
       TRUE
FROM clientes_demo c
CROSS JOIN usuario_demo u
WHERE NOT EXISTS (
    SELECT 1
    FROM ventas v
    WHERE v.id_cliente = c.id_cliente
      AND v.tipo_pago = 'CREDITO'
      AND v.total = 1200.00 + c.numero * 20.00
);

WITH clientes_demo AS (
    SELECT id_cliente,
           ROW_NUMBER() OVER (ORDER BY cedula) AS numero
    FROM clientes
    WHERE cedula BETWEEN '90000000001' AND '90000000005'
), usuario_demo AS (
    SELECT MIN(id_usuario) AS id_usuario
    FROM usuarios
)
INSERT INTO ventas (
    fecha,
    id_cliente,
    id_usuario,
    tipo_pago,
    subtotal,
    descuento,
    impuesto,
    total,
    estado
)
SELECT CURRENT_DATE - LEAST(c.numero - 1, EXTRACT(DAY FROM CURRENT_DATE)::INTEGER - 1)::INTEGER + TIME '15:00',
       c.id_cliente,
       u.id_usuario,
       'CONTADO',
       300.00 + c.numero * 25.00,
       0,
       0,
       300.00 + c.numero * 25.00,
       TRUE
FROM clientes_demo c
CROSS JOIN usuario_demo u
WHERE NOT EXISTS (
    SELECT 1
    FROM ventas v
    WHERE v.id_cliente = c.id_cliente
      AND v.tipo_pago = 'CONTADO'
      AND v.total = 300.00 + c.numero * 25.00
);

WITH clientes_demo AS (
    SELECT id_cliente,
           ROW_NUMBER() OVER (ORDER BY cedula) AS numero
    FROM clientes
    WHERE cedula BETWEEN '90000000001' AND '90000000020'
), ventas_demo AS (
    SELECT v.id_venta,
           c.numero,
           v.total
    FROM ventas v
    INNER JOIN clientes_demo c ON c.id_cliente = v.id_cliente
    WHERE v.tipo_pago = 'CREDITO'
      AND v.total = 1200.00 + c.numero * 20.00
), productos_demo AS (
    SELECT id_producto,
           ROW_NUMBER() OVER (ORDER BY codigo) AS numero
    FROM productos
    WHERE codigo BETWEEN 'DEMO-001' AND 'DEMO-020'
)
INSERT INTO ventas_detalle (
    id_venta,
    id_producto,
    cantidad,
    precio,
    descuento,
    subtotal
)
SELECT v.id_venta,
       p.id_producto,
       1,
       v.total,
       0,
       v.total
FROM ventas_demo v
INNER JOIN productos_demo p ON p.numero = v.numero
WHERE NOT EXISTS (
    SELECT 1
    FROM ventas_detalle vd
    WHERE vd.id_venta = v.id_venta
);

WITH clientes_demo AS (
    SELECT id_cliente,
           ROW_NUMBER() OVER (ORDER BY cedula) AS numero
    FROM clientes
    WHERE cedula BETWEEN '90000000001' AND '90000000005'
), ventas_demo AS (
    SELECT v.id_venta,
           c.numero,
           v.total
    FROM ventas v
    INNER JOIN clientes_demo c ON c.id_cliente = v.id_cliente
    WHERE v.tipo_pago = 'CONTADO'
      AND v.total = 300.00 + c.numero * 25.00
), productos_demo AS (
    SELECT id_producto,
           ROW_NUMBER() OVER (ORDER BY codigo) AS numero
    FROM productos
    WHERE codigo BETWEEN 'DEMO-001' AND 'DEMO-020'
)
INSERT INTO ventas_detalle (
    id_venta,
    id_producto,
    cantidad,
    precio,
    descuento,
    subtotal
)
SELECT v.id_venta,
       p.id_producto,
       1,
       v.total,
       0,
       v.total
FROM ventas_demo v
INNER JOIN productos_demo p ON p.numero = v.numero + 5
WHERE NOT EXISTS (
    SELECT 1
    FROM ventas_detalle vd
    WHERE vd.id_venta = v.id_venta
);

WITH clientes_demo AS (
    SELECT id_cliente,
           ROW_NUMBER() OVER (ORDER BY cedula) AS numero
    FROM clientes
    WHERE cedula BETWEEN '90000000001' AND '90000000020'
), ventas_demo AS (
    SELECT v.id_venta,
           v.id_cliente,
           c.numero,
           v.total
    FROM ventas v
    INNER JOIN clientes_demo c ON c.id_cliente = v.id_cliente
    WHERE v.tipo_pago = 'CREDITO'
      AND v.total = 1200.00 + c.numero * 20.00
)
INSERT INTO cuentas_cobrar (
    id_venta,
    id_cliente,
    saldo,
    fecha_vencimiento,
    estado
)
SELECT id_venta,
       id_cliente,
       total - 200.00,
       CASE WHEN numero <= 10 THEN CURRENT_DATE + 15 ELSE CURRENT_DATE - 15 END,
       TRUE
FROM ventas_demo
ON CONFLICT (id_venta) DO NOTHING;

WITH cuentas_demo AS (
    SELECT cc.id_cuenta,
           ROW_NUMBER() OVER (ORDER BY c.cedula) AS numero
    FROM cuentas_cobrar cc
    INNER JOIN clientes c ON c.id_cliente = cc.id_cliente
    WHERE c.cedula BETWEEN '90000000001' AND '90000000020'
), metodo_demo AS (
    SELECT id_metodo_pago
    FROM metodos_pago
    WHERE nombre = 'EFECTIVO'
), usuario_demo AS (
    SELECT MIN(id_usuario) AS id_usuario
    FROM usuarios
)
INSERT INTO abonos (
    id_cuenta,
    fecha,
    monto,
    id_metodo_pago,
    id_usuario
)
SELECT c.id_cuenta,
       CURRENT_DATE - LEAST(c.numero - 1, EXTRACT(DAY FROM CURRENT_DATE)::INTEGER - 1)::INTEGER + TIME '11:00',
       200.00,
       m.id_metodo_pago,
       u.id_usuario
FROM cuentas_demo c
CROSS JOIN metodo_demo m
CROSS JOIN usuario_demo u
WHERE NOT EXISTS (
    SELECT 1
    FROM abonos a
    WHERE a.id_cuenta = c.id_cuenta
      AND a.monto = 200.00
);

WITH proveedores_demo AS (
    SELECT id_proveedor,
           ROW_NUMBER() OVER (ORDER BY rnc_cedula) AS numero
    FROM proveedores
    WHERE rnc_cedula BETWEEN '70000000001' AND '70000000020'
), usuario_demo AS (
    SELECT MIN(id_usuario) AS id_usuario
    FROM usuarios
), metodo_demo AS (
    SELECT id_metodo_pago,
           ROW_NUMBER() OVER (ORDER BY id_metodo_pago) AS numero
    FROM metodos_pago
    WHERE nombre IN ('EFECTIVO', 'TARJETA', 'TRANSFERENCIA')
)
INSERT INTO compras (
    fecha,
    id_proveedor,
    id_usuario,
    id_metodo_pago,
    subtotal,
    impuesto,
    total,
    estado
)
SELECT CURRENT_DATE - LEAST(p.numero - 1, EXTRACT(DAY FROM CURRENT_DATE)::INTEGER - 1)::INTEGER + TIME '08:00',
       p.id_proveedor,
       u.id_usuario,
       m.id_metodo_pago,
       2000.00 + p.numero * 50.00,
       ROUND((2000.00 + p.numero * 50.00) * 0.18, 2),
       ROUND((2000.00 + p.numero * 50.00) * 1.18, 2),
       TRUE
FROM proveedores_demo p
CROSS JOIN usuario_demo u
INNER JOIN metodo_demo m ON m.numero = ((p.numero - 1) % 3) + 1
WHERE NOT EXISTS (
    SELECT 1
    FROM compras co
    WHERE co.id_proveedor = p.id_proveedor
      AND co.subtotal = 2000.00 + p.numero * 50.00
);

WITH proveedores_demo AS (
    SELECT id_proveedor,
           ROW_NUMBER() OVER (ORDER BY rnc_cedula) AS numero
    FROM proveedores
    WHERE rnc_cedula BETWEEN '70000000001' AND '70000000020'
), compras_demo AS (
    SELECT co.id_compra,
           p.numero,
           co.subtotal
    FROM compras co
    INNER JOIN proveedores_demo p ON p.id_proveedor = co.id_proveedor
    WHERE co.subtotal = 2000.00 + p.numero * 50.00
), productos_demo AS (
    SELECT id_producto,
           ROW_NUMBER() OVER (ORDER BY codigo) AS numero
    FROM productos
    WHERE codigo BETWEEN 'DEMO-001' AND 'DEMO-020'
)
INSERT INTO compras_detalle (
    id_compra,
    id_producto,
    cantidad,
    precio,
    subtotal
)
SELECT co.id_compra,
       p.id_producto,
       10,
       co.subtotal / 10,
       co.subtotal
FROM compras_demo co
INNER JOIN productos_demo p ON p.numero = co.numero
WHERE NOT EXISTS (
    SELECT 1
    FROM compras_detalle cd
    WHERE cd.id_compra = co.id_compra
);

WITH clientes_demo AS (
    SELECT id_cliente,
           ROW_NUMBER() OVER (ORDER BY cedula) AS numero
    FROM clientes
    WHERE cedula BETWEEN '90000000001' AND '90000000020'
), usuario_demo AS (
    SELECT MIN(id_usuario) AS id_usuario
    FROM usuarios
), metodo_demo AS (
    SELECT id_metodo_pago,
           ROW_NUMBER() OVER (ORDER BY id_metodo_pago) AS numero
    FROM metodos_pago
    WHERE nombre IN ('EFECTIVO', 'TARJETA', 'TRANSFERENCIA')
)
INSERT INTO cobros (
    fecha,
    id_cliente,
    id_usuario,
    id_metodo_pago,
    total,
    estado
)
SELECT CURRENT_DATE - LEAST(c.numero - 1, EXTRACT(DAY FROM CURRENT_DATE)::INTEGER - 1)::INTEGER + TIME '13:00',
       c.id_cliente,
       u.id_usuario,
       m.id_metodo_pago,
       700.00 + c.numero * 10.00,
       TRUE
FROM clientes_demo c
CROSS JOIN usuario_demo u
INNER JOIN metodo_demo m ON m.numero = ((c.numero - 1) % 3) + 1
WHERE NOT EXISTS (
    SELECT 1
    FROM cobros co
    WHERE co.id_cliente = c.id_cliente
      AND co.total = 700.00 + c.numero * 10.00
);

WITH clientes_demo AS (
    SELECT id_cliente,
           ROW_NUMBER() OVER (ORDER BY cedula) AS numero
    FROM clientes
    WHERE cedula BETWEEN '90000000001' AND '90000000020'
), cobros_demo AS (
    SELECT co.id_cobro,
           co.id_cliente,
           c.numero,
           co.total
    FROM cobros co
    INNER JOIN clientes_demo c ON c.id_cliente = co.id_cliente
    WHERE co.total = 700.00 + c.numero * 10.00
), cargos_pagados AS (
    SELECT id_cargo,
           id_cliente,
           monto
    FROM cargos
    WHERE concepto = 'Cargo pagado de demostración'
)
INSERT INTO cobros_detalle (
    id_cobro,
    tipo_detalle,
    id_cargo,
    descripcion,
    cantidad,
    precio,
    subtotal
)
SELECT co.id_cobro,
       'SERVICIO',
       ca.id_cargo,
       'Pago de membresía de demostración',
       1,
       co.total,
       co.total
FROM cobros_demo co
INNER JOIN cargos_pagados ca ON ca.id_cliente = co.id_cliente AND ca.monto = co.total
WHERE NOT EXISTS (
    SELECT 1
    FROM cobros_detalle cd
    WHERE cd.id_cobro = co.id_cobro
);

WITH clientes_demo AS (
    SELECT id_cliente,
           ROW_NUMBER() OVER (ORDER BY cedula) AS numero
    FROM clientes
    WHERE cedula BETWEEN '90000000001' AND '90000000025'
), horarios_demo AS (
    SELECT h.id_horario,
           ROW_NUMBER() OVER (ORDER BY ca.nombre, h.hora_inicio) AS numero
    FROM horarios_clases h
    INNER JOIN clases_actividades ca ON ca.id_clase = h.id_clase
    WHERE ca.nombre IN ('Yoga inicial', 'Spinning', 'Zumba', 'Entrenamiento funcional', 'Boxeo recreativo', 'Pilates', 'Cross training', 'Calistenia', 'Movilidad', 'Cardio intenso')
      AND h.dia_semana = CASE EXTRACT(DOW FROM CURRENT_DATE)::INTEGER
          WHEN 0 THEN 'DOMINGO'
          WHEN 1 THEN 'LUNES'
          WHEN 2 THEN 'MARTES'
          WHEN 3 THEN 'MIERCOLES'
          WHEN 4 THEN 'JUEVES'
          WHEN 5 THEN 'VIERNES'
          ELSE 'SABADO'
      END
)
INSERT INTO reservas_clases (
    id_cliente,
    id_horario,
    fecha_clase,
    fecha_reserva,
    estado
)
SELECT c.id_cliente,
       h.id_horario,
       CURRENT_DATE,
       CURRENT_TIMESTAMP - MAKE_INTERVAL(days => 2),
       TRUE
FROM clientes_demo c
INNER JOIN horarios_demo h ON h.numero = ((c.numero - 1) % 20) + 1
ON CONFLICT (id_cliente, id_horario, fecha_clase) DO UPDATE
SET estado = TRUE;

WITH usuario_demo AS (
    SELECT MIN(id_usuario) AS id_usuario
    FROM usuarios
), reservas_demo AS (
    SELECT r.id_reserva,
           r.id_cliente,
           ROW_NUMBER() OVER (ORDER BY c.cedula) AS numero
    FROM reservas_clases r
    INNER JOIN clientes c ON c.id_cliente = r.id_cliente
    WHERE c.cedula BETWEEN '90000000001' AND '90000000020'
      AND r.fecha_clase = CURRENT_DATE
)
INSERT INTO asistencias (
    id_cliente,
    id_reserva,
    fecha_hora,
    id_usuario
)
SELECT r.id_cliente,
       r.id_reserva,
       CURRENT_DATE + TIME '06:00' + ((r.numero - 1) % 12)::INTEGER * INTERVAL '30 minutes',
       u.id_usuario
FROM reservas_demo r
CROSS JOIN usuario_demo u
ON CONFLICT (id_reserva) DO NOTHING;

WITH compras_demo AS (
    SELECT co.id_compra,
           co.id_usuario,
           p.numero
    FROM compras co
    INNER JOIN (
        SELECT id_proveedor,
               ROW_NUMBER() OVER (ORDER BY rnc_cedula) AS numero
        FROM proveedores
        WHERE rnc_cedula BETWEEN '70000000001' AND '70000000020'
    ) p ON p.id_proveedor = co.id_proveedor
    WHERE co.subtotal = 2000.00 + p.numero * 50.00
), productos_demo AS (
    SELECT id_producto,
           ROW_NUMBER() OVER (ORDER BY codigo) AS numero
    FROM productos
    WHERE codigo BETWEEN 'DEMO-001' AND 'DEMO-020'
)
INSERT INTO movimientos_inventario (
    id_producto,
    tipo_movimiento,
    cantidad,
    fecha,
    id_usuario,
    id_compra
)
SELECT p.id_producto,
       'ENTRADA',
       10,
       CURRENT_DATE - LEAST(co.numero - 1, EXTRACT(DAY FROM CURRENT_DATE)::INTEGER - 1)::INTEGER + TIME '08:30',
       co.id_usuario,
       co.id_compra
FROM compras_demo co
INNER JOIN productos_demo p ON p.numero = co.numero
WHERE NOT EXISTS (
    SELECT 1
    FROM movimientos_inventario mi
    WHERE mi.id_compra = co.id_compra
      AND mi.id_producto = p.id_producto
      AND mi.tipo_movimiento = 'ENTRADA'
);

WITH clientes_demo AS (
    SELECT id_cliente,
           ROW_NUMBER() OVER (ORDER BY cedula) AS numero
    FROM clientes
    WHERE cedula BETWEEN '90000000001' AND '90000000020'
), ventas_demo AS (
    SELECT v.id_venta,
           v.id_usuario,
           c.numero,
           v.tipo_pago
    FROM ventas v
    INNER JOIN clientes_demo c ON c.id_cliente = v.id_cliente
    WHERE (v.tipo_pago = 'CREDITO' AND v.total = 1200.00 + c.numero * 20.00)
       OR (v.tipo_pago = 'CONTADO' AND c.numero <= 5 AND v.total = 300.00 + c.numero * 25.00)
), detalles_demo AS (
    SELECT vd.id_venta,
           vd.id_producto
    FROM ventas_detalle vd
    INNER JOIN ventas_demo v ON v.id_venta = vd.id_venta
)
INSERT INTO movimientos_inventario (
    id_producto,
    tipo_movimiento,
    cantidad,
    fecha,
    id_usuario,
    id_venta
)
SELECT d.id_producto,
       'SALIDA',
       1,
       CURRENT_DATE - LEAST(v.numero - 1, EXTRACT(DAY FROM CURRENT_DATE)::INTEGER - 1)::INTEGER + TIME '16:00',
       v.id_usuario,
       v.id_venta
FROM ventas_demo v
INNER JOIN detalles_demo d ON d.id_venta = v.id_venta
WHERE NOT EXISTS (
    SELECT 1
    FROM movimientos_inventario mi
    WHERE mi.id_venta = v.id_venta
      AND mi.id_producto = d.id_producto
      AND mi.tipo_movimiento = 'SALIDA'
);

COMMIT;

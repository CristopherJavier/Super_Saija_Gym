CREATE TABLE IF NOT EXISTS clientes (
    id_cliente INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    nombre VARCHAR(60) NOT NULL,
    apellido VARCHAR(60) NOT NULL,
    cedula VARCHAR(20) NOT NULL UNIQUE,
    telefono VARCHAR(20) NOT NULL,
    correo VARCHAR(120) UNIQUE,
    direccion VARCHAR(200),
    fecha_nacimiento DATE,
    sexo VARCHAR(15),
    foto TEXT,
    fecha_registro TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    estado BOOLEAN NOT NULL DEFAULT TRUE,
    CHECK (sexo IN ('MASCULINO', 'FEMENINO', 'OTRO'))
);

CREATE TABLE IF NOT EXISTS entrenadores (
    id_entrenador INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    nombre VARCHAR(60) NOT NULL,
    apellido VARCHAR(60) NOT NULL,
    cedula VARCHAR(20) NOT NULL UNIQUE,
    telefono VARCHAR(20) NOT NULL,
    correo VARCHAR(120) UNIQUE,
    especialidad VARCHAR(100) NOT NULL,
    fecha_contratacion DATE NOT NULL DEFAULT CURRENT_DATE,
    estado BOOLEAN NOT NULL DEFAULT TRUE
);

CREATE TABLE IF NOT EXISTS tipos_membresias (
    id_tipo_membresia INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    nombre VARCHAR(60) NOT NULL UNIQUE,
    descripcion VARCHAR(200),
    duracion_dias INTEGER NOT NULL,
    precio NUMERIC(10,2) NOT NULL,
    estado BOOLEAN NOT NULL DEFAULT TRUE,
    CHECK (duracion_dias > 0),
    CHECK (precio >= 0)
);

CREATE TABLE IF NOT EXISTS clases_actividades (
    id_clase INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    nombre VARCHAR(80) NOT NULL UNIQUE,
    descripcion VARCHAR(200),
    cupo_maximo INTEGER NOT NULL,
    estado BOOLEAN NOT NULL DEFAULT TRUE,
    CHECK (cupo_maximo > 0)
);

CREATE TABLE IF NOT EXISTS horarios_clases (
    id_horario INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    id_clase INTEGER NOT NULL,
    id_entrenador INTEGER NOT NULL,
    dia_semana VARCHAR(10) NOT NULL,
    hora_inicio TIME NOT NULL,
    hora_fin TIME NOT NULL,
    estado BOOLEAN NOT NULL DEFAULT TRUE,
    FOREIGN KEY (id_clase) REFERENCES clases_actividades (id_clase),
    FOREIGN KEY (id_entrenador) REFERENCES entrenadores (id_entrenador),
    CHECK (dia_semana IN ('LUNES', 'MARTES', 'MIERCOLES', 'JUEVES', 'VIERNES', 'SABADO', 'DOMINGO')),
    CHECK (hora_fin > hora_inicio),
    UNIQUE (id_clase, dia_semana, hora_inicio)
);

CREATE TABLE IF NOT EXISTS categorias_productos (
    id_categoria INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    nombre VARCHAR(80) NOT NULL UNIQUE,
    descripcion VARCHAR(200),
    estado BOOLEAN NOT NULL DEFAULT TRUE
);

CREATE TABLE IF NOT EXISTS marcas (
    id_marca INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    nombre VARCHAR(80) NOT NULL UNIQUE,
    estado BOOLEAN NOT NULL DEFAULT TRUE
);

CREATE TABLE IF NOT EXISTS productos (
    id_producto INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    codigo VARCHAR(30) NOT NULL UNIQUE,
    nombre VARCHAR(100) NOT NULL,
    descripcion VARCHAR(250),
    id_categoria INTEGER NOT NULL,
    id_marca INTEGER,
    precio_compra NUMERIC(12,2) NOT NULL,
    precio_venta NUMERIC(12,2) NOT NULL,
    stock INTEGER NOT NULL DEFAULT 0,
    stock_minimo INTEGER NOT NULL DEFAULT 0,
    imagen TEXT,
    estado BOOLEAN NOT NULL DEFAULT TRUE,
    FOREIGN KEY (id_categoria) REFERENCES categorias_productos (id_categoria),
    FOREIGN KEY (id_marca) REFERENCES marcas (id_marca),
    CHECK (precio_compra >= 0),
    CHECK (precio_venta >= 0),
    CHECK (stock >= 0),
    CHECK (stock_minimo >= 0)
);

CREATE TABLE IF NOT EXISTS proveedores (
    id_proveedor INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    rnc_cedula VARCHAR(20) NOT NULL UNIQUE,
    telefono VARCHAR(20) NOT NULL,
    correo VARCHAR(120) UNIQUE,
    direccion VARCHAR(200),
    estado BOOLEAN NOT NULL DEFAULT TRUE
);

CREATE TABLE IF NOT EXISTS productos_proveedores (
    id_producto INTEGER NOT NULL,
    id_proveedor INTEGER NOT NULL,
    costo_referencia NUMERIC(12,2),
    estado BOOLEAN NOT NULL DEFAULT TRUE,
    PRIMARY KEY (id_producto, id_proveedor),
    FOREIGN KEY (id_producto) REFERENCES productos (id_producto) ON DELETE CASCADE,
    FOREIGN KEY (id_proveedor) REFERENCES proveedores (id_proveedor) ON DELETE CASCADE,
    CHECK (costo_referencia IS NULL OR costo_referencia >= 0)
);

CREATE INDEX idx_clientes_nombre_apellido
ON clientes (nombre, apellido);

CREATE INDEX idx_entrenadores_nombre_apellido
ON entrenadores (nombre, apellido);

CREATE INDEX idx_productos_nombre
ON productos (nombre);

CREATE INDEX idx_horarios_clases_clase_dia
ON horarios_clases (id_clase, dia_semana);

CREATE INDEX idx_productos_proveedores_proveedor
ON productos_proveedores (id_proveedor);

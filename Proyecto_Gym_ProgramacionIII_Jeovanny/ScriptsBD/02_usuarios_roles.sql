CREATE TABLE IF NOT EXISTS roles (
    id_rol INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    nombre VARCHAR(30) NOT NULL UNIQUE,
    descripcion VARCHAR(150),
    estado BOOLEAN NOT NULL DEFAULT TRUE
);

INSERT INTO roles (nombre, descripcion)
VALUES
    ('ADMIN', 'Administrador del sistema'),
    ('EMPLEADO', 'Empleado del gimnasio')
ON CONFLICT (nombre) DO NOTHING;

CREATE TABLE IF NOT EXISTS usuarios (
    id_usuario INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    nombre_usuario VARCHAR(50) NOT NULL UNIQUE,
    nombre_completo VARCHAR(100) NOT NULL,
    contrasena_hash TEXT NOT NULL,
    contrasena_salt TEXT NOT NULL,
    id_rol INTEGER NOT NULL,
    activo BOOLEAN NOT NULL DEFAULT TRUE,
    fecha_creacion TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (id_rol) REFERENCES roles (id_rol)
);

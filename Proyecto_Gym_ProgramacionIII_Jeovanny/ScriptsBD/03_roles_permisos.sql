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

ALTER TABLE usuarios
ADD COLUMN id_rol INTEGER;

UPDATE usuarios
SET id_rol = roles.id_rol
FROM roles
WHERE UPPER(TRIM(usuarios.rol)) = roles.nombre;

ALTER TABLE usuarios
ALTER COLUMN id_rol SET NOT NULL;

ALTER TABLE usuarios
ADD CONSTRAINT fk_usuarios_roles
FOREIGN KEY (id_rol) REFERENCES roles (id_rol);

ALTER TABLE usuarios
DROP COLUMN rol;

CREATE TABLE IF NOT EXISTS permisos (
    id_permiso INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    nombre VARCHAR(60) NOT NULL UNIQUE,
    descripcion VARCHAR(150),
    estado BOOLEAN NOT NULL DEFAULT TRUE
);

CREATE TABLE IF NOT EXISTS roles_permisos (
    id_rol INTEGER NOT NULL,
    id_permiso INTEGER NOT NULL,
    PRIMARY KEY (id_rol, id_permiso),
    FOREIGN KEY (id_rol) REFERENCES roles (id_rol) ON DELETE CASCADE,
    FOREIGN KEY (id_permiso) REFERENCES permisos (id_permiso) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS usuarios_perfiles (
    id_usuario INTEGER PRIMARY KEY,
    telefono VARCHAR(20),
    correo VARCHAR(120) UNIQUE,
    foto TEXT,
    FOREIGN KEY (id_usuario) REFERENCES usuarios (id_usuario) ON DELETE CASCADE
);

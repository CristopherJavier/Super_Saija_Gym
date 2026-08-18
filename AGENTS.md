# Reglas permanentes del proyecto

- Responder y explicar todos los cambios en español.
- Utilizar C# sencillo, claro y apropiado para un estudiante.
- El código debe ser fácil de entender y explicar frente a un profesor.
- Evitar soluciones innecesariamente avanzadas.
- Evitar patrones complejos, inyección de dependencias y arquitecturas exageradas.
- Utilizar nombres descriptivos.

## Convenciones de nombres

- `Frm` para formularios.
- `btn` para botones.
- `txt` para controles `TextBox`.
- `lbl` para controles `Label`.
- `pnl` para controles `Panel`.
- `chk` para controles `CheckBox`.
- `dgv` para controles `DataGridView`.

## Código y diseñador

- Mantener la lógica fuera de los archivos `Designer.cs`.
- No editar manualmente código generado por el diseñador, excepto cuando sea necesario para reparar esta inconsistencia inicial.
- No modificar archivos que no estén relacionados con la tarea actual.
- No tocar manualmente las carpetas `.vs`, `bin` u `obj`.
- No modificar el archivo `.csproj.user`.

## Interfaz y validaciones

- Cuando existan dos o más botones relacionados y haya espacio horizontal suficiente, preferir distribuirlos horizontalmente en lugar de uno debajo del otro.
- Los botones que formen una pareja visual deben mantener el mismo tamaño.
- Evitar scrolls innecesarios y distribuciones que desperdicien espacio.
- Los controles de entrada deben respetar los límites definidos por PostgreSQL.
- Los campos de nombres y apellidos deben impedir números cuando corresponda.
- Los campos numéricos identificables como cédula o teléfono deben impedir letras.
- No agregar textos de ejemplo, ayudas o placeholders que el usuario no haya solicitado.
- No agregar campos, controles, opciones, estados, validaciones o reglas de negocio que no estén solicitados por los requisitos o por el usuario.
- Las decisiones puramente visuales pueden resolverse buscando la mejor presentación, siempre sin cambiar reglas del negocio.
- Cuando falte una regla funcional necesaria para continuar, detenerse y pedir autorización antes de inventarla.

## Base de datos y seguridad

- No utilizar Entity Framework.
- Para PostgreSQL se utilizará Npgsql más adelante.
- No guardar contraseñas ni cadenas de conexión directamente en el código.
- Utilizar consultas SQL parametrizadas.
- Nunca guardar contraseñas de usuarios como texto normal.
- No agregar paquetes externos sin explicar primero por qué son necesarios.

## Flujo de trabajo

- Ejecutar `dotnet build` después de cada fase.
- No continuar con otra fase sin autorización del usuario.
- Al terminar cada fase, indicar:
  - Archivos creados.
  - Archivos modificados.
  - Qué hace cada cambio.
  - Cómo puede explicarse al profesor.

## Git y secretos

- Nunca subir contraseñas a Git.
- Nunca subir credenciales de PostgreSQL.
- Nunca escribir secretos directamente en archivos versionados.
- Revisar `git status` antes de cada commit.
- Revisar los archivos preparados antes de crear un commit.
- No ejecutar `git push` sin autorización.
- No crear repositorios remotos sin autorización.
- No cambiar de rama sin explicar primero el motivo.
- No utilizar `force push`.
- No agregar comentarios dentro del código.
- No agregar comentarios dentro de scripts SQL.
- Mantener las explicaciones en archivos Markdown.

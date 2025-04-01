# **Team Manager - Backend (.NET 9)**
Este es el backend de la aplicación Team Manager, desarrollado con .NET 8 y SQL Server. Proporciona una API REST segura con autenticación JWT para la gestión de usuarios.

🛠 Tecnologías
- .NET 9

- ASP.NET Core Web API

- Dapper

- SQL Server

- JWT (Json Web Tokens)

- AutoMapper

- CORS

📦 Instalación y ejecución
Clonar el repositorio: git clone https://github.com/usuario/team-manager-api.git
cd team-manager-api

Abrir el proyecto en Visual Studio o Visual Studio Code.

Verificar la cadena de conexión en appsettings.json:

"ConnectionStrings": {
"DefaultConnection": "Server=(localdb)\MSSQLLocalDB;Database=SystemIW;User Id=TU_USUARIO;Password=TU_PASSWORD;"
}

Ejecutar la solución: dotnet run

Acceder a Swagger: https://localhost:7168/swagger

Acceder a Frontend: https://github.com/JulianaSosa99/crud-iw-js-front.git

🔐 Seguridad
Se utiliza JWT para autenticación y autorización.

Usuarios con rol "admin" pueden acceder a rutas protegidas como /api/usuario.

🧱 Endpoints principales
🔑 Autenticación
POST /api/Login/login
→ Retorna un token JWT al iniciar sesión

POST /api/Login/registro
→ Permite registrar un nuevo usuario (rol: admin)

👤 Usuarios (requiere token JWT con rol admin)
GET /api/usuario

POST /api/usuario/registro

PUT /api/usuario/{id}

DELETE /api/usuario/{id}

🛢 Base de datos
Incluye stored procedures:

- sp_CrearUsuario

- sp_ActualizarUsuario

- sp_EliminarUsuario

- sp_obtenerUsuarios

- sp_obtenerUsuariosPorEmail

- sp_obtenerUsuariosPorId

🌐 CORS
Se permite conexión desde Angular (http://localhost:4200).

✨ Extras
- Middleware personalizado para extraer info del token.

- Passwords encriptadas con SHA256 (via PasswordHelper).

- AutoMapper para mapear entre entidades y DTOs.


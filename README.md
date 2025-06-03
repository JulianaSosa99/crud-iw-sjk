# Sistema Académico - Backend (.NET 9)

**Autor:** Juliana Sosa

---

Este es el backend de un sistema académico desarrollado en **.NET 9**. Permite gestionar objetivos, hitos y temas académicos, asignarlos a usuarios y registrar su progreso. También genera recomendaciones personalizadas basadas en el desempeño del usuario.
---
DELPLOY: https://crud-iw-js-front.onrender.com/
---
## 🚀 Tecnologías usadas

- ASP.NET Core 9
- Entity Framework Core
- SQL Server
- Docker
- Render (deploy)

## 🌐 Despliegue

El backend está desplegado en Render usando Docker.  
Se configuró la cadena de conexión como **variable de entorno segura**, tanto en desarrollo local como en producción.

## 🔐 Seguridad

- Login con JWT
- Roles: `admin` y `usuario`
- Las credenciales y cadenas sensibles están protegidas

## 🔧 Endpoints principales

### Autenticación
- `POST /api/auth/login`: Login y token JWT

### Usuarios (admin)
- `GET /api/usuarios`: Obtener todos los usuarios

### Administración (admin)
- `POST /api/temas`: Crear tema
- `POST /api/objetivos`: Crear objetivo
- `POST /api/hitos`: Crear hito

### Asignación y progreso (usuario)
- `POST /api/asignacion`: Asignar objetivo al usuario
- `POST /api/progreso`: Registrar progreso en hito
- `GET /api/recomendaciones`: Obtener recomendaciones

## 🐳 Docker

### Construir imagen
```bash
docker build -t sistema-academico-backend .

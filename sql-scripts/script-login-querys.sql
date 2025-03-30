CREATE DATABASE SystemIW

USE SystemIW
go

CREATE TABLE Usuarios (
    Id INT IDENTITY(1,1),  -- ID autoincremental para identificar de forma única al usuario
    Nombre NVARCHAR(100) NOT NULL,       -- Nombre del usuario
    Email NVARCHAR(100) NOT NULL UNIQUE, -- Correo electrónico del usuario, único para cada usuario
    PasswordHash NVARCHAR(255) NOT NULL, -- Contraseña del usuario (debe estar encriptada)
    Rol NVARCHAR(50) NOT NULL  
	CONSTRAINT Pk_Usuarios_Id PRIMARY KEY  (Id)
	-- Rol del usuario (por ejemplo: "Admin", "Usuario")
);

--Obtener Usuarios
CREATE PROCEDURE sp_obtenerUsuarios
AS 
BEGIN
	SELECT Id,Nombre,Email,Rol From
	Usuarios
END

--Query de Obtener Usuarios por ID

CREATE PROCEDURE sp_obtenerUsuariosPorId
 @Id INT
AS 
BEGIN
	SELECT Id,Nombre,Email,Rol From
	Usuarios
	Where Id= @Id
END
--Query de Obtener Email

CREATE PROCEDURE sp_obtenerUsuariosPorEmail
 @Email NVARCHAR(100)
AS 
BEGIN
	SELECT Id,Nombre,Email,Rol From
	Usuarios
	Where Email= @Email
END

--Query de Crear Usuarios

CREATE PROCEDURE sp_CrearUsuario
	@Nombre NVARCHAR(100),
    @Email NVARCHAR(100),
    @PasswordHash NVARCHAR(255),
    @Rol NVARCHAR(50)
AS 
BEGIN
	INSERT INTO Usuarios(Nombre, Email, PasswordHash, Rol)
	VALUES (@Nombre,@Email,@PasswordHash,@Rol)
END

--Query de Actualizar Usuarios 

CREATE PROCEDURE sp_ActualizarUsuario
	@Id INT,
	@Nombre NVARCHAR(100),
    @Email NVARCHAR(100),
    @PasswordHash NVARCHAR(255),
    @Rol NVARCHAR(50)
AS 
BEGIN
	UPDATE Usuarios
	SET
	Nombre= @Nombre, Email= @Email, PasswordHash= @PasswordHash,Rol= @Rol
	WHERE Id= @Id;
END

--Query de Eliminar Usuarios por ID

CREATE PROCEDURE sp_EliminarUsuario
	@Id INT
	 
AS 
BEGIN
	DELETE FROM Usuarios
	WHERE Id= @Id;
END



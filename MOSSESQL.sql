create database Medico;
 use Medico;

CREATE TABLE Pacientes (
    IdPaciente INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100),
    FechaNacimiento DATE,
    Dni NVARCHAR(20),
    Sexo NVARCHAR(20),
    Direccion NVARCHAR(200),
    Telefono NVARCHAR(20)
);

select * from Pacientes;

CREATE TABLE Doctores (
    IdDoctor INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100),
    Telefono NVARCHAR(20),
    Especialidad NVARCHAR(100)
);

CREATE TABLE Sintomas (
    IdSintoma INT PRIMARY KEY IDENTITY(1,1),
    Descripcion NVARCHAR(200)
);

CREATE TABLE Diagnostico (
    IdDiagnostico INT PRIMARY KEY IDENTITY(1,1),
    IdPaciente INT,
    IdDoctor INT,
    FechaDiagnostico DATE,
    DiagnosticoFinal NVARCHAR(200),

    FOREIGN KEY (IdPaciente) REFERENCES Pacientes(IdPaciente),
    FOREIGN KEY (IdDoctor) REFERENCES Doctores(IdDoctor)
);

CREATE TABLE Diagnostico_Sintomas (
    IdDiagnostico INT,
    IdSintoma INT,

    PRIMARY KEY (IdDiagnostico, IdSintoma),
    FOREIGN KEY (IdDiagnostico) REFERENCES Diagnostico(IdDiagnostico),
    FOREIGN KEY (IdSintoma) REFERENCES Sintomas(IdSintoma)
);

CREATE TABLE Historial (
    IdHistorial INT PRIMARY KEY IDENTITY(1,1),
    IdPaciente INT,
    FechaRegistro DATE,
    Observaciones NVARCHAR(500),

    FOREIGN KEY (IdPaciente) REFERENCES Pacientes(IdPaciente)
);

CREATE TABLE Usuarios (
    IdUsuario INT PRIMARY KEY IDENTITY(1,1),
    Usuario NVARCHAR(50),
    Clave NVARCHAR(50)
);

-- Ejemplo de usuario admin
INSERT INTO Usuarios (Usuario, Clave)
VALUES ('admin', '123');

CREATE PROCEDURE ValidarUsuario
    @Usuario NVARCHAR(50),
    @Clave NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT COUNT(*) AS Existe
    FROM Usuarios
    WHERE Usuario = @Usuario AND Clave = @Clave;
END;

CREATE PROCEDURE InsertarPaciente
    @Nombre NVARCHAR(100),
    @FechaNacimiento DATE,
    @Dni NVARCHAR(20),
    @Sexo NVARCHAR(20),
    @Direccion NVARCHAR(200),
    @Telefono NVARCHAR(20)
AS
BEGIN
    INSERT INTO Pacientes (Nombre, FechaNacimiento, Dni, Sexo, Direccion, Telefono)
    VALUES (@Nombre, @FechaNacimiento, @Dni, @Sexo, @Direccion, @Telefono);
END;
GO

CREATE PROCEDURE ObtenerPacientes
AS
BEGIN
    SELECT * FROM Pacientes;
END;
GO

-- Insertar nuevo doctor
CREATE PROCEDURE InsertarDoctor
    @Nombre NVARCHAR(100),
    @Especialidad NVARCHAR(100),
    @Telefono NVARCHAR(20)
AS
BEGIN
    INSERT INTO Doctores (Nombre, Especialidad, Telefono)
    VALUES (@Nombre, @Especialidad, @Telefono);
END
GO

-- Modificar doctor
CREATE PROCEDURE ModificarDoctor
    @IdDoctor INT,
    @Nombre NVARCHAR(100),
    @Especialidad NVARCHAR(100),
    @Telefono NVARCHAR(20)
AS
BEGIN
    UPDATE Doctores
    SET Nombre = @Nombre,
        Especialidad = @Especialidad,
        Telefono = @Telefono
    WHERE IdDoctor = @IdDoctor;
END
GO

-- Eliminar doctor
CREATE PROCEDURE EliminarDoctor
    @IdDoctor INT
AS
BEGIN
    DELETE FROM Doctores
    WHERE IdDoctor = @IdDoctor;
END
GO

-- Obtener todos los doctores
CREATE PROCEDURE ObtenerDoctores
AS
BEGIN
    SELECT * FROM Doctores;
END
GO








CREATE PROCEDURE InsertarSintoma
    @Descripcion NVARCHAR(200)
AS
BEGIN
    INSERT INTO Sintomas (Descripcion)
    VALUES (@Descripcion);
END;
GO

CREATE PROCEDURE EliminarSintoma
    @IdSintoma INT
AS
BEGIN
    DELETE FROM Sintomas WHERE IdSintoma = @IdSintoma;
END;
GO

CREATE PROCEDURE ObtenerSintomas
AS
BEGIN
    SELECT * FROM Sintomas;
END;
GO


CREATE PROCEDURE InsertarDiagnostico
    @IdPaciente INT,
    @IdDoctor INT,
    @FechaDiagnostico DATE,
    @DiagnosticoFinal NVARCHAR(200),
    @IdDiagnostico INT OUTPUT
AS
BEGIN
    INSERT INTO Diagnostico (IdPaciente, IdDoctor, FechaDiagnostico, DiagnosticoFinal)
    VALUES (@IdPaciente, @IdDoctor, @FechaDiagnostico, @DiagnosticoFinal);

    SET @IdDiagnostico = SCOPE_IDENTITY();
END;
GO

CREATE PROCEDURE InsertarDiagnosticoSintoma
    @IdDiagnostico INT,
    @IdSintoma INT
AS
BEGIN
    INSERT INTO Diagnostico_Sintomas (IdDiagnostico, IdSintoma)
    VALUES (@IdDiagnostico, @IdSintoma);
END;
GO


CREATE PROCEDURE InsertarHistorial
    @IdPaciente INT,
    @FechaRegistro DATE,
    @Observaciones NVARCHAR(500)
AS
BEGIN
    INSERT INTO Historial (IdPaciente, FechaRegistro, Observaciones)
    VALUES (@IdPaciente, @FechaRegistro, @Observaciones);
END;
GO

CREATE PROCEDURE ObtenerHistorialPorPaciente
    @IdPaciente INT
AS
BEGIN
    SELECT H.IdHistorial, P.Nombre AS Paciente, H.FechaRegistro, H.Observaciones
    FROM Historial H
    INNER JOIN Pacientes P ON H.IdPaciente = P.IdPaciente
    WHERE H.IdPaciente = @IdPaciente;
END;
GO



Select * from Pacientes;

delete  Pacientes;

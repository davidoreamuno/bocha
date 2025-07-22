CREATE PROCEDURE CrearMarca
    @Id UNIQUEIDENTIFIER,
    @Nombre VARCHAR(MAX)
AS
BEGIN
    INSERT INTO [dbo].[Marcas] (Id, Nombre)
    VALUES (@Id, @Nombre);
END;
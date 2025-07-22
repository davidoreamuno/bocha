CREATE PROCEDURE ActualizarMarca
    @Id UNIQUEIDENTIFIER,
    @Nombre VARCHAR(MAX)
AS
BEGIN
    UPDATE [dbo].[Marcas]
    SET Nombre = @Nombre
    WHERE Id = @Id;
END;
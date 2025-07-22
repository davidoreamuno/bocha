CREATE PROCEDURE [dbo].[ObtenerMarca]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SELECT Id, Nombre
    FROM Marcas
    WHERE Id = @Id
END
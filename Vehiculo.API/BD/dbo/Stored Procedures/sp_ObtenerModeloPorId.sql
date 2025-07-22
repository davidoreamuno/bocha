
-- =============================================
-- Procedimiento: Obtener Modelo por ID
-- =============================================
CREATE PROCEDURE sp_ObtenerModeloPorId
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        m.Id,
        m.IdMarca,
        m.Nombre,
        ma.Nombre AS NombreMarca
    FROM Modelos m
    INNER JOIN Marcas ma ON m.IdMarca = ma.Id
    WHERE m.Id = @Id;
END;
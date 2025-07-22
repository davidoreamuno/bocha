
-- =============================================
-- Procedimiento: Eliminar Modelo
-- =============================================
CREATE PROCEDURE sp_EliminarModelo
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    -- Validar existencia del modelo
    IF NOT EXISTS (SELECT 1 FROM Modelos WHERE Id = @Id)
    BEGIN
        THROW 50006, 'El modelo no existe.', 1;
    END

    DELETE FROM Modelos
    WHERE Id = @Id;
END;
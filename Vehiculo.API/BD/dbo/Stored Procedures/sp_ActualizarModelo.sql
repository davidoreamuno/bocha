
-- =============================================
-- Procedimiento: Actualizar Modelo
-- =============================================
CREATE PROCEDURE sp_ActualizarModelo
    @Id UNIQUEIDENTIFIER,
    @IdMarca UNIQUEIDENTIFIER,
    @Nombre VARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    -- Validar existencia del modelo
    IF NOT EXISTS (SELECT 1 FROM Modelos WHERE Id = @Id)
    BEGIN
        THROW 50003, 'El modelo no existe.', 1;
    END

    -- Validar existencia de la marca
    IF NOT EXISTS (SELECT 1 FROM Marcas WHERE Id = @IdMarca)
    BEGIN
        THROW 50004, 'La marca especificada no existe.', 1;
    END

    -- Validar duplicado en otro registro
    IF EXISTS (
        SELECT 1 FROM Modelos
        WHERE Nombre = @Nombre AND IdMarca = @IdMarca AND Id <> @Id
    )
    BEGIN
        THROW 50005, 'Ya existe otro modelo con ese nombre para esta marca.', 1;
    END

    UPDATE Modelos
    SET IdMarca = @IdMarca,
        Nombre = @Nombre
    WHERE Id = @Id;
END;
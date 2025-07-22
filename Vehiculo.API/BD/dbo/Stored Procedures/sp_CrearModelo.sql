CREATE PROCEDURE sp_CrearModelo
    @Id UNIQUEIDENTIFIER OUTPUT,
    @IdMarca UNIQUEIDENTIFIER,
    @Nombre VARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM Marcas WHERE Id = @IdMarca)
    BEGIN
        THROW 50001, 'La marca especificada no existe.', 1;
    END

    IF EXISTS (SELECT 1 FROM Modelos WHERE Nombre = @Nombre AND IdMarca = @IdMarca)
    BEGIN
        THROW 50002, 'Ya existe un modelo con ese nombre para esta marca.', 1;
    END

    SET @Id = NEWID();

    INSERT INTO Modelos (Id, IdMarca, Nombre)
    VALUES (@Id, @IdMarca, @Nombre);
END;
CREATE PROCEDURE GetAnimalsByClientId
    @ClientId INT
AS
BEGIN
    SELECT * 
    FROM Animal
    WHERE ClientId = @ClientId;
END

EXEC GetAnimalsByClientId @ClientId = 1;

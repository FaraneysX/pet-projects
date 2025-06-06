CREATE PROCEDURE GetClientsBySurname
    @Surname NVARCHAR(50)
AS
BEGIN
    SELECT * 
    FROM Client
    WHERE Surname = @Surname;
END

EXEC GetClientsBySurname 'Иванов';

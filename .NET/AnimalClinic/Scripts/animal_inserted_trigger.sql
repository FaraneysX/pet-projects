CREATE TRIGGER AnimalInsertedTrigger
ON Animal
AFTER INSERT
AS
BEGIN
    PRINT 'Ќовое животное добавлено в базу данных.';
END;

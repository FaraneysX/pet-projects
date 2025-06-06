CREATE TRIGGER ClientInsertedTrigger
ON Client
AFTER INSERT
AS
BEGIN
    PRINT 'Новый клиент добавлен в базу данных.';
END;

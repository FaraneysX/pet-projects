CREATE TRIGGER ClientInsertedTrigger
ON Client
AFTER INSERT
AS
BEGIN
    PRINT '����� ������ �������� � ���� ������.';
END;

CREATE TRIGGER AnimalInsertedTrigger
ON Animal
AFTER INSERT
AS
BEGIN
    PRINT '����� �������� ��������� � ���� ������.';
END;

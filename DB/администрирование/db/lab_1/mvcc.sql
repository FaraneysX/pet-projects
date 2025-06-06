-- Многоверсионность.
GRANT SELECT, UPDATE
ON hotel
TO admin;

GRANT SELECT
ON hotel
TO manager;

-- Транзакция.
BEGIN;

SELECT *, xmin, xmax
FROM hotel
WHERE id = 1;

UPDATE hotel
SET name = 'HOTEL1'
WHERE id = 1;

COMMIT;

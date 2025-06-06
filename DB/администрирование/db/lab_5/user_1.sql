BEGIN;

UPDATE book
SET title = 'title1'
WHERE id = 1;

SELECT pg_sleep(15);
SELECT pg_backend_pid();

COMMIT;

--------------------------------------------

BEGIN;

UPDATE book
SET title = 'title1'
WHERE id = 1;

SELECT pg_sleep(10);

UPDATE book
SET title = 'title1'
WHERE id = 2;

COMMIT;
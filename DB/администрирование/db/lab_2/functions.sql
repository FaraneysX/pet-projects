DROP FUNCTION IF EXISTS get_booking_count_by_client_id(INTEGER);

-- Создание функций (количество бронирований клиента).
CREATE
    OR REPLACE FUNCTION get_booking_count_by_client_id(input_client_id INTEGER)
    RETURNS INTEGER AS
$$
DECLARE
    booking_count INTEGER;
BEGIN
    SELECT COUNT(b.id)
    INTO booking_count
    FROM booking b
    WHERE b.client_id = input_client_id;

    RETURN booking_count;
END;
$$
    LANGUAGE plpgsql
    SECURITY DEFINER;


CREATE
    OR REPLACE FUNCTION get_booking_count_by_client_id(input_client_id INTEGER)
    RETURNS INTEGER AS
$$
DECLARE
    booking_count INTEGER;
BEGIN
    SELECT COUNT(b.id)
    INTO booking_count
    FROM booking b
    WHERE b.client_id = input_client_id;

    RETURN booking_count;
END;
$$
    LANGUAGE plpgsql
    SECURITY INVOKER;


-- Запросы.

-- С использованием функции.
EXPLAIN ANALYZE
SELECT DISTINCT c.id                                 AS client_id,
                c.name                               AS client_name,
                get_booking_count_by_client_id(c.id) AS booking_count
FROM client c
ORDER BY booking_count DESC;


-- Без использования функции.
EXPLAIN ANALYZE
SELECT DISTINCT c.id                       AS client_id,
                c.name                     AS client_name,
                (SELECT COUNT(b.id)
                 FROM booking b
                 WHERE b.client_id = c.id) AS booking_count
FROM client c
ORDER BY booking_count DESC;


-----------------------------
RESET
    ROLE;

SET ROLE default_role;
SET ROLE default_role_up;

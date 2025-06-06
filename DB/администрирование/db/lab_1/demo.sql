------------------------------------------------------------------------------------------------
SELECT session_user, current_user;

INSERT INTO owner (name, surname, patronymic)
VALUES ('John', 'Doe', 'Edwardovich');

INSERT INTO hotel (owner_id, name, address, email, phone, opening, area)
VALUES (1, 'Hotel', 'Area 1', 'hotel1@example.com', '12345678901', 2010, 500);
------------------------------------------------------------------------------------------------

-------------------------------------------------------------------------------
-- Проверка прав доступа.

-- Сотрудник.
SET ROLE employee;

SELECT name, address, email, phone
FROM hotel;

UPDATE hotel
SET name = 'HOTEL'
WHERE address = 'Area 1';

RESET
    ROLE;


-- Менеджер.
SET ROLE manager;

SELECT name, address, email, phone
FROM hotel;

UPDATE hotel
SET name = 'HOTEL'
WHERE address = 'Area 1';

INSERT INTO hotel(owner_id, name, address, email, phone)
VALUES (1, 'Hotel Two', 'Area 2', 'hotel2@example.com', '12345678902');

RESET
    ROLE;


-- Администратор.
SET ROLE admin;

SELECT name, address, email, phone
FROM hotel;

UPDATE hotel
SET name = 'HOTEL'
WHERE address = 'Area 1';
------------!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
INSERT INTO hotel(owner_id, name, address, email, phone)
VALUES (1, 'Hotel Two', 'Area 2', 'hotel2@example.com', '12345678902');

RESET
    ROLE;
-------------------------------------------------------------------------------

-- Групповая роль.

-- Проверка.
SET ROLE admin;
SET ROLE manager;

SELECT owner_id
FROM hotel
WHERE name = 'HOTEL';

RESET
    ROLE;


-- Создание групповой роли.
CREATE ROLE root_privileges
    WITH LOGIN;

GRANT SELECT (owner_id)
    ON hotel
    TO root_privileges;

GRANT root_privileges
    TO admin;

GRANT root_privileges
    TO manager;

-- Передача прав.
SET ROLE admin;

GRANT INSERT
    ON hotel
    TO manager
    WITH GRANT OPTION;

GRANT USAGE, SELECT
    ON SEQUENCE hotel_id_seq
    TO manager
    WITH GRANT OPTION;

RESET
    ROLE;

-- Проверка.
SET ROLE manager;
---------------------!@!!!!!!!!!!!!!!!!!!!!!!!!!!
INSERT INTO hotel(owner_id, name, address, email, phone)
VALUES (1, 'Hotel Two', 'Area 2', 'hotel2@example.com', '12345678902');

-- Выдача прав.
GRANT INSERT
    ON hotel
    TO employee
    WITH GRANT OPTION;

GRANT USAGE, SELECT
    ON SEQUENCE hotel_id_seq
    TO employee
    WITH GRANT OPTION;

RESET
    ROLE;

-- Проверка.
SET ROLE employee;
------------!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
INSERT INTO hotel(owner_id, name, address, email, phone)
VALUES (1, 'Hotel Two', 'Area 2', 'hotel2@example.com', '12345678902');

RESET
    ROLE;

-- Забираем права.
SELECT *
FROM hotel;
---------------
REVOKE GRANT OPTION FOR INSERT
    ON hotel
    FROM admin
    CASCADE;

REVOKE GRANT OPTION FOR USAGE, SELECT
    ON SEQUENCE hotel_id_seq
    FROM admin
    CASCADE;

REVOKE INSERT
    ON hotel
    FROM admin;

SET ROLE admin;
INSERT INTO hotel(owner_id, name, address, email, phone)
VALUES (1, 'Hotel Two', 'Area 2', 'hotel2@example.com', '12345678902');

SET ROLE manager;
INSERT INTO hotel(owner_id, name, address, email, phone)
VALUES (1, 'Hotel Two', 'Area 2', 'hotel2@example.com', '12345678902');

SET ROLE employee;
INSERT INTO hotel(owner_id, name, address, email, phone)
VALUES (1, 'Hotel Two', 'Area 2', 'hotel2@example.com', '12345678902');

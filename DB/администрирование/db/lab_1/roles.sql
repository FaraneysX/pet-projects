REVOKE USAGE, SELECT
    ON SEQUENCE hotel_id_seq
    FROM admin;

REVOKE ALL PRIVILEGES
    ON hotel
    FROM admin;
DROP ROLE admin;

REVOKE ALL PRIVILEGES
    ON hotel
    FROM manager;
DROP ROLE manager;

REVOKE ALL PRIVILEGES
    ON hotel
    FROM employee;
DROP ROLE employee;

REVOKE ALL PRIVILEGES
    ON hotel
    FROM root_privileges;
DROP ROLE root_privileges;

-- Создание ролей.

-- Администратор.
CREATE ROLE admin
    WITH LOGIN
    PASSWORD 'admin';

GRANT SELECT (name, address, email, phone)
    ON hotel
    TO admin;

GRANT UPDATE (name, address, email, phone)
    ON hotel
    TO admin;

GRANT INSERT
    ON hotel
    TO admin
    WITH GRANT OPTION;

GRANT USAGE, SELECT
    ON SEQUENCE hotel_id_seq
    TO admin
    WITH GRANT OPTION;

---------------------------

-- Менеджер.
CREATE ROLE manager
    WITH LOGIN
    PASSWORD 'manager';

GRANT SELECT (name, address, email, phone)
    ON hotel
    TO manager;

GRANT UPDATE (name, address, email, phone)
    ON hotel
    TO manager;
---------------------------

-- Сотрудник.
CREATE ROLE employee
    WITH LOGIN
    PASSWORD 'employee';

GRANT SELECT (name, address, email, phone)
    ON hotel
    TO employee;
---------------------------

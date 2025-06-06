SELECT session_user, current_user;

-- Создание роли.
CREATE ROLE default_role
    WITH LOGIN
    PASSWORD 'default_role';

GRANT SELECT
    ON client
    TO default_role;


CREATE ROLE default_role_up
    WITH LOGIN
    PASSWORD 'default_role_up';

GRANT
    SELECT
    ON client, booking
    TO default_role_up;

-----------------------------
REVOKE ALL PRIVILEGES
    ON client
    FROM default_role;
DROP ROLE default_role;

REVOKE ALL PRIVILEGES
    ON client, booking
    FROM default_role_up;
DROP ROLE default_role_up;

DROP TABLE IF EXISTS orders CASCADE;
DROP TABLE IF EXISTS inventory CASCADE;
DROP TRIGGER IF EXISTS after_order_insert ON orders;
DROP FUNCTION IF EXISTS update_inventory_on_order;

CREATE TABLE orders
(
    id         SERIAL PRIMARY KEY,
    client_id  INTEGER NOT NULL,
    product_id INTEGER NOT NULL,
    quantity   INTEGER NOT NULL,
    price      REAL    NOT NULL,
    order_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE inventory
(
    product_id   INTEGER PRIMARY KEY,
    product_name VARCHAR(100) NOT NULL,
    quantity     INTEGER      NOT NULL,
    price        REAL         NOT NULL
);

-- Вычитание количества товара в инвентаре после оформления заказа.
CREATE
    OR REPLACE FUNCTION update_inventory_on_order()
    RETURNS TRIGGER AS
$$
BEGIN
    UPDATE inventory
    SET quantity = quantity - NEW.quantity
    WHERE product_id = NEW.product_id;

    RETURN NEW;
END;
$$
    LANGUAGE plpgsql;


CREATE TRIGGER after_order_insert
    AFTER INSERT
    ON orders
    FOR EACH ROW
EXECUTE FUNCTION update_inventory_on_order();


INSERT INTO inventory (product_id, product_name, quantity, price)
VALUES (101, 'Laptop', 50, 1000),
       (102, 'Smartphone', 30, 500),
       (103, 'Tablet', 20, 300);

INSERT INTO orders (client_id, product_id, quantity, price)
VALUES (1, 101, 1, 1000),
       (2, 102, 1, 500),
       (1, 103, 1, 300);

SELECT *
FROM inventory;

----------------------
CREATE
    OR REPLACE FUNCTION check_positive_quantity()
    RETURNS TRIGGER AS
$$
BEGIN
    IF
        NEW.quantity <= 0 THEN
        RAISE EXCEPTION 'Количество товара должно быть положительным числом';
    END IF;

    RETURN NEW;
END;
$$
    LANGUAGE plpgsql;

CREATE TRIGGER before_order_insert
    BEFORE INSERT
    ON orders
    FOR EACH ROW
EXECUTE FUNCTION check_positive_quantity();

INSERT INTO orders (client_id, product_id, quantity, price)
VALUES (1, 101, -3, 1000);

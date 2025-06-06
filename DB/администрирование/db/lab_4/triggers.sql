---------------------------------------------------------------------------------------------------------------------------
-- Вставка.
CREATE OR REPLACE FUNCTION insert_book()
    RETURNS TRIGGER
AS
$$
BEGIN
    INSERT INTO book_audit(operation_time, operation_type, user_name, book_id, book_title, book_created)
    VALUES (now(), 'Insert', user, new.id, new.title, new.created);

    RETURN NULL;
END;
$$
    LANGUAGE plpgsql;

CREATE TRIGGER insert_book_trigger
    AFTER INSERT
    ON book
    FOR EACH ROW
EXECUTE FUNCTION insert_book();
---------------------------------------------------------------------------------------------------------------------------

---------------------------------------------------------------------------------------------------------------------------
-- Обновление.
CREATE OR REPLACE FUNCTION update_book()
    RETURNS TRIGGER
AS
$$
BEGIN
    INSERT INTO book_audit(operation_time, operation_type, user_name, book_id, book_title, book_created)
    VALUES (now(), 'Update (old data)', user, old.id, old.title, old.created);

    INSERT INTO book_audit(operation_time, operation_type, user_name, book_id, book_title, book_created)
    VALUES (now(), 'Update (new data)', user, new.id, new.title, new.created);

    RETURN NULL;
END;
$$
    LANGUAGE plpgsql;

CREATE TRIGGER update_book_trigger
    AFTER UPDATE
    ON book
    FOR EACH ROW
EXECUTE FUNCTION update_book();
---------------------------------------------------------------------------------------------------------------------------

---------------------------------------------------------------------------------------------------------------------------
-- Удаление.
CREATE OR REPLACE FUNCTION delete_book()
    RETURNS TRIGGER
AS
$$
BEGIN
    INSERT INTO book_audit(operation_time, operation_type, user_name, book_id, book_title, book_created)
    VALUES (now(), 'Delete', user, old.id, old.title, old.created);

    RETURN NULL;
END;
$$
    LANGUAGE plpgsql;

CREATE TRIGGER delete_book_trigger
    AFTER DELETE
    ON book
    FOR EACH ROW
EXECUTE FUNCTION delete_book();
---------------------------------------------------------------------------------------------------------------------------

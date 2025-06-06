DROP TABLE IF EXISTS book;
DROP TABLE IF EXISTS book_audit;

-- Книга.
CREATE TABLE book
(
    id      SERIAL PRIMARY KEY,
    title   TEXT NOT NULL,
    created DATE NOT NULL
);

-- Аудит таблицы Книга.
CREATE TABLE book_audit
(
    id             SERIAL PRIMARY KEY,
    operation_time TIMESTAMP NOT NULL,
    operation_type TEXT      NOT NULL,
    user_name      TEXT      NOT NULL,
    book_id        INTEGER   NOT NULL,
    book_title     TEXT      NOT NULL,
    book_created   DATE      NOT NULL
);

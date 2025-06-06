DROP TABLE IF EXISTS book;
CREATE TABLE IF NOT EXISTS book
(
    id      SERIAL PRIMARY KEY,
    title   TEXT NOT NULL,
    created DATE NOT NULL
);

DO
$$
    DECLARE
        i          INTEGER := 1;
        start_date DATE    := '2020-01-01';
    BEGIN
        FOR i IN 1..100
            LOOP
                INSERT INTO book (title, created)
                VALUES (FORMAT('Book_%s', i),
                        start_date + (i % 1050)::INTEGER);
            END LOOP;
    END
$$;

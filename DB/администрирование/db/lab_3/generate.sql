DO $$
DECLARE
    i INTEGER := 1;
    start_date DATE := '2020-01-01';
BEGIN
    FOR i IN 1..1000000 LOOP
        INSERT INTO book (title, created_at)
        VALUES (
            FORMAT('Book_%s', i),
            start_date + (i % 1050)::INTEGER
        );
    END LOOP;
END $$;


DO $$
DECLARE
    i INTEGER := 1;
    start_date DATE := '2020-01-01';
BEGIN
    FOR i IN 1..1000000 LOOP
        INSERT INTO book_partitioned (title, created_at)
        VALUES (
            FORMAT('Book_%s', i),
            start_date + (i % 1050)::INTEGER
        );
    END LOOP;
END $$;

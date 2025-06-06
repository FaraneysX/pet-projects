DROP TABLE IF EXISTS book_2020_2021 CASCADE;
DROP TABLE IF EXISTS book_2021_2022 CASCADE;
DROP TABLE IF EXISTS book_2022_2023 CASCADE;

-- Создание секций для каждой категории книг.

-- Секция для книг, созданных с 2020 по 2021 год.
CREATE TABLE book_2020_2021
PARTITION OF book_partitioned
FOR VALUES
FROM ('2020-01-01') TO ('2021-01-01');

-- Секция для книг, созданных с 2021 по 2022 год.
CREATE TABLE book_2021_2022
PARTITION OF book_partitioned
FOR VALUES
FROM ('2021-01-01') TO ('2022-01-01');

-- Секция для книг, созданных с 2022 года по 2023 год.
CREATE TABLE book_2022_2023
PARTITION OF book_partitioned
FOR VALUES
FROM ('2022-01-01') TO ('2023-01-01');

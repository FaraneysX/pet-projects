DROP TABLE IF EXISTS book CASCADE;
DROP TABLE IF EXISTS book_partitioned CASCADE;

-- Книга.
CREATE TABLE book (
	id					SERIAL,
	title				VARCHAR(100)	NOT NULL,
	created_at			DATE			NOT NULL
);

-- Секционированная таблица книг.
CREATE TABLE book_partitioned (
	id					SERIAL,
	title				VARCHAR(100)	NOT NULL,
	created_at			DATE			NOT NULL,

	PRIMARY KEY (id, created_at)
) PARTITION BY RANGE (created_at);

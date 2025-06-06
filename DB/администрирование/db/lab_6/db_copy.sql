-- Копия таблицы book в файл.
COPY "book"
    TO 'C:\code\labs\course_4\semester_1\db\lab_6\book.txt';

-- Очистка таблицы.
TRUNCATE book;

SELECT *
FROM book;

-- Вставка данных в таблицу из файла.
COPY "book"
    FROM 'C:\code\labs\course_4\semester_1\db\lab_6\book.txt';

---------------------------------------------------------------------------------------------

-- Копирование всей БД.
-- pg_dump -d copy -U postgres -f "C:\code\labs\course_4\semester_1\db\lab_6\copy_db.txt"

-- Восстановление БД из файла.
-- psql -d copy -U postgres -f "C:\code\labs\course_4\semester_1\db\lab_6\copy_db.txt"

---------------------------------------------------------------------------------------------

-- Копирование всей БД в формате каталога, который содержит несколько файлов.
-- pg_dump -d copy -U postgres -f "C:\code\labs\course_4\semester_1\db\lab_6\copy_db" -F directory

-- Восстановление БД из директории.
-- pg_restore -d copy -U postgres "C:\code\labs\course_4\semester_1\db\lab_6\copy_db"

-- Копирование кластера.
-- pg_dumpall -U postgres -f "C:\code\labs\course_4\semester_1\db\lab_6\cluster.txt" -v

-- Восстановление кластера.
-- psql -U postgres -f "C:\code\labs\course_4\semester_1\db\lab_6\cluster.txt"

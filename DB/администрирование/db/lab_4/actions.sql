INSERT INTO book (title, created)
VALUES ('Title 1', '2021-10-10'),
       ('Title 2', '2022-10-10'),
       ('Title 3', '2023-10-10');

UPDATE book
SET title = 'Title X'
WHERE title = 'Title 3';

DELETE
FROM book
WHERE title = 'Title 1';



SELECT *
FROM book;

SELECT *
FROM book_audit;

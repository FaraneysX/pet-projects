-- "Seq Scan on book  (cost=0.00..21370.00 rows=347799 width=19) (actual time=0.012..50.476 rows=347844 loops=1)"
-- "  Filter: ((created_at >= '2020-01-01'::date) AND (created_at <= '2020-12-30'::date))"
-- "  Rows Removed by Filter: 652156"
-- "Planning Time: 0.055 ms"
-- "Execution Time: 58.171 ms"

EXPLAIN ANALYZE
SELECT *
FROM book
WHERE created_at BETWEEN '2020-01-01' AND '2020-12-30';


-- "Seq Scan on book_2020_2021 book_partitioned  (cost=0.00..7453.95 rows=347608 width=19) (actual time=0.012..22.325 rows=347844 loops=1)"
-- "  Filter: ((created_at >= '2020-01-01'::date) AND (created_at <= '2020-12-30'::date))"
-- "  Rows Removed by Filter: 953"
-- "Planning Time: 0.088 ms"
-- "Execution Time: 30.194 ms"

EXPLAIN ANALYZE
SELECT *
FROM book_partitioned
WHERE created_at BETWEEN '2020-01-01' AND '2020-12-30';






-- "Gather  (cost=1000.00..12578.43 rows=1 width=19) (actual time=0.275..40.347 rows=1 loops=1)"
-- "  Workers Planned: 2"
-- "  Workers Launched: 2"
-- "  ->  Parallel Seq Scan on book  (cost=0.00..11578.33 rows=1 width=19) (actual time=3.737..15.711 rows=0 loops=3)"
-- "        Filter: ((title)::text = 'Book_500'::text)"
-- "        Rows Removed by Filter: 333333"
-- "Planning Time: 0.063 ms"
-- "Execution Time: 40.368 ms"

EXPLAIN ANALYZE
SELECT *
FROM book
WHERE title = 'Book_500';


-- "Gather  (cost=1000.00..14724.26 rows=3 width=19) (actual time=15.073..145.635 rows=1 loops=1)"
-- "  Workers Planned: 2"
-- "  Workers Launched: 2"
-- "  ->  Parallel Append  (cost=0.00..13723.96 rows=3 width=19) (actual time=8.022..16.121 rows=0 loops=3)"
-- "        ->  Parallel Seq Scan on book_2020_2021 book_partitioned_1  (cost=0.00..4786.68 rows=1 width=19) (actual time=9.357..9.357 rows=0 loops=2)"
-- "              Filter: ((title)::text = 'Book_500'::text)"
-- "              Rows Removed by Filter: 174398"
-- "        ->  Parallel Seq Scan on book_2021_2022 book_partitioned_2  (cost=0.00..4769.26 rows=1 width=19) (actual time=0.009..14.836 rows=1 loops=1)"
-- "              Filter: ((title)::text = 'Book_500'::text)"
-- "              Rows Removed by Filter: 347514"
-- "        ->  Parallel Seq Scan on book_2022_2023 book_partitioned_3  (cost=0.00..4168.00 rows=1 width=19) (actual time=14.803..14.803 rows=0 loops=1)"
-- "              Filter: ((title)::text = 'Book_500'::text)"
-- "              Rows Removed by Filter: 303688"
-- "Planning Time: 0.158 ms"
-- "Execution Time: 145.654 ms"

EXPLAIN ANALYZE
SELECT *
FROM book_partitioned
WHERE title = 'Book_500';

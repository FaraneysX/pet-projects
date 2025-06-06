/* 1. Выбрать все данные о типах задач. Результат отсортировать
по названию типа в лексикографическом порядке. */

SELECT *
FROM task_type
ORDER BY _name ASC;

/* 2. Выбрать года рождения сотрудников без повторений. */

SELECT DISTINCT EXTRACT(YEAR FROM date_of_birth) AS birth_year
FROM employee;

/* 3. Выбрать года и месяца вступления сотрудников в должность
без повторений. Учитывать только текущие должности. Результат
отсортировать в порядке убывания года и в порядке возрастания
месяца. */

SELECT DISTINCT EXTRACT(YEAR FROM start_date)  AS entry_year,
                EXTRACT(MONTH FROM start_date) AS entry_month
FROM job_history
WHERE end_date IS NULL
ORDER BY entry_year DESC,
         entry_month ASC;


/* 4. Выбрать id, фамилию и инициалы сотрудников в одном столбце.
Результат отсортировать по id в порядке возрастания. */

SELECT _id,
       CONCAT(surname, ' ', LEFT(_name, 1), '. ', LEFT(patronymic, 1)) AS full_name
FROM employee
ORDER BY _id ASC;

/* 5. Выбрать название, краткое название и описание проектов,
в описании которых в описании которых есть цифры или слова,
написанные через дефис. */

SELECT _name       AS project_name,
       short_name  AS project_short_name,
       description AS project_description
FROM project
WHERE description ~ '[0-9]'
   OR description LIKE '%-%';

/* 6. Выбрать все данные о проектах, начавшихся более двух лет назад. */

SELECT *
FROM project
WHERE EXTRACT(YEAR FROM start_date) < EXTRACT(YEAR FROM CURRENT_DATE) - 2;

/* 7. Выбрать все данные о задачах, выполнение которых было
завершено позже запланированного срока на 3-10 дней. Результат
отсортировать по id проекта в порядке возрастания, по дате старта в
порядке убывания, по названию в порядке обратном
лексикографическому. */

SELECT *
FROM task
WHERE EXTRACT(DAY FROM actual_end_date) >= EXTRACT(DAY FROM planned_end_date) + 3
  AND EXTRACT(DAY FROM actual_end_date) <= EXTRACT(DAY FROM planned_end_date) + 10
ORDER BY project_id ASC,
         start_date DESC,
         _name DESC;

/* 8. Выбрать фамилию и инициалы сотрудников, для которых указана
дата рождения и id равен 2, 3, 5, 7, 8 или 11. Результат
отсортировать следующим образом в первую очередь сотрудники с
старше 35 лет, а затем остальные сотрудники. */

SELECT CONCAT(surname, ' ', LEFT(_name, 1), '. ', LEFT(patronymic, 1)) AS full_name
FROM employee
WHERE _id IN (2, 3, 5, 7, 8, 11)
  AND date_of_birth IS NOT NULL
ORDER BY CASE
             WHEN EXTRACT(YEAR FROM date_of_birth) <= EXTRACT(YEAR FROM CURRENT_DATE) - 35 THEN 1
             ELSE 2
             END,
         full_name ASC;

/* 9. Выбрать названия проектов, начатых в прошлом и текущем годах. В
описании которых есть цифры. */

SELECT _name AS project_name
FROM project
WHERE (EXTRACT(YEAR FROM start_date) = EXTRACT(YEAR FROM CURRENT_DATE) OR
       EXTRACT(YEAR FROM start_date) = EXTRACT(YEAR FROM CURRENT_DATE) - 1)
  AND description ~ '[0-9]';

/* 10. Выбрать названия и даты начала разработки проектов, в описании
которых есть хотя бы три слова и нет символов !, ?, %, _. */

SELECT _name, start_date
FROM project
WHERE TRIM(description) LIKE '% % %'
  AND description NOT LIKE '%!%'
  AND description NOT LIKE '%?%'
  AND description NOT LIKE '%\%%' ESCAPE '\' AND
                                         description NOT LIKE '%\_%' ESCAPE '\';

/* 11. Выбрать все данные о задачах. В последнем столбце результирующей
таблицы указать сообщение 'Задача выполнена досрочно', если фактическая дата
завершения раньше планируемой даты завершения, 'Задача не завершена', если не указана
дата фактического завершения и 'Задача завершена с опозданием', если фактическая дата
завершения больше планируемой даты завершения. */

SELECT *,
       CASE
           WHEN actual_end_date < planned_end_date THEN 'Задача выполнена досрочно'
           WHEN actual_end_date IS NULL THEN 'Задача не завершена'
           ELSE 'Задача завершена с опозданием'
           END AS task_status
FROM task;

/* 12. Выбрать общее количество проектов. */

SELECT COUNT(*) AS total_projects
FROM project;

/* 13. Выбрать количество различных имен сотрудников. */

SELECT COUNT(DISTINCT _name) AS unique_names_count
FROM employee;

/* 14. Выбрать дату начала разработки первого и последнего проектов. */

SELECT MIN(start_date) AS first_project_start_date,
       MAX(start_date) AS last_project_start_date
FROM project;

/* 15. Выбрать среднюю продолжительность фактической реализации
задач. Проектов с id 2, 3, 6, 7, 8. */

SELECT AVG(actual_end_date - start_date) AS average_duration
FROM task
WHERE project_id IN (2, 3, 6, 7, 8)
  AND actual_end_date IS NOT NULL;

/* 16. Выбрать название, описание, даты начала, планируемую
и фактическую даты завершения задач проекта с кратким названием
ИПС BOOK. */

SELECT t._name            AS task_name,
       t.description      AS task_description,
       t.start_date       AS task_start_date,
       t.planned_end_date AS task_planned_end_date,
       t.actual_end_date  AS task_actual_end_date
FROM task t
         JOIN
     project p ON t.project_id = p._id
WHERE p.short_name = 'ИПС BOOK';

/* 17. Выбрать краткое название проекта, фамилию и инициалы
руководителя проекта, название, описание, дату начала
и планируемую дату завершения задачи, название типа задачи,
фамилию и инициалы сотрудника, ответственного за выполнение
задачи, фамилию, имя, отчество сотрудников, являющихся
исполнителями задачи. Результат отсортировать по фамилии
руководителей проектов в лексикографическом порядке,
по краткому названию проекта в порядке обратном
лексикографическому, по названию типа задач в алфавитном
порядке и по названию задач тоже в лексикографическом порядке. */

SELECT pr.short_name                                                                   AS project_short_name,
       e.surname || ' ' || CONCAT(LEFT(e._name, 1), '. ', LEFT(e.patronymic, 1))       AS project_manager,
       task._name                                                                      AS task_name,
       task.description                                                                AS task_description,
       task.start_date                                                                 AS task_start_date,
       task.planned_end_date                                                           AS task_planned_end_date,
       tt._name                                                                        AS task_type_name,
       emp.surname || ' ' || CONCAT(LEFT(emp._name, 1), '. ', LEFT(emp.patronymic, 1)) AS responsible_employee,
       emp2.surname || ' ' || emp2._name || ' ' || emp2.patronymic                     AS performer_employee
FROM task
         JOIN
     project pr ON task.project_id = pr._id
         JOIN
     employee e ON pr.manager_id = e._id
         JOIN
     task_type tt ON task.type_id = tt._id
         JOIN
     task_participation tp ON task._id = tp.task_id AND tp.is_task_manager = true
         JOIN
     employee emp ON tp.employee_id = emp._id
         JOIN
     task_participation tp2 ON tt._id = tp2.task_id AND tp2.is_task_manager = false
         JOIN
     employee emp2 ON tp2.employee_id = emp2._id
ORDER BY e.surname ASC,
         pr.short_name DESC,
         tt._name ASC,
         task._name ASC;

/* 18. Выбрать фамилию, имя, отчество сотрудников, которые работают
над не завершенными задачами проекта ИПС BOOK. Результат
отсортировать по фамилии, имени и отчеству в лексикографическом
порядке. */

SELECT e.surname,
       e._name,
       e.patronymic
FROM employee e
         JOIN
     task_participation tp ON e._id = tp.employee_id
         JOIN
     task ON tp.task_id = task._id
         JOIN
     project pr ON task.project_id = pr._id
WHERE pr.short_name = 'ИПС BOOK'
  AND task.actual_end_date IS NULL
ORDER BY e.surname,
         e._name,
         e.patronymic;

/* 19. Выбрать год и количество проектов, начатых в этом году. Результат
отсортировать по году в убывающем порядке. */

SELECT EXTRACT(YEAR FROM start_date) AS project_year,
       COUNT(*)                      AS project_count
FROM project
GROUP BY project_year
ORDER BY project_year DESC;

/* 20. Выбрать названия типов задач и среднюю продолжительность работ
над решением задач соответствующего типа. Результат
отсортировать по названию в лексикографическом порядке. */

SELECT tt._name                              AS task_type_name,
       AVG(t.actual_end_date - t.start_date) AS average_duration_days
FROM task t
         JOIN
     task_type tt ON t.type_id = tt._id
WHERE t.actual_end_date IS NOT NULL
GROUP BY task_type_name
ORDER BY task_type_name;

/* 21. Выбрать название проекта, краткое название проекта, фамилию
и инициалы руководителя проекта и количество незавершенных
на данный момент задач по проекту. */

SELECT project._name                                             AS project_name,
       project.short_name                                        AS project_short_name,
       CONCAT(manager.surname, ' ', LEFT(manager._name, 1), '.') AS project_manager,
       COUNT(task._id)                                           AS unfinished_tasks_count
FROM project
         JOIN
     employee manager ON project.manager_id = manager._id
         LEFT JOIN
     task ON project._id = task.project_id AND task.actual_end_date IS NULL
GROUP BY project_name, project_short_name, project_manager
ORDER BY project_name;

/* 22. Выбрать фамилию и инициалы тех руководителей проектов,
которые руководят несколькими проектами. Результат
отсортировать по фамилии в лексикографическом порядке. */

SELECT manager.surname,
       CONCAT(manager._name, ' ', LEFT(manager.patronymic, 1), '.') AS manager_initials
FROM employee manager
         JOIN
     project ON manager._id = project.manager_id
GROUP BY manager._id, manager.surname, manager._name, manager.patronymic
HAVING COUNT(project._id) > 1
ORDER BY manager.surname;

/* 23. Выбрать название должности и количество сотрудников
вступивших в должность в текущем году. Результат отсортировать
по количеству в порядке убывания. */

SELECT job_title._name     AS job_title_name,
       COUNT(employee._id) AS employees_count
FROM job_history
         JOIN
     job_title ON job_history.job_title_id = job_title._id
         JOIN
     employee ON job_history.employee_id = employee._id
WHERE EXTRACT(YEAR FROM job_history.start_date) = EXTRACT(YEAR FROM CURRENT_DATE)
GROUP BY job_title_name
ORDER BY employees_count DESC;

/* 24. Выбрать год начала проекта, название проекта, краткое название
проекта, количество незавершенных задач, количество завершенных
задач в срок, количество задач, завершенных с опозданием. */

SELECT EXTRACT(YEAR FROM project.start_date)                    AS start_year,
       project._name                                            AS project_name,
       project.short_name                                       AS project_short_name,
       COUNT(CASE WHEN task.actual_end_date IS NULL THEN 1 END) AS unfinished_tasks,
       COUNT(CASE
                 WHEN task.actual_end_date IS NOT NULL AND task.actual_end_date <= task.planned_end_date
                     THEN 1 END)                                AS completed_on_time,
       COUNT(CASE
                 WHEN task.actual_end_date IS NOT NULL AND task.actual_end_date > task.planned_end_date
                     THEN 1 END)                                AS completed_with_delay
FROM project
         LEFT JOIN
     task ON project._id = task.project_id
GROUP BY start_year, project_name, project_short_name
ORDER BY start_year, project_name;

/* 25. Выбрать проекты, начатые более года назад, и имеющие более трех
незавершенных задач. */

SELECT project._name      AS project_name,
       project.short_name AS project_short_name,
       project.start_date AS project_start_date,
       COUNT(task._id)    AS unfinished_tasks_count
FROM project
         JOIN
     task ON project._id = task.project_id
WHERE EXTRACT(YEAR FROM project.start_date) < EXTRACT(YEAR FROM CURRENT_DATE) - 1
  AND task.actual_end_date IS NULL
GROUP BY project._id, project_name, project_short_name, project_start_date
HAVING COUNT(task._id) > 3;

/* 26. Выбрать названия типов задач, имеющих более трех незавершенных
задач по одному проекту. Каждое название в результирующей
таблицы должно упоминаться только один раз. Результат
отсортировать по названию в лексикографическом порядке. */

SELECT DISTINCT tt._name AS task_type_name
FROM task_type tt
         JOIN
     task ON tt._id = task.type_id
WHERE task.actual_end_date IS NULL
GROUP BY tt._id, tt._name
HAVING COUNT(task._id) > 3
ORDER BY task_type_name;

/* 27. Выбрать все данные по проектам, над которыми работает более пяти
сотрудников моложе 25 лет. переделать группировку */

SELECT p.*
FROM project p
         JOIN task t ON p._id = t.project_id
         JOIN task_participation tp ON t._id = tp.task_id
         JOIN employee e ON tp.employee_id = e._id
WHERE EXTRACT(YEAR FROM CURRENT_DATE) - EXTRACT(YEAR FROM e.date_of_birth) < 25
GROUP BY p._id, p.manager_id, p._name, p.short_name, p.description, p.start_date
HAVING COUNT(e._id) >= 5;

/* 28. Выбрать фамилию, имя, отчество всех сотрудников, и если
сотрудник, является руководителем проекта, то название проекта.
Результат отсортировать по фамилии, имени, отчеству в порядке
обратном лексикографическому. */

SELECT e.surname,
       e._name,
       e.patronymic,
       pr._name AS project_name
FROM employee e
         LEFT JOIN project pr ON e._id = pr.manager_id
ORDER BY e.surname DESC,
         e._name DESC,
         e.patronymic DESC;

/* 29. Выбрать названия всех типов задач, и если есть не завершенные
задачи этого типа, то id_задачи и краткое название проекта. */

SELECT tt._name     AS task_type_name,
       t._id        AS task_id,
       p.short_name AS project_short_name,
       t.actual_end_date
FROM task_type tt
         LEFT JOIN task t ON tt._id = t.type_id AND t.actual_end_date IS NULL
         LEFT JOIN project p ON t.project_id = p._id;

/* 30. Выбрать названия всех должностей, и если есть сотрудники
работающие на данный момент в соответствующей должности,
то их количество. */

SELECT jt._name     AS job_title_name,
       COUNT(e._id) AS employee_count
FROM job_title jt
         LEFT JOIN job_history jh ON jt._id = jh.job_title_id AND jh.end_date IS NULL
         LEFT JOIN employee e ON jh.employee_id = e._id
GROUP BY jt._id, jt._name;


/* 31. Для каждого названия проекта выбрать названия всех типов задач. через cross join */

SELECT DISTINCT p._name  AS project_name,
                tt._name AS task_type_name
FROM project p
         CROSS JOIN task_type tt;

/* SELECT DISTINCT p._name AS project_name,
                tt._name AS task_type_name
FROM project p
JOIN task t ON p._id = t.project_id
JOIN task_type tt ON t.type_id = tt._id; */

/* 32. Для каждого названия проекта выбрать названия всех типов задач,
и если есть в проекте соответствующего типа задачи, то их количество. cross join*/

SELECT p._name      AS project_name,
       tt._name     AS task_type_name,
       COUNT(t._id) AS task_count
FROM project p
         CROSS JOIN task_type tt
         LEFT JOIN task t ON p._id = t.project_id AND tt._id = t.type_id
GROUP BY p._id, tt._id, p._name, tt._name;

/* SELECT p._name AS project_name,
       tt._name AS task_type_name,
       COUNT(t._id) AS task_count
FROM project p
JOIN task t ON p._id = t.project_id
JOIN task_type tt ON t.type_id = tt._id
GROUP BY p._id, tt._id, p._name, tt._name; */

/* 33. Выбрать пары задач с одинаковым названием, но из разных
проектов. */

SELECT t1._id   AS task_id1,
       t1._name AS task_name1,
       p1._name AS project_name1,
       t2._id   AS task_id2,
       t2._name AS task_name2,
       p2._name AS project_name2
FROM task t1
         JOIN project p1 ON t1.project_id = p1._id
         JOIN task t2 ON t1._name = t2._name AND t1._id < t2._id
         JOIN project p2 ON t2.project_id = p2._id;

/* 34. Выбрать тройки сотрудников, которые работают в одной и той же
должности. + актуальная должность. */

SELECT e1._name AS employee1_name,
       e2._name AS employee2_name,
       e3._name AS employee3_name,
       jt._name AS job_title
FROM job_history j1
         JOIN employee e1 ON j1.employee_id = e1._id AND
                             (j1.end_date IS NULL OR j1.end_date > CURRENT_DATE)
         JOIN job_history j2 ON j1.job_title_id = j2.job_title_id AND
                                j1.employee_id < j2.employee_id AND
                                (j2.end_date IS NULL OR j2.end_date > CURRENT_DATE)
         JOIN employee e2 ON j2.employee_id = e2._id
         JOIN job_history j3 ON j2.job_title_id = j3.job_title_id
    AND j2.employee_id < j3.employee_id AND
                                (j3.end_date IS NULL OR j3.end_date > CURRENT_DATE)
         JOIN employee e3 ON j3.employee_id = e3._id
         JOIN job_title jt ON j1.job_title_id = jt._id;

-- второй вариант.
SELECT e1._name AS employee1_name,
       e2._name AS employee2_name,
       e3._name AS employee3_name,
       jt._name AS job_title
FROM job_history j1
         JOIN employee e1 ON j1.employee_id = e1._id
         JOIN job_history j2 ON j1.job_title_id = j2.job_title_id AND j1.employee_id < j2.employee_id
         JOIN employee e2 ON j2.employee_id = e2._id
         JOIN job_history j3 ON j2.job_title_id = j3.job_title_id AND j2.employee_id < j3.employee_id
         JOIN employee e3 ON j3.employee_id = e3._id
         JOIN job_title jt ON j1.job_title_id = jt._id
WHERE (j1.end_date IS NULL OR j1.end_date > CURRENT_DATE)
  AND (j2.end_date IS NULL OR j2.end_date > CURRENT_DATE)
  AND (j3.end_date IS NULL OR j3.end_date > CURRENT_DATE);

-- это прошлое.
SELECT e1._name AS employee1_name,
       e2._name AS employee2_name,
       e3._name AS employee3_name,
       jt._name AS job_title
FROM job_history j1
         JOIN employee e1 ON j1.employee_id = e1._id
         JOIN job_history j2 ON j1.job_title_id = j2.job_title_id AND j1.employee_id < j2.employee_id
         JOIN employee e2 ON j2.employee_id = e2._id
         JOIN job_history j3 ON j2.job_title_id = j3.job_title_id AND j2.employee_id < j3.employee_id
         JOIN employee e3 ON j3.employee_id = e3._id
         JOIN job_title jt ON j1.job_title_id = jt._id;

/* 35. Выбрать количество тезок однофамильцев среди сотрудников. */

SELECT surname,
       COUNT(surname) AS number_of_namesakes
FROM employee
GROUP BY surname
HAVING COUNT(surname) > 1;

/* 36. Выбрать краткое название и описание самого первого проекта. */

SELECT short_name,
       description
FROM project
WHERE start_date = (SELECT MIN(start_date)
                    FROM project);

/* 37. Выбрать фамилию, имя, отчество самого молодого и самого
старшего из сотрудников. через подзапросы where*/

SELECT youngest.surname    AS youngest_surname,
       youngest._name      AS youngest_name,
       youngest.patronymic AS youngest_patronymic,
       oldest.surname      AS oldest_surname,
       oldest._name        AS oldest_name,
       oldest.patronymic   AS oldest_patronymic
FROM employee youngest
         CROSS JOIN employee oldest
WHERE youngest.date_of_birth = (SELECT MAX(date_of_birth)
                                FROM employee)
  AND oldest.date_of_birth = (SELECT MIN(date_of_birth)
                              FROM employee);


/* SELECT youngest.surname AS youngest_surname,
       youngest._name AS youngest_name,
       youngest.patronymic AS youngest_patronymic,
       oldest.surname AS oldest_surname,
       oldest._name AS oldest_name,
       oldest.patronymic AS oldest_patronymic
FROM (
    SELECT surname,
           _name,
           patronymic,
           date_of_birth
    FROM employee
	WHERE date_of_birth = (SELECT MAX(date_of_birth)
					       FROM employee)
) AS youngest
CROSS JOIN (
    SELECT surname,
	       _name,
	       patronymic,
           date_of_birth
    FROM employee
	WHERE date_of_birth = (SELECT MIN(date_of_birth)
					       FROM employee)
) AS oldest; */

/* 38. Выбрать фамилию, имя, отчество сотрудников и название
должности актуальной на данный момент. Результат отсортировать
по фамилии, имени, отчеству в лексикографическом порядке. */

SELECT e.surname,
       e._name,
       e.patronymic,
       jt._name AS job_title
FROM employee e
         JOIN job_history jh ON e._id = jh.employee_id
         JOIN job_title jt ON jh.job_title_id = jt._id
WHERE jh.end_date IS NULL
ORDER BY e.surname,
         e._name,
         e.patronymic;

/* 39. Выбрать название должности и фамилию и инициалы сотрудника
принятого на эту должность последним. */

SELECT jt._name                                                              AS job_title,
       CONCAT(e.surname, ' ', LEFT(e._name, 1), '. ', LEFT(e.patronymic, 1)) AS full_name,
       jh.start_date
FROM job_history jh
         JOIN employee e ON jh.employee_id = e._id
         JOIN job_title jt ON jh.job_title_id = jt._id
WHERE jh.start_date = (SELECT MIN(start_date)
                       FROM job_history
                       WHERE job_title_id = jh.job_title_id);

/* 40. Выбрать названия проектов, в которых количество разработчиков
превышает среднее количество разработчиков, работающих
над проектом. */

SELECT pr._name AS project_name, COUNT(tp.employee_id)
FROM project pr
         JOIN task t ON pr._id = t.project_id
         JOIN task_participation tp ON t._id = tp.task_id
GROUP BY pr._id, pr._name
HAVING COUNT(DISTINCT tp.employee_id) > (SELECT AVG(dev_count)
                                         FROM (SELECT COUNT(tp.employee_id) AS dev_count
                                               FROM task t
                                                        JOIN task_participation tp ON t._id = tp.task_id
                                               GROUP BY t._id, t.project_id) AS dev_count);

/* 41. Выбрать id, название и описание задач, для которых
еще не назначены исполнители. NOT EXIST!*/

SELECT t._id, t._name, t.description
FROM task t
WHERE NOT EXISTS (SELECT 1
                  FROM task_participation tp
                  WHERE t._id = tp.task_id);

/* 42. Выбрать сотрудника, который никогда не завершает задачи в срок. */

SELECT e.*
FROM employee e
WHERE NOT EXISTS (SELECT 1
                  FROM task t
                           JOIN task_participation tp ON t._id = tp.task_id
                  WHERE tp.employee_id = e._id
                    AND t.actual_end_date <= t.planned_end_date);

/* 43. Выбрать названия вакантных должностей, т.е. названия должностей,
в которых никто не работает на данный момент. Результат
отсортировать в лексикографическом порядке. EXIST!*/

SELECT j._name AS vacant_job_title
FROM job_title j
WHERE NOT EXISTS (SELECT 1
                  FROM job_history jh
                  WHERE j._id = jh.job_title_id)
ORDER BY j._name ASC;

/* 44. Выбрать все данные о сотруднике(ах), который работал во всех
проектах. -----------------*/

/* 45. Выбрать название проекта с наибольшим количеством
незавершенных задач. использовать cte */

WITH UnfinishedTasksCount AS (SELECT project_id, COUNT(_id) AS unfinished_tasks_count
                              FROM task
                              WHERE actual_end_date IS NULL
                              GROUP BY project_id)
SELECT p._name AS project_name
FROM project p
         LEFT JOIN UnfinishedTasksCount utc ON p._id = utc.project_id
WHERE utc.unfinished_tasks_count = (SELECT MAX(unfinished_tasks_count)
                                    FROM UnfinishedTasksCount);

/* SELECT p._name AS project_name
FROM project p
LEFT JOIN (
    SELECT project_id, COUNT(_id) AS unfinished_tasks_count
    FROM task
    WHERE actual_end_date IS NULL
    GROUP BY project_id
) AS unfinished_tasks ON p._id = unfinished_tasks.project_id
WHERE unfinished_tasks.unfinished_tasks_count = (
    SELECT MAX(unfinished_tasks_count)
    FROM (
        SELECT COUNT(_id) AS unfinished_tasks_count
        FROM task
        WHERE actual_end_date IS NULL
        GROUP BY project_id
    ) AS max_unfinished_tasks
); */

/* 46. Выбрать фамилию и инициалы сотрудника, завершившего вовремя
наибольшее количество задач по проекту ИПС BOOK. cte*/

WITH CompletedTasksCount AS (SELECT tp.employee_id, COUNT(t._id) AS completed_tasks_count
                             FROM task_participation tp
                                      JOIN task t ON tp.task_id = t._id
                                      JOIN project p ON t.project_id = p._id
                             WHERE p._name = 'ИПС BOOK'
                               AND t.actual_end_date IS NOT NULL
                             GROUP BY tp.employee_id)
SELECT CONCAT(e.surname, ' ', LEFT(e._name, 1), '. ', LEFT(e.patronymic, 1)) AS full_name,
       ctc.completed_tasks_count
FROM employee e
         JOIN CompletedTasksCount ctc ON e._id = ctc.employee_id
WHERE ctc.completed_tasks_count = (SELECT MAX(completed_tasks_count)
                                   FROM CompletedTasksCount);

/* 47. Выбрать все даты, которые есть в базе данных. Результат
отсортировать в убывающем порядке. без подзапроса */

SELECT date_of_birth AS date_column
FROM employee
WHERE date_of_birth IS NOT NULL
UNION ALL
SELECT start_date AS date_column
FROM job_history
WHERE start_date IS NOT NULL
UNION ALL
SELECT end_date AS date_column
FROM job_history
WHERE end_date IS NOT NULL
UNION ALL
SELECT start_date AS date_column
FROM project
WHERE start_date IS NOT NULL
UNION ALL
SELECT start_date AS date_column
FROM task
WHERE start_date IS NOT NULL
UNION ALL
SELECT planned_end_date AS date_column
FROM task
WHERE planned_end_date IS NOT NULL
UNION ALL
SELECT actual_end_date AS date_column
FROM task
WHERE actual_end_date IS NOT NULL
ORDER BY date_column DESC;

/* 48. Выбрать пары сотрудников, которые работали хотя бы над двумя
общими проектами. через соединения таблиц 2 раза таблицу сотрудников, 2 раза проекты (1 из вариантов) можно пустить на лабу*/


/* 49. Выбрать дату, которая чаще других встречается в базе данных.  */
SELECT date_column
FROM (SELECT start_date AS date_column
      FROM job_history
      WHERE start_date IS NOT NULL
      UNION ALL
      SELECT end_date AS date_column
      FROM job_history
      WHERE end_date IS NOT NULL
      UNION ALL
      SELECT start_date AS date_column
      FROM project
      WHERE start_date IS NOT NULL
      UNION ALL
      SELECT start_date AS date_column
      FROM task
      WHERE start_date IS NOT NULL) AS all_dates
GROUP BY date_column
ORDER BY COUNT(*) DESC;

/* 50. Выбрать все данные о незавершенных задачах, над которыми
работают сотрудники, принявшие участие по крайней мере в 1/3
от всех проектов. с подзапросами */

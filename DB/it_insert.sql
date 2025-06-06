-- Добавление данных.

-- Сотрудники.
INSERT INTO employee (_name, surname, patronymic, date_of_birth)
VALUES
	('Иван', 'Смирнов', 'Иванович', '2000-01-01'),
	('Елена', 'Петрова', 'Сергеевна', '2002-03-12'),
	('Андрей', 'Смирнов', 'Александрович', '2002-05-25'),
	('Наталья', 'Козлова', 'Владимировна', '2002-07-18'),
	('Сергей', 'Федоров', 'Дмитриевич', '2002-09-30'),
	('Алиса', 'Козлова', 'Алексеевна', '2000-11-11'),
	('Дмитрий', 'Морозов', 'Игоревич', '2005-02-14'),
	('Марина', 'Васнецова', 'Владимировна', '2002-04-23'),
	('Александр', 'Кузнецов', 'Олегович', '2000-06-06'),
	('Ольга', 'Сидорова', 'Андреевна', '1995-08-09'),
	('Павел', 'Ковалев', 'Игоревич', '2002-10-22'),
	('Татьяна', 'Новикова', 'Павловна', '1982-12-25'),
	('Игорь', 'Белов', 'Николаевич', '1993-02-28'),
	('Екатерина', 'Соколова', 'Алексеевна', '1978-04-04'),
	('Артем', 'Григорьев', 'Валерьевич', '1996-06-17'),
	('Светлана', 'Титова', 'Сергеевна', '1984-08-20'),
	('Владимир', 'Попов', 'Иванович', '1991-11-03'),
	('Анна', 'Медведева', 'Андреевна', '1979-01-06'),
	('Денис', 'Жуков', 'Олегович', '1997-03-09'),
	('Юлия', 'Комарова', 'Игоревна', '1986-05-12'),
	('Антон', 'Борисов', 'Владимирович', '1990-07-15'),
	('Нина', 'Егорова', 'Петровна', '2000-09-28'),
	('Глеб', 'Фролов', 'Николаевич', '2005-12-31'),
	('Людмила', 'Савельева', 'Ивановна', '1976-02-02'),
	('Станислав', 'Панов', 'Алексеевич', '2002-04-07'),
	('Маргарита', 'Горбунова', 'Сергеевна', '1981-06-10'),
	('Илья', 'Беляев', 'Олегович', '2002-08-13'),
	('Анастасия', 'Куликова', 'Андреевна', '1987-10-16'),
	('Арсений', 'Тимофеев', 'Игоревич', '1995-01-19'),
	('Евгения', 'Лебедева', 'Владимировна', '1979-03-24'),
	('Максим', 'Поляков', 'Александрович', '1984-05-27'),
	('Елизавета', 'Степанова', 'Петровна', '1996-08-30'),
	('Григорий', 'Николаев', 'Сергеевич', '2001-11-02'),
	('Дарья', 'Гусева', 'Олеговна', '1993-01-05'),
	('Сергей', 'Комиссаров', 'Игоревич', '2002-03-08'),
	('Александра', 'Лебединская', 'Александровна', '2005-05-11'),
	('Валентин', 'Исаев', 'Николаевич', '1982-07-14'),
	('Екатерина', 'Тарасова', 'Дмитриевна', '1990-09-17'),
	('Андрей', 'Королев', 'Олегович', '2002-12-20'),
	('Наталья', 'Пестова', 'Игоревна', '1997-02-23');

-- Должности.
INSERT INTO job_title (_name)
VALUES
	('Программист'),
	('Технический писатель'),
	('Системный администратор'),
	('Дизайнер интерфейсов'),
	('Тестировщик ПО'),
	('Архитектор систем'),
	('Бизнес-аналитик'),
	('Менеджер проекта'),
	('Инженер сетей'),
	('Специалист по безопасности'),
	('Администратор баз данных'),
	('Аналитик данных'),
	('Разработчик мобильных приложений'),
	('Специалист по искусственному интеллекту'),
	('Эксперт по облачным технологиям'),
	('Инженер по автоматизации тестирования'),
	('Менеджер по продукту'),
	('DevOps инженер'),
	('Специалист по машинному обучению'),
	('Администратор систем электронного документооборота'),
	('Следящий'),
	('Инженер');

-- История должностей сотрудников.
INSERT INTO job_history (employee_id, job_title_id, start_date, end_date)
VALUES
	((SELECT _id
	  	FROM employee
	   WHERE _name = 'Иван' AND date_of_birth = '2000-01-01'), 
	 (SELECT _id
	  	FROM job_title
	   WHERE _name = 'DevOps инженер'), 
	 '2022-01-01', NULL),
	 
	((SELECT _id
	  	FROM employee
	   WHERE _name = 'Елена' AND date_of_birth = '2002-03-12'), 
	 (SELECT _id
	  	FROM job_title
	   WHERE _name = 'Технический писатель'), 
	 '2021-05-10', NULL),
	 
	((SELECT _id
	  	FROM employee
	   WHERE _name = 'Андрей' AND date_of_birth = '2002-05-25'), 
	 (SELECT _id
	  	FROM job_title
	   WHERE _name = 'Системный администратор'), 
	 '2023-02-15', NULL),
	 
	((SELECT _id
	  	FROM employee
	   WHERE _name = 'Наталья' AND date_of_birth = '2002-07-18'), 
	 (SELECT _id
	  	FROM job_title
	   WHERE _name = 'Дизайнер интерфейсов'), 
	 '2022-09-20', NULL),
	 
	((SELECT _id
	  	FROM employee
	   WHERE _name = 'Сергей' AND date_of_birth = '2002-09-30'), 
	 (SELECT _id
	  	FROM job_title
	   WHERE _name = 'Тестировщик ПО'), 
	 '2023-03-01', NULL),
	 
	((SELECT _id
	  	FROM employee
	   WHERE _name = 'Алиса' AND date_of_birth = '2000-11-11'), 
	 (SELECT _id
	  	FROM job_title
	   WHERE _name = 'Архитектор систем'), 
	 '2021-07-12', NULL),
	 
	((SELECT _id
	  	FROM employee
	   WHERE _name = 'Дмитрий' AND date_of_birth = '2005-02-14'), 
	 (SELECT _id
	  	FROM job_title
	   WHERE _name = 'Бизнес-аналитик'), 
	 '2020-12-05', NULL),
	 
	((SELECT _id
	  	FROM employee
	   WHERE _name = 'Марина' AND date_of_birth = '2002-04-23'), 
	 (SELECT _id
	  	FROM job_title
	   WHERE _name = 'Менеджер проекта'), 
	 '2022-08-18', NULL),
	 
	((SELECT _id
	  	FROM employee
	   WHERE _name = 'Александр' AND date_of_birth = '2000-06-06'), 
	 (SELECT _id
	  	FROM job_title
	   WHERE _name = 'Инженер сетей'), 
	 '2023-06-10', NULL),
	 
	((SELECT _id
	  	FROM employee
	   WHERE _name = 'Ольга' AND date_of_birth = '1995-08-09'), 
	 (SELECT _id
	  	FROM job_title
	   WHERE _name = 'Специалист по безопасности'), 
	 '2021-11-30', NULL),
	 
	((SELECT _id
	  	FROM employee
	   WHERE _name = 'Павел' AND date_of_birth = '2002-10-22'), 
	 (SELECT _id
	  	FROM job_title
	   WHERE _name = 'Администратор баз данных'), 
	 '2020-10-15', NULL),
	 
	((SELECT _id
	  	FROM employee
	   WHERE _name = 'Татьяна' AND date_of_birth = '1982-12-25'), 
	 (SELECT _id
	  	FROM job_title
	   WHERE _name = 'Аналитик данных'), 
	 '2023-04-22', NULL),
	 
	((SELECT _id
	  	FROM employee
	   WHERE _name = 'Игорь' AND date_of_birth = '1993-02-28'), 
	 (SELECT _id
	  	FROM job_title
	   WHERE _name = 'Разработчик мобильных приложений'), 
	 '2022-03-07', NULL),
	 
	((SELECT _id
	  	FROM employee
	   WHERE _name = 'Екатерина' AND date_of_birth = '1978-04-04'), 
	 (SELECT _id
	  	FROM job_title
	   WHERE _name = 'Специалист по искусственному интеллекту'), 
	 '2021-05-25', NULL),
	 
	((SELECT _id
	  	FROM employee
	   WHERE _name = 'Артем' AND date_of_birth = '1996-06-17'), 
	 (SELECT _id
	  	FROM job_title
	   WHERE _name = 'Эксперт по облачным технологиям'), 
	 '2023-02-01', NULL),
	 
	((SELECT _id
	  	FROM employee
	   WHERE _name = 'Светлана' AND date_of_birth = '1984-08-20'), 
	 (SELECT _id
	  	FROM job_title
	   WHERE _name = 'Инженер по автоматизации тестирования'), 
	 '2022-07-14', NULL),
	 
	((SELECT _id
	  	FROM employee
	   WHERE _name = 'Владимир' AND date_of_birth = '1991-11-03'), 
	 (SELECT _id
	  	FROM job_title
	   WHERE _name = 'Менеджер по продукту'), 
	 '2020-11-28', NULL),
	 
	((SELECT _id
	  	FROM employee
	   WHERE _name = 'Анна' AND date_of_birth = '1979-01-06'), 
	 (SELECT _id
	  	FROM job_title
	   WHERE _name = 'DevOps инженер'), 
	 '2023-01-10', NULL),
	 
	((SELECT _id
	  	FROM employee
	   WHERE _name = 'Денис' AND date_of_birth = '1997-03-09'), 
	 (SELECT _id
	  	FROM job_title
	   WHERE _name = 'Специалист по машинному обучению'), 
	 '2021-09-05', NULL),
	 
	((SELECT _id
	  	FROM employee
	   WHERE _name = 'Юлия' AND date_of_birth = '1986-05-12'), 
	 (SELECT _id
	  	FROM job_title
	   WHERE _name = 'Администратор систем электронного документооборота'), 
	 '2022-04-15', NULL),
	 
	((SELECT _id
	  	FROM employee
	   WHERE _name = 'Антон' AND date_of_birth = '1990-07-15'), 
	 (SELECT _id
	  	FROM job_title
	   WHERE _name = 'Программист'), 
	 '2022-03-15', NULL),
	 
	((SELECT _id
	  	FROM employee
	   WHERE _name = 'Нина' AND date_of_birth = '2000-09-28'), 
	 (SELECT _id
	  	FROM job_title
	   WHERE _name = 'Тестировщик ПО'), 
	 '2023-08-01', NULL),
	 
	((SELECT _id
	  	FROM employee
	   WHERE _name = 'Глеб' AND date_of_birth = '2005-12-31'), 
	 (SELECT _id
	  	FROM job_title
	   WHERE _name = 'Системный администратор'), 
	 '2020-10-30', NULL),
	 
	((SELECT _id
	  	FROM employee
	   WHERE _name = 'Людмила' AND date_of_birth = '1976-02-02'), 
	 (SELECT _id
	  	FROM job_title
	   WHERE _name = 'Дизайнер интерфейсов'), 
	 '2021-12-10', NULL),
	 
	((SELECT _id
	  	FROM employee
	   WHERE _name = 'Станислав' AND date_of_birth = '2002-04-07'), 
	 (SELECT _id
	  	FROM job_title
	   WHERE _name = 'Бизнес-аналитик'), 
	 '2023-02-20', NULL),
	 
	((SELECT _id
	  	FROM employee
	   WHERE _name = 'Маргарита' AND date_of_birth = '1981-06-10'), 
	 (SELECT _id
	  	FROM job_title
	   WHERE _name = 'Менеджер проекта'), 
	 '2022-05-05', NULL),
	 
	((SELECT _id
	  	FROM employee
	   WHERE _name = 'Илья' AND date_of_birth = '2002-08-13'), 
	 (SELECT _id
	  	FROM job_title
	   WHERE _name = 'Инженер сетей'), 
	 '2023-07-10', NULL),
	 
	((SELECT _id
	  	FROM employee
	   WHERE _name = 'Анастасия' AND date_of_birth = '1987-10-16'), 
	 (SELECT _id
	  	FROM job_title
	   WHERE _name = 'Специалист по безопасности'), 
	 '2021-10-15', NULL),
	 
	((SELECT _id
	  	FROM employee
	   WHERE _name = 'Арсений' AND date_of_birth = '1995-01-19'), 
	 (SELECT _id
	  	FROM job_title
	   WHERE _name = 'Администратор баз данных'), 
	 '2020-09-01', NULL),
	 
	((SELECT _id
	  	FROM employee
	   WHERE _name = 'Евгения' AND date_of_birth = '1979-03-24'), 
	 (SELECT _id
	  	FROM job_title
	   WHERE _name = 'Аналитик данных'), 
	 '2023-03-22', NULL),
	 
	((SELECT _id
	  	FROM employee
	   WHERE _name = 'Максим' AND date_of_birth = '1984-05-27'), 
	 (SELECT _id
	  	FROM job_title
	   WHERE _name = 'Разработчик мобильных приложений'), 
	 '2022-04-07', NULL),
	 
	((SELECT _id
	  	FROM employee
	   WHERE _name = 'Елизавета' AND date_of_birth = '1996-08-30'), 
	 (SELECT _id
	  	FROM job_title
	   WHERE _name = 'Специалист по искусственному интеллекту'), 
	 '2021-06-25', NULL),
	 
	((SELECT _id
	  	FROM employee
	   WHERE _name = 'Григорий' AND date_of_birth = '2001-11-02'), 
	 (SELECT _id
	  	FROM job_title
	   WHERE _name = 'Эксперт по облачным технологиям'), 
	 '2023-03-01', NULL),
	 
	((SELECT _id
	  	FROM employee
	   WHERE _name = 'Дарья' AND date_of_birth = '1993-01-05'), 
	 (SELECT _id
	  	FROM job_title
	   WHERE _name = 'Инженер по автоматизации тестирования'), 
	 '2022-08-14', NULL),
	 
	((SELECT _id
	  	FROM employee
	   WHERE _name = 'Сергей' AND date_of_birth = '2002-03-08'), 
	 (SELECT _id
	  	FROM job_title
	   WHERE _name = 'Менеджер по продукту'), 
	 '2020-12-30', NULL),
	 
	((SELECT _id
	  	FROM employee
	   WHERE _name = 'Александра' AND date_of_birth = '2005-05-11'), 
	 (SELECT _id
	  	FROM job_title
	   WHERE _name = 'DevOps инженер'), 
	 '2023-02-10', NULL),
	 
	((SELECT _id
	  	FROM employee
	   WHERE _name = 'Валентин' AND date_of_birth = '1982-07-14'), 
	 (SELECT _id FROM job_title WHERE _name = 'Специалист по машинному обучению'), 
	 '2021-08-05', NULL),
	 
	((SELECT _id
	  	FROM employee
	   WHERE _name = 'Екатерина' AND date_of_birth = '1990-09-17'), 
	 (SELECT _id FROM job_title WHERE _name = 'Администратор систем электронного документооборота'), 
	 '2022-05-15', NULL),
	 
	((SELECT _id
	  	FROM employee
	   WHERE _name = 'Андрей' AND date_of_birth = '2002-12-20'), 
	 (SELECT _id
	  	FROM job_title
	   WHERE _name = 'Программист'), 
	 '2022-01-15', NULL),
	 
	((SELECT _id
	  	FROM employee
	   WHERE _name = 'Наталья' AND date_of_birth = '1997-02-23'), 
	 (SELECT _id
	  	FROM job_title
	   WHERE _name = 'Администратор систем электронного документооборота'), 
	 '2022-03-15', NULL);

-- Проекты.
INSERT INTO project (manager_id, _name, short_name, description, start_date)
VALUES
	((SELECT _id
	  	FROM employee
	  	WHERE _name = 'Марина' AND date_of_birth = '2002-04-23'),
	 'ИПС BOOK',
	 'ИПС BOOK',
	 'Разработка веб-приложения.',
	 '2020-01-15'),
	 
	((SELECT _id
	  	FROM employee
	   WHERE _name = 'Александр' AND date_of_birth = '2000-06-06'),
	 'Проект внедрения системы безопасности',
	 'SecuritySys',
	 'Внедрение новой системы безопасности 2 для защиты корпоративных данных.',
	 '2022-02-28'),
	 
	((SELECT _id
	  	FROM employee
	   WHERE _name = 'Ольга' AND date_of_birth = '1995-08-09'), 
	 'Проект оптимизации сетевой инфраструктуры',
	 'NetworkOpt',
	 'Оптимизация сетевой инфраструктуры для повышения производительности!',
	 '2023-05-10'),
	 
	((SELECT _id
	  	FROM employee
	   WHERE _name = 'Павел' AND date_of_birth = '2002-10-22'),
	 'Проект по внедрению системы управления проектами',
	 'ProjectMgmtSys',
	 'Внедрение системы управления проектами для улучшения планирования и контроля.', 
	 '2022-12-10'),
	 
	((SELECT _id
	  	FROM employee
	   WHERE _name = 'Татьяна' AND date_of_birth = '1982-12-25'),
	 'Проект по разработке искусственного интеллекта',
	 'AIProject',
	 'ИПС BOOK', 
	 '1990-03-15');

-- Типы задач.
INSERT INTO task_type (_name)
VALUES
	('Разработка'),
	('Тестирование'),
	('Дизайн'),
	('Администрирование'),
	('Анализ'),
	('Управление проектом'),
	('Исследование'),
	('Поддержка'),
	('Оптимизация'),
	('Интеграция');

-- Задачи.
INSERT INTO task (project_id, _name, description, start_date, planned_end_date, actual_end_date, type_id)
VALUES
	((SELECT _id
	  	FROM project
	   WHERE _name = 'ИПС BOOK'),
	 'Разработка новой функциональности', 'Разработка новой функции для приложения', '2023-01-20', '2023-02-10', '2023-02-10',
	 (SELECT _id
	  	FROM task_type
	   WHERE _name = 'Тестирование')),
	   
	   /*
	   ((SELECT _id
	  	FROM project
	   WHERE _name = 'ИПС BOOK'),
	 'Разработка нового', 'Разработка новой функции', '2023-01-20', '2023-02-10', '2023-02-10',
	 (SELECT _id
	  	FROM task_type
	   WHERE _name = 'Разработка')),*/
	   
	   ((SELECT _id
	  	FROM project
	   WHERE _name = 'ИПС BOOK'),
	 'Разработка новых функциональностей 2', 'Разработка новой функции для приложения', '2023-01-20', '2023-02-10', '2023-02-10',
	 (SELECT _id
	  	FROM task_type
	   WHERE _name = 'Разработка')),
	   
	   ((SELECT _id
	  	FROM project
	   WHERE _name = 'Проект внедрения системы безопасности'),
	 'Разработка новых функциональностей 2', 'Разработка новой функции для приложения', '2023-01-20', '2023-02-10', NULL,
	 (SELECT _id
	  	FROM task_type
	   WHERE _name = 'Разработка')),
	   
	   ((SELECT _id
	  	FROM project
	   WHERE _name = 'ИПС BOOK'),
	 'Разработка новых тестов', 'Разработка новой функции для приложения', '2023-01-20', '2023-02-10', NULL,
	 (SELECT _id
	  	FROM task_type
	   WHERE _name = 'Разработка')),
	   
	   ((SELECT _id
	  	FROM project
	   WHERE _name = 'ИПС BOOK'),
	 'Разработка тестов', 'Разработка новой функции для приложения', '2023-01-20', '2023-02-10', NULL,
	 (SELECT _id
	  	FROM task_type
	   WHERE _name = 'Разработка')),
	   
	   ((SELECT _id
	  	FROM project
	   WHERE _name = 'ИПС BOOK'),
	 'Разработка игровых тестов', 'Разработка новой функции для приложения', '2023-01-20', '2023-02-10', NULL,
	 (SELECT _id
	  	FROM task_type
	   WHERE _name = 'Разработка')),
	   
	((SELECT _id
		FROM project
	   WHERE _name = 'Проект внедрения системы безопасности'),
	 'Тестирование багфиксов', 'Проведение тестирования после внесения исправлений', '2023-03-01', '2023-03-30', '2023-03-25',
	 (SELECT _id
	  	FROM task_type 
	   WHERE _name = 'Тестирование')),

	((SELECT _id
	  	FROM project
	   WHERE _name = 'Проект оптимизации сетевой инфраструктуры'),
	 'Дизайн лендинга', 'Создание дизайна для нового лендинга продукта', '2023-10-15', '2023-11-01', NULL,
	 (SELECT _id
	  	FROM task_type
	   WHERE _name = 'Дизайн')),
  
	((SELECT _id
	  	FROM project
	   WHERE _name = 'Проект по внедрению системы управления проектами'),
	 'Администрирование серверов', 'Обновление операционной системы и настройка безопасности', '2023-01-01', '2023-01-20', NULL,
	 (SELECT _id
	  	FROM task_type
	   WHERE _name = 'Администрирование')),
  
	((SELECT _id
	  	FROM project
	   WHERE _name = 'Проект по разработке искусственного интеллекта'),
	 'Анализ рынка конкурентов', 'Проведение исследования для выявления конкурентных преимуществ', '2023-03-20', '2023-04-10', NULL,
	 (SELECT _id
	  	FROM task_type
	   WHERE _name = 'Администрирование'));

-- Участие сотрудников в задаче.
INSERT INTO task_participation (task_id, employee_id, participation_percentage, is_task_manager)
VALUES
	((SELECT _id
		FROM task
	   WHERE _name = 'Разработка новой функциональности'),
	 (SELECT _id
	    FROM employee
	   WHERE _name = 'Иван' AND date_of_birth = '2000-01-01'),
	 30, false),
	 
	((SELECT _id
		FROM task
	   WHERE _name = 'Тестирование багфиксов'),
	 (SELECT _id
	    FROM employee
	   WHERE _name = 'Елена' AND date_of_birth = '2002-03-12'),
	 25, false),
	 
	 ((SELECT _id
		FROM task
	   WHERE _name = 'Дизайн лендинга'),
	 (SELECT _id
	    FROM employee
	   WHERE _name = 'Андрей' AND date_of_birth = '2002-05-25'),
	 20, false),

	((SELECT _id
		FROM task
	   WHERE _name = 'Администрирование серверов'),
	 (SELECT _id
	    FROM employee
	   WHERE _name = 'Наталья' AND date_of_birth = '2002-07-18'),
	 15, false),

	((SELECT _id
		FROM task
	   WHERE _name = 'Анализ рынка конкурентов'),
	 (SELECT _id
	    FROM employee
	   WHERE _name = 'Сергей' AND date_of_birth = '2002-09-30'),
	 10, true),

	((SELECT _id
		FROM task
	   WHERE _name = 'Разработка новой функциональности'),
	 (SELECT _id
	    FROM employee
	   WHERE _name = 'Алиса' AND date_of_birth = '2000-11-11'),
	 30, false),

	((SELECT _id
		FROM task
	   WHERE _name = 'Тестирование багфиксов'),
	 (SELECT _id
	    FROM employee
	   WHERE _name = 'Дмитрий' AND date_of_birth = '2005-02-14'),
	 25, false),

	((SELECT _id
		FROM task
	   WHERE _name = 'Дизайн лендинга'),
	 (SELECT _id
	    FROM employee
	   WHERE _name = 'Марина' AND date_of_birth = '2002-04-23'),
	 20, false),

	((SELECT _id
		FROM task
	   WHERE _name = 'Администрирование серверов'),
	 (SELECT _id
	    FROM employee
	   WHERE _name = 'Александр' AND date_of_birth = '2000-06-06'),
	 15, false),

	((SELECT _id
		FROM task
	   WHERE _name = 'Анализ рынка конкурентов'),
	 (SELECT _id
	    FROM employee
	   WHERE _name = 'Ольга' AND date_of_birth = '1995-08-09'),
	 10, true),
	 
	 ((SELECT _id
		FROM task
	   WHERE _name = 'Тестирование багфиксов'),
	 (SELECT _id
	    FROM employee
	   WHERE _name = 'Павел' AND date_of_birth = '2002-10-22'),
	 25, false),

	((SELECT _id
		FROM task
	   WHERE _name = 'Дизайн лендинга'),
	 (SELECT _id
	    FROM employee
	   WHERE _name = 'Татьяна' AND date_of_birth = '1982-12-25'),
	 20, false),

	((SELECT _id
		FROM task
	   WHERE _name = 'Администрирование серверов'),
	 (SELECT _id
	    FROM employee
	   WHERE _name = 'Игорь' AND date_of_birth = '1993-02-28'),
	 15, false),

	((SELECT _id
		FROM task
	   WHERE _name = 'Анализ рынка конкурентов'),
	 (SELECT _id
	    FROM employee
	   WHERE _name = 'Екатерина' AND date_of_birth = '1978-04-04'),
	 10, true),

	((SELECT _id
		FROM task
	   WHERE _name = 'Разработка новой функциональности'),
	 (SELECT _id
	    FROM employee
	   WHERE _name = 'Артем' AND date_of_birth = '1996-06-17'),
	 30, false),

	((SELECT _id
		FROM task
	   WHERE _name = 'Тестирование багфиксов'),
	 (SELECT _id
	    FROM employee
	   WHERE _name = 'Светлана' AND date_of_birth = '1984-08-20'),
	 25, false),

	((SELECT _id
		FROM task
	   WHERE _name = 'Дизайн лендинга'),
	 (SELECT _id
	    FROM employee
	   WHERE _name = 'Владимир' AND date_of_birth = '1991-11-03'),
	 20, false),

	((SELECT _id
		FROM task
	   WHERE _name = 'Администрирование серверов'),
	 (SELECT _id
	    FROM employee
	   WHERE _name = 'Анна' AND date_of_birth = '1979-01-06'),
	 15, false),

	((SELECT _id
		FROM task
	   WHERE _name = 'Анализ рынка конкурентов'),
	 (SELECT _id
	    FROM employee
	   WHERE _name = 'Денис' AND date_of_birth = '1997-03-09'),
	 10, true),
	 
	((SELECT _id
		FROM task
	   WHERE _name = 'Анализ рынка конкурентов'),
	 (SELECT _id
	    FROM employee
	   WHERE _name = 'Юлия' AND date_of_birth = '1986-05-12'),
	 30, false),

	((SELECT _id
		FROM task
	   WHERE _name = 'Тестирование багфиксов'),
	 (SELECT _id
	    FROM employee
	   WHERE _name = 'Антон' AND date_of_birth = '1990-07-15'),
	 25, false),

	((SELECT _id
		FROM task
	   WHERE _name = 'Дизайн лендинга'),
	 (SELECT _id
	    FROM employee
	   WHERE _name = 'Нина' AND date_of_birth = '2000-09-28'),
	 20, false),

	((SELECT _id
		FROM task
	   WHERE _name = 'Администрирование серверов'),
	 (SELECT _id
	    FROM employee
	   WHERE _name = 'Глеб' AND date_of_birth = '2005-12-31'),
	 15, false),

	((SELECT _id
		FROM task
	   WHERE _name = 'Анализ рынка конкурентов'),
	 (SELECT _id
	    FROM employee
	   WHERE _name = 'Людмила' AND date_of_birth = '1976-02-02'),
	 10, true),
	 
	 ((SELECT _id
		FROM task
	   WHERE _name = 'Разработка новой функциональности'),
	 (SELECT _id
	    FROM employee
	   WHERE _name = 'Станислав' AND date_of_birth = '2002-04-07'),
	 30, false),

	((SELECT _id
		FROM task
	   WHERE _name = 'Тестирование багфиксов'),
	 (SELECT _id
	    FROM employee
	   WHERE _name = 'Маргарита' AND date_of_birth = '1981-06-10'),
	 25, false),

	((SELECT _id
		FROM task
	   WHERE _name = 'Дизайн лендинга'),
	 (SELECT _id
	    FROM employee
	   WHERE _name = 'Илья' AND date_of_birth = '2002-08-13'),
	 20, false),

	((SELECT _id
		FROM task
	   WHERE _name = 'Администрирование серверов'),
	 (SELECT _id
	    FROM employee
	   WHERE _name = 'Анастасия' AND date_of_birth = '1987-10-16'),
	 15, false),

	((SELECT _id
		FROM task
	   WHERE _name = 'Анализ рынка конкурентов'),
	 (SELECT _id
	    FROM employee
	   WHERE _name = 'Арсений' AND date_of_birth = '1995-01-19'),
	 10, true),

	((SELECT _id
		FROM task
	   WHERE _name = 'Разработка новой функциональности'),
	 (SELECT _id
	    FROM employee
	   WHERE _name = 'Евгения' AND date_of_birth = '1979-03-24'),
	 5, false),
	 
	 	((SELECT _id
		FROM task
	   WHERE _name = 'Анализ рынка конкурентов'),
	 (SELECT _id
	    FROM employee
	   WHERE _name = 'Максим' AND date_of_birth = '1984-05-27'),
	 30, false),

	((SELECT _id
		FROM task
	   WHERE _name = 'Тестирование багфиксов'),
	 (SELECT _id
	    FROM employee
	   WHERE _name = 'Елизавета' AND date_of_birth = '1996-08-30'),
	 25, false),

	((SELECT _id
		FROM task
	   WHERE _name = 'Дизайн лендинга'),
	 (SELECT _id
	    FROM employee
	   WHERE _name = 'Григорий' AND date_of_birth = '2001-11-02'),
	 20, false),

	((SELECT _id
		FROM task
	   WHERE _name = 'Разработка новой функциональности'),
	 (SELECT _id
	    FROM employee
	   WHERE _name = 'Дарья' AND date_of_birth = '1993-01-05'),
	 15, false),
	  /**/
	 ((SELECT _id
		FROM task
	   WHERE _name = 'Разработка новой функциональности'),
	 (SELECT _id
	    FROM employee
	   WHERE _name = 'Дарья' AND date_of_birth = '1993-01-05'),
	 15, false),

	((SELECT _id
		FROM task
	   WHERE _name = 'Администрирование серверов'),
	 (SELECT _id
	    FROM employee
	   WHERE _name = 'Сергей' AND date_of_birth = '2002-03-08'),
	 10, true),

	((SELECT _id
		FROM task
	   WHERE _name = 'Анализ рынка конкурентов'),
	 (SELECT _id
	    FROM employee
	   WHERE _name = 'Александра' AND date_of_birth = '2005-05-11'),
	 5, false),
	 
	 ((SELECT _id
		FROM task
	   WHERE _name = 'Анализ рынка конкурентов'),
	 (SELECT _id
	    FROM employee
	   WHERE _name = 'Валентин' AND date_of_birth = '1982-07-14'),
	 30, false),

	((SELECT _id
		FROM task
	   WHERE _name = 'Дизайн лендинга'),
	 (SELECT _id
	    FROM employee
	   WHERE _name = 'Екатерина' AND date_of_birth = '1990-09-17'),
	 25, false),

	((SELECT _id
		FROM task
	   WHERE _name = 'Разработка новой функциональности'),
	 (SELECT _id
	    FROM employee
	   WHERE _name = 'Андрей' AND date_of_birth = '2002-12-20'),
	 20, false),

	((SELECT _id
		FROM task
	   WHERE _name = 'Тестирование багфиксов'),
	 (SELECT _id
	    FROM employee
	   WHERE _name = 'Наталья' AND date_of_birth = '1997-02-23'),
	 15, false);
	 
UPDATE employee
SET project_id = (SELECT _id
				  FROM project
				  WHERE _name = 'ИПС BOOK')
WHERE _name = 'Иван' AND
	  surname = 'Смирнов' AND
	  patronymic = 'Иванович';

UPDATE employee
SET project_id = (SELECT _id
				  FROM project
				  WHERE _name = 'ИПС BOOK')
WHERE _name = 'Елена' AND
	  surname = 'Петрова' AND
	  patronymic = 'Сергеевна';
	  
UPDATE employee
SET project_id = (SELECT _id
				  FROM project
				  WHERE _name = 'Проект внедрения системы безопасности')
WHERE _name = 'Андрей' AND
	  surname = 'Смирнов' AND
	  patronymic = 'Александрович';
	  
UPDATE employee
SET project_id = (SELECT _id
				  FROM project
				  WHERE _name = 'Проект внедрения системы безопасности')
WHERE _name = 'Наталья' AND
	  surname = 'Козлова' AND
	  patronymic = 'Владимировна';
	  
UPDATE employee
SET project_id = (SELECT _id
				  FROM project
				  WHERE _name = 'Проект оптимизации сетевой инфраструктуры')
WHERE _name = 'Сергей' AND
	  surname = 'Федоров' AND
	  patronymic = 'Дмитриевич';
	  
UPDATE employee
SET project_id = (SELECT _id
				  FROM project
				  WHERE _name = 'Проект оптимизации сетевой инфраструктуры')
WHERE _name = 'Алиса' AND
	  surname = 'Козлова' AND
	  patronymic = 'Алексеевна';
	  
UPDATE employee
SET project_id = (SELECT _id
				  FROM project
				  WHERE _name = 'Проект по внедрению системы управления проектами')
WHERE _name = 'Дмитрий' AND
	  surname = 'Морозов' AND
	  patronymic = 'Игоревич';

UPDATE employee
SET project_id = (SELECT _id
				  FROM project
				  WHERE _name = 'ИПС BOOK')
WHERE _name = 'Марина' AND
	  surname = 'Васнецова' AND
	  patronymic = 'Владимировна';

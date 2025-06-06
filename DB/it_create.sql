-- Удаление таблиц (если они существуют).
DROP TABLE IF EXISTS employee CASCADE;
DROP TABLE IF EXISTS job_title CASCADE;
DROP TABLE IF EXISTS job_history CASCADE;
DROP TABLE IF EXISTS project CASCADE;
DROP TABLE IF EXISTS task_type CASCADE;
DROP TABLE IF EXISTS task CASCADE;
DROP TABLE IF EXISTS task_participation CASCADE;

-- Создание таблиц.
CREATE TABLE employee
(
    _id           SERIAL PRIMARY KEY,
    project_id    INTEGER     NULL,
    _name         VARCHAR(30) NOT NULL,
    surname       VARCHAR(30) NOT NULL,
    patronymic    VARCHAR(30) NULL,
    date_of_birth DATE        NOT NULL,
    CONSTRAINT employee_data_valid
        CHECK (
            _name ~ '^[^0-9]*$' AND
            surname ~ '^[^0-9]*$' AND
            patronymic ~ '^[^0-9]*$' AND
            date_of_birth <= CURRENT_DATE
            )
);

CREATE TABLE job_title
(
    _id   SERIAL PRIMARY KEY,
    _name VARCHAR(60) NOT NULL UNIQUE,
    CONSTRAINT job_title_data_valid
        CHECK (_name ~ '^[^0-9]*$'
            )
);

CREATE TABLE job_history
(
    _id          SERIAL PRIMARY KEY,
    employee_id  INTEGER NOT NULL,
    job_title_id INTEGER NOT NULL,
    start_date   DATE    NOT NULL DEFAULT CURRENT_DATE,
    end_date     DATE    NULL,
    CONSTRAINT job_history_data_valid
        CHECK (end_date >= start_date)
);

CREATE TABLE project
(
    _id         SERIAL PRIMARY KEY,
    manager_id  INTEGER      NOT NULL,
    _name       VARCHAR(100) NOT NULL,
    short_name  VARCHAR(30)  NOT NULL,
    description TEXT         NOT NULL,
    start_date  DATE         NOT NULL DEFAULT CURRENT_DATE
);

CREATE TABLE task_type
(
    _id   SERIAL PRIMARY KEY,
    _name VARCHAR(30) NOT NULL UNIQUE
);

CREATE TABLE task
(
    _id              SERIAL PRIMARY KEY,
    project_id       INTEGER     NOT NULL,
    _name            VARCHAR(50) NOT NULL,
    description      TEXT        NOT NULL,
    start_date       DATE        NOT NULL DEFAULT CURRENT_DATE,
    planned_end_date DATE        NOT NULL,
    actual_end_date  DATE        NULL,
    type_id          INTEGER     NOT NULL,
    CONSTRAINT task_data_valid
        CHECK (
            planned_end_date >= start_date AND
            actual_end_date >= start_date
            )
);

CREATE TABLE task_participation
(
    _id                      SERIAL PRIMARY KEY,
    task_id                  INTEGER NOT NULL,
    employee_id              INTEGER NOT NULL,
    participation_percentage INTEGER NOT NULL,
    is_task_manager          BOOLEAN NOT NULL,
    CONSTRAINT task_participation_data_valid
        CHECK (participation_percentage BETWEEN 1 AND 100)
);

-- Внешние ключи.
ALTER TABLE employee
    ADD CONSTRAINT fk_employee_project_id
        FOREIGN KEY (project_id)
            REFERENCES project (_id);

ALTER TABLE job_history
    ADD CONSTRAINT fk_job_history_employee_id
        FOREIGN KEY (employee_id)
            REFERENCES employee (_id);

ALTER TABLE job_history
    ADD CONSTRAINT fk_job_history_job_title_id
        FOREIGN KEY (job_title_id)
            REFERENCES job_title (_id);

ALTER TABLE project
    ADD CONSTRAINT fk_project_manager_id
        FOREIGN KEY (manager_id)
            REFERENCES employee (_id);

ALTER TABLE task
    ADD CONSTRAINT fk_task_project_id
        FOREIGN KEY (project_id)
            REFERENCES project (_id);

ALTER TABLE task
    ADD CONSTRAINT fk_task_type_id
        FOREIGN KEY (type_id)
            REFERENCES task_type (_id);

ALTER TABLE task_participation
    ADD CONSTRAINT fk_task_participation_task_id
        FOREIGN KEY (task_id)
            REFERENCES task_type (_id);

ALTER TABLE task_participation
    ADD CONSTRAINT fk_task_participation_employee_id
        FOREIGN KEY (employee_id)
            REFERENCES employee (_id);

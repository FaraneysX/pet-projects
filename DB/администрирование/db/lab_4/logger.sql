-- Сбросить все параметры.
ALTER SYSTEM RESET ALL;

-- Перезагрузить конфигурацию.
SELECT pg_reload_conf();

-- Посмотреть настройки.
SELECT name, setting
FROM pg_settings
WHERE name IN ('lc_messages',
               'logging_collector',
               'log_statement',
               'log_directory');

-- Установить локализацию на английский.
ALTER SYSTEM SET lc_messages = 'en_US.UTF-8';

-- Включить запись логов.
ALTER SYSTEM SET logging_collector = on;

-- Логировать команды, изменяющие данные.
ALTER SYSTEM SET log_statement = 'mod';

SELECT pg_reload_conf();

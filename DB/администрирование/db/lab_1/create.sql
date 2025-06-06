DROP TABLE IF EXISTS client CASCADE;
DROP TABLE IF EXISTS booking CASCADE;
DROP TABLE IF EXISTS booking_invoice CASCADE;
DROP TABLE IF EXISTS penalty CASCADE;
DROP TABLE IF EXISTS penalty_type CASCADE;
DROP TABLE IF EXISTS service CASCADE;
DROP TABLE IF EXISTS service_invoice CASCADE;
DROP TABLE IF EXISTS room_view CASCADE;
DROP TABLE IF EXISTS tariff CASCADE;
DROP TABLE IF EXISTS room_type CASCADE;
DROP TABLE IF EXISTS status CASCADE;
DROP TABLE IF EXISTS owner CASCADE;
DROP TABLE IF EXISTS hotel CASCADE;
DROP TABLE IF EXISTS room CASCADE;
DROP TABLE IF EXISTS booking_log CASCADE;
DROP TABLE IF EXISTS job_type CASCADE;
DROP TABLE IF EXISTS employee CASCADE;
DROP TABLE IF EXISTS assignment CASCADE;

CREATE TABLE client (
	id					SERIAL		PRIMARY KEY,
	name				CHAR(20)	NOT NULL,
	surname				CHAR(40)	NOT NULL,
	patronymic			CHAR(30)	NULL,
	passport_series		INTEGER		NOT NULL,
	passport_number		INTEGER		NOT NULL
);

CREATE TABLE booking (
	id					SERIAL		PRIMARY KEY,
	checkin				DATE		NOT NULL,
	departure			DATE		NOT NULL,
	client_id			INTEGER		NOT NULL REFERENCES client(id) ON DELETE CASCADE
);

CREATE TABLE booking_invoice (
	id					SERIAL		PRIMARY KEY,
	booking_id			INTEGER		NOT NULL REFERENCES booking(id) ON DELETE CASCADE,
	price				REAL		NOT NULL
);

CREATE TABLE penalty_type (
	id					SERIAL		PRIMARY KEY,
	name				CHAR(50)	NOT NULL,
	price				INTEGER		NOT NULL
);

CREATE TABLE penalty (
	id					SERIAL		PRIMARY KEY,
	penalty_type_id		INTEGER		NOT NULL REFERENCES penalty_type(id) ON DELETE CASCADE,
	booking_id			INTEGER		NOT NULL REFERENCES booking(id) ON DELETE CASCADE,
	penalty_date		DATE		NOT NULL
);

CREATE TABLE service (
	id					SERIAL		PRIMARY KEY,
	description			CHAR(100)	NOT NULL
);

CREATE TABLE service_invoice (
	id					SERIAL		PRIMARY KEY,
	service_id			INTEGER		NOT NULL REFERENCES service(id) ON DELETE CASCADE,
	booking_id			INTEGER		NOT NULL REFERENCES booking(id) ON DELETE CASCADE,
	quantity			INTEGER		NOT NULL,
	price				REAL		NOT NULL
);

CREATE TABLE room_view (
	id					SERIAL		PRIMARY KEY,
	name				CHAR(60)	NOT NULL
);

CREATE TABLE tariff (
	id					SERIAL		PRIMARY KEY,
	price				INTEGER		NOT NULL,
	description			CHAR(100)	NOT NULL,
	start_date			DATE		NOT NULL
);

CREATE TABLE room_type (
	id					SERIAL		PRIMARY KEY,
	room_view_id		INTEGER		NOT NULL REFERENCES room_view(id) ON DELETE CASCADE,
	tariff_id			INTEGER		NOT NULL REFERENCES tariff(id) ON DELETE CASCADE,
	name				CHAR(50)	NOT NULL,
	capacity			INTEGER		NOT NULL
);

CREATE TABLE booking_log (
	id					SERIAL		PRIMARY KEY,
	room_type_id		INTEGER		NOT NULL REFERENCES room_type(id) ON DELETE CASCADE,
	booking_id			INTEGER		NOT NULL REFERENCES booking(id) ON DELETE CASCADE
);

CREATE TABLE status (
	id					SERIAL		PRIMARY KEY,
	name				CHAR(20)	NOT NULL
);

CREATE TABLE owner (
	id					SERIAL		PRIMARY KEY,
	name				CHAR(20)	NOT NULL,
	surname				CHAR(40)	NOT NULL,
	patronymic			CHAR(40)	NULL
);

CREATE TABLE hotel (
	id					SERIAL		PRIMARY KEY,
	owner_id			INTEGER		NOT NULL REFERENCES owner(id) ON DELETE CASCADE,
	name				CHAR(20)	NOT NULL,
	address				CHAR(30)	NOT NULL,
	email				CHAR(40)	NOT NULL,
	phone				CHAR(11)	NOT NULL,
	opening				INTEGER		NULL,
	area				INTEGER		NULL
);

CREATE TABLE room (
	id					SERIAL		PRIMARY KEY,
	status_id			INTEGER		NOT NULL REFERENCES status(id) ON DELETE CASCADE,
	room_type_id		INTEGER		NOT NULL REFERENCES room_type(id) ON DELETE CASCADE,
	hotel_id			INTEGER		NOT NULL REFERENCES hotel(id) ON DELETE CASCADE,
	floor				INTEGER		NOT NULL,
	number				INTEGER		NOT NULL
);

CREATE TABLE job_type (
	id					SERIAL		PRIMARY KEY,
	name				CHAR(20)	NOT NULL,
	salary				REAL		NOT NULL
);

CREATE TABLE employee (
	id					SERIAL		PRIMARY KEY,
	name				CHAR(20)	NOT NULL,
	surname				CHAR(40)	NOT NULL,
	patronymic			CHAR(30)	NULL,
	birth				DATE		NOT NULL,
	job_type_id			INTEGER		NOT NULL REFERENCES job_type(id) ON DELETE CASCADE,
	passport_series		INTEGER		NOT NULL,
	passport_number		INTEGER		NOT NULL
);

CREATE TABLE assignment (
	id					SERIAL		PRIMARY KEY,
	employee_id			INTEGER		NOT NULL REFERENCES employee(id) ON DELETE CASCADE,
	room_id				INTEGER		NOT NULL REFERENCES room(id) ON DELETE CASCADE
);

CREATE DATABASE dashboard
    WITH 
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'German_Germany.1252'
    LC_CTYPE = 'German_Germany.1252'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1;

-- DROP SEQUENCE public.hibernate_sequence;

CREATE SEQUENCE public.hibernate_sequence
    INCREMENT 1
    START 1
    MINVALUE 1
    MAXVALUE 9223372036854775807
    CACHE 1;

ALTER SEQUENCE public.hibernate_sequence
    OWNER TO postgres;

-- TABLES    

CREATE TABLE depot
    (
        id BIGINT DEFAULT nextval('hibernate_sequence'::regclass) NOT NULL,
        version BIGINT NOT NULL,
		
        code CHARACTER VARYING(255) not null,
        name CHARACTER VARYING(255) not null,
		
        PRIMARY KEY (id)
    );

CREATE TABLE employee
    (
        id BIGINT DEFAULT nextval('hibernate_sequence'::regclass) NOT NULL,
        version BIGINT NOT NULL,
		
        code CHARACTER VARYING(10),
        firstname CHARACTER VARYING(255),
        lastname CHARACTER VARYING(255),
		isreadysubstitute BOOLEAN DEFAULT false NOT NULL,
		depot_id BIGINT NULL,

		PRIMARY KEY (id),
        CONSTRAINT fk_Employee_Depot FOREIGN KEY (depot_id) REFERENCES depot (id)
    );

CREATE TABLE tour
    (
        id BIGINT DEFAULT nextval('hibernate_sequence'::regclass) NOT NULL,
        version BIGINT NOT NULL,
		
        code CHARACTER VARYING(10),
		depot_id BIGINT NULL,

		PRIMARY KEY (id),
        CONSTRAINT fk_Tour_Depot FOREIGN KEY (depot_id) REFERENCES depot (id)
    );

CREATE TABLE fach
    (
        id BIGINT DEFAULT nextval('hibernate_sequence'::regclass) NOT NULL,
        version BIGINT NOT NULL,
		
        code CHARACTER VARYING(10),
        name CHARACTER VARYING(255),
		tour_id BIGINT NULL,

		PRIMARY KEY (id),
        CONSTRAINT fk_Fach_Tour FOREIGN KEY (tour_id) REFERENCES tour (id)
    );

CREATE TABLE tourassignment
    (
        id BIGINT DEFAULT nextval('hibernate_sequence'::regclass) NOT NULL,
        version BIGINT NOT NULL,
		
		tour_id BIGINT NULL,
		validfrom TIMESTAMP(6) WITHOUT TIME ZONE NULL,
		employee_id BIGINT NULL,
		fact_h INT NULL,
		plan_h INT NULL,

		PRIMARY KEY (id),
        CONSTRAINT fk_tourassignment_Tour FOREIGN KEY (tour_id) REFERENCES tour (id),
        CONSTRAINT fk_tourassignment_Employee FOREIGN KEY (employee_id) REFERENCES employee (id)
    );

CREATE TABLE tourfachassignment
    (
        id BIGINT DEFAULT nextval('hibernate_sequence'::regclass) NOT NULL,
        version BIGINT NOT NULL,
		
		tourassignment_id BIGINT NOT NULL,
		fach_id BIGINT NOT NULL,
		employee_id BIGINT NULL,

		PRIMARY KEY (id),
        CONSTRAINT fk_tourfachassignment_tourassignment FOREIGN KEY (tourassignment_id) REFERENCES tourassignment (id),
        CONSTRAINT fk_tourfachassignment_Fach FOREIGN KEY (fach_id) REFERENCES fach (id),
        CONSTRAINT fk_tourfachassignment_Employee FOREIGN KEY (employee_id) REFERENCES employee (id)
    );







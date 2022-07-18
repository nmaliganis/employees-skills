create table if not exists skills
(
    id           uuid                    not null
        constraint skills_pk
            primary key,
    name         varchar(128)            not null,
    created_date timestamp default now() not null,
    description  text                    not null,
    active       boolean   default true  not null
);

alter table skills
    owner to postdbaegis;

create unique index if not exists skills_id_uindex
    on skills (id);

create unique index if not exists skills_name_uindex
    on skills (name);

create table if not exists employees
(
    id           uuid                    not null
        constraint employees_pk
            primary key,
    firstname    varchar(128)            not null,
    lastname     varchar(128)            not null,
    created_date timestamp               not null,
    email        varchar(128)            not null,
    active       boolean   default true  not null,
    hired_date   timestamp default now() not null
);

alter table employees
    owner to postdbaegis;

create unique index if not exists employees_email_uindex
    on employees (email);

create unique index if not exists employees_id_uindex
    on employees (id);

create table if not exists employeesskills
(
    id           uuid                    not null
        constraint employees_skills_pk
            primary key,
    employee_id  uuid                    not null
        constraint employees_skills_employees_id_fk
            references employees
            on update cascade on delete cascade,
    skill_id     uuid                    not null
        constraint employees_skills_skills_id_fk
            references skills
            on update cascade on delete cascade,
    created_date timestamp default now() not null,
    active       boolean   default true  not null
);

alter table employeesskills
    owner to postdbaegis;

create unique index if not exists employees_skills_id_uindex
    on employeesskills (id);


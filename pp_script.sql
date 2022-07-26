create database Lotus;
use Lotus;

select * from Todo_list
call Todo_list_Insert('ПП','2022-06-21','все документы',false,1) #join #Employee on Employee_ID = ID_Employee where Login='gord'
create table Post
(
ID_Post int not null auto_increment primary key,
Post_name varchar(40) not null unique
);

create table Employee
(
ID_Employee int not null auto_increment primary key,
Employee_Surname varchar(25) not null,
Employee_name varchar(25) not null,
Employee_patronymic varchar(25) null default '-',
Employee_email varchar(50) not null unique  check (Employee_email like '%@%.%'),
Login varchar(20) not null unique,
Passwordd varchar(20) not null,
Post_ID int not null,
foreign key (Post_ID) references Post (ID_Post)
);

DELIMITER //
create or replace procedure Employee_Update(in p_ID_Employee int, in p_Employee_Surname varchar(25), in p_Employee_name varchar(25), in p_Employee_patronymic varchar(25), in p_Employee_email varchar(50), in p_Login varchar(20), in p_Passwordd varchar(20), in p_Post_ID int)
begin
DECLARE have_record int;
select count(*) into have_record from Employee where Employee_Surname = p_Employee_Surname and Employee_name = p_Employee_name and Employee_patronymic = p_Employee_patronymic and Employee_email = p_Employee_email and Login = p_Login and Passwordd = p_Passwordd and Post_ID = p_Post_ID ;
if have_record>0 then
select 'Уже существует' as "Error Message!";
else
update Employee set
Employee_Surname = p_Employee_Surname,
Employee_name = p_Employee_name,
Employee_patronymic = p_Employee_patronymic,
Employee_email = p_Employee_email,
Login = p_Login,
Passwordd = p_Passwordd,
Post_ID = p_Post_ID
where
ID_Employee=p_ID_Employee;
end if;
end;

DELIMITER //
create or replace procedure Employee_Insert(in p_Employee_Surname varchar(25), in p_Employee_name varchar(25), in p_Employee_patronymic varchar(25), in p_Employee_email varchar(50), in p_Login varchar(20), in p_Passwordd varchar(20), in p_Post_ID int)
begin
DECLARE have_record int;
select count(*) into have_record from Employee where Employee_Surname = p_Employee_Surname and Employee_name = p_Employee_name and Employee_patronymic = p_Employee_patronymic and Employee_email = p_Employee_email and Login = p_Login and Passwordd = p_Passwordd and Post_ID = p_Post_ID ;
if have_record>0 then
select 'Уже существует' as "Error Message!";
else
insert into Employee(Employee_Surname,Employee_name,Employee_patronymic,Employee_email,Login,Passwordd,Post_ID)
values (p_Employee_Surname,p_Employee_name,p_Employee_patronymic,p_Employee_email,p_Login,p_Passwordd,p_Post_ID);
end if;
end;

DELIMITER //
create or replace procedure Employee_Delete(in p_ID_Employee int)
begin
delete from Employee
where
ID_Employee = p_ID_Employee;
end;

create table Task
(
ID_Task int not null auto_increment primary key,
Task_name varchar(40) not null unique,
Task_deadline datetime null,
Task_description varchar(255) null default '-',
Task_status boolean null default (false)
);

DELIMITER //
create or replace procedure Task_Update(in p_ID_Task int, in p_Task_name varchar(40), in p_Task_deadline datetime, in p_Task_description varchar(255), in p_Task_status boolean)
begin
DECLARE have_record int;
select count(*) into have_record from Task where Task_name = p_Task_name and Task_deadline = p_Task_deadline and Task_description = p_Task_description and Task_status = p_Task_status ;
if have_record>0 then
select 'Уже существует' as "Error Message!";
else
update Task set
Task_name = p_Task_name,
Task_deadline = p_Task_deadline,
Task_description = p_Task_description,
Task_status = p_Task_status
where
ID_Task=p_ID_Task;
end if;
end;

DELIMITER //
create or replace procedure Task_Insert(in p_Task_name varchar(40), in p_Task_deadline datetime, in p_Task_description varchar(255), in p_Task_status boolean)
begin
DECLARE have_record int;
select count(*) into have_record from Task where Task_name = p_Task_name and Task_deadline = p_Task_deadline and Task_description = p_Task_description and Task_status = p_Task_status ;
if have_record>0 then
select 'Уже существует' as "Error Message!";
else
insert into Task(Task_name,Task_deadline,Task_description,Task_status)
values (p_Task_name,p_Task_deadline,p_Task_description,p_Task_status);
end if;
end;

DELIMITER //
create or replace procedure Task_Delete(in p_ID_Task int)
begin
delete from Task
where
ID_Task = p_ID_Task;
end;

create table Todo_list
(
ID_Todo_list int not null auto_increment primary key,
Case_name varchar(40) not null unique,
Case_deadline datetime null,
Case_description varchar(255) null default '-',
Case_status boolean null default(false),
Employee_ID int not null,
foreign key (Employee_ID) references Employee (ID_Employee)
);

DELIMITER //
create or replace procedure Todo_list_Update(in p_ID_Todo_list int, in p_Case_name varchar(40), in p_Case_deadline datetime, in p_Case_description varchar(255), in p_Case_status boolean, in p_Employee_ID int)
begin
DECLARE have_record int;
select count(*) into have_record from Todo_list where Case_name = p_Case_name and Case_deadline = p_Case_deadline and Case_description = p_Case_description and Case_status = p_Case_status and Employee_ID = p_Employee_ID ;
if have_record>0 then
select 'Уже существует' as "Error Message!";
else
update Todo_list set
Case_name = p_Case_name,
Case_deadline = p_Case_deadline,
Case_description = p_Case_description,
Case_status = p_Case_status,
Employee_ID = p_Employee_ID
where
ID_Todo_list=p_ID_Todo_list;
end if;
end;

DELIMITER //
create or replace procedure Todo_list_Insert(in p_Case_name varchar(40), in p_Case_deadline datetime, in p_Case_description varchar(255), in p_Case_status boolean, in p_Employee_ID int)
begin
DECLARE have_record int;
select count(*) into have_record from Todo_list where Case_name = p_Case_name and Case_deadline = p_Case_deadline and Case_description = p_Case_description and Case_status = p_Case_status and Employee_ID = p_Employee_ID ;
if have_record>0 then
select 'Уже существует' as "Error Message!";
else
insert into Todo_list(Case_name,Case_deadline,Case_description,Case_status,Employee_ID)
values (p_Case_name,p_Case_deadline,p_Case_description,p_Case_status,p_Employee_ID);
end if;
end;

DELIMITER //
create or replace procedure Todo_list_Delete(in p_ID_Todo_list int)
begin
delete from Todo_list
where
ID_Todo_list = p_ID_Todo_list;
end;

create table Project
(
ID_Project int not null auto_increment primary key,
Project_name varchar(40) not null unique,
Project_status boolean null default(false)
);
select * from Employee_projects join Project on Project_ID = ID_Project join Employee where Employee_ID = ID_Employee and Login = 'aks'

select Login from Employee_projects where ID_Employee_projects = 1 join Employee where Employee_ID = ID_Employee 
DELIMITER //
create or replace procedure Project_Update(in p_ID_Project int, in p_Project_name varchar(40), in p_Project_status boolean)
begin
DECLARE have_record int;
select count(*) into have_record from Project where Project_name = p_Project_name and Project_status = p_Project_status ;
if have_record>0 then
select 'Уже существует' as "Error Message!";
else
update Project set
Project_name = p_Project_name,
Project_status = p_Project_status
where
ID_Project=p_ID_Project;
end if;
end;

DELIMITER //
create or replace procedure Project_Insert(in p_Project_name varchar(40), in p_Project_status boolean)
begin
DECLARE have_record int;
select count(*) into have_record from Project where Project_name = p_Project_name and Project_status = p_Project_status ;
if have_record>0 then
select 'Уже существует' as "Error Message!";
else
insert into Project(Project_name,Project_status)
values (p_Project_name,p_Project_status);
end if;
end;

DELIMITER //
create or replace procedure Project_Delete(in p_ID_Project int)
begin
delete from Project
where
ID_Project = p_ID_Project;
end;

create table Employee_projects
(
ID_Employee_projects int not null auto_increment primary key,
Employee_ID int not null,
foreign key (Employee_ID) references Employee (ID_Employee),
Project_ID int not null,
foreign key (Project_ID) references Project (ID_Project)
);
select * from Project
DELIMITER //
create or replace procedure Employee_projects_Update(in p_ID_Employee_projects int, in p_Employee_ID int, in p_Project_ID int)
begin
DECLARE have_record int;
select count(*) into have_record from Employee_projects where Employee_ID = p_Employee_ID and Project_ID = p_Project_ID ;
if have_record>0 then
select 'Уже существует' as "Error Message!";
else
update Employee_projects set
Employee_ID = p_Employee_ID,
Project_ID = p_Project_ID
where
ID_Employee_projects=p_ID_Employee_projects;
end if;
end;

DELIMITER //
create or replace procedure Employee_projects_Insert(in p_Employee_ID int, in p_Project_ID int)
begin
DECLARE have_record int;
select count(*) into have_record from Employee_projects where Employee_ID = p_Employee_ID and Project_ID = p_Project_ID ;
if have_record>0 then
select 'Уже существует' as "Error Message!";
else
insert into Employee_projects(Employee_ID,Project_ID)
values (p_Employee_ID,p_Project_ID);
end if;
end;

DELIMITER //
create or replace procedure Employee_projects_Delete(in p_ID_Employee_projects int)
begin
delete from Employee_projects
where
ID_Employee_projects = p_ID_Employee_projects;
end;

create table Project_tasks
(
ID_Project_tasks int not null auto_increment primary key,
Project_ID int not null,
foreign key (Project_ID) references Project (ID_Project),
Task_ID int not null,
foreign key (Task_ID) references Task (ID_Task)
);

DELIMITER //
create or replace procedure Project_tasks_Update(in p_ID_Project_tasks int, in p_Project_ID int, in p_Task_ID int)
begin
DECLARE have_record int;
select count(*) into have_record from Project_tasks where Project_ID = p_Project_ID and Task_ID = p_Task_ID ;
if have_record>0 then
select 'Уже существует' as "Error Message!";
else
update Project_tasks set
Project_ID = p_Project_ID,
Task_ID = p_Task_ID
where
ID_Project_tasks=p_ID_Project_tasks;
end if;
end;

DELIMITER //
create or replace procedure Project_tasks_Insert(in p_Project_ID int, in p_Task_ID int)
begin
DECLARE have_record int;
select count(*) into have_record from Project_tasks where Project_ID = p_Project_ID and Task_ID = p_Task_ID ;
if have_record>0 then
select 'Уже существует' as "Error Message!";
else
insert into Project_tasks(Project_ID,Task_ID)
values (p_Project_ID,p_Task_ID);
end if;
end;

DELIMITER //
create or replace procedure Project_tasks_Delete(in p_ID_Project_tasks int)
begin
delete from Project_tasks
where
ID_Project_tasks = p_ID_Project_tasks;
end;

create table Note
(
ID_Note int not null auto_increment primary key,
Note_name varchar(40) not null unique,
Note_description varchar(255) null,
Employee_ID int not null,
foreign key (Employee_ID) references Employee (ID_Employee)
);

DELIMITER //
create or replace procedure Note_Update(in p_ID_Note int, in p_Note_name varchar(40), in p_Note_description varchar(255), in p_Employee_ID int)
begin
DECLARE have_record int;
select count(*) into have_record from Note where Note_name = p_Note_name and Note_description = p_Note_description and Employee_ID = p_Employee_ID ;
if have_record>0 then
select 'Уже существует' as "Error Message!";
else
update Note set
Note_name = p_Note_name,
Note_description = p_Note_description,
Employee_ID = p_Employee_ID
where
ID_Note=p_ID_Note;
end if;
end;

DELIMITER //
create or replace procedure Note_Insert(in p_Note_name varchar(40), in p_Note_description varchar(255), in p_Employee_ID int)
begin
DECLARE have_record int;
select count(*) into have_record from Note where Note_name = p_Note_name and Note_description = p_Note_description and Employee_ID = p_Employee_ID ;
if have_record>0 then
select 'Уже существует' as "Error Message!";
else
insert into Note(Note_name,Note_description,Employee_ID)
values (p_Note_name,p_Note_description,p_Employee_ID);
end if;
end;

DELIMITER //
create or replace procedure Note_Delete(in p_ID_Note int)
begin
delete from Note
where
ID_Note = p_ID_Note;
end;



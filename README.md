https://github.com/Tachka22/TestTask_DEPO-Computers/assets/100880411/81cc4f63-0e8f-4e2b-820a-cc2006c4d6d2

Задача:
Создать приложение на языке C# с БД MSSQL.

Разработку вести с помощью системы контроля версий git

Приложение представляет из себя базу сотрудников организаций.

Должно включать возможность:


Заведения новой организации из интерфейса (Наименование, ИНН, юр. адрес, факт. адрес)
Заведения сотрудников из интерфейса (Фамилия, Имя, Отчество, дата рождения, паспорт серия, паспорт номер)
Иметь возможность импорта и экспорта из\в csv-файл
Результаты предоставить ссылкой на открытый репозиторий github (либо аналоги).

Репозиторий должен содержать:

Проект, выполненный в visual studio
Скрипт создания и первоначального заполнения базы данных
Данные в csv для загрузки
Пример выгрузки csv


Реализация:
1. Создана БД с таблицами "Сотрудник" и "Компания" (связь один ко многим)
2. Database first подход создания модели
3. Реализован вывод сотрудников и компаний ввиде таблицы
4. Реализовано добавления нового сотрудника/компании
5. Импорт таблиц в БД из CSV
6. Экспорт таблиц в CSV

   
sql:

CREATE DATABASE DEPO_Computers;


USE DEPO_Computers;


CREATE TABLE Company (
    id_company INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(255) NOT NULL,
    inn NVARCHAR(20) NOT NULL UNIQUE,
    legal_address NVARCHAR(255) NOT NULL,
    actual_address NVARCHAR(255) NOT NULL
);

CREATE TABLE Employee (
    id_employee INT IDENTITY(1,1) PRIMARY KEY,
    last_name NVARCHAR(255) NOT NULL,
    first_name NVARCHAR(255) NOT NULL,
    middle_name NVARCHAR(255),
    date_of_birth DATE NOT NULL,
    passport_series NVARCHAR(4) NOT NULL,
    passport_number NVARCHAR(6) NOT NULL,
    company_id INT NOT NULL,
    FOREIGN KEY (company_id) REFERENCES Company(id_company)
);

INSERT INTO Company (name, inn, legal_address, actual_address)
VALUES
    ('ООО "Депо Техники"', '7712345678', 'г. Москва, ул. Ленина, д. 1', 'г. Москва, ул. Тверская, д. 2'),
    ('ООО "Связь и Ко"', '5543217890', 'г. Санкт-Петербург, Невский пр., д. 3', 'г. Санкт-Петербург, ул. Садовая, д. 4'),
    ('АО "ЭнергоСистемы"', '123456789012', 'г. Екатеринбург, ул. Ленина, д. 5', 'г. Екатеринбург, ул. Малышева, д. 6');

INSERT INTO Employee (last_name, first_name, middle_name, date_of_birth, passport_series, passport_number, company_id)
VALUES
    ('Иванов', 'Иван', 'Иванович', '1980-01-01', '1237', '123456', 1),
    ('Петров', 'Петр', 'Петрович', '1985-02-02', '4567', '234567', 1),
    ('Сидоров', 'Сидор', 'Сидорович', '1990-03-03', '4127', '345678', 1),
    ('Кузнецов', 'Кузьма', 'Кузьмич', '1995-04-04', '4522', '456789', 2),
    ('Попова', 'Полина', 'Поповна', '2000-05-05', '4867', '567890', 2),
    ('Васильева', 'Василиса', 'Васильевна', '2005-06-06', '2567', '678901', 3);

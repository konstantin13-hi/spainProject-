
select * from accomodations;

select * from locations;

SELECT a.id, a.name , AVG(r.pointsAvg) AS average_rating,a.price FROM accomodations a LEFT JOIN reviews r ON a.id = r.accomodation_id GROUP BY a.id, a.name, a.price

insert into [dbo].[reviews] ([pointsAvg], [accomodation_id]) values (4,1),(9,1),(3,2),(5,2),(8,3),(9,3),(7,4),(9,4),(4,5),(2,5),(4,6),(9,6);


insert into [dbo].[accomodations] ([name],[adress], [numberofrooms],   [area]   ,[price] ,[hasswimmingpool], [hasgym] ,[hasparking] ,  [ispetfriendly],[description], [location_id] ) 
values ('Отель 1', 'Адрес 1', 50, 200.5, 150.75, 1, 0, 1, 1, 'Описание 1',1),
('Отель 2', 'Адрес 2', 30, 150.25, 100.50, 0, 1, 1, 0, 'Описание 2',1),
('Отель 3', 'Адрес 3', 20, 100.75, 80.25, 0, 0, 1, 0, 'Описание 3',1),
('Отель 4', 'Адрес 4', 40, 180.5, 120.75, 1, 1, 1, 1, 'Описание 4',2),
('Отель 5', 'Адрес 5', 60, 250.25, 180.50, 1, 1, 0, 1, 'Описание 5',2),
('Отель 6', 'Адрес 6', 25, 120.75, 90.25, 0, 0, 1, 0, 'Описание 6',3);

insert into  [dbo].[locations] ([id] ,[name] ,[country])
values (1,'Berlin', 'Germany'),
(2,'Alicante', 'Spain'),(3,'Moskow', 'Russia');


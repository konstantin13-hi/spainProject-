delete from [dbo].[travel_notes];
delete from [dbo].[seasonal_prices];
delete from [dbo].[locations];
delete from [dbo].[categories];
delete from [dbo].[weathers];
delete from [dbo].[landmarks];
delete from [dbo].[history];
delete from [dbo].[accomodations];
delete from [dbo].[bookings];
delete from [dbo].[reviews];
delete from [dbo].[favorites];
delete from [dbo].[users];



SET IDENTITY_INSERT [dbo].[users] ON;
insert into [dbo].[users] ([id],[email],[password],[name],[isAdmin]) values 
(1,'admin@test.com', '$2a$12$3rECM2HX6RabeVSzLNMsye2xidGQ4PG9sHduvFjnWOEDIHoelXope', 'admin', 1),
(2,'user@test.com', '$2a$12$5zW567TzZTEFJXbl65JLyurLbnyOMVycza.jpFcuB80PVNcAah3eW','testuser', 0);
SET IDENTITY_INSERT [dbo].[users] OFF;

SET IDENTITY_INSERT [dbo].[seasonal_prices] ON;
insert into [dbo].[seasonal_prices] ([id],[name],   [startDate] ,[endDate] ,[multiplierPrize]) values
(0,'none','2023-03-18 00:00:00','3000-03-18 00:00:00',1),
(1,'spring_sale','2023-03-18 00:00:00','2023-08-18 00:00:00',0.8),
(2,'winter_sale','2023-03-18 00:00:00','2023-08-18 00:00:00',0.6);
SET IDENTITY_INSERT [dbo].[seasonal_prices] OFF;

SET IDENTITY_INSERT [dbo].[locations] ON;
insert into [dbo].[locations] ([id],[name],[country],[image_path]) values
(1,'Alicante','Spain', '~/Images/Locations/alicante.jpg'),
(2,'Bergen','Norway','~/Images/Locations/bergen.jpeg'),
(3,'Malaga','Spain', '~/Images/Locations/malaga.jpg'),
(4,'Madrid','Spain', '~/Images/Locations/madrid.jpg'),
(5,'Murcia','Spain', '~/Images/Locations/murcia.jpg'),
(6,'Valencia','Spain', '~/Images/Locations/valencia.jpeg'),
(7,'Oslo','Norway', '~/Images/Locations/oslo.jpg'),
(8,'Sveio','Norway', '~/Images/Locations/sveio.jpg'),
(9,'Stockholm','Sweden', '~/Images/Locations/stockholm.jpg');
SET IDENTITY_INSERT [dbo].[locations] OFF;

SET IDENTITY_INSERT [dbo].[categories] ON;
insert into [dbo].[categories] ([id],[name]) values
(1,'Hotel'),
(2,'AirBNB'),
(3,'Cabin');
SET IDENTITY_INSERT [dbo].[categories] OFF;

SET IDENTITY_INSERT [dbo].[weathers] ON;

INSERT INTO [dbo].[weathers] ([id],[temperatureAvg],[temperatureMax],[temperatureMin],[rain],[wind],[location_id],[date]) 
VALUES (1, 25.5, 30.2, 20.8, 10.3, 5.5, 1, GETDATE()),
(2, 27.8, 32.1, 22.6, 8.7, 3.9, 2, GETDATE());
SET IDENTITY_INSERT [dbo].[weathers] OFF;

SET IDENTITY_INSERT [dbo].[landmarks] ON;
insert into [dbo].[landmarks] ([id],[name],[type],[pricerange],[location_id],[adress],[websiteLink], [image_path]) values
(1,'Santa Barbara','Mountain',0,1,'Santa Barbara Castle', 'https://castillodesantabarbara.com/en/', '~/Images/Landmarks/santabarbara.jpg'),
(3,'Nipen','Mountain',0,8,'Emberlandsnipen 271', NULL, '~/Images/Landmarks/nipen.jpeg'),
(4,'Rex Garden','Park',0,8,'5555 Førde i Hordaland', NULL, '~/Images/Landmarks/rexgarden.jpeg'),
(2,'Ulriken','Mountain',0,2,'Ulriken','https://ulriken643.no', '~/Images/Landmarks/ulriken.jpeg');
SET IDENTITY_INSERT [dbo].[landmarks] OFF;

SET IDENTITY_INSERT [dbo].[accomodations] ON;
insert into [dbo].[accomodations] ([id],[name],[adress],[numberofrooms],[area],[price],[hasswimmingpool],[hasgym],[hasparking],[ispetfriendly],[description],[image_path],[category_id],[location_id],[seasonal_price_id]) values 
(1,'Hotel 1', 'Street 1', 30, 150.25, 100.50, 0, 1, 1, 0, 'Description 1', '~/Images/Accomodations/hotel1.jpg', 1, 1, 1),
(2,'Hotel 2', 'Street 2', 30, 150.25, 100.50, 0, 1, 1, 0, 'Description 2', '~/Images/Accomodations/hotel2.jpg', 1, 2, 0),
(3,'AirBNB 3', 'Street 3', 20, 100.75, 80.25, 0, 0, 1, 0, 'Description 3', '~/Images/Accomodations/airbnb3.jpg', 2, 1, 0),
(4,'AirBNB 4', 'Street 4', 40, 180.5, 120.75, 1, 1, 1, 1, 'Description 4', '~/Images/Accomodations/airbnb4.jpeg', 2, 2, 0),
(5,'Cabin 5', 'Street 5', 60, 250.25, 180.50, 1, 1, 0, 1, 'Description 5', '~/Images/Accomodations/cabin5.jpg', 3, 2, 2),
(6,'Cabin 6', 'Street 6', 25, 120.75, 90.25, 0, 0, 1, 0, 'Description 6', '~/Images/Accomodations/cabin6.jpg', 3, 1, 0);
SET IDENTITY_INSERT [dbo].[accomodations] OFF;

SET IDENTITY_INSERT [dbo].[bookings] ON;
insert into [dbo].[bookings] ([id],[accomodation_id],[bookingprice],[checkin],[checkout]) values
(1,2,156,'2022-09-10 00:00:00','2022-09-12 00:00:00'),(2,4,120,'2022-05-15 00:00:00','2022-06-25 00:00:00'),(3,3,100,'2023-05-15 00:00:00','2023-05-20 00:00:00');
SET IDENTITY_INSERT [dbo].[bookings] OFF;



SET IDENTITY_INSERT [dbo].[history] ON;
insert into [dbo].[history] ([id],[user_id],[booking_id]) values
(1,2,1),(2,2,2),(3,2,3);
SET IDENTITY_INSERT [dbo].[history] OFF;



SET IDENTITY_INSERT [dbo].[reviews] ON;
insert into [dbo].[reviews] ([id],[accomodation_id],[user_id],[pointsAvg],[pointsOverall],[pointsArea],[pointsTidiness],[pointsServices],[reviewText]) values
(1,1,2,5,5,5,5,5,'It is ok'),
(2,2,2,8,8,8,8,8,'Very good hotel');
SET IDENTITY_INSERT [dbo].[reviews] OFF;


insert into [dbo].[favorites] ([user_id],[accomodation_id]) values
(2,2),
(2,5);


SET IDENTITY_INSERT [dbo].[travel_notes] ON;
insert into [dbo].[travel_notes] ([id],[title],[user_id],[accomodation_id],[booking_id],[noteGeneral],[noteLocation],[noteAccomodation]) values
(1,'My perfect stay',2,2,1,'it was good','yes','best hotel');
SET IDENTITY_INSERT [dbo].[travel_notes] OFF;


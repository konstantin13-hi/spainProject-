CREATE TABLE [dbo].[users] (
  [id] integer PRIMARY KEY,
  [email] nvarchar(255),
  [password] nvarchar(255),
  [nif] nvarchar(255),
  [name] nvarchar(255),
  [age] nvarchar(255),
  [isAdmin] bit,
  [created_at] datetime,
  [updated_at] datetime
)
GO

CREATE TABLE [dbo].[weathers] (
  [id] integer PRIMARY KEY,
  [temperatureAvg] integer,
  [temperatureMax] integer,
  [temperatureMin] integer,
  [rain] integer,
  [wind] integer,
  [location_id] integer,
  [date] datetime
)
GO

CREATE TABLE [dbo].[accomodations] (
  [id] integer PRIMARY KEY,
  [name] nvarchar(255),
  [adress] nvarchar(255),
  [numberofrooms] integer,
  [area] float,
  [price] decimal,
  [hasswimmingpool] bit,
  [hasgym] bit,
  [hasparking] bit,
  [ispetfriendly] bit,
  [description] nvarchar(255),
  [category_id] integer,
  [location_id] integer
)
GO

CREATE TABLE [dbo].[categories] (
  [id] integer PRIMARY KEY,
  [name] nvarchar(255)
)
GO

CREATE TABLE [dbo].[bookings] (
  [id] integer PRIMARY KEY,
  [accomodation_id] integer,
  [bookingprice] decimal,
  [checkin] datetime,
  [checkout] datetime
)
GO

CREATE TABLE [dbo].[landmarks] (
  [id] integer PRIMARY KEY,
  [name] nvarchar(255),
  [type] nvarchar(255),
  [pricerange] decimal,
  [location_id] integer,
  [adress] nvarchar(255),
  [websiteLink] nvarchar(255)
)
GO

CREATE TABLE [dbo].[reviews] (
  [id] integer PRIMARY KEY,
  [accomodation_id] integer,
  [user_id] integer,
  [pointsAvg] float,
  [pointsOverall] float,
  [pointsArea] float,
  [pointsTidiness] float,
  [pointsServices] float,
  [reviewText] nvarchar(255)
)
GO

CREATE TABLE [dbo].[seasonal_prices] (
  [id] integer PRIMARY KEY,
  [name] nvarchar(255),
  [startDate] datetime,
  [endDate] datetime,
  [multiplierPrize] float,
  [accomodation_id] integer
)
GO

CREATE TABLE [dbo].[favorites] (
  [id] integer PRIMARY KEY,
  [user_id] integer,
  [accomodation_id] integer
)
GO

CREATE TABLE [dbo].[travel_notes] (
  [id] integer PRIMARY KEY,
  [title] nvarchar(255),
  [user_id] integer,
  [accomodation_id] integer,
  [location_id] integer,
  [booking_id] integer,
  [noteLocation] nvarchar(255),
  [noteAccomodation] nvarchar(255),
  [created_at] datetime,
  [updated_at] datetime
)
GO

CREATE TABLE [dbo].[history] (
  [id] integer PRIMARY KEY,
  [user_id] integer,
  [booking_id] integer
)
GO

CREATE TABLE [dbo].[landmark_travel_note] (
  [id] integer PRIMARY KEY,
  [travel_note_id] integer,
  [landmark_id] integer,
  [note] nvarchar(255)
)
GO

CREATE TABLE [dbo].[locations] (
  [id] integer PRIMARY KEY,
  [name] nvarchar(255),
  [country] nvarchar(255)
)
GO

ALTER TABLE [dbo].[weathers] ADD FOREIGN KEY ([location_id]) REFERENCES [dbo].[locations] ([id])
GO

ALTER TABLE [dbo].[travel_notes] ADD FOREIGN KEY ([user_id]) REFERENCES [dbo].[users] ([id])
GO

ALTER TABLE [dbo].[favorites] ADD FOREIGN KEY ([user_id]) REFERENCES [dbo].[users] ([id])
GO

ALTER TABLE [dbo].[history] ADD FOREIGN KEY ([user_id]) REFERENCES [dbo].[users] ([id])
GO

ALTER TABLE [dbo].[reviews] ADD FOREIGN KEY ([user_id]) REFERENCES [dbo].[users] ([id])
GO

ALTER TABLE [dbo].[accomodations] ADD FOREIGN KEY ([id]) REFERENCES [dbo].[travel_notes] ([accomodation_id])
GO

ALTER TABLE [dbo].[bookings] ADD FOREIGN KEY ([id]) REFERENCES [dbo].[travel_notes] ([booking_id])
GO

ALTER TABLE [dbo].[landmark_travel_note] ADD FOREIGN KEY ([travel_note_id]) REFERENCES [dbo].[travel_notes] ([id])
GO

ALTER TABLE [dbo].[landmark_travel_note] ADD FOREIGN KEY ([landmark_id]) REFERENCES [dbo].[landmarks] ([id])
GO

ALTER TABLE [dbo].[reviews] ADD FOREIGN KEY ([accomodation_id]) REFERENCES [dbo].[accomodations] ([id])
GO

ALTER TABLE [dbo].[accomodations] ADD FOREIGN KEY ([category_id]) REFERENCES [dbo].[categories] ([id])
GO

ALTER TABLE [dbo].[accomodations] ADD FOREIGN KEY ([location_id]) REFERENCES [dbo].[locations] ([id])
GO

ALTER TABLE [dbo].[bookings] ADD FOREIGN KEY ([accomodation_id]) REFERENCES [dbo].[accomodations] ([id])
GO

ALTER TABLE [dbo].[landmarks] ADD FOREIGN KEY ([location_id]) REFERENCES [dbo].[locations] ([id])
GO

ALTER TABLE [dbo].[accomodations] ADD FOREIGN KEY ([id]) REFERENCES [dbo].[favorites] ([accomodation_id])
GO

ALTER TABLE [dbo].[accomodations] ADD FOREIGN KEY ([id]) REFERENCES [dbo].[seasonal_prices] ([accomodation_id])
GO

ALTER TABLE [dbo].[bookings] ADD FOREIGN KEY ([id]) REFERENCES [dbo].[history] ([booking_id])
GO

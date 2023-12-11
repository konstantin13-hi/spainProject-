CREATE TABLE [dbo].[users] (
    [id]         INT    IDENTITY (1,1)            NOT NULL,
    [email]      NVARCHAR (255) UNIQUE NULL,
    [password]   NVARCHAR (255) NULL,
    [name]       NVARCHAR (255) NULL,
    [isAdmin]    BIT            NULL,
    [created_at] DATETIME       NULL,
    [updated_at] DATETIME       NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

CREATE TABLE [dbo].[seasonal_prices] (
    [id]              INT IDENTITY (1,1)            NOT NULL,
    [name]            NVARCHAR (255) NULL,
    [startDate]       DATETIME       NULL,
    [endDate]         DATETIME       NULL,
    [multiplierPrize] FLOAT (53)     NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

CREATE TABLE [dbo].[locations] (
    [id]      INT IDENTITY (1,1)           NOT NULL,
    [name]    NVARCHAR (255) NULL,
    [country] NVARCHAR (255) NULL,
    [image_path] NVARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

CREATE TABLE [dbo].[categories] (
    [id]   INT IDENTITY (1,1)           NOT NULL,
    [name] NVARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

CREATE TABLE [dbo].[weathers] (
    [id]             INT IDENTITY (1,1)     NOT NULL,
    [temperatureAvg] INT      NULL,
    [temperatureMax] INT      NULL,
    [temperatureMin] INT      NULL,
    [rain]           INT      NULL,
    [wind]           INT      NULL,
    [location_id]    INT      NULL,
    [date]           DATETIME NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([location_id]) REFERENCES [dbo].[locations] ([id]) ON DELETE CASCADE ON UPDATE CASCADE,
);

CREATE TABLE [dbo].[landmarks] (
    [id]          INT IDENTITY (1,1)           NOT NULL,
    [name]        NVARCHAR (255) NULL,
    [type]        NVARCHAR (255) NULL,
    [pricerange]  DECIMAL (18)   NULL,
    [location_id] INT            NULL,
    [adress]      NVARCHAR (255) NULL,
    [image_path] NVARCHAR (255) NULL,
    [websiteLink] NVARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([location_id]) REFERENCES [dbo].[locations] ([id]) ON DELETE CASCADE ON UPDATE CASCADE,
);


CREATE TABLE [dbo].[accomodations] (
    [id]              INT IDENTITY (1,1)           NOT NULL,
    [name]            NVARCHAR (255) NULL,
    [adress]          NVARCHAR (255) NULL,
    [numberofrooms]   INT            NULL,
    [area]            FLOAT (53)     NULL,
    [price]           DECIMAL (18)   NULL,
    [hasswimmingpool] BIT            NULL,
    [hasgym]          BIT            NULL,
    [hasparking]      BIT            NULL,
    [ispetfriendly]   BIT            NULL,
    [description]     NVARCHAR (255) NULL,
    [image_path]      NVARCHAR (255) NULL,
    [category_id]     INT            NULL,
    [location_id]     INT            NULL,
	[seasonal_price_id] INT			 NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([category_id]) REFERENCES [dbo].[categories] ([id]) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY ([location_id]) REFERENCES [dbo].[locations] ([id]) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY ([seasonal_price_id]) REFERENCES [dbo].[seasonal_prices] ([id]) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE [dbo].[bookings] (
    [id]              INT IDENTITY (1,1)         NOT NULL,
    [accomodation_id] INT          NULL,
    [bookingprice]    DECIMAL (18) NULL,
    [checkin]         DATETIME     NULL,
    [checkout]        DATETIME     NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([accomodation_id]) REFERENCES [dbo].[accomodations] ([id]) ON DELETE CASCADE ON UPDATE CASCADE,
);

CREATE TABLE [dbo].[history] (
    [id]         INT IDENTITY (1,1) NOT NULL,
    [user_id]    INT NULL,
    [booking_id] INT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([user_id]) REFERENCES [dbo].[users] ([id]) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY ([booking_id]) REFERENCES [dbo].[bookings] ([id]) ON DELETE CASCADE ON UPDATE CASCADE 
);

CREATE TABLE [dbo].[reviews] (
    [id]              INT IDENTITY (1,1)           NOT NULL,
    [accomodation_id] INT            NULL,
    [user_id]         INT            NULL,
    [pointsAvg]       FLOAT (53)     NULL,
    [pointsOverall]   FLOAT (53)     NULL,
    [pointsArea]      FLOAT (53)     NULL,
    [pointsTidiness]  FLOAT (53)     NULL,
    [pointsServices]  FLOAT (53)     NULL,
    [reviewText]      NVARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    UNIQUE ([accomodation_id], [user_id]),
    FOREIGN KEY ([accomodation_id]) REFERENCES [dbo].[accomodations] ([id]) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY ([user_id]) REFERENCES [dbo].[users] ([id]) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE [dbo].[favorites] (
    [user_id]         INT NOT NULL,
    [accomodation_id] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([user_id],[accomodation_id] ASC),
    FOREIGN KEY ([user_id]) REFERENCES [dbo].[users] ([id]) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY ([accomodation_id]) REFERENCES [dbo].[accomodations] ([id]) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE [dbo].[travel_notes] (
    [id]               INT IDENTITY (1,1)            NOT NULL,
    [title]            NVARCHAR (255) NULL,
    [user_id]          INT            NULL,
    [accomodation_id]  INT            NULL,
    [booking_id]       INT            NULL,
    [noteGeneral]     NVARCHAR (MAX) NULL,
    [noteLocation]     NVARCHAR (MAX) NULL,
    [noteAccomodation] NVARCHAR (MAX) NULL,
    [created_at]       DATETIME       DEFAULT (GETDATE()) NOT NULL,
    [updated_at]       DATETIME       DEFAULT (GETDATE()) NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([user_id]) REFERENCES [dbo].[users] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION,
    FOREIGN KEY ([accomodation_id]) REFERENCES [dbo].[accomodations] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION,
    FOREIGN KEY ([booking_id]) REFERENCES [dbo].[bookings] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION,
);

--CREATE TABLE [dbo].[landmark_travel_note] (
--    [id]             INT IDENTITY (1,1)           NOT NULL,
--    [travel_note_id] INT            NULL,
--    [landmark_id]    INT            NULL,
--    [note]           NVARCHAR (255) NULL,
--    PRIMARY KEY CLUSTERED ([id] ASC),
--    FOREIGN KEY ([travel_note_id]) REFERENCES [dbo].[travel_notes] ([id]) ON DELETE CASCADE ON UPDATE CASCADE,
--    FOREIGN KEY ([landmark_id]) REFERENCES [dbo].[landmarks] ([id]) ON DELETE CASCADE ON UPDATE CASCADE
--);


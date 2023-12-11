CREATE TABLE [dbo].[accomodations] (
    [id]              INT            NOT NULL,
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
    [category_id]     INT            NULL,
    [location_id]     INT            NULL,
	[seasonal_price_id] INT			 NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([category_id]) REFERENCES [dbo].[categories] ([id]) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY ([location_id]) REFERENCES [dbo].[locations] ([id]) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY ([seasonal_price_id]) REFERENCES [dbo].[seasonal_prices] ([id]) ON DELETE CASCADE ON UPDATE CASCADE
);


CREATE TABLE [dbo].[landmarks] (
    [id]          INT            NOT NULL,
    [name]        NVARCHAR (255) NULL,
    [type]        NVARCHAR (255) NULL,
    [pricerange]  DECIMAL (18)   NULL,
    [location_id] INT            NULL,
    [adress]      NVARCHAR (255) NULL,
    [websiteLink] NVARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([location_id]) REFERENCES [dbo].[locations] ([id]) ON DELETE CASCADE ON UPDATE CASCADE,
);


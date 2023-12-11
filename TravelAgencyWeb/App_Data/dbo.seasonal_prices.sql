CREATE TABLE [dbo].[seasonal_prices] (
    [id]              INT            NOT NULL,
    [name]            NVARCHAR (255) NULL,
    [startDate]       DATETIME       NULL,
    [endDate]         DATETIME       NULL,
    [multiplierPrize] FLOAT (53)     NULL,
    [accomodation_id] INT            NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);


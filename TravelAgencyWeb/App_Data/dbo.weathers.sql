CREATE TABLE [dbo].[weathers] (
    [id]             INT      NOT NULL,
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


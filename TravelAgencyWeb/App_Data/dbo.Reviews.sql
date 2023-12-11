CREATE TABLE [dbo].[reviews] (
    [id]              INT            NOT NULL,
    [accomodation_id] INT            NULL,
    [user_id]         INT            NULL,
    [pointsAvg]       FLOAT (53)     NULL,
    [pointsOverall]   FLOAT (53)     NULL,
    [pointsArea]      FLOAT (53)     NULL,
    [pointsTidiness]  FLOAT (53)     NULL,
    [pointsServices]  FLOAT (53)     NULL,
    [reviewText]      NVARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([accomodation_id]) REFERENCES [dbo].[accomodations] ([id]) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY ([user_id]) REFERENCES [dbo].[users] ([id]) ON DELETE CASCADE ON UPDATE CASCADE
);


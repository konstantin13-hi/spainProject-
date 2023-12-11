CREATE TABLE [dbo].[favorites] (
    [id]              INT NOT NULL,
    [user_id]         INT NULL,
    [accomodation_id] INT NULL UNIQUE,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([user_id]) REFERENCES [dbo].[users] ([id]) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY ([accomodation_id]) REFERENCES [dbo].[accomodations] ([id]) ON DELETE CASCADE ON UPDATE CASCADE
);


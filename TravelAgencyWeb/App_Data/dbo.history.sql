CREATE TABLE [dbo].[history] (
    [id]         INT NOT NULL,
    [user_id]    INT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([user_id]) REFERENCES [dbo].[users] ([id]) ON DELETE CASCADE ON UPDATE CASCADE,
);


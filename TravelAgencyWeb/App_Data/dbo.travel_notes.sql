CREATE TABLE [dbo].[travel_notes] (
    [id]               INT            NOT NULL,
    [title]            NVARCHAR (255) NULL,
    [user_id]          INT            NULL,
    [accomodation_id]  INT            NULL UNIQUE,
    [booking_id]       INT            NULL UNIQUE,
    [noteLocation]     NVARCHAR (255) NULL,
    [noteAccomodation] NVARCHAR (255) NULL,
    [created_at]       DATETIME       NULL,
    [updated_at]       DATETIME       NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([user_id]) REFERENCES [dbo].[users] ([id]) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY ([accomodation_id]) REFERENCES [dbo].[accomodations] ([id]) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY ([booking_id]) REFERENCES [dbo].[bookings] ([id]) ON DELETE CASCADE ON UPDATE CASCADE
);


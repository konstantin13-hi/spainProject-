CREATE TABLE [dbo].[bookings] (
    [id]              INT          NOT NULL,
    [accomodation_id] INT          NULL,
    [bookingprice]    DECIMAL (18) NULL,
    [checkin]         DATETIME     NULL,
    [checkout]        DATETIME     NULL,
	[history_id]	  INT		   NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([accomodation_id]) REFERENCES [dbo].[accomodations] ([id]) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY ([history_id]) REFERENCES [dbo].[history] ([id]) ON DELETE CASCADE ON UPDATE CASCADE
);


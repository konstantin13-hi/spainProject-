CREATE TABLE [dbo].[landmark_travel_note] (
    [id]             INT            NOT NULL,
    [travel_note_id] INT            NULL,
    [landmark_id]    INT            NULL,
    [note]           NVARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([travel_note_id]) REFERENCES [dbo].[travel_notes] ([id]) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY ([landmark_id]) REFERENCES [dbo].[landmarks] ([id]) ON DELETE CASCADE ON UPDATE CASCADE
);


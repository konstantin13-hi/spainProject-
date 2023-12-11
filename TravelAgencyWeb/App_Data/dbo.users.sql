CREATE TABLE [dbo].[users] (
    [id]         INT            NOT NULL,
    [email]      NVARCHAR (255) NULL,
    [password]   NVARCHAR (255) NULL,
    [nif]        NVARCHAR (255) NULL,
    [name]       NVARCHAR (255) NULL,
    [age]        NVARCHAR (255) NULL,
    [isAdmin]    BIT            NULL,
    [created_at] DATETIME       NULL,
    [updated_at] DATETIME       NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);


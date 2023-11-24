use Exercise
GO

CREATE TABLE [dbo].[user](
    [id] [int] IDENTITY(1,1) NOT NULL,
    [username] nvarchar(50) NULL,
    [password] nvarchar(50) NULL,
    [role] nvarchar(50) NULL,
    [date] [datetime] NULL
    ) ON [PRIMARY]

GO

INSERT INTO [dbo].[user] (username, password, role) VALUES ('test', 'test', 'DEV')
CREATE TABLE [dbo].[User]
(
	[userId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [firstName] NCHAR(16) NULL, 
    [lastName] NCHAR(16) NULL, 
    [email] NCHAR(64) NULL, 
    [username] NCHAR(16) NULL, 
    [password] NCHAR(16) NULL
)

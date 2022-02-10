CREATE TABLE [dbo].[User]
(
	[userId] INT NOT NULL PRIMARY KEY, 
    [firstName] NCHAR(16) NOT NULL, 
    [lastName] NCHAR(16) NOT NULL, 
    [email] NCHAR(64) NOT NULL, 
    [username] NCHAR(16) NOT NULL, 
    [password] NCHAR(16) NOT NULL
)

CREATE TABLE [dbo].[Trip]
(
	[tripId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [userId] INT NULL, 
    [tripName] CHAR(128) NULL, 
    [startDate] DATETIME NULL, 
    [endDate] DATETIME NULL 
)

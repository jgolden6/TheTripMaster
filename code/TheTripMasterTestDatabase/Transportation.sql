CREATE TABLE [dbo].[Transportation]
(
	[transportationId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [tripId] INT NULL, 
    [transportationType] CHAR(128) NULL, 
    [startDate] DATETIME NULL, 
    [endDate] DATETIME NULL
)

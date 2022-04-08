CREATE TABLE [dbo].[Waypoint]
(
	[waypointId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [tripId] INT NULL, 
    [waypointName] CHAR(128) NULL,
    [streetAddress] CHAR(128) NULL, 
    [city] CHAR(128) NULL, 
    [state] CHAR(128) NULL,
    [zipCode] CHAR(5) NULL,
    [startDate] DATETIME NULL, 
    [endDate] DATETIME NULL
)

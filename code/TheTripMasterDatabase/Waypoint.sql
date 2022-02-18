CREATE TABLE [dbo].[Waypoint]
(
	[waypointId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [tripId] INT NULL, 
    [waypointName] CHAR(128) NULL, 
    [startDate] DATETIME NULL, 
    [endDate] DATETIME NULL
)

CREATE TABLE [dbo].[Waypoint]
(
	[waypointId] INT NOT NULL PRIMARY KEY, 
    [tripId] INT NULL, 
    [waypointName] CHAR(128) NULL, 
    [startDate] DATETIME NULL, 
    [endDate] DATETIME NULL
)

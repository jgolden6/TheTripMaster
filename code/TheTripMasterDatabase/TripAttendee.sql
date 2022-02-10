CREATE TABLE [dbo].[TripAttendee]
(
	[tripId] INT NOT NULL PRIMARY KEY, 
    [userId] INT NOT NULL,
	CONSTRAINT [FK_dbo.TripAttendee_dbo.Trip_tripId] FOREIGN KEY ([tripId])
		REFERENCES [dbo].[Trip] ([tripId]) ON DELETE CASCADE,
	CONSTRAINT [FK_dbo.TripAttendee_dbo.User_userId] FOREIGN KEY (userId)
		REFERENCES [dbo].[User] ([userId]) ON DELETE CASCADE
)

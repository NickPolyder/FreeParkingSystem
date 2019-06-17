CREATE TABLE [dbo].[Order]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ParkingSpotId] INT NOT NULL, 
    [UserId] INT NOT NULL, 
    [OrderDate] DATETIME NOT NULL, 
    CONSTRAINT [FK_Order_ToParkingSpot] FOREIGN KEY ([ParkingSpotId]) REFERENCES [ParkingSpot]([Id]),
	CONSTRAINT [FK_Order_ToUser] FOREIGN KEY ([UserId]) REFERENCES [User]([Id])
)

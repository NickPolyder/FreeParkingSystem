CREATE TABLE [dbo].[Order]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[ParkingSpotId] INT NOT NULL, 
	[TenantId] INT NOT NULL, 
	[LeaseStartDate] DATETIME NOT NULL, 
	[LeaseEndDate] DATETIME NULL, 
	[IsCancelled] BIT NOT NULL DEFAULT 0, 
	CONSTRAINT [FK_Order_ToParkingSpot] FOREIGN KEY ([ParkingSpotId]) REFERENCES [ParkingSpot]([Id]),
	CONSTRAINT [FK_Order_ToUser] FOREIGN KEY ([TenantId]) REFERENCES [User]([Id])
)

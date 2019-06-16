CREATE TABLE [dbo].[ParkingSpot]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 	
    [ParkingSiteId] INT NOT NULL, 	
    [ParkingSpotTypeId] INT NOT NULL, 
    [Position] INT NOT NULL, 
    [Level] INT NOT NULL, 
    CONSTRAINT [FK_ParkingSpot_ToParkingSite] FOREIGN KEY ([ParkingSiteId]) REFERENCES [ParkingSite]([Id]), 
    CONSTRAINT [AK_ParkingSpot_UniquePosition] UNIQUE ([ParkingSiteId], [Level], [Position]), 
    CONSTRAINT [FK_ParkingSpot_ToParkingSpotType] FOREIGN KEY ([ParkingSpotTypeId]) REFERENCES [ParkingSpotType]([Id])
)

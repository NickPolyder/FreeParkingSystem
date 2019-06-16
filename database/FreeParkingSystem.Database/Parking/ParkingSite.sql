CREATE TABLE [dbo].[ParkingSite]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[Name] NVARCHAR(MAX) NOT NULL, 	
	[ParkingTypeId] INT NOT NULL, 
	[Active] BIT NOT NULL DEFAULT 0, 	
	[OwnerId] INT NOT NULL, 
	[GeolocationX] NVARCHAR(MAX) NOT NULL,
	[GeolocationY] NVARCHAR(MAX) NOT NULL, 
	CONSTRAINT [FK_ParkingSite_ToUser] FOREIGN KEY ([OwnerId]) REFERENCES [User](Id), 
	CONSTRAINT [FK_ParkingSite_ToParkingType] FOREIGN KEY ([ParkingTypeId]) REFERENCES [ParkingType]([Id]),
)

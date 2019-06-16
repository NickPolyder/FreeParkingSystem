CREATE TABLE [dbo].[ParkingType]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(200) NOT NULL, 
    CONSTRAINT [AK_ParkingType_Name] UNIQUE ([Name])
)

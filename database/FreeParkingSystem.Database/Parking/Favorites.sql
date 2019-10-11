CREATE TABLE [dbo].[Favorites]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [FavoriteTypeId] INT NOT NULL, 
    [UserId] INT NOT NULL, 
    [ReferenceId] INT NOT NULL, 
    CONSTRAINT [FK_Favorites_ToFavoriteTypes] FOREIGN KEY ([FavoriteTypeId]) REFERENCES [FavoriteType]([Id]), 
    CONSTRAINT [FK_Favorites_ToUser] FOREIGN KEY ([UserId]) REFERENCES [User]([Id])
)

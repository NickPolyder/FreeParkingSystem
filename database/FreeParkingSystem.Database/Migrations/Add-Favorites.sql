GO
:setvar DatabaseName "FreeParkingSystem.Database"

GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
	BEGIN
		PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
		SET NOEXEC ON;
	END


GO
USE [$(DatabaseName)]
GO

IF NOT EXISTS(SELECT 1 FROM sys.tables t WHERE t.object_id = OBJECT_ID('FavoriteType'))
BEGIN
	PRINT N'Creating [dbo].[FavoriteType]...';
	CREATE TABLE [dbo].[FavoriteType] (
		[Id]   INT            IDENTITY (1, 1) NOT NULL,
		[Name] NVARCHAR (MAX) NULL,
		PRIMARY KEY CLUSTERED ([Id] ASC)
	);


	SET IDENTITY_INSERT [dbo].[FavoriteType] ON 
	
	INSERT [dbo].[FavoriteType] ([Id], [Name]) VALUES (1, N'Parking Site')
	
	INSERT [dbo].[FavoriteType] ([Id], [Name]) VALUES (2, N'Parking Spot')
	
	SET IDENTITY_INSERT [dbo].[FavoriteType] OFF
END
ELSE
	PRINT N'The table [FavoriteType] already exists.'
GO

IF NOT EXISTS(SELECT 1 FROM sys.tables t WHERE t.object_id = OBJECT_ID('Favorites'))
BEGIN
	PRINT N'Creating [dbo].[Favorites]...';
	
	CREATE TABLE [dbo].[Favorites] (
		[Id]             INT IDENTITY (1, 1) NOT NULL,
		[FavoriteTypeId] INT NOT NULL,
		[UserId]         INT NOT NULL,
		[ReferenceId]    INT NOT NULL,
		PRIMARY KEY CLUSTERED ([Id] ASC)
	);

	PRINT N'Creating [dbo].[FK_Favorites_ToFavoriteTypes]...';

	ALTER TABLE [dbo].[Favorites]
	ADD CONSTRAINT [FK_Favorites_ToFavoriteTypes] FOREIGN KEY ([FavoriteTypeId]) REFERENCES [dbo].[FavoriteType] ([Id]);

	PRINT N'Creating [dbo].[FK_Favorites_ToUser]...';

	ALTER TABLE [dbo].[Favorites]
	ADD CONSTRAINT [FK_Favorites_ToUser] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]);
END
ELSE
	PRINT N'The table [Favorites] already exists.'
GO
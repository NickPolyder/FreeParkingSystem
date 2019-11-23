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

IF NOT EXISTS(SELECT 1 FROM sys.columns t WHERE t.object_id = OBJECT_ID('Order') AND t.name = 'IsCancelled')
BEGIN
	PRINT N'Creating [dbo].[Order].[IsCancelled]...';
	ALTER TABLE [dbo].[Order] 
	ADD IsCancelled BIT NOT NULL DEFAULT 0

	PRINT N'Alter [dbo].[OrderView] to include [IsCancelled] column...';
	EXEC('CREATE OR ALTER VIEW [dbo].[OrderView]
	AS 	SELECT 
	ord.Id
	,ord.ParkingSpotId
	,usr.UserName AS "Tenant"
	,ord.LeaseStartDate
	,ord.LeaseEndDate
	,ord.IsCancelled
	,spotView.ParkingSiteId
	,spotView.Parking
	,spotView.IsActive
	,spotView.ParkingTypeId
	,spotView.ParkingType
	,spotView.Owner
	,spotView.GeolocationX 
	,spotView.GeolocationY
	,spotView.Level
	,spotView.Position
	,spotView.IsAvailable
	,spotView.ParkingSpotTypeId
	,spotView.ParkingSpotType	
	FROM [Order] ord
	LEFT JOIN [User] usr ON ord.UserId = usr.Id
	LEFT JOIN [ParkingSpotView] spotView ON ord.ParkingSpotId = spotView.Id
')
END
ELSE
	PRINT N'The column [dbo].[Order].[IsCancelled] already exists.'
GO

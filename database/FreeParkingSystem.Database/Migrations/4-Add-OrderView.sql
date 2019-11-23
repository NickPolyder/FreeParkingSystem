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

IF NOT EXISTS(SELECT 1 FROM sys.views t WHERE t.object_id = OBJECT_ID('OrderView'))
BEGIN
	PRINT N'Creating view [dbo].[OrderView]...';
	
	EXEC('CREATE VIEW [dbo].[OrderView]
	AS 	SELECT 
	ord.Id
	,ord.ParkingSpotId
	,usr.UserName AS "Tenant"
	,ord.LeaseStartDate
	,ord.LeaseEndDate
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
	PRINT N'The view [OrderView] already exists.'
GO
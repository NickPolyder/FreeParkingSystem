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

IF NOT EXISTS(SELECT 1 FROM sys.views t WHERE t.object_id = OBJECT_ID('ParkingSpotView'))
BEGIN
	PRINT N'Creating view [dbo].[ParkingSpotView]...';
	
	EXEC('CREATE VIEW [dbo].[ParkingSpotView]
AS SELECT 
	spot.Id
	,spot.ParkingSiteId
	,siteView.Parking
	,siteView.IsActive
	,siteView.ParkingTypeId
	,siteView.ParkingType
	,siteView.Owner
	,siteView.GeolocationX 
	,siteView.GeolocationY
	,spot.Level
	,spot.Position
	,spot.IsAvailable
	,spot.ParkingSpotTypeId
	,prkSpotType.Name AS "ParkingSpotType"
	FROM [ParkingSpot] spot
	LEFT JOIN [ParkingSiteView] siteView ON spot.ParkingSiteId = siteView.Id
	LEFT JOIN [ParkingSpotType] prkSpotType ON spot.ParkingSpotTypeId = prkSpotType.Id
')

END
ELSE
	PRINT N'The view [ParkingSpotView] already exists.'
GO
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


IF EXISTS(SELECT 1 FROM sys.views t WHERE t.object_id = OBJECT_ID('ParkingSiteView'))
BEGIN
	PRINT N'Amending view [dbo].[ParkingSiteView] to include Owner id...';
	
	EXEC('CREATE OR ALTER VIEW [dbo].[ParkingSiteView]
	AS SELECT 
	site.Id
	,site.Name AS "Parking"
	,site.IsActive
	,site.ParkingTypeId
	,prkType.Name AS "ParkingType"	
	,site.OwnerId
	,usr.UserName AS "Owner"
	,site.GeolocationX 
	,site.GeolocationY
	FROM [ParkingSite] site
	LEFT JOIN [User] usr ON site.OwnerId = usr.Id
	LEFT JOIN [ParkingType] prkType ON site.ParkingTypeId = prkType.Id
')
END


IF EXISTS(SELECT 1 FROM sys.views t WHERE t.object_id = OBJECT_ID('ParkingSpotView'))
BEGIN
	PRINT N'Amending view [dbo].[ParkingSpotView] to Owner id...';
	
	EXEC('CREATE OR ALTER VIEW [dbo].[ParkingSpotView]
	AS SELECT 
	spot.Id
	,spot.ParkingSiteId
	,siteView.Parking
	,siteView.IsActive
	,siteView.ParkingTypeId
	,siteView.ParkingType
	,siteView.OwnerId
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

IF EXISTS(SELECT 1 FROM sys.views t WHERE t.object_id = OBJECT_ID('OrderView'))
BEGIN
	PRINT N'Amending view [dbo].[OrderView] to include Tenant Id and Owner id...';
	
	EXEC('CREATE OR ALTER VIEW [dbo].[OrderView]
	AS 	SELECT 
	ord.Id
	,ord.ParkingSpotId	
	,ord.TenantId
	,usr.UserName AS "Tenant"
	,ord.LeaseStartDate
	,ord.LeaseEndDate
	,ord.IsCancelled
	,spotView.ParkingSiteId
	,spotView.Parking
	,spotView.IsActive
	,spotView.ParkingTypeId
	,spotView.ParkingType
	,spotView.OwnerId
	,spotView.Owner
	,spotView.GeolocationX 
	,spotView.GeolocationY
	,spotView.Level
	,spotView.Position
	,spotView.IsAvailable
	,spotView.ParkingSpotTypeId
	,spotView.ParkingSpotType	
	FROM [Order] ord
	LEFT JOIN [User] usr ON ord.TenantId = usr.Id
	LEFT JOIN [ParkingSpotView] spotView ON ord.ParkingSpotId = spotView.Id

')
END
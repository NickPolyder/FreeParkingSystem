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

IF NOT EXISTS(SELECT 1 FROM sys.views t WHERE t.object_id = OBJECT_ID('ParkingSiteView'))
BEGIN
	PRINT N'Creating view [dbo].[ParkingSiteView]...';
	
	EXEC('CREATE VIEW [dbo].[ParkingSiteView]
	AS SELECT 
	site.Id
	,site.Name AS "Parking"
	,site.IsActive
	,site.ParkingTypeId
	,prkType.Name AS "ParkingType"
	,usr.UserName AS "Owner"
	,site.GeolocationX 
	,site.GeolocationY
	FROM [ParkingSite] site
	LEFT JOIN [User] usr ON site.OwnerId = usr.Id
	LEFT JOIN [ParkingType] prkType ON site.ParkingTypeId = prkType.Id')

END
ELSE
	PRINT N'The view [ParkingSiteView] already exists.'
GO
CREATE VIEW [dbo].[ParkingSiteView]
	AS SELECT 
	site.Id
	,site.Name AS 'Parking'
	,site.IsActive
	,site.ParkingTypeId	
	,prkType.Name AS 'ParkingType'
	,site.OwnerId
	,usr.UserName AS 'Owner'
	,site.GeolocationX 
	,site.GeolocationY
	FROM [ParkingSite] site
	LEFT JOIN [User] usr ON site.OwnerId = usr.Id
	LEFT JOIN [ParkingType] prkType ON site.ParkingTypeId = prkType.Id

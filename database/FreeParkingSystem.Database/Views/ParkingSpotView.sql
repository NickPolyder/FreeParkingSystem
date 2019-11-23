CREATE VIEW [dbo].[ParkingSpotView]
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
	,prkSpotType.Name AS 'ParkingSpotType'
	FROM [ParkingSpot] spot
	LEFT JOIN [ParkingSiteView] siteView ON spot.ParkingSiteId = siteView.Id
	LEFT JOIN [ParkingSpotType] prkSpotType ON spot.ParkingSpotTypeId = prkSpotType.Id

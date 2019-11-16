CREATE VIEW [dbo].[OrderView]
	AS 	SELECT 
	ord.Id
	,ord.ParkingSpotId
	,usr.UserName AS 'Tenant'
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

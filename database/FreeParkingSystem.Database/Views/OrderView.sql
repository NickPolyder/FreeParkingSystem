CREATE VIEW [dbo].[OrderView]
	AS 	SELECT 
	ord.Id
	,ord.ParkingSpotId	
	,ord.TenantId
	,usr.UserName AS 'Tenant'
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

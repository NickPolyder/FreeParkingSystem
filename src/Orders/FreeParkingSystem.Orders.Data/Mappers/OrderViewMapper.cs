using System.Collections.Generic;
using FreeParkingSystem.Common;
using FreeParkingSystem.Orders.Contract;
using FreeParkingSystem.Orders.Data.Models;
using FreeParkingSystem.Parking.Data.Models;

namespace FreeParkingSystem.Orders.Data.Mappers
{
	public class OrderViewMapper : IMap<DbOrderView, OrderView>
	{
		public OrderView Map(DbOrderView input, IDictionary<object, object> context)
		{
			if (input == null)
				return null;

			return new OrderView
			{
				Id = input.Id,
				TenantId = input.TenantId,
				Tenant = input.Tenant,
				LeaseStartDate = input.LeaseStartDate,
				LeaseEndDate = input.LeaseEndDate,
				IsCancelled = input.IsCancelled,
				ParkingSpot = new ParkingSpotView
				{
					Id = input.ParkingSpotId,
					IsAvailable = input.IsAvailable,
					Level = input.Level,
					Position = input.Position,
					ParkingSpotType = new ParkingSpotType
					{
						Id = input.ParkingSpotTypeId,
						Name = input.ParkingSpotType
					},
					ParkingSite = new ParkingSiteView
					{
						Id = input.ParkingSiteId,
						Name = input.Parking,
						IsActive = input.IsActive,
						OwnerId = input.OwnerId,
						Owner = input.Owner,
						ParkingType = new ParkingType
						{
							Id = input.ParkingTypeId,
							Name = input.ParkingType
						},
						Geolocation = new Geolocation(input.GeolocationX, input.GeolocationY)
					}
				}
			};
		}

		public DbOrderView Map(OrderView input, IDictionary<object, object> context)
		{
			if (input == null)
				return null;

			return new DbOrderView
			{
				Id = input.Id,
				TenantId = input.TenantId,
				Tenant = input.Tenant,
				LeaseStartDate = input.LeaseStartDate,
				LeaseEndDate = input.LeaseEndDate,
				IsCancelled = input.IsCancelled,
				ParkingSpotId = input.ParkingSpot.Id,
				IsAvailable = input.ParkingSpot.IsAvailable,
				Level = input.ParkingSpot.Level,
				Position = input.ParkingSpot.Position,
				ParkingSpotTypeId = input.ParkingSpot.ParkingSpotType.Id,
				ParkingSpotType = input.ParkingSpot.ParkingSpotType.Name,
				ParkingSiteId = input.ParkingSpot.ParkingSite.Id,
				ParkingTypeId = input.ParkingSpot.ParkingSite.ParkingType.Id,
				ParkingType = input.ParkingSpot.ParkingSite.ParkingType.Name,
				Parking = input.ParkingSpot.ParkingSite.Name,
				IsActive = input.ParkingSpot.ParkingSite.IsActive,
				OwnerId = input.ParkingSpot.ParkingSite.OwnerId,
				Owner = input.ParkingSpot.ParkingSite.Owner,
				GeolocationX = input.ParkingSpot.ParkingSite.Geolocation.X,
				GeolocationY = input.ParkingSpot.ParkingSite.Geolocation.Y,
			};
		}
	}
}
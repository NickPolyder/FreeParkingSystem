using System.Collections.Generic;
using FreeParkingSystem.Common;
using FreeParkingSystem.Parking.Data.Models;

namespace FreeParkingSystem.Parking.Data.DatabaseModels.Mappers
{
	public class ParkingSpotViewMapper : IMap<DbParkingSpotView,ParkingSpotView>
	{
		public ParkingSpotView Map(DbParkingSpotView input, IDictionary<object, object> context)
		{
			return new ParkingSpotView
			{
				Id = input.Id,
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
					Id = input.Id,
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
			};
		}

		public DbParkingSpotView Map(ParkingSpotView input, IDictionary<object, object> context)
		{
			return new DbParkingSpotView
			{
				Id = input.Id,
				IsAvailable = input.IsAvailable,
				Level = input.Level,
				Position = input.Position,
				ParkingSpotTypeId = input.ParkingSpotType.Id,
				ParkingSpotType = input.ParkingSpotType.Name,
				ParkingSiteId = input.ParkingSite.Id,
				ParkingTypeId = input.ParkingSite.ParkingType.Id,
				ParkingType = input.ParkingSite.ParkingType.Name,
				Parking = input.ParkingSite.Name,
				IsActive = input.ParkingSite.IsActive,
				OwnerId = input.ParkingSite.OwnerId,
				Owner = input.ParkingSite.Owner,
				GeolocationX = input.ParkingSite.Geolocation.X,
				GeolocationY = input.ParkingSite.Geolocation.Y
			};
		}
	}
}
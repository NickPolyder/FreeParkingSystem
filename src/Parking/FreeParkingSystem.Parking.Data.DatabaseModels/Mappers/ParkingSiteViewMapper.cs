using System.Collections.Generic;
using FreeParkingSystem.Common;
using FreeParkingSystem.Parking.Data.Models;

namespace FreeParkingSystem.Parking.Data.DatabaseModels.Mappers
{
	public class ParkingSiteViewMapper : IMap<DbParkingSiteView,ParkingSiteView>
	{
		public ParkingSiteView Map(DbParkingSiteView input, IDictionary<object, object> context)
		{
			if (input == null)
				return null;

			return new ParkingSiteView
			{
				Id = input.Id,
				Name = input.Parking,
				IsActive = input.IsActive,
				OwnerId = input.OwnerId,
				Owner = input.Owner,
				ParkingType =  new ParkingType
				{
					Id= input.ParkingTypeId,
					Name = input.ParkingType
				},
				Geolocation = new Geolocation(input.GeolocationX,input.GeolocationY)
			};
		}

		public DbParkingSiteView Map(ParkingSiteView input, IDictionary<object, object> context)
		{
			if (input == null)
				return null;

			return new DbParkingSiteView
			{
				Id = input.Id,
				Parking = input.Name,
				IsActive = input.IsActive,
				OwnerId = input.OwnerId,
				Owner = input.Owner,
				ParkingTypeId = input.ParkingType.Id,
				ParkingType = input.ParkingType.Name,
				GeolocationX = input.Geolocation.X,
				GeolocationY = input.Geolocation.Y,
			};
		}
	}
}
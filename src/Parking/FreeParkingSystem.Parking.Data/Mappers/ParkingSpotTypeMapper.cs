using System.Collections.Generic;
using FreeParkingSystem.Common;
using FreeParkingSystem.Parking.Contract;
using FreeParkingSystem.Parking.Data.Models;

namespace FreeParkingSystem.Parking.Data.Mappers
{
	public class ParkingSpotTypeMapper: IMap<DbParkingSpotType,ParkingSpotType>
	{
		public ParkingSpotType Map(DbParkingSpotType input, IDictionary<object, object> context)
		{
			if (input == null)
				return null;

			return new ParkingSpotType
			{
				Id = input.Id,
				Name = input.Name
			};
		}

		public DbParkingSpotType Map(ParkingSpotType input, IDictionary<object, object> context)
		{
			if (input == null)
				return null;

			return new DbParkingSpotType
			{
				Id = input.Id,
				Name = input.Name
			};
		}
	}
}
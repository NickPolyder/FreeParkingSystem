using System.Collections.Generic;
using FreeParkingSystem.Common;
using FreeParkingSystem.Parking.Contract;
using FreeParkingSystem.Parking.Data.Models;

namespace FreeParkingSystem.Parking.Data.Mappers
{
	public class ParkingTypeMapper : IMap<DbParkingType, ParkingType>
	{
		public ParkingType Map(DbParkingType input, IDictionary<object, object> context)
		{
			if (input == null)
				return null;

			return new ParkingType
			{
				Id = input.Id,
				Name = input.Name
			};
		}

		public DbParkingType Map(ParkingType input, IDictionary<object, object> context)
		{
			if (input == null)
				return null;

			return new DbParkingType
			{
				Id = input.Id,
				Name = input.Name
			};
		}
	}
}
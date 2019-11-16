using System.Collections.Generic;
using FreeParkingSystem.Common;
using FreeParkingSystem.Common.ExtensionMethods;
using FreeParkingSystem.Parking.Data.Models;

namespace FreeParkingSystem.Parking.Data.DatabaseModels.Mappers
{
	public class ParkingSiteMapper : IMap<DbParkingSite, ParkingSite>
	{
		private readonly IMap<DbParkingType, ParkingType> _parkingTypeMapper;

		public ParkingSiteMapper(IMap<DbParkingType, ParkingType> parkingTypeMapper)
		{
			_parkingTypeMapper = parkingTypeMapper;
		}
		public ParkingSite Map(DbParkingSite input, IDictionary<object, object> context)
		{
			if (input == null)
				return null;

			return new ParkingSite
			{
				Id = input.Id,
				Name = input.Name,
				IsActive = input.IsActive,
				OwnerId = input.OwnerId,
				ParkingTypeId = input.ParkingTypeId,
				ParkingType = _parkingTypeMapper.Map(input.ParkingType),
				Geolocation = new Geolocation(input.GeolocationX, input.GeolocationY)
			};
		}

		public DbParkingSite Map(ParkingSite input, IDictionary<object, object> context)
		{
			if (input == null)
				return null;

			return new DbParkingSite
			{
				Id = input.Id,
				Name = input.Name,
				IsActive = input.IsActive,
				OwnerId = input.OwnerId,
				ParkingTypeId = input.ParkingTypeId,
				ParkingType = _parkingTypeMapper.Map(input.ParkingType),
				GeolocationX = input.Geolocation.X,
				GeolocationY = input.Geolocation.Y
			};
		}
	}
}
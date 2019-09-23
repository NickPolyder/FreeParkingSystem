using System.Collections.Generic;
using FreeParkingSystem.Common;
using FreeParkingSystem.Common.ExtensionMethods;
using FreeParkingSystem.Parking.Contract;
using FreeParkingSystem.Parking.Data.Models;

namespace FreeParkingSystem.Parking.Data.Mappers
{
	public class ParkingSpotMapper :IMap<DbParkingSpot, ParkingSpot>
	{
		private readonly IMap<DbParkingSite, ParkingSite> _parkingSite;
		private readonly IMap<DbParkingSpotType, ParkingSpotType> _parkingSpotType;

		public ParkingSpotMapper(IMap<DbParkingSite, ParkingSite> parkingSite, IMap<DbParkingSpotType, ParkingSpotType> parkingSpotType)
		{
			_parkingSite = parkingSite;
			_parkingSpotType = parkingSpotType;
		}
		public ParkingSpot Map(DbParkingSpot input, IDictionary<object, object> context)
		{
			if (input == null)
				return null;


			return new ParkingSpot
			{
				Id = input.Id,
				IsAvailable = input.IsAvailable,
				Level = input.Level,
				Position = input.Position,
				ParkingSpotTypeId = input.ParkingSpotTypeId,
				ParkingSiteId = input.ParkingSiteId,
				ParkingSite = _parkingSite.Map(input.ParkingSite),
				ParkingSpotType = _parkingSpotType.Map(input.ParkingSpotType)
			};
		}

		public DbParkingSpot Map(ParkingSpot input, IDictionary<object, object> context)
		{
			if (input == null)
				return null;

			return new DbParkingSpot
			{
				Id = input.Id,
				IsAvailable = input.IsAvailable,
				Level = input.Level,
				Position = input.Position,
				ParkingSpotTypeId = input.ParkingSpotTypeId,
				ParkingSiteId = input.ParkingSiteId,
				ParkingSite = _parkingSite.Map(input.ParkingSite),
				ParkingSpotType = _parkingSpotType.Map(input.ParkingSpotType)
			};
		}
	}
}
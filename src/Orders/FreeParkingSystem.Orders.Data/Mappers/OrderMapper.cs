using System.Collections.Generic;
using FreeParkingSystem.Common;
using FreeParkingSystem.Common.ExtensionMethods;
using FreeParkingSystem.Orders.Contract;
using FreeParkingSystem.Orders.Data.Models;
using FreeParkingSystem.Parking.Data.DatabaseModels;
using FreeParkingSystem.Parking.Data.Models;

namespace FreeParkingSystem.Orders.Data.Mappers
{
	public class OrderMapper : IMap<DbOrder,Order>
	{
		private readonly IMap<DbParkingSpot, ParkingSpot> _parkingSpotMapper;

		public OrderMapper(IMap<DbParkingSpot, ParkingSpot> parkingSpotMapper)
		{
			_parkingSpotMapper = parkingSpotMapper;
		}
		public Order Map(DbOrder input, IDictionary<object, object> context)
		{
			return new Order
			{
				Id = input.Id,
				ParkingSpotId = input.ParkingSpotId,
				UserId =  input.UserId,
				ParkingSpot = _parkingSpotMapper.Map(input.ParkingSpot),
				LeaseStartDate = input.LeaseStartDate,
				LeaseEndDate = input.LeaseEndDate
			};
		}

		public DbOrder Map(Order input, IDictionary<object, object> context)
		{
			return new DbOrder
			{
				Id = input.Id,
				ParkingSpotId = input.ParkingSpotId,
				UserId = input.UserId,
				ParkingSpot = _parkingSpotMapper.Map(input.ParkingSpot),
				LeaseStartDate = input.LeaseStartDate,
				LeaseEndDate = input.LeaseEndDate
			};
		}
	}
}
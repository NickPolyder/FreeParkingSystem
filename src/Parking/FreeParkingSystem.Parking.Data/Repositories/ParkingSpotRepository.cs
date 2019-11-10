using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FreeParkingSystem.Common;
using FreeParkingSystem.Common.Data;
using FreeParkingSystem.Common.ExtensionMethods;
using FreeParkingSystem.Parking.Contract;
using FreeParkingSystem.Parking.Contract.Repositories;
using FreeParkingSystem.Parking.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace FreeParkingSystem.Parking.Data.Repositories
{
	public class ParkingSpotRepository : BaseRepository<ParkingSpot, DbParkingSpot>, IParkingSpotRepository
	{
		public ParkingSpotRepository(ParkingDbContext dbContext, IMap<DbParkingSpot, ParkingSpot> mapper) : base(dbContext, mapper)
		{
		}

		protected override IEnumerable<Expression<Func<DbParkingSpot, object>>> GetIncludes()
		{
			return new Expression<Func<DbParkingSpot, object>>[]
			{
				spot => spot.ParkingSite,
				spot => spot.ParkingSpotType
			};
		}

		public IEnumerable<ParkingSpot> GetAllBy(int parkingSiteId)
		{
			var spots = GetDataSetWithIncludes()
				.Where(spot => spot.ParkingSiteId == parkingSiteId
							   && spot.ParkingSite != null
							   && spot.ParkingSite.IsActive).ToArray();

			return Mapper.Map(spots);
		}

		public ParkingSpot GetBy(int parkingSpotId, int parkingSiteId)
		{
			var spot = GetDataSetWithIncludes()
				.FirstOrDefault(item => item.Id == parkingSpotId
			&& item.ParkingSiteId == parkingSiteId
			&& item.ParkingSite != null
			&& item.ParkingSite.IsActive);

			return spot == null ? null : Mapper.Map(spot);
		}

		public bool Exists(ParkingSpot parkingSpot)
		{
			return Set.Any(spot => spot.Id == parkingSpot.Id
								   || (spot.ParkingSiteId == parkingSpot.ParkingSiteId
									   && spot.Level == parkingSpot.Level
									   && spot.Position == parkingSpot.Position));
		}

		public void DeleteBySiteId(int parkingSiteId)
		{
			var parkingSpots = Set.Where(spot => spot.ParkingSiteId == parkingSiteId);

			Set.RemoveRange(parkingSpots);

			DbContext.SaveChanges();
		}
	}
}
using System.Linq;
using FreeParkingSystem.Common;
using FreeParkingSystem.Common.Data;
using FreeParkingSystem.Parking.Contract;
using FreeParkingSystem.Parking.Contract.Repositories;
using FreeParkingSystem.Parking.Data.Models;
using Microsoft.EntityFrameworkCore.Internal;

namespace FreeParkingSystem.Parking.Data.Repositories
{
	public class ParkingSpotRepository: BaseRepository<ParkingSpot, DbParkingSpot>, IParkingSpotRepository
	{
		public ParkingSpotRepository(ParkingDbContext dbContext, IMap<DbParkingSpot, ParkingSpot> mapper) : base(dbContext, mapper)
		{
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
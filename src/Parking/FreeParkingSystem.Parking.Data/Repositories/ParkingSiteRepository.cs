using System;
using System.Linq;
using FreeParkingSystem.Common;
using FreeParkingSystem.Common.Data;
using FreeParkingSystem.Parking.Contract.Repositories;
using FreeParkingSystem.Parking.Data.DatabaseModels;
using FreeParkingSystem.Parking.Data.Models;

namespace FreeParkingSystem.Parking.Data.Repositories
{
	public class ParkingSiteRepository : BaseRepository<ParkingSite, DbParkingSite>, IParkingSiteRepository
	{
		public ParkingSiteRepository(ParkingDbContext dbContext, IMap<DbParkingSite, ParkingSite> mapper) : base(dbContext, mapper)
		{
		}

		public bool Exists(ParkingSite parkingSite)
		{
			return parkingSite.Id == 0 
				? Set.Any(item => item.Name.Equals(parkingSite.Name, StringComparison.InvariantCultureIgnoreCase))
				: Set.Any(item => item.Id != parkingSite.Id && item.Name.Equals(parkingSite.Name, StringComparison.InvariantCultureIgnoreCase));
		}
	}
}
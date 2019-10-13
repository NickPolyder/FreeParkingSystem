using System;
using System.Linq;
using FreeParkingSystem.Common;
using FreeParkingSystem.Common.Data;
using FreeParkingSystem.Parking.Contract;
using FreeParkingSystem.Parking.Contract.Repositories;
using FreeParkingSystem.Parking.Data.Models;

namespace FreeParkingSystem.Parking.Data.Repositories
{
	public class ParkingSiteRepository : BaseRepository<ParkingSite, DbParkingSite>, IParkingSiteRepository
	{
		public ParkingSiteRepository(ParkingDbContext dbContext, IMap<DbParkingSite, ParkingSite> mapper) : base(dbContext, mapper)
		{
		}

		public bool Exists(string name)
		{
			return Set.Any(item => item.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
		}
	}
}
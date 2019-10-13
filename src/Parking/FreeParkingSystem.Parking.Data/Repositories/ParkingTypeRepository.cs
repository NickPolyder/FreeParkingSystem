using System.Linq;
using FreeParkingSystem.Common;
using FreeParkingSystem.Common.Data;
using FreeParkingSystem.Parking.Contract;
using FreeParkingSystem.Parking.Contract.Repositories;
using FreeParkingSystem.Parking.Data.Models;

namespace FreeParkingSystem.Parking.Data.Repositories
{
	public class ParkingTypeRepository : BaseRepository<ParkingType, DbParkingType>, IParkingTypeRepository
	{
		public ParkingTypeRepository(ParkingDbContext dbContext, IMap<DbParkingType, ParkingType> mapper) : base(dbContext, mapper)
		{
		}

		public bool Exists(int parkingTypeId)
		{
			return Set.Any(item => item.Id == parkingTypeId);
		}
	}
}
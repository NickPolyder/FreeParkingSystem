using FreeParkingSystem.Common;
using FreeParkingSystem.Common.Data;
using FreeParkingSystem.Parking.Contract.Repositories;
using FreeParkingSystem.Parking.Data.DatabaseModels;
using FreeParkingSystem.Parking.Data.Models;

namespace FreeParkingSystem.Parking.Data.Repositories
{
	public class ParkingSpotTypeRepository : BaseRepository<ParkingSpotType, DbParkingSpotType>,
		IParkingSpotTypeRepository
	{

		public ParkingSpotTypeRepository(ParkingDbContext dbContext, IMap<DbParkingSpotType, ParkingSpotType> mapper) : base(
			dbContext, mapper)
		{
		}
	}
}
using FreeParkingSystem.Common;
using FreeParkingSystem.Common.Data;
using FreeParkingSystem.Parking.Contract;
using FreeParkingSystem.Parking.Contract.Repositories;
using FreeParkingSystem.Parking.Data.Models;
namespace FreeParkingSystem.Parking.Data.Repositories
{
	public class ParkingSpotRepository: BaseRepository<ParkingSpot, DbParkingSpot>, IParkingSpotRepository
	{
		public ParkingSpotRepository(ParkingDbContext dbContext, IMap<DbParkingSpot, ParkingSpot> mapper) : base(dbContext, mapper)
		{
		}
	}
}
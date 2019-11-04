using FreeParkingSystem.Common;
using FreeParkingSystem.Common.Data;
using FreeParkingSystem.Parking.Contract;
using FreeParkingSystem.Parking.Contract.Repositories;
using FreeParkingSystem.Parking.Data.Models;

namespace FreeParkingSystem.Parking.Data.Repositories
{
	public class ParkingSiteViewRepository : BaseViewRepository<ParkingSiteView, DbParkingSiteView>, IParkingSiteViewRepository
	{
		public ParkingSiteViewRepository(ParkingDbContext dbContext, IMap<DbParkingSiteView, ParkingSiteView> mapper) : base(dbContext, mapper)
		{
		}
	}
}
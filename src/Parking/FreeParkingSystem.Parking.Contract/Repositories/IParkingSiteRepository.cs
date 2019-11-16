using FreeParkingSystem.Common;
using FreeParkingSystem.Parking.Data.Models;

namespace FreeParkingSystem.Parking.Contract.Repositories
{
	public interface IParkingSiteRepository : IRepository<ParkingSite>
	{
		bool Exists(ParkingSite parkingSite);
	}
}
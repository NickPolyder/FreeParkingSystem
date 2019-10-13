using FreeParkingSystem.Common;

namespace FreeParkingSystem.Parking.Contract.Repositories
{
	public interface IParkingSiteRepository : IRepository<ParkingSite>
	{
		bool Exists(string name);
	}
}
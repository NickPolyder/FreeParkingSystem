using FreeParkingSystem.Common;
using FreeParkingSystem.Parking.Data.Models;

namespace FreeParkingSystem.Parking.Contract.Repositories
{
	public interface IParkingTypeRepository : IRepository<ParkingType>
	{
		bool Exists(int parkingTypeId);
	}
}
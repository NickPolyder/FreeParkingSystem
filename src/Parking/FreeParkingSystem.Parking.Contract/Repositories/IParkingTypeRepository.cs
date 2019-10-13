using FreeParkingSystem.Common;

namespace FreeParkingSystem.Parking.Contract.Repositories
{
	public interface IParkingTypeRepository : IRepository<ParkingType>
	{
		bool Exists(int parkingTypeId);
	}
}
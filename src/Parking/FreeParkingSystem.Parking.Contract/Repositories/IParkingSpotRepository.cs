using FreeParkingSystem.Common;

namespace FreeParkingSystem.Parking.Contract.Repositories
{
	public interface IParkingSpotRepository: IRepository<ParkingSpot>
	{
		bool Exists(ParkingSpot parkingSpot);

		void DeleteBySiteId(int parkingSiteId);
	}
}
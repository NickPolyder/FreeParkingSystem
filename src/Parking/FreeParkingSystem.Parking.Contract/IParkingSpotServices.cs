using System.Collections.Generic;

namespace FreeParkingSystem.Parking.Contract
{
	public interface IParkingSpotServices
	{
		//IEnumerable<ParkingSpotView> GetViews();
		//ParkingSpotView GetView(int parkingSpotId);
		ParkingSpot Get(int parkingSpotId);

		ParkingSpot Add(ParkingSpot parkingSpot);

		ParkingSpot Update(ParkingSpot parkingSpot);

		void Delete(int parkingSpotId);

		void DeleteBySiteId(int parkingSiteId);
	}
}
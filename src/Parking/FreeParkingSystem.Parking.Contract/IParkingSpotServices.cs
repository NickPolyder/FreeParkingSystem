using System.Collections.Generic;
using FreeParkingSystem.Parking.Data.Models;

namespace FreeParkingSystem.Parking.Contract
{
	public interface IParkingSpotServices
	{
		IEnumerable<ParkingSpot> GetViews(int parkingSiteId);
		ParkingSpot GetView(int parkingSpotId,int parkingSiteId);
		ParkingSpot Get(int parkingSpotId);

		ParkingSpot Add(ParkingSpot parkingSpot);

		ParkingSpot Update(ParkingSpot parkingSpot);

		void Delete(int parkingSpotId);

		void DeleteBySiteId(int parkingSiteId);
	}
}
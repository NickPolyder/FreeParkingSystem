namespace FreeParkingSystem.Common.Data
{
	public interface ICommonFunctionsRepository
	{
		bool HasActiveLeaseOnAParkingSite(int parkingSiteId);

		bool HasActiveLeaseOnAParkingSpot(int parkingSpotId);

		bool ParkingSpotExists(int parkingSpotId);

		bool IsOwnerOfParkingSpot(int parkingSpotId, int userId);
	}
}
namespace FreeParkingSystem.Common.Data
{
	public interface ICommonFunctionsRepository
	{
		bool HasActiveLeaseOnAParkingSite(int parkingSiteId);

		bool HasActiveLeaseOnAParkingSpot(int parkingSpotId);
	}
}
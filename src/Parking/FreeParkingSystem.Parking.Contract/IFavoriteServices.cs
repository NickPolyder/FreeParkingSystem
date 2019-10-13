using FreeParkingSystem.Parking.Contract.Constants;

namespace FreeParkingSystem.Parking.Contract
{
	public interface IFavoriteServices
	{
		void AddFavorite(int userId, ParkingSite parkingSite);
		void AddFavorite(int userId, ParkingSpot parkingSpot);
	}
}
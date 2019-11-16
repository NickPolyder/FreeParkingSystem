using FreeParkingSystem.Parking.Data.Models;

namespace FreeParkingSystem.Parking.Contract
{
	public interface IFavoriteServices
	{
		void AddFavorite(int userId, ParkingSite parkingSite);
		void AddFavorite(int userId, ParkingSpot parkingSpot);

		void RemoveFavorite(int userId, ParkingSite parkingSite);
		void RemoveFavorite(int userId, ParkingSpot parkingSpot);
	}
}
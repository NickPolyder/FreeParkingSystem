using FreeParkingSystem.Common;
using FreeParkingSystem.Parking.Contract.Constants;
using FreeParkingSystem.Parking.Data.Models;

namespace FreeParkingSystem.Parking.Contract.Repositories
{
	public interface IFavoriteRepository : IRepository<Favorite>
	{
		bool Exists(Favorite favorite);

		Favorite GetBy(int userId, FavoriteType favoriteType, int referenceId);
	}
}
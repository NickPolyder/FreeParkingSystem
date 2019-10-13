using FreeParkingSystem.Common;
using FreeParkingSystem.Parking.Contract.Constants;

namespace FreeParkingSystem.Parking.Contract.Repositories
{
	public interface IFavoriteRepository : IRepository<Favorite>
	{
		bool Exists(Favorite favorite);
	}
}
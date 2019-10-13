using System.Linq;
using FreeParkingSystem.Common;
using FreeParkingSystem.Common.Data;
using FreeParkingSystem.Parking.Contract;
using FreeParkingSystem.Parking.Contract.Repositories;
using FreeParkingSystem.Parking.Data.Models;

namespace FreeParkingSystem.Parking.Data.Repositories
{
	public class FavoriteRepository : BaseRepository<Favorite, DbFavorite>, IFavoriteRepository
	{
		public FavoriteRepository(ParkingDbContext dbContext, IMap<DbFavorite, Favorite> mapper) : base(dbContext, mapper)
		{
		}

		public bool Exists(Favorite favorite)
		{
			if (favorite == null)
				return false;

			var favoriteTypeId = (int)favorite.FavoriteType;
			return Set.Any(item => item.UserId == favorite.UserId
								   && item.FavoriteTypeId == favoriteTypeId
								   && item.ReferenceId == favorite.ReferenceId);
		}
	}
}
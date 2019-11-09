using FreeParkingSystem.Parking.Contract;
using FreeParkingSystem.Parking.Contract.Constants;
using FreeParkingSystem.Parking.Contract.Exceptions;
using FreeParkingSystem.Parking.Contract.Repositories;

namespace FreeParkingSystem.Parking
{
	public class FavoriteServices : IFavoriteServices
	{
		private readonly IFavoriteRepository _favoriteRepository;

		public FavoriteServices(IFavoriteRepository favoriteRepository)
		{
			_favoriteRepository = favoriteRepository;
		}
		public void AddFavorite(int userId, ParkingSite parkingSite)
		{
			if (parkingSite == null
			   || parkingSite.Id < 0
			   || !parkingSite.IsActive)
				throw new FavoriteException(Contract.Resources.Validation.ParkingSite_DoesNotExist);

			var favorite = new Favorite
			{
				FavoriteType = FavoriteType.ParkingSite,
				UserId = userId,
				ReferenceId = parkingSite.Id,
			};

			if (_favoriteRepository.Exists(favorite))
				return;

			_favoriteRepository.Add(favorite);
		}

		public void AddFavorite(int userId, ParkingSpot parkingSpot)
		{
			if (parkingSpot.Id < 0)
				throw new FavoriteException(Contract.Resources.Validation.ParkingSpot_DoesNotExist);

			var favorite = new Favorite
			{
				FavoriteType = FavoriteType.ParkingSpot,
				UserId = userId,
				ReferenceId = parkingSpot.Id,
			};

			if (_favoriteRepository.Exists(favorite))
				return;

			_favoriteRepository.Add(favorite);
		}

		public void RemoveFavorite(int userId, ParkingSite parkingSite)
		{
			if (parkingSite == null
				|| parkingSite.Id < 0
				|| !parkingSite.IsActive)
				throw new FavoriteException(Contract.Resources.Validation.ParkingSite_DoesNotExist);

			var favoriteItem = _favoriteRepository.GetBy(userId, FavoriteType.ParkingSite, parkingSite.Id);

			if (favoriteItem == null)
				return;

			_favoriteRepository.Delete(favoriteItem.Id);
		}

		public void RemoveFavorite(int userId, ParkingSpot parkingSpot)
		{
			throw new System.NotImplementedException();
		}
	}
}
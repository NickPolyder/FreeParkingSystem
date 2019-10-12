using System.Collections.Generic;
using FreeParkingSystem.Common;
using FreeParkingSystem.Parking.Contract;
using FreeParkingSystem.Parking.Contract.Constants;
using FreeParkingSystem.Parking.Data.Models;

namespace FreeParkingSystem.Parking.Data.Mappers
{
	public class FavoriteMapper: IMap<DbFavorite,Favorite>
	{
		public Favorite Map(DbFavorite input, IDictionary<object, object> context)
		{
			return new Favorite
			{
				Id = input.Id,
				FavoriteType = (FavoriteType)input.FavoriteTypeId,
				ReferenceId =  input.ReferenceId,
				UserId =  input.UserId
			};
		}

		public DbFavorite Map(Favorite input, IDictionary<object, object> context)
		{
			return new DbFavorite
			{
				Id = input.Id,
				FavoriteTypeId = (int)input.FavoriteType,
				ReferenceId = input.ReferenceId,
				UserId = input.UserId
			};
		}
	}
}
using FreeParkingSystem.Common.Data.Models;

namespace FreeParkingSystem.Parking.Data.DatabaseModels
{
	public class DbFavorite: IEntity
	{
		public int Id { get; set; }

		public int FavoriteTypeId { get; set; }

		public int UserId { get; set; }

		public int ReferenceId { get; set; }
	}
}
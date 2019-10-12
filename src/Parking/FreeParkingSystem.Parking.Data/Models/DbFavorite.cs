using FreeParkingSystem.Common.Data;

namespace FreeParkingSystem.Parking.Data.Models
{
	public class DbFavorite: IEntity
	{
		public int Id { get; set; }

		public int FavoriteTypeId { get; set; }

		public int UserId { get; set; }

		public int ReferenceId { get; set; }
	}
}
using FreeParkingSystem.Parking.Contract.Constants;

namespace FreeParkingSystem.Parking.Contract
{
	public class Favorite
	{
		public int Id { get; set; }

		public FavoriteType FavoriteType { get; set; }

		public int UserId { get; set; }

		public int ReferenceId { get; set; }
	}
}
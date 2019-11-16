using FreeParkingSystem.Parking.Contract;

namespace FreeParkingSystem.Parking.Data.Models
{
	public class ParkingSite
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public int ParkingTypeId { get; set; }

		public bool IsActive { get; set; }

		public int OwnerId { get; set; }

		public Geolocation Geolocation { get; set; }

		public ParkingType ParkingType { get; set; }
	}
}
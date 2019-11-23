using Newtonsoft.Json;

namespace FreeParkingSystem.Parking.Data.Models
{
	public class ParkingSiteView
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public ParkingType ParkingType { get; set; }
		public bool IsActive { get; set; }

		[JsonIgnore]
		public int OwnerId { get; set; }
		public string Owner { get; set; }
		public Geolocation Geolocation { get; set; }
	}
}
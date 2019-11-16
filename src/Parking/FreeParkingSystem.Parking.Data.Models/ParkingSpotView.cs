namespace FreeParkingSystem.Parking.Data.Models
{
	public class ParkingSpotView
	{
		public int Id { get; set; }
		public int Position { get; set; }
		public int Level { get; set; }
		public bool IsAvailable { get; set; }
		public ParkingSpotType ParkingSpotType { get; set; }
		public ParkingSiteView ParkingSite { get; set; }
	}
}
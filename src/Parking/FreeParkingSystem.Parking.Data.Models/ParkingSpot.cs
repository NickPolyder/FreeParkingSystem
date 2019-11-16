namespace FreeParkingSystem.Parking.Data.Models
{
	public class ParkingSpot
	{
		public int Id { get; set; }
		public int ParkingSiteId { get; set; }
		public int ParkingSpotTypeId { get; set; }
		public int Position { get; set; }
		public int Level { get; set; }
		public bool IsAvailable { get; set; }
		public ParkingSpotType ParkingSpotType { get; set; }
		public ParkingSite ParkingSite { get; set; }
	}
}
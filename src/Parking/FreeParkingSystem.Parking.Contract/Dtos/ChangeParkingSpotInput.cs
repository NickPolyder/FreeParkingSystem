using Newtonsoft.Json;

namespace FreeParkingSystem.Parking.Contract.Dtos
{
	public class ChangeParkingSpotInput
	{
		[JsonIgnore]
		public int Id { get; set; }
		[JsonIgnore]
		public int ParkingSiteId { get; set; }
		public int? ParkingSpotTypeId { get; set; }
		public int? Level { get; set; }
		public int? Position { get; set; }
		public bool? IsAvailable { get; set; }
	}
}
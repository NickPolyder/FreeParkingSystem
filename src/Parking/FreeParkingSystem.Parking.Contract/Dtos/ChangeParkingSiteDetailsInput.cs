using FreeParkingSystem.Parking.Data.Models;
using Newtonsoft.Json;

namespace FreeParkingSystem.Parking.Contract.Dtos
{
	public class ChangeParkingSiteDetailsInput
	{
		[JsonIgnore]
		public int ParkingSiteId { get; set; }
		public string Name { get; set; }
		public int ParkingTypeId { get; set; }
		public bool IsActive { get; set; }
		public Geolocation Geolocation { get; set; }
	}
}
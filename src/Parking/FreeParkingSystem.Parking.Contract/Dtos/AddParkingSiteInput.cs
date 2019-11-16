using FreeParkingSystem.Parking.Data.Models;

namespace FreeParkingSystem.Parking.Contract.Dtos
{
	public class AddParkingSiteInput
	{
		public string Name { get; set; }

		public int ParkingTypeId { get; set; }

		public Geolocation Geolocation { get; set; }
	}
}
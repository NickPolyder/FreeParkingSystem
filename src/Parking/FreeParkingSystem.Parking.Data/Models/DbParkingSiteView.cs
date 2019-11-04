using System.ComponentModel.DataAnnotations;
using FreeParkingSystem.Common.Data;

namespace FreeParkingSystem.Parking.Data.Models
{
	public class DbParkingSiteView : IEntity
	{
		[Key]
		public int Id { get; set; }
		public string Parking { get; set; }
		public int ParkingTypeId { get; set; }
		public string ParkingType { get; set; }
		public bool IsActive { get; set; }
		public string Owner { get; set; }
		public string GeolocationX { get; set; }
		public string GeolocationY { get; set; }
	}
}
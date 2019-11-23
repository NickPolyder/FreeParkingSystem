using System.ComponentModel.DataAnnotations;
using FreeParkingSystem.Common.Data.Models;

namespace FreeParkingSystem.Parking.Data.DatabaseModels
{
	public class DbParkingSpotView : IEntity
	{
		[Key]
		public int Id { get; set; }
		public int ParkingSiteId { get; set; }
		public string Parking { get; set; }
		public int ParkingTypeId { get; set; }
		public string ParkingType { get; set; }
		public bool IsActive { get; set; }
		public int OwnerId { get; set; }
		public string Owner { get; set; }
		public string GeolocationX { get; set; }
		public string GeolocationY { get; set; }
		public int ParkingSpotTypeId { get; set; }
		public int Position { get; set; }
		public int Level { get; set; }
		public bool IsAvailable { get; set; }
		public string ParkingSpotType { get; set; }
	}
}
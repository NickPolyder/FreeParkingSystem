using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FreeParkingSystem.Common.Data.Models;

namespace FreeParkingSystem.Parking.Data.DatabaseModels
{
	public class DbParkingSite : IEntity
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public int ParkingTypeId { get; set; }

		[Required]
		public bool IsActive { get; set; }

		[Required]
		public int OwnerId { get; set; }

		[Required]
		public string GeolocationX { get; set; }

		[Required]
		public string GeolocationY { get; set; }

		[ForeignKey(nameof(ParkingTypeId))]
		public virtual DbParkingType ParkingType { get; set; }
	}
}
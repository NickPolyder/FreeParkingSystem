using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FreeParkingSystem.Common.Data.Models;

namespace FreeParkingSystem.Parking.Data.DatabaseModels
{
	public class DbParkingSpot : IEntity
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public int ParkingSiteId { get; set; }

		[Required]
		public int ParkingSpotTypeId { get; set; }

		[Required]
		public int Position { get; set; }

		[Required]
		public int Level { get; set; }

		[Required]
		public bool IsAvailable { get; set; }

		[ForeignKey(nameof(ParkingSpotTypeId))]
		public virtual DbParkingSpotType ParkingSpotType { get; set; }

		[ForeignKey(nameof(ParkingSiteId))]
		public virtual DbParkingSite ParkingSite { get; set; }
	}
}
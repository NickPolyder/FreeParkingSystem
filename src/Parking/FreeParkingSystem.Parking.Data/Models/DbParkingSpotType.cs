using System.ComponentModel.DataAnnotations;
using FreeParkingSystem.Common.Data;

namespace FreeParkingSystem.Parking.Data.Models
{
	public class DbParkingSpotType : IEntity
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }
	}
}
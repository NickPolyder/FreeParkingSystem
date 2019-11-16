using System.ComponentModel.DataAnnotations;

using FreeParkingSystem.Common.Data.Models;

namespace FreeParkingSystem.Parking.Data.DatabaseModels
{
	public class DbParkingType : IEntity
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }
	}
}
using System.ComponentModel.DataAnnotations;
using FreeParkingSystem.Common.Data.Models;

namespace FreeParkingSystem.Common.Data.Tests.TestObjects
{
	public class TestEntity : IEntity
	{
		[Key]
		public int Id { get; set; }

		public string Name { get; set; }

	}
}
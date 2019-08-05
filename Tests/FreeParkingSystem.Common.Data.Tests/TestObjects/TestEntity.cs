using System.ComponentModel.DataAnnotations;

namespace FreeParkingSystem.Common.Data.Tests.TestObjects
{
	public class TestEntity : IEntity
	{
		[Key]
		public int Id { get; set; }

		public string Name { get; set; }

	}
}
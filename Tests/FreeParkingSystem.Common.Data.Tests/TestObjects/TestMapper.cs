using System.Collections.Generic;

namespace FreeParkingSystem.Common.Data.Tests.TestObjects
{
	public class TestMapper : IMap<TestEntity, TestBusinessObject>
	{
		public TestBusinessObject Map(TestEntity input, IDictionary<object, object> context)
		{
			return new TestBusinessObject
			{
				Id = input.Id,
				Name = input.Name
			};
		}

		public TestEntity Map(TestBusinessObject input, IDictionary<object, object> context)
		{
			return new TestEntity
			{
				Id = input.Id,
				Name = input.Name
			};
		}
	}
}
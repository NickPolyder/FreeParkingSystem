namespace FreeParkingSystem.Common.Data.Tests.TestObjects
{
	public class TestRepository :BaseRepository<TestBusinessObject,TestEntity>
	{
		public TestRepository(TestContext dbContext, IMap<TestEntity, TestBusinessObject> testMapper) : base(dbContext, testMapper)
		{
		}
	}
}
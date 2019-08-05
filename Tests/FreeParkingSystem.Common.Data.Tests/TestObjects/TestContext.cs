using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FreeParkingSystem.Common.Data.Tests.TestObjects
{
	public class TestContext : DbContext
	{
		public DbSet<TestEntity> TestEntities { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
		}
		
		public override void Dispose()
		{
			var entities = TestEntities.ToList();
			TestEntities.RemoveRange(entities);
			base.Dispose();
		}
	}
}
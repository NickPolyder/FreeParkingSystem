using Microsoft.EntityFrameworkCore;

namespace FreeParkingSystem.Orders.Data.Models
{
	public class OrdersDbContext : DbContext
	{

		public OrdersDbContext(DbContextOptions<OrdersDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<DbOrder>()
				.ToTable("order");

			modelBuilder.Entity<DbOrderView>()
				.ToTable("OrderView");
		}
	}
}
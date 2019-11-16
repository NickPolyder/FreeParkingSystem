using FreeParkingSystem.Parking.Data.DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace FreeParkingSystem.Parking.Data.Models
{
	public class ParkingDbContext : DbContext
	{

		public ParkingDbContext(DbContextOptions<ParkingDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<DbParkingType>()
				.ToTable("ParkingType");
			
			modelBuilder.Entity<DbParkingSpotType>()
				.ToTable("ParkingSpotType");

			modelBuilder.Entity<DbParkingSite>()
				.ToTable("ParkingSite");

			modelBuilder.Entity<DbParkingSpot>()
				.ToTable("ParkingSpot");

			modelBuilder.Entity<DbFavorite>()
				.ToTable("Favorites");

			modelBuilder.Entity<DbParkingSiteView>()
				.ToTable("ParkingSiteView");

			modelBuilder.Entity<DbParkingSpotView>()
				.ToTable("ParkingSpotView");
		}
	}
}
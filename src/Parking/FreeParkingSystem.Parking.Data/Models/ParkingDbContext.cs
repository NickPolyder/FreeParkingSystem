using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

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
		}

		public DbSet<DbParkingType> ParkingTypes { get; set; }

		public DbSet<DbParkingSpotType> ParkingSpotTypes { get; set; }

		public DbSet<DbParkingSite> ParkingSites { get; set; }

		public DbSet<DbParkingSpot> ParkingSpots { get; set; }
	}
}
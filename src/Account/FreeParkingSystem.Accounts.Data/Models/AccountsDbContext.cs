using Microsoft.EntityFrameworkCore;

namespace FreeParkingSystem.Accounts.Data.Models
{
	public class AccountsDbContext : DbContext
	{

		public AccountsDbContext(DbContextOptions<AccountsDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<DbUser>()
				.ToTable("User");

			modelBuilder.Entity<DbClaims>()
				.ToTable("Claims");
		}
	}
}
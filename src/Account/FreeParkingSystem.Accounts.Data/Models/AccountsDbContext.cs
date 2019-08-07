using Microsoft.EntityFrameworkCore;

namespace FreeParkingSystem.Accounts.Data.Models
{
	public class AccountsDbContext : DbContext
	{

		public AccountsDbContext(DbContextOptions<AccountsDbContext> options) : base(options)
		{
		}
		
		public DbSet<DbUser> Users { get; set; }

		public DbSet<DbClaims> Claims { get; set; }
	}
}
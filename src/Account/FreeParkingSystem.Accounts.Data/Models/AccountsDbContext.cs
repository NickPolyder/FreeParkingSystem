using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Options;

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
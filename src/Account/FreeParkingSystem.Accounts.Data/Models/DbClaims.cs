using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeParkingSystem.Accounts.Data.Models
{
	public class DbClaims
	{
		[Key]
		public int Id { get; set; }

		public int UserId { get; set; }

		[Required]
		public string ClaimType { get; set; }

		public string ClaimValue { get; set; }

		[ForeignKey(nameof(UserId))]
		public DbUser User { get; set; }

	}
}
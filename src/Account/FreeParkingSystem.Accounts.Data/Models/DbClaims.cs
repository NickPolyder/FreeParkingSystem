using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FreeParkingSystem.Common.Data;
using FreeParkingSystem.Common.Data.Models;

namespace FreeParkingSystem.Accounts.Data.Models
{
	public class DbClaims : IEntity
	{
		[Key]
		public int Id { get; set; }

		public int UserId { get; set; }

		[Required]
		public string ClaimType { get; set; }

		public string ClaimValue { get; set; }

		public DbUser User { get; set; }
	}
}
using System.Collections.Generic;
using System.Security.Claims;

namespace FreeParkingSystem.Accounts.Contract
{
	public class User
	{
		public int Id { get; set; }

		public string UserName { get; set; }

		public Password Password { get; set; }
		public ICollection<Claim> Claims { get; set; }
	}
}
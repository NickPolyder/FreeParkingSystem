using System.Collections.Generic;

namespace FreeParkingSystem.Accounts.Contract
{
	public class User
	{
		public int Id { get; set; }

		public string UserName { get; set; }

		public Password Password { get; set; }
		public ICollection<UserClaim> Claims { get; set; }
	}
}
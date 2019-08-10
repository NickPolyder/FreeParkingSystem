using System.Collections.Generic;
using Newtonsoft.Json;

namespace FreeParkingSystem.Accounts.Contract
{
	public class User
	{
		public int Id { get; set; }

		public string UserName { get; set; }
		
		[JsonIgnore]
		public Password Password { get; set; }
		public ICollection<UserClaim> Claims { get; set; }
	}
}
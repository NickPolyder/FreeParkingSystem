using System;
using System.Collections.Generic;
using System.Linq;
using FreeParkingSystem.Common.Authorization;
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

		public Role GetRole()
		{
			var role = Claims.FirstOrDefault(claim => claim.Type == UserClaimTypes.Role.ToString());
			return Enum.TryParse<Role>(role?.Value, true, out var result) ? result : Role.Anonymous;
		}
	}
}
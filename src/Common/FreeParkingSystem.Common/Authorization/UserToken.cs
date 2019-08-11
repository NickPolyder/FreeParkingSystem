using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace FreeParkingSystem.Common.Authorization
{
	public class UserToken
	{
		public static UserToken Empty { get; } = new UserToken
		{
			Claims = Array.Empty<Claim>(),
			Token = string.Empty,
			Username = string.Empty
		};

		public string Username { get; set; }

		public string Token { get; set; }

		public IEnumerable<Claim> Claims { get; set; }
	}
}
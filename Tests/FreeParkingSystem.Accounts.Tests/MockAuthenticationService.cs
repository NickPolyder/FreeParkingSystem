using System;
using System.Collections.Generic;
using System.Security.Claims;
using FreeParkingSystem.Common.Authorization;

namespace FreeParkingSystem.Accounts.Tests
{
	public class MockAuthenticationService : IAuthenticationServices
	{
		private Dictionary<string, UserToken> _cache = new Dictionary<string, UserToken>();
		public UserToken CreateToken(string username, IEnumerable<Claim> claims)
		{
			var token = new UserToken
			{
				Token = Guid.NewGuid().ToString(),
				Username = username,
				Claims = claims,
			};
			_cache.Add(token.Token, token);
			return token;
		}

		public bool VerifyToken(string token, out UserToken userToken)
		{
			userToken = new UserToken
			{
				Token = token
			};

			return !string.IsNullOrWhiteSpace(token) && _cache.TryGetValue(token, out userToken);
		}
	}
}
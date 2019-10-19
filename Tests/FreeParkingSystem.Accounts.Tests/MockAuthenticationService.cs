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
			var token = new UserToken(username, claims, Guid.NewGuid().ToString());
			_cache.Add(token.Token, token);
			return token;
		}

		public bool VerifyToken(string token, out UserToken userToken)
		{
			userToken = new UserToken(token);
			return !string.IsNullOrWhiteSpace(token) && _cache.TryGetValue(token, out userToken);
		}
	}
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using FreeParkingSystem.Common.ExtensionMethods;

namespace FreeParkingSystem.Common.Authorization
{
	public class UserToken
	{
		public static UserToken Empty { get; } = new UserToken();

		public string Username { get; }

		public string Token { get; }

		public IEnumerable<Claim> Claims { get; }

		public UserToken() : this(string.Empty, Array.Empty<Claim>())
		{ }

		public UserToken(string token) : this(string.Empty, Array.Empty<Claim>(), token)
		{ }

		public UserToken(string username, IEnumerable<Claim> claims, string token = null)
		{
			Username = username ?? string.Empty;
			Token = token ?? string.Empty;
			Claims = claims ?? Array.Empty<Claim>();
		}
		public TCast Get<TCast>(UserClaimTypes type) where TCast : struct
		{
			var claimType = type.ToSecurityClaimType();
			var claim = Claims.FirstOrDefault(item => item.Type == claimType)?.Value;
			var converter = TypeDescriptor.GetConverter(typeof(TCast));
			return !string.IsNullOrWhiteSpace(claim) && converter.CanConvertFrom(typeof(string))
				? (TCast)converter.ConvertFromString(claim)
				: default(TCast);
		}
	}
}
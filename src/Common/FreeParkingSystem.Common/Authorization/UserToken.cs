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
		public static UserToken Empty { get; } = new UserToken
		{
			Claims = Array.Empty<Claim>(),
			Token = string.Empty,
			Username = string.Empty
		};

		public string Username { get; set; }

		public string Token { get; set; }

		public IEnumerable<Claim> Claims { get; set; }

		public TCast Get<TCast>(UserClaimTypes type) where TCast : struct
		{
			var claimType = type.ToSecurityClaimType();
			var claim = Claims.FirstOrDefault(item => item.Type == claimType)?.Value;
			var converter = TypeDescriptor.GetConverter(typeof(TCast));
			return !string.IsNullOrWhiteSpace(claim) && converter.CanConvertFrom(typeof(string))
				? (TCast) converter.ConvertFromString(claim)
				: default(TCast);
		}
	}
}
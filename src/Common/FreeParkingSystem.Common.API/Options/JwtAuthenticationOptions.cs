using System;
using System.Collections.Generic;

namespace FreeParkingSystem.Common.API.Options
{
	public class JwtAuthenticationOptions
	{
		public bool ValidateIssuerSigningKey { get; set; } = true;

		public bool RequireHttpsMetadata { get; set; }

		public bool SaveToken { get; set; } = true;

		public bool ValidateIssuer { get; set; } = true;

		public bool ValidateAudience { get; set; } = true;

		public string ValidIssuer { get; set; }

		public string ValidAudience { get; set; }

		public IEnumerable<string> ValidAudiences { get; set; }

		public IEnumerable<string> ValidIssuers { get; set; }

		public byte[] Secret { get; set; }

		public TimeSpan ExpiresAfter { get; set; }

		public bool RequireExpirationTime { get; set; }
	}
}
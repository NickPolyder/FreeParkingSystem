using System;
using FreeParkingSystem.Common.Authorization;

namespace FreeParkingSystem.Common.ExtensionMethods
{
	public static class ClaimsExtensions
	{
		public static string ToSecurityClaimType(this UserClaimTypes claim)
		{
			switch (claim)
			{
				case UserClaimTypes.Email:
					return System.Security.Claims.ClaimTypes.Email;
				case UserClaimTypes.Role:
					return System.Security.Claims.ClaimTypes.Role;
				default:
					return claim.ToString();
			}
		}
	}
}
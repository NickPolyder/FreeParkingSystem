using System.Security.Claims;

namespace FreeParkingSystem.Accounts.Contract
{
	public static class ClaimExtensions
	{
		private const string ClaimIdKey = "claim-id";

		public static void SetId(this Claim claim, int value)
		{
			claim.Properties[ClaimIdKey] = value.ToString();
		}

		public static int GetId(this Claim claim)
		{
			if (claim.Properties.TryGetValue(ClaimIdKey, out var value) && int.TryParse(value, out var intValue))
			{
				return intValue;
			}

			return default;
		}
	}
}
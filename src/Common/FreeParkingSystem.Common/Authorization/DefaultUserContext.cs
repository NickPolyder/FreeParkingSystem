using System;
using System.Linq;
using System.Security.Claims;

namespace FreeParkingSystem.Common.Authorization
{
	public class DefaultUserContext : IUserContext
	{
		public UserToken UserToken { get; }

		public DefaultUserContext(UserToken userToken)
		{
			UserToken = userToken;
		}

		public bool HasRole(Role role)
		{
			var roleAsString = role.ToString();
			return UserToken.Claims.Any(claim =>
				claim.Type == ClaimTypes.Role &&
				claim.Value.Equals(roleAsString, StringComparison.InvariantCultureIgnoreCase));
		}

		public bool IsAuthenticated()
		{
			return UserToken == UserToken.Empty;
		}
	}
}
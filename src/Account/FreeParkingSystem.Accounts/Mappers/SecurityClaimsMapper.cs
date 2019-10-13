using System.Collections.Generic;
using System.Security.Claims;
using FreeParkingSystem.Accounts.Contract;
using FreeParkingSystem.Common;
using FreeParkingSystem.Common.Authorization;

namespace FreeParkingSystem.Accounts.Mappers
{
	public class SecurityClaimsMapper: IMap<UserClaim,Claim>
	{
		public Claim Map(UserClaim input, IDictionary<object, object> context)
		{
			var type = input.Type;
			
			if (type.Equals(UserClaimTypes.Email.ToString()))
			{
				type = System.Security.Claims.ClaimTypes.Email;
			}

			if (type.Equals(UserClaimTypes.Role.ToString()))
			{
				type = System.Security.Claims.ClaimTypes.Role;
			}

			return  new Claim(type, input.Value);
		}

		public UserClaim Map(Claim input, IDictionary<object, object> context)
		{
			var type = input.Type;
			if (type.Equals(System.Security.Claims.ClaimTypes.Email))
			{
				type = UserClaimTypes.Email.ToString();
			}

			if (type.Equals(System.Security.Claims.ClaimTypes.Role))
			{
				type = UserClaimTypes.Role.ToString();
			}
		
			return new UserClaim
			{
				Type = type,
				Value = input.Value
			};
		}
	}
}
using System.Collections.Generic;
using System.Security.Claims;
using FreeParkingSystem.Accounts.Contract;
using FreeParkingSystem.Accounts.Data.Models;
using FreeParkingSystem.Common;

namespace FreeParkingSystem.Accounts.Data.Mappers
{
	public class ClaimsMapper : IMap<DbClaims, UserClaim>
	{
		public UserClaim Map(DbClaims input, IDictionary<object, object> context)
		{
			if (input == null)
				return null;

			return new UserClaim
			{
				Id = input.Id,
				UserId =  input.UserId,
				Type =  input.ClaimType,
				Value = input.ClaimValue,
			};
		}

		public DbClaims ReverseMap(UserClaim input, IDictionary<object, object> context)
		{
			if (input == null)
				return null;
			
			return new DbClaims
			{
				Id = input.Id,
				UserId = input.UserId,
				ClaimType = input.Type,
				ClaimValue = input.Value
			};
		}
	}
}
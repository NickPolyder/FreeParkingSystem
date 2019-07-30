using System.Collections.Generic;
using System.Security.Claims;
using FreeParkingSystem.Accounts.Contract;
using FreeParkingSystem.Accounts.Data.Models;
using FreeParkingSystem.Common;

namespace FreeParkingSystem.Accounts.Data.Mappers
{
	public class ClaimsMapper : IMap<DbClaims, Claim>
	{
		public Claim Map(DbClaims input, IDictionary<object, object> context)
		{
			if (input == null)
				return null;

			var claim = new Claim(input.ClaimType, input.ClaimValue);
			claim.SetId(input.Id);
			return claim;
		}

		public DbClaims ReverseMap(Claim input, IDictionary<object, object> context)
		{
			if (input == null)
				return null;

			if (!(context.TryGetValue(typeof(User), out var resultUser) && resultUser is User user))
			{
				throw new MappingContextException(Contract.Resources.Validations.MappingContext_MissingUser);
			}

			return new DbClaims
			{
				Id = input.GetId(),
				UserId = user.Id,
				ClaimType = input.Type,
				ClaimValue = input.Value
			};
		}
	}
}
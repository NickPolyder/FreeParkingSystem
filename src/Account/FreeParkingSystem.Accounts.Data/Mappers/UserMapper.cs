using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using FreeParkingSystem.Accounts.Contract;
using FreeParkingSystem.Accounts.Data.Models;
using FreeParkingSystem.Common;
using FreeParkingSystem.Common.ExtensionMethods;

namespace FreeParkingSystem.Accounts.Data.Mappers
{
	public class UserMapper : IMap<DbUser, User>
	{
		private readonly IMap<DbClaims, UserClaim> _claimsMapper;

		public UserMapper(IMap<DbClaims, UserClaim> claimsMapper)
		{
			_claimsMapper = claimsMapper;
		}

		public User Map(DbUser input, IDictionary<object, object> context)
		{
			if (input == null)
				return null;

			return new User
			{
				Id = input.Id,
				UserName = input.UserName,
				Password = new Password(input.Password, input.Salt, true, true),
				Claims = _claimsMapper.Map(input.Claims).ToArray()
			};
		}

		public DbUser Map(User input, IDictionary<object, object> context)
		{
			if (input == null)
				return null;
			
			return new DbUser
			{
				Id = input.Id,
				UserName = input.UserName,
				Password = input.Password.ToString(),
				Salt = input.Password.Salt,
				Claims = _claimsMapper.Map(input.Claims).ToArray()
			};
		}

	}
}
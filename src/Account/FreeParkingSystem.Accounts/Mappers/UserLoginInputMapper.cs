using System;
using System.Collections.Generic;
using FreeParkingSystem.Accounts.Contract.Dtos;
using FreeParkingSystem.Accounts.Contract.Queries;
using FreeParkingSystem.Common;

namespace FreeParkingSystem.Accounts.Mappers
{
	public class UserLoginInputMapper: IMap<UserLoginInput, UserLoginRequest>
	{
		public UserLoginRequest Map(UserLoginInput input, IDictionary<object, object> context)
		{
			if(input == null)
				throw new ArgumentNullException(nameof(input));

			return new UserLoginRequest(input.UserName,input.Password);
		}

		public UserLoginInput Map(UserLoginRequest input, IDictionary<object, object> context)
		{
			if (input == null)
				throw new ArgumentNullException(nameof(input));

			return new UserLoginInput
			{
				UserName = input.Username,
				Password = input.Password
			};
		}
	}
}
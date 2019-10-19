using System;
using System.Collections.Generic;
using FreeParkingSystem.Accounts.Contract.Commands;
using FreeParkingSystem.Accounts.Contract.Dtos;
using FreeParkingSystem.Common;
using FreeParkingSystem.Common.Authorization;

namespace FreeParkingSystem.Accounts.Mappers
{
	public class CreateUserInputMapper: IMap<CreateUserInput, CreateUserRequest>
	{
		public CreateUserRequest Map(CreateUserInput input, IDictionary<object, object> context)
		{
			if (input == null)
				throw new ArgumentNullException(nameof(input));

			return new CreateUserRequest(input.UserName, input.Password,input.Email,Role.Member);
		}

		public CreateUserInput Map(CreateUserRequest input, IDictionary<object, object> context)
		{
			if (input == null)
				throw new ArgumentNullException(nameof(input));

			return new CreateUserInput
			{
				UserName = input.UserName,
				Password = input.Password,
				Email = input.Email
			};
		}
	}
}
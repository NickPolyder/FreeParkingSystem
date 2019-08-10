using System;
using System.Collections;
using System.Collections.Generic;
using FreeParkingSystem.Accounts.Contract;

namespace FreeParkingSystem.Accounts.Tests.Validators
{
	public class PasswordValidData : IEnumerable<object[]>
	{
		public IEnumerator<object[]> GetEnumerator()
		{
			yield return new object[]
			{
				new PasswordOptions(0,0,PasswordRequirements.Numbers),
				new Password("password1",false)
			};

			yield return new object[]
			{
				new PasswordOptions(0,0,PasswordRequirements.Numbers),
				new Password("password1",Guid.NewGuid().ToString(),false)
			};

			yield return new object[]
			{
				new PasswordOptions(0,0,PasswordRequirements.Capitals),
				new Password("passworD",false)
			};

			yield return new object[]
			{
				new PasswordOptions(0,0,PasswordRequirements.Special),
				new Password("p@ssword",false)
			};

			yield return new object[]
			{
				new PasswordOptions(0,0,PasswordRequirements.Numbers | PasswordRequirements.Capitals),
				new Password("Password1",false)
			};

			yield return new object[]
			{
				new PasswordOptions(0,0,PasswordRequirements.Special | PasswordRequirements.Capitals),
				new Password("p@ssWord",false)
			};

			yield return new object[]
			{
				new PasswordOptions(0,0,PasswordRequirements.Special | PasswordRequirements.Numbers),
				new Password("P@ssw0rd",false)
			};
			yield return new object[]
			{
				new PasswordOptions(0,0,PasswordRequirements.Capitals | PasswordRequirements.Numbers | PasswordRequirements.Special),
				new Password("p@ssWord1",false)
			};
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
using System;
using FreeParkingSystem.Accounts.Contract.User;
using FreeParkingSystem.Common;

namespace FreeParkingSystem.Accounts.User
{
	public class PasswordHasher : IHash<Password>
	{
		private const int RepeatHash = 10000;
		private readonly IHash<string> _stringHasher;

		public PasswordHasher(IHash<string> stringHasher)
		{
			_stringHasher = stringHasher;
		}

		public Password Hash(Password input)
		{
			if (input.IsHashed)
				return input;

			if (input.IsEncrypted)
				throw new ArgumentException();

			var stringPassword = input.ToString();

			for (int i = 0; i < RepeatHash; i++)
			{
				stringPassword = _stringHasher.Hash(stringPassword);
			}

			return new Password(stringPassword, input.Salt, true);
		}
	}
}
using FreeParkingSystem.Accounts.Contract;
using FreeParkingSystem.Accounts.Contract.Exceptions;
using FreeParkingSystem.Common;

namespace FreeParkingSystem.Accounts
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
				throw new PasswordEncryptionException(Contract.Resources.Validations.PasswordEncryption_EncryptedPasswordCannotBeHashed, input);

			var stringPassword = string.Format("{1}{0}{1}", input.ToString(), input.Salt);

			for (int i = 0; i < RepeatHash; i++)
			{
				stringPassword = _stringHasher.Hash(stringPassword);
			}

			return new Password(stringPassword, input.Salt, true);
		}
	}
}
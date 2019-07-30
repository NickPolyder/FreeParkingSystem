using FreeParkingSystem.Accounts.Contract;
using FreeParkingSystem.Common;

namespace FreeParkingSystem.Accounts.User
{
	public class PasswordEncryptor : IEncrypt<Password>
	{
		private readonly IEncrypt<string> _stringEncryptor;

		public PasswordEncryptor(IEncrypt<string> stringEncryptor)
		{
			_stringEncryptor = stringEncryptor;
		}
		public Password Encrypt(Password input)
		{
			if (input.IsEncrypted)
				return input;

			var encryptedPassword = _stringEncryptor.Encrypt(input.ToString());

			return new Password(encryptedPassword, input.Salt, input.IsHashed,true);
		}

		public Password Decrypt(Password input)
		{
			if (!input.IsEncrypted)
				return input;

			var decryptedPassword = _stringEncryptor.Decrypt(input.ToString());

			return new Password(decryptedPassword, input.Salt, input.IsHashed, false);
		}
	}
}
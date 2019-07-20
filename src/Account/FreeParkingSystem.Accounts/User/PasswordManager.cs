using System;
using System.Security.Cryptography;
using FreeParkingSystem.Accounts.Contract.User;
using FreeParkingSystem.Common;

namespace FreeParkingSystem.Accounts
{
	public class PasswordManager : IPasswordManager
	{
		private readonly IValidate<Password> _passwordValidator;
		private readonly IHash<Password> _passwordHasher;
		private readonly IEncrypt<Password> _passwordEncrypter;

		public PasswordManager(IValidate<Password> passwordValidator, IHash<Password> passwordHasher, IEncrypt<Password> passwordEncrypter)
		{
			_passwordValidator = passwordValidator;
			_passwordHasher = passwordHasher;
			_passwordEncrypter = passwordEncrypter;
		}
		public Password Create(string password)
		{
			var salt = GenerateSalt();
			var pass = new Password(password, salt);

			_passwordValidator.Validate(pass);
			var hashedPassword = _passwordHasher.Hash(pass);
			var encryptedPassword = _passwordEncrypter.Encrypt(hashedPassword);

			return encryptedPassword;
		}

		public bool Verify(Password password, Password otherPassword)
		{
			Password decryptedPassword = password.IsEncrypted 
				? _passwordEncrypter.Decrypt(password) 
				: password;

			Password decryptedOtherPassword = otherPassword.IsEncrypted
				? _passwordEncrypter.Decrypt(otherPassword)
				: otherPassword;

			return decryptedPassword.Equals(decryptedOtherPassword);
		}


		private string GenerateSalt()
		{
			var buffer = new byte[128];
			RandomNumberGenerator.Create().GetBytes(buffer);
			return System.Text.Encoding.UTF8.GetString(buffer);
		}


	}
}
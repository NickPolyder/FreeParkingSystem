﻿using System.Security.Cryptography;
using FreeParkingSystem.Accounts.Contract;
using FreeParkingSystem.Common;

namespace FreeParkingSystem.Accounts
{
	public class PasswordManager : IPasswordManager
	{
		private readonly IValidate<Password> _passwordValidator;
		private readonly IHash<Password> _passwordHasher;
		private readonly IEncrypt<Password> _passwordEncryptor;

		public PasswordManager(IValidate<Password> passwordValidator, IHash<Password> passwordHasher, IEncrypt<Password> passwordEncryptor)
		{
			_passwordValidator = passwordValidator;
			_passwordHasher = passwordHasher;
			_passwordEncryptor = passwordEncryptor;
		}
		public Password Create(string password)
		{
			var salt = GenerateSalt();
			var pass = new Password(password, salt);

			_passwordValidator.Validate(pass);
			var hashedPassword = _passwordHasher.Hash(pass);
			var encryptedPassword = _passwordEncryptor.Encrypt(hashedPassword);

			return encryptedPassword;
		}

		public bool Verify(Password password, Password otherPassword)
		{
			Password decryptedPassword = password.IsEncrypted 
				? _passwordEncryptor.Decrypt(password) 
				: password;

			Password decryptedOtherPassword = otherPassword.IsEncrypted
				? _passwordEncryptor.Decrypt(otherPassword)
				: otherPassword;

			Password hashedPassword = decryptedPassword.IsHashed
				? decryptedPassword
				: _passwordHasher.Hash(decryptedPassword);

			Password otherHashedPassword = decryptedOtherPassword.IsHashed 
				? decryptedOtherPassword
				: _passwordHasher.Hash(decryptedOtherPassword);

			return hashedPassword.Equals(otherHashedPassword);
		}


		private byte[] GenerateSalt()
		{
			var buffer = new byte[32];
			RandomNumberGenerator.Create().GetBytes(buffer);
			return buffer;
		}


	}
}
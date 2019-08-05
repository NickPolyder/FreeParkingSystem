using System;

namespace FreeParkingSystem.Accounts.Contract.Exceptions
{
	public class PasswordEncryptionException : PasswordException
	{
		public PasswordEncryptionException()
		{
		}

		public PasswordEncryptionException(Password password) : base(password)
		{

		}
		public PasswordEncryptionException(string message) : base(message)
		{
		}

		public PasswordEncryptionException(string message, Password password) : base(message, password)
		{
		}

		public PasswordEncryptionException(string message, Exception innerException) : base(message, innerException)
		{
		}

		public PasswordEncryptionException(string message, Password password, Exception innerException) : base(message, password, innerException)
		{
		}
	}
}
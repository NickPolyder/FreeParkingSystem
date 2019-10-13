using System;
using System.Diagnostics.CodeAnalysis;

namespace FreeParkingSystem.Accounts.Contract.Exceptions
{
	[ExcludeFromCodeCoverage]
	public class PasswordValidationException : PasswordException
	{
		public PasswordValidationException()
		{
		}

		public PasswordValidationException(Password password) : base(password)
		{
		}

		public PasswordValidationException(Password password, Exception innerException) : base(password, innerException)
		{
		}

		public PasswordValidationException(string message) : base(message)
		{
		}

		public PasswordValidationException(string message, Password password) : base(message, password, null)
		{
		}

		public PasswordValidationException(string message, Exception innerException) : base(message, innerException)
		{
		}

		public PasswordValidationException(string message, Password password, Exception innerException) : base(message, innerException)
		{
			Password = password;
		}
	}
}
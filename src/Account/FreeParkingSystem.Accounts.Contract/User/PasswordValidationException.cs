using System;
using FreeParkingSystem.Accounts.Contract.Resources;

namespace FreeParkingSystem.Accounts.Contract.User
{
	public class PasswordValidationException : Exception
	{
		public Password Password
		{
			get => Data.Contains(nameof(Password)) && Data[nameof(Password)] is Password value ? value : default;
			set => Data[nameof(Password)] = value;
		}
		public PasswordValidationException():this(Validations.PasswordValidation_GeneralMessage)
		{
		}

		public PasswordValidationException(Password password):this(Validations.PasswordValidation_GeneralMessage, password)
		{
		}

		public PasswordValidationException(string message) : base(message)
		{
		}

		public PasswordValidationException(string message, Password password) : this(message, password, null)
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
using System;
using System.Diagnostics.CodeAnalysis;
using FreeParkingSystem.Accounts.Contract.Resources;
using FreeParkingSystem.Common;

namespace FreeParkingSystem.Accounts.Contract.Exceptions
{
	[ExcludeFromCodeCoverage]
	public class PasswordException: ValidationException
	{
		public Password Password
		{
			get => Data.Contains(nameof(Password)) && Data[nameof(Password)] is Password value ? value : default;
			set => Data[nameof(Password)] = value;
		}
		public PasswordException() : this(Validations.Password_GeneralMessage)
		{
		}

		public PasswordException(Password password) : this(Validations.Password_GeneralMessage, password)
		{
		}

		public PasswordException(Password password,Exception innerException) : this(Validations.Password_GeneralMessage, password, innerException)
		{
		}

		public PasswordException(string message) : base(message)
		{
		}

		public PasswordException(string message, Password password) : this(message, password, null)
		{
		}

		public PasswordException(string message, Exception innerException) : base(message, innerException)
		{
		}

		public PasswordException(string message, Password password, Exception innerException) : base(message, innerException)
		{
			Password = password;
		}
	}
}
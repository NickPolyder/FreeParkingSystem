using System;
using System.Diagnostics.CodeAnalysis;
using FreeParkingSystem.Accounts.Contract.Resources;

namespace FreeParkingSystem.Accounts.Contract.Exceptions
{
	[ExcludeFromCodeCoverage]
	public abstract class PasswordException: Exception
	{
		public Password Password
		{
			get => Data.Contains(nameof(Password)) && Data[nameof(Password)] is Password value ? value : default;
			set => Data[nameof(Password)] = value;
		}
		protected PasswordException() : this(Validations.Password_GeneralMessage)
		{
		}

		protected PasswordException(Password password) : this(Validations.Password_GeneralMessage, password)
		{
		}

		protected PasswordException(Password password,Exception innerException) : this(Validations.Password_GeneralMessage, password, innerException)
		{
		}

		protected PasswordException(string message) : base(message)
		{
		}

		protected PasswordException(string message, Password password) : this(message, password, null)
		{
		}

		protected PasswordException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected PasswordException(string message, Password password, Exception innerException) : base(message, innerException)
		{
			Password = password;
		}
	}
}
using FreeParkingSystem.Accounts.Contract.Resources;
using FreeParkingSystem.Accounts.Contract.User;
using FreeParkingSystem.Common;
using FreeParkingSystem.Common.ExtensionMethods;

namespace FreeParkingSystem.Accounts.Validators
{
	public class PasswordValidator : IValidate<Password>
	{
		private const string _numbers = "0123456789";
		private const string _alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		private readonly PasswordOptions _options;

		public PasswordValidator(PasswordOptions options)
		{
			_options = options;
		}

		public void Validate(Password password)
		{
			if (password.IsEncrypted)
			{
				throw new PasswordValidationException(Validations.PasswordValidation_CannotValidateEncryptedPassword, password);
			}

			var stringPassword = password.ToString();

			if (string.IsNullOrWhiteSpace(stringPassword))
			{
				throw new PasswordValidationException(Validations.PasswordValidation_EmptyPassword, password);
			}

			ValidatePasswordLength(password, stringPassword);

			ValidateRequiredNumbers(password, stringPassword);

			ValidateRequiredCapitals(password, stringPassword);
		}

		private void ValidateRequiredCapitals(Password password, string stringPassword)
		{
			if (_options.Requirements == PasswordRequirements.Capitals)
			{
				bool hasCapitals = false;
				foreach (var character in _alphabet)
				{
					if (stringPassword.IndexOf(character) > -1)
					{
						hasCapitals = true;
						break;
					}
				}

				if (!hasCapitals)
				{
					throw new PasswordValidationException(Validations.PasswordValidation_CapitalsRequired, password);
				}
			}
		}

		private void ValidatePasswordLength(Password password, string stringPassword)
		{
			if (_options.MinimumCharacters <= stringPassword.Length)
			{
				throw new PasswordValidationException(
					Validations.PasswordValidation_MinimumCharacterRequired.WithArgs(_options.MinimumCharacters), password);
			}

			if (_options.MaximumCharacters >= stringPassword.Length)
			{
				throw new PasswordValidationException(
					Validations.PasswordValidation_MaximumCharacterRequired.WithArgs(_options.MaximumCharacters), password);
			}
		}

		private void ValidateRequiredNumbers(Password password, string stringPassword)
		{
			if (_options.Requirements == PasswordRequirements.Numbers)
			{
				bool hasNumber = false;
				foreach (var number in _numbers)
				{
					if (stringPassword.IndexOf(number) > -1)
					{
						hasNumber = true;
						break;
					}
				}

				if (!hasNumber)
				{
					throw new PasswordValidationException(Validations.PasswordValidation_NumbersRequired, password);
				}
			}
		}
	}
}
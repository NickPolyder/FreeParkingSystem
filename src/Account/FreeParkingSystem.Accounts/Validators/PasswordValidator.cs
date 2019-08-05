using FreeParkingSystem.Accounts.Contract.Resources;
using FreeParkingSystem.Accounts.Contract;
using FreeParkingSystem.Accounts.Contract.Exceptions;
using FreeParkingSystem.Common;
using FreeParkingSystem.Common.ExtensionMethods;

namespace FreeParkingSystem.Accounts.Validators
{
	public class PasswordValidator : IValidate<Password>
	{
		private const string Numbers = "0123456789";
		private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		private const string DefaultSpecialCharacters = " !\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~";

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
			
			ValidatePasswordLength(password, stringPassword);

			ValidateRequiredNumbers(password, stringPassword);

			ValidateRequiredCapitals(password, stringPassword);

			ValidateRequiredSpecials(password, stringPassword);
		}

		private void ValidateRequiredSpecials(Password password, string stringPassword)
		{
			if (_options.Requirements.HasFlag(PasswordRequirements.Special))
			{
				string allowedSpecialCharacters = !string.IsNullOrWhiteSpace(_options.AllowedSpecialCharacters)
					? _options.AllowedSpecialCharacters
					: DefaultSpecialCharacters;

				bool hasSpecialCharacters = false;
				foreach (var character in allowedSpecialCharacters)
				{
					if (stringPassword.IndexOf(character) > -1)
					{
						hasSpecialCharacters = true;
						break;
					}
				}

				if (!hasSpecialCharacters)
				{
					throw new PasswordValidationException(Validations.PasswordValidation_SpecialsRequired, password);
				}
			}
		}

		private void ValidateRequiredCapitals(Password password, string stringPassword)
		{
			if (_options.Requirements.HasFlag(PasswordRequirements.Capitals))
			{
				bool hasCapitals = false;
				foreach (var character in Alphabet)
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

		private void ValidateRequiredNumbers(Password password, string stringPassword)
		{
			if (_options.Requirements.HasFlag(PasswordRequirements.Numbers))
			{
				bool hasNumber = false;
				foreach (var number in Numbers)
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

		private void ValidatePasswordLength(Password password, string stringPassword)
		{
			if (_options.MinimumCharacters > 0 && stringPassword.Length <= _options.MinimumCharacters)
			{
				throw new PasswordValidationException(
					Validations.PasswordValidation_MinimumCharacterRequired.WithArgs(_options.MinimumCharacters), password);
			}

			if (_options.MaximumCharacters > 0 && stringPassword.Length > _options.MaximumCharacters)
			{
				throw new PasswordValidationException(
					Validations.PasswordValidation_MaximumCharacterRequired.WithArgs(_options.MaximumCharacters), password);
			}
		}
	}
}
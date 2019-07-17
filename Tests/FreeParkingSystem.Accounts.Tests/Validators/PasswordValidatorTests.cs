using FreeParkingSystem.Accounts.Contract.User;
using FreeParkingSystem.Accounts.Validators;
using FreeParkingSystem.Testing;
using Shouldly;
using Xunit;

namespace FreeParkingSystem.Accounts.Tests.Validators
{
	public class PasswordValidatorTests
	{
		[Theory, FixtureData]
		public void WhenPasswordIsEncrypted_ShouldThrowPasswordValidationException(
			string passwordString,
			PasswordValidator sut)
		{
			// Arrange
			var password = new Password(passwordString, true,true);

			// Act
			var exception = Record.Exception(() => sut.Validate(password));
			
			// Assert
			exception.ShouldNotBeNull();
			var passwordValidationException = exception.ShouldBeOfType<PasswordValidationException>();
			passwordValidationException.Password.ShouldBe(password);
			passwordValidationException.Message.ShouldBe(Contract.Resources.Validations.PasswordValidation_CannotValidateEncryptedPassword);

		}

		[Theory, ClassFixtureData(typeof(PasswordInvalidData))]
		public void ShouldThrowPasswordValidationException(
			PasswordOptions options,
			Password password,
			string expectedMessage)
		{
			// Arrange
			var sut = new PasswordValidator(options);

			// Act
			var exception = Record.Exception(() => sut.Validate(password));

			// Assert
			exception.ShouldNotBeNull();
			var passwordValidationException = exception.ShouldBeOfType<PasswordValidationException>();
			passwordValidationException.Password.ShouldBe(password);
			passwordValidationException.Message.ShouldBe(expectedMessage);

		}

		[Theory, ClassFixtureData(typeof(PasswordValidData))]
		public void ShouldPassTheValidation(
			PasswordOptions options,
			Password password)
		{
			// Arrange
			var sut = new PasswordValidator(options);

			// Act
			var exception = Record.Exception(() => sut.Validate(password));

			// Assert
			exception.ShouldBeNull();

		}
	}
}
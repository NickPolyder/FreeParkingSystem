using AutoFixture;
using AutoFixture.Xunit;
using FreeParkingSystem.Accounts.Contract;
using FreeParkingSystem.Accounts.Contract.Exceptions;
using FreeParkingSystem.Accounts.Contract.Options;
using FreeParkingSystem.Accounts.Validators;
using FreeParkingSystem.Common;
using FreeParkingSystem.Common.Encryption;
using FreeParkingSystem.Common.Hashing;
using FreeParkingSystem.Testing;
using Moq;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace FreeParkingSystem.Accounts.Tests
{
	public class PasswordManagerTests
	{
		private readonly ITestOutputHelper _testOutputHelper;


		public PasswordManagerTests(ITestOutputHelper testOutputHelper)
		{
			_testOutputHelper = testOutputHelper;

		}

		private static void ContainerSetup(IFixture fixture)
		{
			var secretKey = TestConstants.SecretKey;

			fixture.Build<EncryptionOptions>()
				.FromFactory(() => new EncryptionOptions(secretKey))
				.ToCustomization()
				.Customize(fixture);

			fixture.Build<IEncrypt<byte[]>>()
				.FromFactory((EncryptionOptions options) => new AesByteEncryptor(options))
				.ToCustomization()
				.Customize(fixture);

			fixture.Build<IEncrypt<string>>()
				.FromFactory((IEncrypt<byte[]> byteEncryptor) => new AesStringEncryptor(byteEncryptor))
				.ToCustomization()
				.Customize(fixture);
			fixture.Build<IEncrypt<Password>>()
				.FromFactory((IEncrypt<string> stringEncryptor) => new PasswordEncryptor(stringEncryptor))
				.ToCustomization()
				.Customize(fixture);

			fixture.Build<IValidate<Password>>()
				.FromFactory(() => new PasswordValidator(new PasswordOptions(TestConstants.MinimumCharacters, TestConstants.MaximumCharacters, TestConstants.DefaultPasswordRequirements)))
				.ToCustomization()
				.Customize(fixture);

			ContainerHashSetup(fixture);
		}

		private static void ContainerHashSetup(IFixture fixture)
		{
			fixture.Build<IHash<byte[]>>()
				.FromFactory(() => new ShaByteHasher())
				.ToCustomization()
				.Customize(fixture);

			fixture.Build<IHash<string>>()
				.FromFactory((IHash<byte[]> byteHash) => new ShaStringHasher(byteHash))
				.ToCustomization()
				.Customize(fixture);

			fixture.Build<IHash<Password>>()
				.FromFactory((IHash<string> stringHash) => new PasswordHasher(stringHash))
				.ToCustomization()
				.Customize(fixture);
		}
		
		[Theory]
		[InlineFixtureData(null, RunContainerSetup = false)]
		[InlineFixtureData("", RunContainerSetup = false)]
		[InlineFixtureData(" ", RunContainerSetup = false)]
		public void Create_WhenPasswordIsNullOrEmpty_ShouldThrowValidationException(
			string password,
			PasswordManager sut)
		{
			// Act
			var exception = Record.Exception(() => sut.Create(password));

			// Assert
			exception.ShouldNotBeNull();
			exception.ShouldBeOfType<PasswordValidationException>();
			exception.Message.ShouldBe(Contract.Resources.Validations.PasswordValidation_EmptyPassword);
		}

		[Theory, FixtureData(RunContainerSetup = false)]
		public void Create_ShouldGenerateSalt(
			[Frozen] Mock<IValidate<Password>> passwordValidatorMock,
			[Frozen] Mock<IHash<Password>> passwordHashMock,
			[Frozen] Mock<IEncrypt<Password>> passwordEncryptorMock,
			string password,
			PasswordManager sut)
		{
			// Assign
			passwordHashMock.Setup(hash => hash.Hash(It.IsAny<Password>())).Returns(new Password(password));
			passwordEncryptorMock.Setup(encryptor => encryptor.Encrypt(It.IsAny<Password>())).Returns(new Password(password));

			// Act
			sut.Create(password);

			// Assert
			passwordValidatorMock.Verify(svc =>
				svc.Validate(It.Is<Password>(pass => !string.IsNullOrWhiteSpace(pass.Salt) &&
													 pass.ToString().Contains(password))), Times.Once);
		}

		[Theory, FixtureData(RunContainerSetup = false)]
		public void Create_ShouldCallValidate(
			[Frozen] Mock<IValidate<Password>> passwordValidatorMock,
			[Frozen] Mock<IHash<Password>> passwordHashMock,
			[Frozen] Mock<IEncrypt<Password>> passwordEncryptorMock,
			string password,
			PasswordManager sut)
		{
			// Assign
			passwordHashMock.Setup(hash => hash.Hash(It.IsAny<Password>())).Returns(new Password(password));
			passwordEncryptorMock.Setup(encryptor => encryptor.Encrypt(It.IsAny<Password>())).Returns(new Password(password));

			// Act
			sut.Create(password);

			// Assert
			passwordValidatorMock.Verify(svc => svc.Validate(It.IsAny<Password>()), Times.Once);
		}

		[Theory, FixtureData(RunContainerSetup = false)]
		public void Create_WhenValidateThrows_ShouldLetItThrow(
			[Frozen] Mock<IValidate<Password>> passwordValidatorMock,
			string password,
			PasswordManager sut)
		{
			// Arrange
			passwordValidatorMock
				.Setup(svc => svc.Validate(It.IsAny<Password>()))
				.Throws<PasswordValidationException>();

			// Act
			var exception = Record.Exception(() => sut.Create(password));

			// Assert
			exception.ShouldNotBeNull();
			exception.ShouldBeOfType<PasswordValidationException>();
		}

		[Theory, FixtureData(RunContainerSetup = false)]
		public void Create_ShouldCall_Hash(
			[Frozen] Mock<IHash<Password>> passwordHashMock,
			[Frozen] Mock<IEncrypt<Password>> passwordEncryptorMock,
			string password,
			PasswordManager sut)
		{
			// Assign
			passwordHashMock.Setup(hash => hash.Hash(It.IsAny<Password>())).Returns(new Password(password));
			passwordEncryptorMock.Setup(encryptor => encryptor.Encrypt(It.IsAny<Password>())).Returns(new Password(password));

			// Act
			sut.Create(password);

			// Assert
			passwordHashMock.Verify(svc => svc.Hash(It.IsAny<Password>()), Times.Once);
		}

		[Theory, FixtureData(ContainerMethod = "ContainerHashSetup")]
		public void Create_WhenCallingEncrypt_ShouldHaveHashedPassword(
			[Frozen] Mock<IEncrypt<Password>> passwordEncryptorMock,
			string password,
			PasswordManager sut)
		{
			// Assign
			passwordEncryptorMock.Setup(encryptor => encryptor.Encrypt(It.IsAny<Password>())).Returns(new Password(password,true));

			// Act
			sut.Create(password);

			// Assert
			passwordEncryptorMock.Verify(svc => svc.Encrypt(It.Is<Password>(pass => pass.IsHashed)), Times.Once);
		}

		[Theory, FixtureData]
		public void Create_ShouldReturnTheEncryptedPassword(
			string password,
			PasswordManager sut)
		{
			// Act
			var result = sut.Create(password);
			_testOutputHelper.WriteLine($"password: {password} = {result}");

			// Assert
			result.Salt.ShouldNotBeNull();
			result.IsHashed.ShouldBeTrue();
			result.IsEncrypted.ShouldBeTrue();
			result.ToString().ShouldNotBe(password);
		}


		[Theory, FixtureData(RunContainerSetup = false)]
		public void Verify_WhenPasswordIsEncrypted_ShouldCallDecrypt(
			[Frozen] Mock<IEncrypt<Password>> passwordEncryptorMock,
			string passwordString,
			string otherPasswordString,
			PasswordManager sut)
		{
			// Arrange
			var password = new Password(passwordString, true, true);
			var otherPassword = new Password(otherPasswordString, true, false);

			passwordEncryptorMock.Setup(svc => svc.Decrypt(password))
				.Returns(new Password(passwordString, true, false));

			// Act
			sut.Verify(password, otherPassword);

			// Assert
			passwordEncryptorMock.Verify(svc => svc.Decrypt(password), Times.Once);
		}

		[Theory, FixtureData(RunContainerSetup = false)]
		public void Verify_WhenPasswordIsNotHashed_ShouldCallHash(
			[Frozen] Mock<IHash<Password>> passwordHasherMock,
			string passwordString,
			string otherPasswordString,
			PasswordManager sut)
		{
			// Arrange
			var password = new Password(passwordString, false, false);
			var otherPassword = new Password(otherPasswordString, true, false);

			passwordHasherMock.Setup(svc => svc.Hash(password))
				.Returns(new Password(passwordString, true, false));

			// Act
			sut.Verify(password, otherPassword);

			// Assert
			passwordHasherMock.Verify(svc => svc.Hash(password), Times.Once);
		}

		[Theory, FixtureData(RunContainerSetup = false)]
		public void Verify_WhenOtherPasswordIsEncrypted_ShouldCallDecrypt(
			[Frozen] Mock<IEncrypt<Password>> passwordEncryptorMock,
			string passwordString,
			string otherPasswordString,
			PasswordManager sut)
		{
			// Arrange
			var password = new Password(passwordString, true, false);
			var otherPassword = new Password(otherPasswordString, true, true);

			passwordEncryptorMock.Setup(svc => svc.Decrypt(otherPassword))
				.Returns(new Password(otherPasswordString, true, false));

			// Act
			sut.Verify(password, otherPassword);

			// Assert
			passwordEncryptorMock.Verify(svc => svc.Decrypt(otherPassword), Times.Once);
		}

		[Theory, FixtureData(RunContainerSetup = false)]
		public void Verify_WhenOtherPasswordIsNotHashed_ShouldCallHash(
			[Frozen] Mock<IHash<Password>> passwordHasherMock,
			string passwordString,
			string otherPasswordString,
			PasswordManager sut)
		{
			// Arrange
			var password = new Password(passwordString, true, false);
			var otherPassword = new Password(otherPasswordString, false, false);

			passwordHasherMock.Setup(svc => svc.Hash(otherPassword))
				.Returns(new Password(otherPasswordString, true, false));

			// Act
			sut.Verify(password, otherPassword);

			// Assert
			passwordHasherMock.Verify(svc => svc.Hash(otherPassword), Times.Once);
		}

		[Theory, FixtureData(RunContainerSetup = false)]
		public void Verify_WhenBothPasswordAreEncrypted_ShouldCallDecrypt(
			[Frozen] Mock<IEncrypt<Password>> passwordEncryptorMock,
			string passwordString,
			string otherPasswordString,
			PasswordManager sut)
		{
			// Arrange
			var password = new Password(passwordString, true, true);
			var otherPassword = new Password(otherPasswordString, true, true);

			passwordEncryptorMock.Setup(svc => svc.Decrypt(password))
				.Returns(new Password(otherPasswordString, true, true));

			passwordEncryptorMock.Setup(svc => svc.Decrypt(otherPassword))
				.Returns(new Password(otherPasswordString, true, false));

			// Act
			sut.Verify(password, otherPassword);

			// Assert
			passwordEncryptorMock.Verify(svc => svc.Decrypt(password), Times.Once);
			passwordEncryptorMock.Verify(svc => svc.Decrypt(otherPassword), Times.Once);
		}

		[Theory, FixtureData(RunContainerSetup = false)]
		public void Verify_WhenPasswordsAreDifferent_ShouldReturnFalse(
			string passwordString,
			string otherPasswordString,
			PasswordManager sut)
		{
			// Arrange
			var password = new Password(passwordString, true, false);
			var otherPassword = new Password(otherPasswordString, true, false);


			// Act
			var result = sut.Verify(password, otherPassword);

			// Assert
			result.ShouldBeFalse();
		}

		[Theory, FixtureData(RunContainerSetup = false)]
		public void Verify_WhenPasswordsAreTheSame_ShouldReturnTrue(
			string passwordString,
			PasswordManager sut)
		{
			// Arrange
			var password = new Password(passwordString, true, false);
			var otherPassword = new Password(passwordString, true, false);


			// Act
			var result = sut.Verify(password, otherPassword);

			// Assert
			result.ShouldBeTrue();
		}

		[Theory]
		[InlineFixtureData("oVlHZtjAwFq3pb+nK92TiW86wo+TEeeUFTPTvw2g/dHBGNI3VdZsHKG7VwzjHqcKveJSktDrFn2Tkfst939BpV4vCmEofBSzwsmvrPQ3ens=", "oVlHZtjAwFq3pb+nK92TiW86wo+TEeeUFTPTvw2g/dHBGNI3VdZsHKG7VwzjHqcKveJSktDrFn2Tkfst939BpV4vCmEofBSzwsmvrPQ3ens=", true)]
		[InlineFixtureData("dEdBOMHKQEbadEQk1n1HL/XDOPJiihl3PLA36u+lUemwYCVy1kh8BZ2Z88lcMYbuHGu7Jc1A1dtrQsLH3qMROZiwTe/NTfrg5zGSr+77SFY=", "oVlHZtjAwFq3pb+nK92TiW86wo+TEeeUFTPTvw2g/dHBGNI3VdZsHKG7VwzjHqcKveJSktDrFn2Tkfst939BpV4vCmEofBSzwsmvrPQ3ens=", true)]
		[InlineFixtureData("oVlHZtjAwFq3pb+nK92TiW86wo+TEeeUFTPTvw2g/dHBGNI3VdZsHKG7VwzjHqcKveJSktDrFn2Tkfst939BpV4vCmEofBSzwsmvrPQ3ens=", " mWVU5efJCkUS5B9GEH1qlX5d+yNXLmcIaix4XyFZMixFOM+XGAWL3F2B4BwlQEV4cI61NoMskWxDNTv6197Sdp0nutA/lwHFG371zOiE8VE=", false)]
		[InlineFixtureData("/D9RqqGDwM7heqLR567XzcXaYhgzs7awkWgtGgQzEkQdZsrHbvKe8h9Gi3+ufEyp2cFm9teSNwJHsj7l+jgY8lFi1sFTWfLjoFNHuFnDKIY=", " mWVU5efJCkUS5B9GEH1qlX5d+yNXLmcIaix4XyFZMixFOM+XGAWL3F2B4BwlQEV4cI61NoMskWxDNTv6197Sdp0nutA/lwHFG371zOiE8VE=", false)]
		public void Verify_ShouldVerifyPasswords(
			string passwordString,
			string otherPasswordString,
			bool expected,
			PasswordManager sut)
		{
			// Arrange
			var password = new Password(passwordString, true, true);
			var otherPassword = new Password(otherPasswordString, true, true);


			// Act
			var result = sut.Verify(password, otherPassword);

			// Assert
			result.ShouldBe(expected);
		}
	}
}
using AutoFixture;
using AutoFixture.Xunit;
using FreeParkingSystem.Accounts.Contract;
using FreeParkingSystem.Accounts;
using FreeParkingSystem.Common;
using FreeParkingSystem.Common.Encryption;
using FreeParkingSystem.Testing;
using Moq;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace FreeParkingSystem.Accounts.Tests.User
{
	public class PasswordEncryptorTests
	{
		private readonly ITestOutputHelper _testOutputHelper;


		public PasswordEncryptorTests(ITestOutputHelper testOutputHelper)
		{
			_testOutputHelper = testOutputHelper;

		}

		private static void ContainerSetup(IFixture fixture)
		{
			var secretKey = new byte[32]
			{
				237, 201, 222, 52, 18, 152, 49, 135, 198, 143, 48, 247, 22, 185, 5, 216, 43, 6, 37, 243, 13, 52, 149, 119, 74, 104, 70, 130, 246, 76, 231, 147
			};
			
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
		}

		[Theory, FixtureData(RunContainerSetup = false)]
		public void Encrypt_WhenPasswordIsAlreadyEncrypted_ShouldReturnImmediately(
			string passwordString,
			PasswordEncryptor sut)
		{
			// Arrange
			var password = new Password(passwordString, true, true);

			// Act
			var result = sut.Encrypt(password);

			// Assert
			result.ShouldBe(password);
			result.IsEncrypted.ShouldBeTrue();
		}

		[Theory, FixtureData(RunContainerSetup = false)]
		public void Decrypt_WhenPasswordIsAlreadyDecrypted_ShouldReturnImmediately(
			string passwordString,
			PasswordEncryptor sut)
		{
			// Arrange
			var password = new Password(passwordString, true, false);

			// Act
			var result = sut.Decrypt(password);

			// Assert
			result.ShouldBe(password);
			result.IsEncrypted.ShouldBeFalse();
		}

		[Theory, FixtureData(RunContainerSetup = false)]
		public void Encrypt_WhenPasswordIsNotEncrypted_ShouldCallEncryptorEncrypt(
			[Frozen] Mock<IEncrypt<string>> stringEncryptorMock,
			string passwordString,
			string encryptedString,
			PasswordEncryptor sut)
		{
			// Arrange
			stringEncryptorMock
				.Setup(svc => svc.Encrypt(It.IsAny<string>()))
				.Returns(encryptedString);

			var password = new Password(passwordString, true, false);

			// Act
			sut.Encrypt(password);

			// Assert
			stringEncryptorMock.Verify(svc => svc.Encrypt(It.IsAny<string>()), Times.Once);
		}

		[Theory, FixtureData(RunContainerSetup = false)]
		public void Decrypt_WhenPasswordIsEncrypted_ShouldCallEncryptorDecrypt(
			[Frozen] Mock<IEncrypt<string>> stringEncryptorMock,
			string passwordString,
			string decryptedString,
			PasswordEncryptor sut)
		{
			// Arrange
			stringEncryptorMock
				.Setup(svc => svc.Decrypt(It.IsAny<string>()))
				.Returns(decryptedString);

			var password = new Password(passwordString, true, true);

			// Act
			sut.Decrypt(password);

			// Assert
			stringEncryptorMock.Verify(svc => svc.Decrypt(It.IsAny<string>()), Times.Once);
		}

		[Theory, FixtureData]
		public void Encrypt_WhenPasswordIsValid_ShouldReturnTheEncryptedValue(
			string passwordString,
			PasswordEncryptor sut)
		{
			// Arrange
			var password = new Password(passwordString, false, false);

			// Act
			var result = sut.Encrypt(password);
			_testOutputHelper.WriteLine($"password: {passwordString} = {result}");

			// Assert
			result.IsEncrypted.ShouldBeTrue();
			result.ShouldNotBe(password);
		}


		[Theory, FixtureData]
		public void Encrypt_WhenPasswordHasSalt_ItShouldUseTheSaltWhileEncrypting_ShouldReturnTheEncryptedValue(
			string passwordString,
			string salt,
			PasswordEncryptor sut)
		{
			// Arrange
			var password = new Password(passwordString, salt, false, false);

			// Act
			var result = sut.Encrypt(password);
			_testOutputHelper.WriteLine($"password: {password} = {result}");

			// Assert
			result.IsEncrypted.ShouldBeTrue();
			result.ShouldNotBe(password);
		}

		[Theory]
		[InlineFixtureData("v5AvkoFNOyj/L/heh9TeHHMql2x6Dav2ESY/1xj1lqibYDdUUDtIeubwe4CrDMqtqbYS7wcKmzGMyqQruMsFRDIU44U8XSHVp3R2VJUMRvA=", "passwordStringefe0d123-3bba-4bf3-9be8-c1fb04bce79c")]
		[InlineFixtureData("sQBuWUvWQrmVW0KJ5yQ7c1mBbpiVPRC8Ya4y8s9Og4DVLNqDKKF0sJSV5aoJekKnF1Q5ofT9WiBu7IWi0vKI0SQd+V2Bk6eGRHIcmiOZu44=", "passwordStringcab5f606-b4c9-4c7d-99bf-56ba9b67d325")]
		[InlineFixtureData("nrX0bfijRFHLxLjIT3gyXD3RFKCwE1CJjjoaBZdO2QM2dHReTymMYruht0XjE241XCGoM6v7yafbNMDMEenOPrPdoqqu0RiE4raYchB/OUo=", "passwordString1eea7f04-70aa-44a1-bbf2-f8f11bd39eca")]
		[InlineFixtureData("PwEPDECgbHMbTT55vil4ktX8YgB1trezThgj0r6bJsifLO2EmkEgpDpckUK8k9rpVjVxyrKYWpVD11lzcCjBiaBs/ZAsyaqz1ACu9ASic2Y=", "passwordStringf19e1a6e-5e09-46ad-8f58-f4cd78191c7e")]
		public void Decrypt_WhenPasswordIsEncrypted_ShouldReturnTheDecryptValue(
			string passwordString,
			string expected,
			PasswordEncryptor sut)
		{
			// Arrange
			var password = new Password(passwordString, false, true);

			var expectedPassword = new Password(expected, false, false);
			// Act
			var result = sut.Decrypt(password);
			_testOutputHelper.WriteLine($"password: {passwordString} = {result}");

			// Assert
			result.IsEncrypted.ShouldBeFalse();
			result.ShouldBe(expectedPassword);
		}


		[Theory]
		[InlineFixtureData("vkWC2KTQQRFCucFNJerRJ/McMsA18AYgj6KZz8RJpygH2XfwvETOaiq+leYJfUWp+F1u46T1snIWfgv7mlWv/2c0z55FxrlImaa3MP0hLnEYF9vLYhH7XdBwhgVu6IS7MTFNUkHv/UpLtJuInzuhkQ==", "e4180c3f-2ac1-4550-a524-1ce53de7c684", "passwordString1")]
		[InlineFixtureData("OsIsKQrwx5HBu084MLvjYGPpAvdbXkvNwNwouQQMlP3asgsyWikLDR22HeqSu2wgarwC3kKcRbuWYhd2AcQ3wzllF40FQuuj7orXXtDCI3I/x2B3lfQ+mfvpfbvyuo/oiosbXEHIKH9swVmGJFRzZA==", "2ce536c3-d06d-431d-a7a9-5e54c10f9e76", "passwordString2")]
		[InlineFixtureData("hLwwvwSe5T5oiKAgADfPiiN1CuRTFmI9WI+jOVkVDjYqd3wKrepc15CUF5b+rEahy3jrPfJ1yPVu9O2w6xSUVo1Udvf0UQTifhFlmnxfU4FiCegVKF1v75vgC3X4o7zDBURQBZ9fWO8uwdSRaPPyiw==", "d80d7412-8a3f-4b8e-a702-7ac0ff483f6c", "passwordString3")]
		[InlineFixtureData("n2H/z+OehGrzhkzpaY3kmBlL+ihFvm2r2d9fY22nqPbHt5tLsmBh+K0+4SaonetI4jvmtqxM44HDS1b4Aclp/FJqhCUWblaW8BChSj87B1+fv8MbGZU7WRMy/5YL8XB7AV/oGwNnipEtQoTTOPisvA==", "20b4d957-9706-49b5-81b3-f61c33450946", "passwordString4")]
		public void Decrypt_WhenPasswordIsEncryptedAndHasSalt_ShouldReturnTheDecryptValue(
			string passwordString,
			string salt,
			string expected,
			PasswordEncryptor sut)
		{
			// Arrange
			var password = new Password(passwordString, salt, false, true);
			var expectedPassword = new Password(expected, salt, false, false);
			// Act
			var result = sut.Decrypt(password);
			_testOutputHelper.WriteLine($"password: {passwordString} = {result}");

			// Assert
			result.IsEncrypted.ShouldBeFalse();
			result.ShouldBe(expectedPassword);
		}
	}
}
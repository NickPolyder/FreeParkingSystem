using System;
using AutoFixture;
using AutoFixture.Xunit;
using FreeParkingSystem.Common.Encryption;
using FreeParkingSystem.Testing;
using Moq;
using Shouldly;
using Xunit;

namespace FreeParkingSystem.Common.Tests.Encryption
{
	public class AesStringEncryptorTests
	{
		public static void ContainerSetup(IFixture fixture)
		{
			var secretKey = new byte[32]
			{
				237, 201, 222, 52, 18, 152, 49, 135, 198, 143, 48, 247, 22, 185, 5, 216, 43, 6, 37, 243, 13, 52, 149, 119, 74, 104, 70, 130, 246, 76, 231, 147
			};

			fixture.Build<IEncrypt<byte[]>>()
				.FromFactory(() =>new AesByteEncryptor(new EncryptionOptions(secretKey)))
				.ToCustomization()
				.Customize(fixture);
		}

		[Theory, FixtureData]
		public void Encrypt_ShouldCall_ByteEncryptor_Encrypt(
			Mock<IEncrypt<byte[]>> byteEncryptorMock,
			string input)
		{
			// Arrange
			byteEncryptorMock
				.Setup(encryptor => encryptor.Encrypt(It.IsAny<byte[]>()))
				.Returns(new byte[0]);

			var sut = new AesStringEncryptor(byteEncryptorMock.Object);

			// Act
			sut.Encrypt(input);

			// Assert
			byteEncryptorMock.Verify(encryptor => encryptor.Encrypt(It.IsAny<byte[]>()), Times.Once);
		}


		[Theory, FixtureData]
		public void Decrypt_ShouldCall_ByteEncryptor_Decrypt(
			[Frozen]Mock<IEncrypt<byte[]>> byteEncryptorMock,
			byte[] input)
		{
			// Arrange
			byteEncryptorMock
				.Setup(encryptor => encryptor.Decrypt(It.IsAny<byte[]>()))
				.Returns(new byte[0]);

			var base64Input = Convert.ToBase64String(input);

			var sut = new AesStringEncryptor(byteEncryptorMock.Object);
			// Act

			sut.Decrypt(base64Input);

			// Assert
			byteEncryptorMock.Verify(encryptor => encryptor.Decrypt(It.IsAny<byte[]>()), Times.Once);
		}


		[Theory, FixtureData]
		
		public void Encrypt_ShouldEncryptValues(
			string input,
			AesStringEncryptor sut)
		{
			// Arrange

			// Act
			var result = sut.Encrypt(input);

			// Assert
			result.ShouldNotBe(input);
			result.ShouldNotBeNullOrEmpty();
		}

		[Theory]
		[InlineFixtureData("R7RMNs748OpiKMsgA8ojILCbA6rImTKpaU0UT78VejL33uMW4mPG0vMeQCgVBnz/XvMP3QDQoadoIo5aEjNAOg==", "inputd9c54f6e-9659-476d-bd99-bea9dcbf2bc0")]
		[InlineFixtureData("GJJcwzv4LzsobxUKw7T0M2hFAykiYnG+i5srxPohIeXEJ+KCRVLYcUGHB0b2E7W3uPGpB6zOO1pj4AqDqUxT9A==", "inputf5597d6c-396e-423d-bedd-3ab142aa497b")]
		[InlineFixtureData("TiijlBfXiivzdNfYCAbOWhqyCJv62dP+GWt90mM9EjNr6nt892jmHnz/cYpq73GJSNlgdU6PT+jf3npOQ9YtMw==", "input26891dc4-ed43-4f70-a3e3-2c0a16ecbc35")]
		public void Decrypt_ShouldDecryptValues(
			string input,
			string expected,
			AesStringEncryptor sut)
		{
			// Arrange

			// Act
			var result = sut.Decrypt(input);

			// Assert
			result.ShouldBe(expected);
		}

		[Theory, FixtureData]
		public void ShouldBeAbleToEncryptAndDecryptTheInformation(
			string input,
			AesStringEncryptor sut)
		{
			// Arrange

			// Act
			var encrypted = sut.Encrypt(input);

			var result = sut.Decrypt(encrypted);

			// Assert
			result.ShouldBe(input);
		}
	}
}
using AutoFixture;
using FreeParkingSystem.Common.Encryption;
using FreeParkingSystem.Testing;
using Shouldly;
using Xunit;

namespace FreeParkingSystem.Common.Tests.Encryption
{
	public class AesByteEncryptorTests
	{
		public static void ContainerSetup(IFixture fixture)
		{
			var secretKey = new byte[32]
			{
				237, 201, 222, 52, 18, 152, 49, 135, 198, 143, 48, 247, 22, 185, 5, 216, 43, 6, 37, 243, 13, 52, 149, 119, 74, 104, 70, 130, 246, 76, 231, 147
			};
			
			fixture.Build<EncryptionOptions>()
				.FromFactory(() => 
					new EncryptionOptions(secretKey))
				.ToCustomization()
				.Customize(fixture);
		}

		[Theory, FixtureData]
		public void Encrypt_ShouldReturnTheEncryptedValue(
			byte[] input,
			AesByteEncryptor sut)
		{
			// Arrange

			// Act
			var result = sut.Encrypt(input);

			// Assert
			result.ShouldNotBe(input);
			result.ShouldNotBeEmpty();

		}

		[Theory, ClassFixtureData(typeof(AesByteData))]
		public void Decrypt_ShouldReturnTheDecryptedValue(
			byte[] input,
			byte[] expected,
			AesByteEncryptor sut)
		{
			// Arrange

			// Act
			var result = sut.Decrypt(input);

			// Assert
			result.ShouldBe(expected);

		}

		[Theory, FixtureData]
		public void ShouldBeAbleToEncryptAndDecryptTheInformation(
			byte[] input,
			AesByteEncryptor sut)
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
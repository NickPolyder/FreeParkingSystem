using AutoFixture;
using FreeParkingSystem.Common.Encryption;
using FreeParkingSystem.Testing;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace FreeParkingSystem.Common.Tests.Encryption
{
	public class AesByteEncryptorTests
	{
		private readonly ITestOutputHelper _testOutputHelper;

		public AesByteEncryptorTests(ITestOutputHelper testOutputHelper)
		{
			_testOutputHelper = testOutputHelper;
		}

		private static void ContainerSetup(IFixture fixture)
		{
			var secretKey = CommonTestsConstants.SecretKey;

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
			_testOutputHelper.WriteLine($"input: [{string.Join(", ", input)}] = [{string.Join(", ", result)}]");

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
			_testOutputHelper.WriteLine($"input: [{string.Join(", ",input)}] = [{string.Join(", ", result)}]");

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
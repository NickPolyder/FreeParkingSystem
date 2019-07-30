using AutoFixture;
using AutoFixture.Xunit;
using FreeParkingSystem.Accounts.Contract;
using FreeParkingSystem.Accounts.User;
using FreeParkingSystem.Common;
using FreeParkingSystem.Common.Hashing;
using FreeParkingSystem.Testing;
using Moq;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace FreeParkingSystem.Accounts.Tests.User
{
	public class PasswordHasherTests
	{
		private const int RepeatHash = 10000;

		private readonly ITestOutputHelper _testOutputHelper;


		public PasswordHasherTests(ITestOutputHelper testOutputHelper)
		{
			_testOutputHelper = testOutputHelper;
			
		}

		private static void ContainerSetup(IFixture fixture)
		{
			fixture.Build<IHash<byte[]>>()
				.FromFactory(() =>new ShaByteHasher())
				.ToCustomization()
				.Customize(fixture);

			fixture.Build<IHash<string>>()
				.FromFactory((IHash<byte[]> byteHash) => new ShaStringHasher(byteHash))
				.ToCustomization()
				.Customize(fixture);
		}

		[Theory, FixtureData(RunContainerSetup = false)]
		public void WhenPasswordIsAlreadyHashed_ShouldReturnImmediately(
			string passwordString,
			PasswordHasher sut)
		{
			// Arrange
			var password = new Password(passwordString, true);

			// Act
			var result = sut.Hash(password);

			// Assert
			result.ShouldBe(password);
			result.IsHashed.ShouldBeTrue();
		}

		[Theory, FixtureData(RunContainerSetup = false)]
		public void WhenPasswordIsEncrypted_ShouldThrowEncryptedException(
			string passwordString,
			PasswordHasher sut)
		{
			// Arrange
			var password = new Password(passwordString, false, true);

			// Act
			var exception = Record.Exception(() => sut.Hash(password));

			// Assert
			exception.ShouldNotBeNull();
			exception.ShouldBeOfType<PasswordEncryptionException>();
			exception.Message.ShouldBe(Contract.Resources.Validations.PasswordEncryption_EncryptedPasswordCannotBeHashed);
		}

		[Theory, FixtureData(RunContainerSetup = false)]
		public void WhenPasswordIsValid_ShouldCallStringHasher(
			[Frozen] Mock<IHash<string>> stringHasherMock,
			string passwordString,
			string hashedString,
			PasswordHasher sut)
		{
			// Arrange
			var password = new Password(passwordString, false, false);
			stringHasherMock.Setup(svc => svc.Hash(It.IsAny<string>())).Returns(hashedString);

			// Act
			sut.Hash(password);

			// Assert
			stringHasherMock.Verify(svc => svc.Hash(It.IsAny<string>()),Times.Exactly(RepeatHash));
		}


		[Theory]
		[InlineFixtureData("input", "QKZvofJUnQZ2nMnGAxxS/YaSxa6BWqNqzoEFNMLgvgrtiwiGu4u9PvKghhQtpKkWb8o3q/fLplpNcEXiICXNFA==")]
		[InlineFixtureData("input1", "QJ7z0jyfUtjDd1XcEgEGgKKHuLSH04fk3E4GVJqE4Tc0pqW4q7RWoG+qzNoW9ya9TR9IuPEIQ38jXZ4u2dxISA==")]
		[InlineFixtureData("1nput2", "/QHSP+trN6CZWlGrSZCCrucuPfwUf9M64uxImrdrGag0OLEfRDNMXMHeRg9He17E8OGSVOhLVeFHTxddBYRMLQ==")]
		[InlineFixtureData("i2put3", "LvXdS88vzZwOjrlFdTytEYla5HKtpZ8yIZAkgTMbqNBPcKQspCjqcxcWS+/Ql0MZw3vqmjBto8xBqG6/hDt5SA==")]
		[InlineFixtureData("in5ut4", "Pnu3QqsUWC5wl1mYrkBBAlLAlTy3VfKDmsXkVTXfnSt1yvdx0kMM9h1SJCKKR84cwLM+pDzEcpaTIfFQgDj9ng==")]
		[InlineFixtureData("in4ut5", "XWqru11SioD1q9XPI4JPWR062bJ/XU7CqKgu8MKdXjzHRxyPgZCmmxQHh7aYAdmmvof0mqWn7bBCYGCNFTYfXQ==")]
		public void WhenPasswordIsValid_ShouldReturnTheHashedValue(
			string passwordString,
			string expected,
			PasswordHasher sut)
		{
			// Arrange
			var password = new Password(passwordString, false, false);

			// Act
			var result = sut.Hash(password);
			_testOutputHelper.WriteLine($"password: {passwordString} = {result}");

			// Assert
			result.IsHashed.ShouldBeTrue();
			result.ToString().ShouldBe(expected);
		}


		[Theory]
		[InlineFixtureData("input", "e4180c3f-2ac1-4550-a524-1ce53de7c684", "wQxI4vcn5sIyguE2ViRRMhT9LZYJEH+t0A+oHMvftkLn5aW0n11Qdur30CpOUHpJHtMDy73n1uBrWKMzkGzu3A==")]
		[InlineFixtureData("input1", "2ce536c3-d06d-431d-a7a9-5e54c10f9e76", "XJwG3NLWpLLzNjMLddGlz4Q4M0MmvWq0wASIkToK/aIaz3ALtuKCmSwoE02fYjfnv/gLuoHIGJa9WXlinMrbXg==")]
		[InlineFixtureData("1nput2", "d80d7412-8a3f-4b8e-a702-7ac0ff483f6c", "aTIw+yd20Ed2xnFIDfKWYZybHrIyQyWvw2QofA9wSgoct35sAhnx2GEmyOtt9T4J8JKK8DdBJkiMoU25rpagbw==")]
		[InlineFixtureData("i2put3", "20b4d957-9706-49b5-81b3-f61c33450946", "z8mYHRREA0R3lly9ZXA9VBD2s+/nIi4frp01KrXnyBHBnW3zzYmqCyu+3iTmBZ0763JNibZIFboQQoDWkN8/8A==")]
		[InlineFixtureData("in5ut4", "a7e00eb7-96bd-4ccb-a07f-92d652619e7e", "jA66qZTm54KSrrdePzJPNCkksbBZyGmgnNWgNwmIz5mobzna7aldpPlOA9HoAbfLOoKfOLMNSCOYAO74ZvXuIw==")]
		[InlineFixtureData("in4ut5", "4e591aab-2f5d-4300-9551-60396e5ab695", "RDBzgOfON78G6hpbO8dBauP0RcF0s7oMpthD2yp32kBvF4SX12OJb286YUZC3jYqi6g5a6qj0feVYTSh4E3WEQ==")]
		public void WhenPasswordHasSalt_ItShouldUseTheSaltWhileHashing_ShouldReturnTheHashedValue(
			string passwordString,
			string salt,
			string expected,
			PasswordHasher sut)
		{
			// Arrange
			var password = new Password(passwordString, salt, false, false);

			// Act
			var result = sut.Hash(password);
			_testOutputHelper.WriteLine($"password: {password} = {result}");

			// Assert
			result.IsHashed.ShouldBeTrue();
			result.ToString().ShouldBe(expected);
		}
	}
}
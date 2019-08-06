using System;
using AutoFixture;
using FreeParkingSystem.Accounts.Contract;
using FreeParkingSystem.Accounts.Contract.Exceptions;
using FreeParkingSystem.Accounts.Contract.Repositories;
using FreeParkingSystem.Accounts.Contract.Resources;
using FreeParkingSystem.Accounts.Data.Mappers;
using FreeParkingSystem.Accounts.Data.Models;
using FreeParkingSystem.Accounts.Data.Repositories;
using FreeParkingSystem.Accounts.Validators;
using FreeParkingSystem.Common;
using FreeParkingSystem.Common.Encryption;
using FreeParkingSystem.Common.Hashing;
using FreeParkingSystem.Testing;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace FreeParkingSystem.Accounts.Tests.User
{
	public class UserServicesTests
	{
		private readonly ITestOutputHelper _testOutputHelper;

		public UserServicesTests(ITestOutputHelper testOutputHelper)
		{
			_testOutputHelper = testOutputHelper;
		}

		private static void ContainerSetup(IFixture fixture)
		{

			fixture.Build<IUserRepository>()
				.FromFactory(() => new UserRepository(GetDbContext(), new UserMapper(new ClaimsMapper())))
				.ToCustomization()
				.Customize(fixture);

			fixture.Build<IClaimsRepository>()
				.FromFactory(() => new ClaimsRepository(GetDbContext(), new ClaimsMapper()))
				.ToCustomization()
				.Customize(fixture);

			fixture.Build<IPasswordManager>()
				.FromFactory(() =>
				{
					var options = new PasswordOptions(0, int.MaxValue, PasswordRequirements.None);

					var passwordValidator = new PasswordValidator(options);

					var hashingPassword = new PasswordHasher(new ShaStringHasher(new ShaByteHasher()));

					var secretKey = new byte[32]
					{
						237, 201, 222, 52, 18, 152, 49, 135, 198, 143, 48, 247, 22, 185, 5, 216, 43, 6, 37, 243, 13, 52, 149, 119, 74, 104, 70, 130, 246, 76, 231, 147
					};

					var encryptPassword = new PasswordEncryptor(new AesStringEncryptor(new AesByteEncryptor(new EncryptionOptions(secretKey))));

					return new PasswordManager(passwordValidator, hashingPassword, encryptPassword);
				})
				.ToCustomization()
				.Customize(fixture);
		}

		[Theory]
		[InlineFixtureData(null)]
		[InlineFixtureData("")]
		[InlineFixtureData(" ")]
		public void CreateUser_WhenUserNameIsNullOrEmpty_ShouldThrowException(
			string username,
			string password,
			UserServices sut)
		{

			// Act
			var exception = Record.Exception(() => sut.CreateUser(username, password));

			// Assert
			exception.ShouldNotBeNull();
			exception.ShouldBeOfType<ArgumentNullException>();
			(exception as ArgumentNullException).ParamName.ShouldBe("userName");
		}

		[Theory]
		[InlineFixtureData(null)]
		[InlineFixtureData("")]
		[InlineFixtureData(" ")]
		public void CreateUser_WhenPasswordIsNullOrEmpty_ShouldThrowException(
			string password,
			string username,
			UserServices sut)
		{

			// Act
			var exception = Record.Exception(() => sut.CreateUser(username, password));

			// Assert
			exception.ShouldNotBeNull();
			exception.ShouldBeOfType<PasswordValidationException>();
			exception.Message.ShouldBe(Validations.PasswordValidation_EmptyPassword);
		}

		[Theory, FixtureData]
		public void CreateUser_ShouldCreateUser(
			string username,
			string password,
			UserServices sut)
		{
			// Arrange

			// Act
			var result = sut.CreateUser(username, password);

			_testOutputHelper.WriteLine($"User: {result.Id}{Environment.NewLine}" +
			                            $"Username: {result.UserName}{Environment.NewLine}" +
			                            $"Password: {result.Password}");
			// Assert
			result.Id.ShouldNotBe(default);
			result.UserName.ShouldBe(username);
			result.Password.IsEncrypted.ShouldBeTrue();
			result.Password.IsHashed.ShouldBeTrue();
			result.Password.Salt.ShouldNotBeNullOrWhiteSpace();
			result.Password.ToString().ShouldNotBe(password);
		}

		[Theory, FixtureData]
		public void CreateUser_WhenUserExists_ShouldThrowException(
			string username,
			string password,
			string password2,
			UserServices sut)
		{
			// Arrange
			sut.CreateUser(username, password);

			// Act
			var exception = Record.Exception(() => sut.CreateUser(username, password2));

			// Assert
			exception.ShouldNotBeNull();
			exception.ShouldBeOfType<UserException>();
			exception.Message.ShouldBe(Contract.Resources.Validations.User_AlreadyExists);
		}

		private static AccountsDbContext GetDbContext()
		{
			var options = new DbContextOptionsBuilder<AccountsDbContext>()
				.UseInMemoryDatabase(Guid.NewGuid().ToString())
				.Options;

			var dbContext = new AccountsDbContext(options);
			return dbContext;
		}
	}
}
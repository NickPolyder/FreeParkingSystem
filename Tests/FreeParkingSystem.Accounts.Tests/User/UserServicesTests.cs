using System;
using System.Collections.Generic;
using System.Linq;
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

		[Theory, FixtureData]
		public void AddClaim_WhenUserIsNull_ShouldThrowException(
			(string type, string value) claim,
			UserServices sut)
		{
			// Arrange

			// Act
			var exception = Record.Exception(() => sut.AddClaim(null, claim.type, claim.value));

			// Assert
			exception.ShouldNotBeNull();
			exception.ShouldBeOfType<ArgumentNullException>();
			(exception as ArgumentNullException).ParamName.ShouldBe("user");
		}

		[Theory]
		[InlineFixtureData(null)]
		[InlineFixtureData("")]
		[InlineFixtureData(" ")]
		public void AddClaim_WhenTypeIsNull_ShouldThrowException(
			string claimType,
			string username,
			string password,
			string claimValue,
			UserServices sut)
		{
			// Arrange
			var user = sut.CreateUser(username, password);

			_testOutputHelper.WriteLine($"User: {user.Id}{Environment.NewLine}" +
										$"Username: {user.UserName}{Environment.NewLine}" +
										$"Password: {user.Password}");

			// Act
			var exception = Record.Exception(() => sut.AddClaim(user, claimType, claimValue));

			// Assert
			exception.ShouldNotBeNull();
			exception.ShouldBeOfType<ArgumentNullException>();
			(exception as ArgumentNullException).ParamName.ShouldBe("type");
		}

		[Theory]
		[InlineFixtureData(null)]
		[InlineFixtureData("")]
		[InlineFixtureData(" ")]
		public void AddClaim_WhenValueIsNull_ShouldThrowException(
			string claimValue,
			string username,
			string password,
			string claimType,
			UserServices sut)
		{
			// Arrange
			var user = sut.CreateUser(username, password);

			_testOutputHelper.WriteLine($"User: {user.Id}{Environment.NewLine}" +
										$"Username: {user.UserName}{Environment.NewLine}" +
										$"Password: {user.Password}");

			// Act
			var exception = Record.Exception(() => sut.AddClaim(user, claimType, claimValue));

			// Assert
			exception.ShouldNotBeNull();
			exception.ShouldBeOfType<ArgumentNullException>();
			(exception as ArgumentNullException).ParamName.ShouldBe("value");
		}

		[Theory, FixtureData]
		public void AddClaim_ShouldNotAddTheSameClaimTwice(
			string username,
			string password,
			(string type, string value) claim,
			UserServices sut)
		{
			// Arrange
			var user = sut.CreateUser(username, password);

			_testOutputHelper.WriteLine($"User: {user.Id}{Environment.NewLine}" +
										$"Username: {user.UserName}{Environment.NewLine}" +
										$"Password: {user.Password}");
			sut.AddClaim(user, claim.type, claim.value);

			// Act
			var exception = Record.Exception(() => sut.AddClaim(user, claim.type, claim.value));


			// Assert
			exception.ShouldNotBeNull();
			exception.ShouldBeOfType<ClaimException>();
			exception.Message.ShouldBe(Contract.Resources.Validations.Claim_AlreadyExists);
		}

		[Theory, FixtureData]
		public void AddClaim_ShouldCreateClaim(
			string username,
			string password,
			(string type, string value) claim,
			UserServices sut)
		{
			// Arrange
			var user = sut.CreateUser(username, password);

			_testOutputHelper.WriteLine($"User: {user.Id}{Environment.NewLine}" +
										$"Username: {user.UserName}{Environment.NewLine}" +
										$"Password: {user.Password}");
			// Act
			sut.AddClaim(user, claim.type, claim.value);

			var result = user.Claims.FirstOrDefault(cl => cl.Type.Equals(claim.type));

			_testOutputHelper.WriteLine($"Claim: {result.Id}{Environment.NewLine}" +
										$"Type: {result.Type}{Environment.NewLine}" +
										$"Value: {result.Value}");

			// Assert
			result.ShouldNotBeNull();
			result.Id.ShouldNotBe(default);
			result.UserId.ShouldBe(user.Id);
			result.Type.ShouldBe(claim.type);
			result.Value.ShouldBe(claim.value);
		}

		[Theory, FixtureData]
		public void AddClaim_WhenAClaimAlreadyExistsInTheList_ShouldCreateClaim_AndReplaceClaimInUser(
			string username,
			string password,
			(string type, string value) claim,
			UserServices sut)
		{
			// Arrange
			var user = sut.CreateUser(username, password);

			_testOutputHelper.WriteLine($"User: {user.Id}{Environment.NewLine}" +
										$"Username: {user.UserName}{Environment.NewLine}" +
										$"Password: {user.Password}");

			user.Claims = new List<UserClaim>
			{
				new UserClaim
				{
					Type = claim.type,
					Value = claim.value,
					UserId = user.Id
				}
			};

			// Act
			sut.AddClaim(user, claim.type, claim.value);

			var result = user.Claims.FirstOrDefault(cl => cl.Type.Equals(claim.type));

			_testOutputHelper.WriteLine($"Claim: {result.Id}{Environment.NewLine}" +
										$"Type: {result.Type}{Environment.NewLine}" +
										$"Value: {result.Value}");

			// Assert
			result.ShouldNotBeNull();
			result.Id.ShouldNotBe(default);
			result.UserId.ShouldBe(user.Id);
			result.Type.ShouldBe(claim.type);
			result.Value.ShouldBe(claim.value);
		}


		[Theory, FixtureData]
		public void ChangeClaim_WhenUserIsNull_ShouldThrowException(
			(string type, string value) claim,
			UserServices sut)
		{
			// Arrange

			// Act
			var exception = Record.Exception(() => sut.ChangeClaim(null, claim.type, claim.value));

			// Assert
			exception.ShouldNotBeNull();
			exception.ShouldBeOfType<ArgumentNullException>();
			(exception as ArgumentNullException).ParamName.ShouldBe("user");
		}

		[Theory]
		[InlineFixtureData(null)]
		[InlineFixtureData("")]
		[InlineFixtureData(" ")]
		public void ChangeClaim_WhenTypeIsNull_ShouldThrowException(
			string claimType,
			string username,
			string password,
			string claimValue,
			UserServices sut)
		{
			// Arrange
			var user = sut.CreateUser(username, password);

			_testOutputHelper.WriteLine($"User: {user.Id}{Environment.NewLine}" +
										$"Username: {user.UserName}{Environment.NewLine}" +
										$"Password: {user.Password}");

			// Act
			var exception = Record.Exception(() => sut.ChangeClaim(user, claimType, claimValue));

			// Assert
			exception.ShouldNotBeNull();
			exception.ShouldBeOfType<ArgumentNullException>();
			(exception as ArgumentNullException).ParamName.ShouldBe("type");
		}

		[Theory]
		[InlineFixtureData(null)]
		[InlineFixtureData("")]
		[InlineFixtureData(" ")]
		public void ChangeClaim_WhenValueIsNull_ShouldThrowException(
			string claimValue,
			string username,
			string password,
			string claimType,
			UserServices sut)
		{
			// Arrange
			var user = sut.CreateUser(username, password);

			_testOutputHelper.WriteLine($"User: {user.Id}{Environment.NewLine}" +
										$"Username: {user.UserName}{Environment.NewLine}" +
										$"Password: {user.Password}");

			// Act
			var exception = Record.Exception(() => sut.ChangeClaim(user, claimType, claimValue));

			// Assert
			exception.ShouldNotBeNull();
			exception.ShouldBeOfType<ArgumentNullException>();
			(exception as ArgumentNullException).ParamName.ShouldBe("value");
		}

		[Theory, FixtureData]
		public void ChangeClaim_WhenThereAreNoClaims_ShouldThrowExceptions(
			string username,
			string password,
			(string type, string value) otherClaim,
			(string type, string value) claim,
			UserServices sut)
		{
			// Arrange
			var user = sut.CreateUser(username, password);

			_testOutputHelper.WriteLine($"User: {user.Id}{Environment.NewLine}" +
										$"Username: {user.UserName}{Environment.NewLine}" +
										$"Password: {user.Password}");

			sut.AddClaim(user, claim.type, claim.value);

			// Act
			var exception = Record.Exception(() => sut.ChangeClaim(user, otherClaim.type, otherClaim.value));

			// Assert
			exception.ShouldNotBeNull();
			exception.ShouldBeOfType<ClaimException>();
			exception.Message.ShouldBe(Contract.Resources.Validations.Claim_DoesNotExist);
		}

		[Theory, FixtureData]
		public void ChangeClaim_ShouldChangeClaim(
			string username,
			string password,
			(string type, string value) claim,
			string newValue,
			UserServices sut)
		{
			// Arrange
			var user = sut.CreateUser(username, password);

			_testOutputHelper.WriteLine($"User: {user.Id}{Environment.NewLine}" +
										$"Username: {user.UserName}{Environment.NewLine}" +
										$"Password: {user.Password}");
			sut.AddClaim(user, claim.type, claim.value);
			user.Claims.Clear();

			// Act
			sut.ChangeClaim(user, claim.type, newValue);

			var result = user.Claims.FirstOrDefault(cl => cl.Type.Equals(claim.type));

			_testOutputHelper.WriteLine($"Claim: {result.Id}{Environment.NewLine}" +
										$"Type: {result.Type}{Environment.NewLine}" +
										$"Value: {result.Value}");

			// Assert
			result.ShouldNotBeNull();
			result.Id.ShouldNotBe(default);
			result.UserId.ShouldBe(user.Id);
			result.Type.ShouldBe(claim.type);
			result.Value.ShouldBe(newValue);
		}

		[Theory, FixtureData]
		public void ChangeClaim_WhenAClaimAlreadyExistsInTheList_ShouldCreateClaim_AndReplaceClaimInUser(
			string username,
			string password,
			(string type, string value) claim,
			string newValue,
			UserServices sut)
		{
			// Arrange
			var user = sut.CreateUser(username, password);

			_testOutputHelper.WriteLine($"User: {user.Id}{Environment.NewLine}" +
										$"Username: {user.UserName}{Environment.NewLine}" +
										$"Password: {user.Password}");

			sut.AddClaim(user, claim.type, claim.value);

			// Act
			sut.ChangeClaim(user, claim.type, newValue);

			var result = user.Claims.FirstOrDefault(cl => cl.Type.Equals(claim.type));

			_testOutputHelper.WriteLine($"Claim: {result.Id}{Environment.NewLine}" +
										$"Type: {result.Type}{Environment.NewLine}" +
										$"Value: {result.Value}");

			// Assert
			result.ShouldNotBeNull();
			result.Id.ShouldNotBe(default);
			result.UserId.ShouldBe(user.Id);
			result.Type.ShouldBe(claim.type);
			result.Value.ShouldBe(newValue);
		}

		[Theory, FixtureData]
		public void RemoveClaim_WhenUserIsNull_ShouldThrowException(
			string username,
			string password,
			string claimType,
			UserServices sut)
		{
			// Arrange
			var user = sut.CreateUser(username, password);

			// Act
			var exception = Record.Exception(() => sut.RemoveClaim(null, claimType));

			// Assert
			exception.ShouldNotBeNull();
			exception.ShouldBeOfType<ArgumentNullException>();
			(exception as ArgumentNullException).ParamName.ShouldBe("user");
		}

		[Theory]
		[InlineFixtureData(null)]
		[InlineFixtureData("")]
		[InlineFixtureData(" ")]
		public void RemoveClaim_WhenTypeIsNull_ShouldThrowException(
			string claimType,
			string username,
			string password,
			UserServices sut)
		{
			// Arrange
			var user = sut.CreateUser(username, password);

			_testOutputHelper.WriteLine($"User: {user.Id}{Environment.NewLine}" +
										$"Username: {user.UserName}{Environment.NewLine}" +
										$"Password: {user.Password}");

			// Act
			var exception = Record.Exception(() => sut.RemoveClaim(user, claimType));

			// Assert
			exception.ShouldNotBeNull();
			exception.ShouldBeOfType<ArgumentNullException>();
			(exception as ArgumentNullException).ParamName.ShouldBe("type");
		}

		[Theory, FixtureData]
		public void RemoveClaim_WhenClaimDoesNotExist_ShouldThrowException(
			string username,
			string password,
			(string type, string value) claim,
			UserServices sut)
		{
			// Arrange
			var user = sut.CreateUser(username, password);

			// Act
			var exception = Record.Exception(() => sut.RemoveClaim(user, claim.type));

			// Assert
			exception.ShouldNotBeNull();
			exception.ShouldBeOfType<ClaimException>();
			exception.Message.ShouldBe(Contract.Resources.Validations.Claim_DoesNotExist);
		}

		[Theory, FixtureData]
		public void RemoveClaim_ShouldDeleteTheClaim(
			string username,
			string password,
			(string type, string value) claim,
			UserServices sut)
		{
			// Arrange
			var user = sut.CreateUser(username, password);
			sut.AddClaim(user, claim.type, claim.value);
			
			// Act
			sut.RemoveClaim(user, claim.type);

			// Assert
			user.Claims.Count.ShouldBe(0);
		}

		private static AccountsDbContext GetDbContext()
		{
			var options = new DbContextOptionsBuilder<AccountsDbContext>()
				.UseInMemoryDatabase(Guid.NewGuid().ToString())
				.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
				.Options;

			var dbContext = new AccountsDbContext(options);
			return dbContext;
		}
	}
}
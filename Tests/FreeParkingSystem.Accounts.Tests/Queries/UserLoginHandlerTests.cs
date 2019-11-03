using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using AutoFixture;
using AutoFixture.Xunit;
using FreeParkingSystem.Accounts.Contract;
using FreeParkingSystem.Accounts.Contract.Options;
using FreeParkingSystem.Accounts.Contract.Queries;
using FreeParkingSystem.Accounts.Data.Mappers;
using FreeParkingSystem.Accounts.Data.Models;
using FreeParkingSystem.Accounts.Data.Repositories;
using FreeParkingSystem.Accounts.Mappers;
using FreeParkingSystem.Accounts.Queries;
using FreeParkingSystem.Accounts.Validators;
using FreeParkingSystem.Common;
using FreeParkingSystem.Common.Authorization;
using FreeParkingSystem.Common.Encryption;
using FreeParkingSystem.Common.ExtensionMethods;
using FreeParkingSystem.Common.Hashing;
using FreeParkingSystem.Common.Messages;
using FreeParkingSystem.Testing;
using Microsoft.EntityFrameworkCore;
using Moq;
using Shouldly;
using Xunit;
using Xunit.Abstractions;
using static FreeParkingSystem.Accounts.Tests.TestConstants;

namespace FreeParkingSystem.Accounts.Tests.Queries
{
	public class UserLoginHandlerTests
	{
		
		private static void ContainerSetup(IFixture fixture)
		{
			fixture.Build<IMap<UserClaim, Claim>>()
				.FromFactory(() => new SecurityClaimsMapper())
				.ToCustomization()
				.Customize(fixture);

			fixture.Build<IAuthenticationServices>()
				.FromFactory(() => new MockAuthenticationService())
				.ToCustomization()
				.Customize(fixture);

			fixture.Build<IUserServices>()
				.FromFactory(() =>
				{
					var accountsDbContext = GetDbContext();
					var claimsMapper = new ClaimsMapper();
					var userRepository = new UserRepository(accountsDbContext, new UserMapper(claimsMapper));
					var claimsRepository = new ClaimsRepository(accountsDbContext, claimsMapper);

					var options = new PasswordOptions(MinimumCharacters, MaximumCharacters, DefaultPasswordRequirements);

					var passwordValidator = new PasswordValidator(options);

					var hashingPassword = new PasswordHasher(new ShaStringHasher(new ShaByteHasher()));

					var secretKey = CommonTestsConstants.SecretKey;

					var encryptPassword = new PasswordEncryptor(new AesStringEncryptor(new AesByteEncryptor(new EncryptionOptions(secretKey))));

					var passwordManager = new PasswordManager(passwordValidator, hashingPassword, encryptPassword);

					return new UserServices(userRepository, claimsRepository, passwordManager);
				})
				.ToCustomization()
				.Customize(fixture);
		}

		private static void ContainerClaimsSetup(IFixture fixture)
		{
			fixture.Build<Claim>()
				.FromFactory((string type, string value) => new Claim(type, value))
				.ToCustomization()
				.Customize(fixture);

			fixture.Build<IEnumerable<Claim>>()
				.FromFactory(((string type, string value)[] claims) => claims.Select(claim => new Claim(claim.type, claim.value)))
				.ToCustomization()
				.Customize(fixture);
		}

		[Theory, FixtureData(RunContainerSetup = false)]
		public void Handle_ShouldCall_UserServices_Login(
			[Frozen] Mock<IUserServices> userServicesMock,
			UserLoginRequest request,
			User user,
			UserLoginHandler sut)
		{
			// Arrange
			userServicesMock
				.Setup(svc => svc.Login(request.Username, request.Password))
				.Returns(user);

			// Act
			sut.Handle(request, CancellationToken.None).RunSync();

			// Assert
			userServicesMock.Verify(svc => svc.Login(request.Username, request.Password), Times.Once);
		}

		[Theory, FixtureData(RunContainerSetup = false)]
		public void Handle_ShouldMake_ThePassword_Empty(
			[Frozen] Mock<IUserServices> userServicesMock,

			UserLoginRequest request,
			User user,
			UserLoginHandler sut)
		{
			// Arrange
			userServicesMock
				.Setup(svc => svc.Login(request.Username, request.Password))
				.Returns(user);

			// Act
			sut.Handle(request, CancellationToken.None).RunSync();

			// Assert
			user.Password.ShouldBe(Password.Empty);
		}

		[Theory, FixtureData(ContainerMethod = nameof(ContainerClaimsSetup))]
		public void Handle_ShouldCall_AuthenticationServices_CreateToken(
			[Frozen] Mock<IUserServices> userServicesMock,
			[Frozen] Mock<IAuthenticationServices> authenticationServicesMock,
			[Frozen] Mock<IMap<UserClaim, Claim>> claimsMapperMock,
			UserLoginRequest request,
			User user,
			IEnumerable<Claim> claims,
			UserLoginHandler sut)
		{
			// Arrange
			userServicesMock
				.Setup(svc => svc.Login(request.Username, request.Password))
				.Returns(user);

			var claimEnumerator = claims.GetEnumerator();
			claimsMapperMock.Setup(map => map.Map(It.IsAny<UserClaim>(), It.IsAny<Dictionary<object, object>>()))
				.Returns(() =>
				{
					if (claimEnumerator.Current == null)
					{
						claimEnumerator.MoveNext();
					}

					var current = claimEnumerator.Current;
					claimEnumerator.MoveNext();

					return current;
				});

			// Act
			sut.Handle(request, CancellationToken.None).RunSync();

			// Assert
			authenticationServicesMock.Verify(svc => svc.CreateToken(user.UserName, It.IsAny<IEnumerable<Claim>>()), Times.Once);

			claimEnumerator.Dispose();
		}

		[Theory, FixtureData(ContainerMethod = nameof(ContainerClaimsSetup))]
		public void Handle_ShouldReturn_UserToken(
			[Frozen] Mock<IUserServices> userServicesMock,
			[Frozen] Mock<IAuthenticationServices> authenticationServicesMock,
			[Frozen] Mock<IMap<UserClaim, Claim>> claimsMapperMock,
			UserLoginRequest request,
			User user,
			UserToken userToken,
			IEnumerable<Claim> claims,
			UserLoginHandler sut)
		{
			// Arrange
			userServicesMock
				.Setup(svc => svc.Login(request.Username, request.Password))
				.Returns(user);
			authenticationServicesMock.Setup(svc => svc.CreateToken(user.UserName, It.IsAny<IEnumerable<Claim>>()))
				.Returns(userToken);

			var claimEnumerator = claims.GetEnumerator();
			claimsMapperMock.Setup(map => map.Map(It.IsAny<UserClaim>(), It.IsAny<Dictionary<object, object>>()))
				.Returns(() =>
				{
					if (claimEnumerator.Current == null)
					{
						claimEnumerator.MoveNext();
					}

					var current = claimEnumerator.Current;
					claimEnumerator.MoveNext();

					return current;
				});

			// Act
			var result = sut.Handle(request, CancellationToken.None).RunSync();

			// Assert
			result.RequestId.ShouldBe(request.Id);
			result.ShouldBeOfType<SuccessResponse<UserToken>>();

			var resultToken = (result as SuccessResponse<UserToken>).Data;

			resultToken.Username.ShouldBe(userToken.Username);
			resultToken.Token.ShouldBe(userToken.Token);

			claimEnumerator.Dispose();
		}

		[Theory]
		[InlineFixtureData(null)]
		[InlineFixtureData("")]
		[InlineFixtureData(" ")]
		public void Handle_WhenUsernameIsInvalid_ShouldThrowException(
			string username,
			string password,
			UserLoginHandler sut)
		{
			// Arrange
			var request = new UserLoginRequest(username, password);

			// Act
			var exception = Record.Exception(() => sut.Handle(request, CancellationToken.None).RunSync());

			// Assert
			exception.ShouldNotBeNull();
		}

		[Theory]
		[InlineFixtureData(null)]
		[InlineFixtureData("")]
		[InlineFixtureData(" ")]
		public void Handle_WhenPasswordIsInvalid_ShouldThrowException(
			string password,
			string username,
			UserLoginHandler sut)
		{
			// Arrange
			var request = new UserLoginRequest(username, password);

			// Act
			var exception = Record.Exception(() => sut.Handle(request, CancellationToken.None).RunSync());

			// Assert
			exception.ShouldNotBeNull();
		}

		[Theory, FixtureData]
		public void Handle_WhenUserDoesNotExist_ShouldThrowException(
			UserLoginRequest request,
			UserLoginHandler sut)
		{
			// Arrange

			// Act
			var exception = Record.Exception(() => sut.Handle(request, CancellationToken.None).RunSync());

			// Assert
			exception.ShouldNotBeNull();
		}

		[Theory, FixtureData]
		public void Handle_WhenUserExists_ShouldReturnToken(
			[Frozen]IUserServices userService,
			UserLoginRequest request,
			UserLoginHandler sut)
		{
			// Arrange
			userService.CreateUser(request.Username, request.Password);

			// Act
			var result = sut.Handle(request, CancellationToken.None).RunSync();

			// Assert
			result.ShouldNotBeNull();
			result.RequestId.ShouldBe(request.Id);
			var resultToken = (result as SuccessResponse<UserToken>).Data;

			resultToken.Username.ShouldBe(request.Username);
			resultToken.Token.ShouldNotBeNull();
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
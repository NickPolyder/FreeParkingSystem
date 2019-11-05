using System;
using System.Threading;
using AutoFixture;
using AutoFixture.Xunit;
using FreeParkingSystem.Accounts.Commands;
using FreeParkingSystem.Accounts.Contract;
using FreeParkingSystem.Accounts.Contract.Commands;
using FreeParkingSystem.Accounts.Contract.Options;
using FreeParkingSystem.Accounts.Contract.Repositories;
using FreeParkingSystem.Accounts.Data.Mappers;
using FreeParkingSystem.Accounts.Data.Models;
using FreeParkingSystem.Accounts.Data.Repositories;
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
using static FreeParkingSystem.Accounts.Tests.TestConstants;

namespace FreeParkingSystem.Accounts.Tests.Commands
{
	public class CreateUserHandlerTests
	{
		private static void ContainerSetup(IFixture fixture)
		{
			var accountsDbContext = GetDbContext();

			fixture.Build<IUserRepository>()
				.FromFactory(() =>
				{
					var claimsMapper = new ClaimsMapper();
					return new UserRepository(accountsDbContext, new UserMapper(claimsMapper));
				})
				.ToCustomization()
				.Customize(fixture);

			fixture.Build<IUserServices>()
				.FromFactory((IUserRepository userRepository) =>
				{
					var claimsMapper = new ClaimsMapper();
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

		[Theory, FixtureData(RunContainerSetup = false)]
		public void ShouldCall_UserServices_CreateUser(
			[Frozen] Mock<IUserServices> userServicesMock,
			CreateUserRequest request,
			CreateUserHandler sut)
		{
			// Arrange

			// Act
			sut.Handle(request, CancellationToken.None).RunSync();

			// Assert
			userServicesMock.Verify(svc => svc.CreateUser(request.UserName, request.Password), Times.Once);
		}


		[Theory, FixtureData(RunContainerSetup = false)]
		public void ShouldCall_UserServices_AddClaim_Role(
			[Frozen] Mock<IUserServices> userServicesMock,
			User user,
			CreateUserRequest request,
			CreateUserHandler sut)
		{
			// Arrange
			userServicesMock.Setup(svc => svc.CreateUser(request.UserName, request.Password))
				.Returns(user);

			// Act
			sut.Handle(request, CancellationToken.None).RunSync();

			// Assert
			userServicesMock.Verify(svc => svc.AddClaim(user, UserClaimTypes.Role.ToString(), request.Role.ToString()), Times.Once);
		}

		[Theory, FixtureData(RunContainerSetup = false)]
		public void WhenRequestHasEmail_ShouldCall_UserServices_AddClaim_Email(
			[Frozen] Mock<IUserServices> userServicesMock,
			User user,
			CreateUserRequest request,
			CreateUserHandler sut)
		{
			// Arrange
			userServicesMock.Setup(svc => svc.CreateUser(request.UserName, request.Password))
				.Returns(user);

			// Act
			sut.Handle(request, CancellationToken.None).RunSync();

			// Assert
			userServicesMock.Verify(svc => svc.AddClaim(user, UserClaimTypes.Email.ToString(), request.Email), Times.Once);
		}

		[Theory]
		[InlineFixtureData(null, RunContainerSetup = false)]
		[InlineFixtureData("", RunContainerSetup = false)]
		[InlineFixtureData(" ", RunContainerSetup = false)]
		public void WhenRequestHasInvalidEmail_ShouldNotCall_UserServices_AddClaim_Email(
			string email,
			[Frozen] Mock<IUserServices> userServicesMock,
			User user,
			string userName,
			string password,
			Role role,
			CreateUserHandler sut)
		{
			// Arrange
			var request = new CreateUserRequest(userName, password, email, role);

			userServicesMock
				.Setup(svc => svc.CreateUser(request.UserName, request.Password))
				.Returns(user);

			// Act
			sut.Handle(request, CancellationToken.None).RunSync();

			// Assert
			userServicesMock.Verify(svc => svc.AddClaim(user, UserClaimTypes.Email.ToString(), request.Email), Times.Never);
		}

		[Theory, FixtureData(RunContainerSetup = false)]
		public void ShouldReturn_SuccessResponse(
			[Frozen] Mock<IUserServices> userServicesMock,
			User user,
			CreateUserRequest request,
			CreateUserHandler sut)
		{
			// Arrange
			userServicesMock.Setup(svc => svc.CreateUser(request.UserName, request.Password))
				.Returns(user);

			// Act
			var result = sut.Handle(request, CancellationToken.None).RunSync();

			// Assert
			result.ShouldNotBeNull();
			result.RequestId.ShouldBe(request.RequestId);
			result.ShouldBeOfType<SuccessResponse>();
		}

		[Theory, FixtureData]
		public void ShouldCreateUser(
			[Frozen]IUserRepository userRepository,
			CreateUserRequest request,
			CreateUserHandler sut
			)
		{
			// Arrange

			// Act
			sut.Handle(request, CancellationToken.None).RunSync();

			// Assert
			var user = userRepository.GetByUsername(request.UserName);
			user.ShouldNotBeNull();

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
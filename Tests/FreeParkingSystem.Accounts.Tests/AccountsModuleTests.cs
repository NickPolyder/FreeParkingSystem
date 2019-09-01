using System;
using System.Security.Claims;
using Autofac;
using FreeParkingSystem.Accounts.Commands;
using FreeParkingSystem.Accounts.Contract;
using FreeParkingSystem.Accounts.Contract.Commands;
using FreeParkingSystem.Accounts.Contract.Options;
using FreeParkingSystem.Accounts.Contract.Queries;
using FreeParkingSystem.Accounts.Data.Models;
using FreeParkingSystem.Accounts.Mappers;
using FreeParkingSystem.Accounts.Queries;
using FreeParkingSystem.Accounts.Validators;
using FreeParkingSystem.Common;
using FreeParkingSystem.Common.Messages;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace FreeParkingSystem.Accounts.Tests
{
	public class AccountsModuleTests : IDisposable
	{
		private readonly IContainer _sut;
		public AccountsModuleTests()
		{
			var containerBuilder = new Autofac.ContainerBuilder();

			// dummy setup
			containerBuilder.RegisterInstance(new DbContextOptionsBuilder<AccountsDbContext>()
				.UseInMemoryDatabase("tempdb")
				.Options);

			containerBuilder.RegisterInstance(new PasswordOptions(TestConstants.MinimumCharacters, TestConstants.MaximumCharacters, TestConstants.DefaultPasswordRequirements));

			containerBuilder.RegisterInstance(new EncryptionOptions(TestConstants.SecretKey));

			containerBuilder.RegisterType<MockAuthenticationService>().AsImplementedInterfaces();

			containerBuilder.RegisterModule<AccountsModule>();


			_sut = containerBuilder.Build();
		}

		[Fact]
		public void Container_ShouldHave_CreateUserHandler()
		{
			// Act
			var result = _sut.Resolve<IRequestHandler<CreateUserRequest, BaseResponse>>();

			// Assert
			result.ShouldNotBeNull();
			result.ShouldBeOfType<CreateUserHandler>();
		}

		[Fact]
		public void Container_ShouldHave_UserLoginHandler()
		{
			// Act
			var result = _sut.Resolve<IRequestHandler<UserLoginRequest, BaseResponse>>();

			// Assert
			result.ShouldNotBeNull();
			result.ShouldBeOfType<UserLoginHandler>();
		}

		[Fact]
		public void Container_ShouldHave_PasswordValidator()
		{
			// Act
			var result = _sut.Resolve<IValidate<Password>>();

			// Assert
			result.ShouldNotBeNull();
			result.ShouldBeOfType<PasswordValidator>();
		}

		[Fact]
		public void Container_ShouldHave_PasswordEncryptor()
		{
			// Act
			var result = _sut.Resolve<IEncrypt<Password>>();

			// Assert
			result.ShouldNotBeNull();
			result.ShouldBeOfType<PasswordEncryptor>();
		}

		[Fact]
		public void Container_ShouldHave_PasswordHasher()
		{
			// Act
			var result = _sut.Resolve<IHash<Password>>();

			// Assert
			result.ShouldNotBeNull();
			result.ShouldBeOfType<PasswordHasher>();
		}

		[Fact]
		public void Container_ShouldHave_PasswordManager()
		{
			// Act
			var result = _sut.Resolve<IPasswordManager>();

			// Assert
			result.ShouldNotBeNull();
			result.ShouldBeOfType<PasswordManager>();
		}

		[Fact]
		public void Container_ShouldHave_UserServices()
		{
			// Act
			var result = _sut.Resolve<IUserServices>();

			// Assert
			result.ShouldNotBeNull();
			result.ShouldBeOfType<UserServices>();
		}

		[Fact]
		public void Container_ShouldHave_SecurityClaimsMapper()
		{
			// Act
			var result = _sut.Resolve<IMap<UserClaim, Claim>>();

			// Assert
			result.ShouldNotBeNull();
			result.ShouldBeOfType<SecurityClaimsMapper>();
		}

		public void Dispose()
		{
			_sut?.Dispose();
		}
	}
}
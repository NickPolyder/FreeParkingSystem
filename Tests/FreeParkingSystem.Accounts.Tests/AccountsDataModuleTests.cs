using System;
using Autofac;
using FreeParkingSystem.Accounts.Commands;
using FreeParkingSystem.Accounts.Contract;
using FreeParkingSystem.Accounts.Contract.Commands;
using FreeParkingSystem.Accounts.Contract.Repositories;
using FreeParkingSystem.Accounts.Data;
using FreeParkingSystem.Accounts.Data.Mappers;
using FreeParkingSystem.Accounts.Data.Models;
using FreeParkingSystem.Accounts.Data.Repositories;
using FreeParkingSystem.Common;
using FreeParkingSystem.Common.Messages;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace FreeParkingSystem.Accounts.Tests
{
	public class AccountsDataModuleTests : IDisposable
	{
		private readonly IContainer _sut;
		public AccountsDataModuleTests()
		{
			var containerBuilder = new Autofac.ContainerBuilder();

			// dummy setup
			containerBuilder.RegisterInstance(new DbContextOptionsBuilder<AccountsDbContext>()
				.UseInMemoryDatabase("tempdb")
				.Options);
			
			containerBuilder.RegisterModule<AccountsDataModule>();


			_sut = containerBuilder.Build();
		}

		[Fact]
		public void Container_ShouldHave_AccountsDbContext()
		{
			// Act
			var result = _sut.Resolve<AccountsDbContext>();

			// Assert
			result.ShouldNotBeNull();
			result.ShouldBeOfType<AccountsDbContext>();
		}

		[Fact]
		public void Container_ShouldHave_ClaimsMapper()
		{
			// Act
			var result = _sut.Resolve<IMap<DbClaims, UserClaim>>();

			// Assert
			result.ShouldNotBeNull();
			result.ShouldBeOfType<ClaimsMapper>();
		}

		[Fact]
		public void Container_ShouldHave_UserMapper()
		{
			// Act
			var result = _sut.Resolve<IMap<DbUser, User>>();

			// Assert
			result.ShouldNotBeNull();
			result.ShouldBeOfType<UserMapper>();
		}

		[Fact]
		public void Container_ShouldHave_ClaimsRepository()
		{
			// Act
			var result = _sut.Resolve<IClaimsRepository>();

			// Assert
			result.ShouldNotBeNull();
			result.ShouldBeOfType<ClaimsRepository>();
		}

		[Fact]
		public void Container_ShouldHave_UserRepository()
		{
			// Act
			var result = _sut.Resolve<IUserRepository>();

			// Assert
			result.ShouldNotBeNull();
			result.ShouldBeOfType<UserRepository>();
		}

		public void Dispose()
		{
			_sut?.Dispose();
		}
	}
}
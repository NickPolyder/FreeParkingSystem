using System;
using AutoFixture;
using FreeParkingSystem.Accounts.Data.Mappers;
using FreeParkingSystem.Accounts.Data.Models;
using FreeParkingSystem.Accounts.Data.Repositories;
using FreeParkingSystem.Common;
using FreeParkingSystem.Testing;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace FreeParkingSystem.Accounts.Tests.Repositories
{
	public class UserRepositoryTests
	{
		private static void ContainerSetup(IFixture fixture)
		{

			UserSetup(fixture);
			MapperSetup(fixture);

		}

		private static void MapperSetup(IFixture fixture)
		{
			fixture.Build<IMap<DbUser, Contract.User>>()
				.FromFactory(() => new UserMapper(new ClaimsMapper()))
				.ToCustomization()
				.Customize(fixture);
		}


		private static void UserSetup(IFixture fixture)
		{
			fixture.Build<Contract.User>()
				.Without(user => user.Claims)
				.ToCustomization()
				.Customize(fixture);

			fixture.Build<DbUser>()
				.Without(user => user.Claims)
				.ToCustomization()
				.Customize(fixture);
		}

		[Theory]
		[InlineFixtureData(null)]
		[InlineFixtureData("")]
		[InlineFixtureData(" ")]
		public void UserExists_ShouldThrowExceptionWhenTypeIsNull(
			string userName,
			IMap<DbUser, Contract.User> mapper)
		{
			// Arrange

			var dbContext = GetDbContext();

			using (var sut = new UserRepository(dbContext, mapper))
			{

				// Act
				var exception = Record.Exception(() => sut.UserExists(userName));

				// Assert
				exception.ShouldNotBeNull();
				exception.ShouldBeOfType<ArgumentNullException>();
				(exception as ArgumentNullException).ParamName.ShouldBe(nameof(userName));
			}
		}

		[Theory, FixtureData]
		public void UserExists_ShouldReturnFalseWhenTheUserDoesNotExist(
			IMap<DbUser, Contract.User> mapper,
			Contract.User[] users,
			string userName)
		{
			// Arrange

			var dbContext = GetDbContext();

			using (var sut = new UserRepository(dbContext, mapper))
			{
				sut.AddRange(users);

				// Act
				var result = sut.UserExists(userName);

				// Assert
				result.ShouldBeFalse();
			}
		}

		[Theory, FixtureData]
		public void UserExists_ShouldReturnTrueWhenTheUserExists(
			IMap<DbUser, Contract.User> mapper,
			Contract.User[] users)
		{
			// Arrange

			var dbContext = GetDbContext();

			using (var sut = new UserRepository(dbContext, mapper))
			{
				sut.AddRange(users);

				// Act
				var result = sut.UserExists(users[0].UserName);

				// Assert
				result.ShouldBeTrue();
			}
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
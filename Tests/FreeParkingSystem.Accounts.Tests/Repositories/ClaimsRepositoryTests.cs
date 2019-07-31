using System;
using AutoFixture;
using FreeParkingSystem.Accounts.Contract;
using FreeParkingSystem.Accounts.Data.Models;
using FreeParkingSystem.Accounts.Data.Repositories;
using FreeParkingSystem.Common;
using FreeParkingSystem.Testing;
using Microsoft.EntityFrameworkCore;
using Moq;
using Shouldly;
using Xunit;

namespace FreeParkingSystem.Accounts.Tests.Repositories
{
	public class ClaimsRepositoryTests
	{
		private static void ContainerSetup(IFixture fixture)
		{
			DbContextSetup(fixture);
		}

		private static void DbContextSetup(IFixture fixture)
		{
			fixture.Build<AccountsDbContext>()
				.FromFactory(() =>
				{
					var options = new DbContextOptionsBuilder<AccountsDbContext>()
						.UseInMemoryDatabase("InMemory")
						.Options;

					return new AccountsDbContext(options);
				})
				.ToCustomization()
				.Customize(fixture);
		}

		[Theory, FixtureData]
		public void Ctor_ShouldThrowExceptionForNullContext(
			Mock<IMap<DbClaims, UserClaim>> claimsMapperMock)
		{
			// Arrange

			// Act
			var exception = Record.Exception(() => new ClaimsRepository(null, claimsMapperMock.Object));

			// Assert
			exception.ShouldNotBeNull();
			exception.ShouldBeOfType<ArgumentNullException>();
		}

		[Theory, FixtureData]
		public void Ctor_ShouldThrowExceptionForNullClaimsMapper(AccountsDbContext context)
		{
			// Arrange

			// Act
			var exception = Record.Exception(() => new ClaimsRepository(context, null));

			// Assert
			exception.ShouldNotBeNull();
			exception.ShouldBeOfType<ArgumentNullException>();
		}
	}
}
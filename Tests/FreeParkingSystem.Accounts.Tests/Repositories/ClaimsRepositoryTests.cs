using System;
using System.Linq;
using AutoFixture;
using AutoFixture.Xunit;
using FreeParkingSystem.Accounts.Contract;
using FreeParkingSystem.Accounts.Data.Mappers;
using FreeParkingSystem.Accounts.Data.Models;
using FreeParkingSystem.Accounts.Data.Repositories;
using FreeParkingSystem.Common;
using FreeParkingSystem.Common.ExtensionMethods;
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

			ClaimsSetup(fixture);
			MapperSetup(fixture);

		}

		private static void MapperSetup(IFixture fixture)
		{
			fixture.Build<IMap<DbClaims, UserClaim>>()
				.FromFactory(() => new ClaimsMapper())
				.ToCustomization()
				.Customize(fixture);
		}


		private static void ClaimsSetup(IFixture fixture)
		{
			fixture.Build<UserClaim>()
				.Without(claim => claim.User)
				.ToCustomization()
				.Customize(fixture);

			fixture.Build<DbClaims>()
				.Without(claim => claim.User)
				.ToCustomization()
				.Customize(fixture);
		}

		[Theory, FixtureData]
		public void GetClaimsByUser_ShouldRetrieveUserClaims(
			IMap<DbClaims, UserClaim> mapper,
			UserClaim[] claims,
			UserClaim[] userClaims,
			int userId)
		{
			// Arrange
			userClaims.ForEach(claim => claim.UserId = userId);

			var dbContext = GetDbContext();

			using (var sut = new ClaimsRepository(dbContext, mapper))
			{
				sut.AddRange(claims.Union(userClaims));

				// Act
				var results = sut.GetClaimsByUser(userId).ToArray();

				// Assert
				results.Length.ShouldBe(userClaims.Length);
				results.ForEach((claim, index) =>
				{
					claim.UserId.ShouldBe(userId);
					claim.Id.ShouldBe(userClaims[index].Id);
					claim.Type.ShouldBe(userClaims[index].Type);
					claim.Value.ShouldBe(userClaims[index].Value);
				});
			}
		}


		private static AccountsDbContext GetDbContext()
		{
			var options = new DbContextOptionsBuilder<AccountsDbContext>()
				.UseInMemoryDatabase("InMemory")
				.Options;

			var dbContext = new AccountsDbContext(options);
			return dbContext;
		}
	}
}
using System;
using System.Linq;
using AutoFixture;
using FreeParkingSystem.Accounts.Contract;
using FreeParkingSystem.Accounts.Data.Mappers;
using FreeParkingSystem.Accounts.Data.Models;
using FreeParkingSystem.Accounts.Data.Repositories;
using FreeParkingSystem.Common;
using FreeParkingSystem.Common.ExtensionMethods;
using FreeParkingSystem.Testing;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace FreeParkingSystem.Accounts.Tests.Repositories
{
	public class ClaimsRepositoryTests
	{
		private static void ContainerSetup(IFixture fixture)
		{
			MapperSetup(fixture);
		}

		private static void MapperSetup(IFixture fixture)
		{
			fixture.Build<IMap<DbClaims, UserClaim>>()
				.FromFactory(() => new ClaimsMapper())
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

		[Theory]
		[InlineFixtureData(null)]
		[InlineFixtureData("")]
		[InlineFixtureData(" ")]
		public void UserHasClaim_ShouldThrowExceptionWhenTypeIsNull(
			string type,
			IMap<DbClaims, UserClaim> mapper,
			int userId)
		{
			// Arrange

			var dbContext = GetDbContext();

			using (var sut = new ClaimsRepository(dbContext, mapper))
			{

				// Act
				var exception = Record.Exception(() => sut.UserHasClaim(userId, type));

				// Assert
				exception.ShouldNotBeNull();
				exception.ShouldBeOfType<ArgumentNullException>();
				(exception as ArgumentNullException).ParamName.ShouldBe(nameof(type));
			}
		}

		[Theory, FixtureData]
		public void UserHasClaim_ShouldReturnTrueIfTheUserHasAClaim(
			IMap<DbClaims, UserClaim> mapper,
			UserClaim[] claims,
			int userId)
		{
			// Arrange
			claims.ForEach(claim => claim.UserId = userId);

			var dbContext = GetDbContext();

			using (var sut = new ClaimsRepository(dbContext, mapper))
			{
				sut.AddRange(claims);

				// Act
				var result = sut.UserHasClaim(userId, claims[0].Type);

				// Assert
				result.ShouldBeTrue();
			}
		}

		[Theory]
		[InlineFixtureData(null)]
		[InlineFixtureData("")]
		[InlineFixtureData(" ")]
		public void GetClaimByType_ShouldThrowExceptionWhenTypeIsNull(
			string type,
			IMap<DbClaims, UserClaim> mapper,
			int userId)
		{
			// Arrange

			var dbContext = GetDbContext();

			using (var sut = new ClaimsRepository(dbContext, mapper))
			{

				// Act
				var exception = Record.Exception(() => sut.GetClaimByType(userId, type));

				// Assert
				exception.ShouldNotBeNull();
				exception.ShouldBeOfType<ArgumentNullException>();
				(exception as ArgumentNullException).ParamName.ShouldBe(nameof(type));
			}
		}

		[Theory, FixtureData]
		public void GetClaimByType_ShouldReturnTrueIfTheUserHasAClaim(
			IMap<DbClaims, UserClaim> mapper,
			UserClaim[] claims,
			int userId)
		{
			// Arrange
			claims.ForEach(claim => claim.UserId = userId);

			var dbContext = GetDbContext();

			using (var sut = new ClaimsRepository(dbContext, mapper))
			{
				sut.AddRange(claims);

				// Act
				var result = sut.GetClaimByType(userId, claims[0].Type);

				// Assert
				result.ShouldNotBeNull();
				result.Id.ShouldBe(claims[0].Id);
				result.UserId.ShouldBe(claims[0].UserId);
				result.Type.ShouldBe(claims[0].Type);
				result.Value.ShouldBe(claims[0].Value);
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
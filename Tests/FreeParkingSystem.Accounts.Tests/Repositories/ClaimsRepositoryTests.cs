using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using AutoFixture.Xunit;
using FreeParkingSystem.Accounts.Contract;
using FreeParkingSystem.Accounts.Data.Mappers;
using FreeParkingSystem.Accounts.Data.Models;
using FreeParkingSystem.Accounts.Data.Repositories;
using FreeParkingSystem.Common;
using FreeParkingSystem.Testing;
using Microsoft.EntityFrameworkCore;
using Moq;
using Newtonsoft.Json;
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


		[Theory, FixtureData]
		public void Add_WhenInputIsNull_ShouldThrowArgumentNullException(
			ClaimsRepository sut)
		{
			// Act
			var exception = Record.Exception(() => sut.Add(null));

			// Assert
			exception.ShouldNotBeNull();
			exception.ShouldBeOfType<ArgumentNullException>();
		}

		[Theory, FixtureData(ContainerMethod = nameof(ClaimsSetup))]
		public void Add_ShouldCallReverseMapAndMap(
			[Frozen] Mock<IMap<DbClaims, UserClaim>> claimMapperMock,
			UserClaim claim,
			DbClaims dbClaims
			)
		{
			// Arrange
			claimMapperMock.Setup(mapper => mapper.ReverseMap(claim, It.IsAny<IDictionary<object, object>>()))
				.Returns(dbClaims).Verifiable();

			claimMapperMock.Setup(mapper => mapper.Map(dbClaims, It.IsAny<IDictionary<object, object>>()))
				.Returns(claim).Verifiable();

			var dbContext = GetDbContext();

			var sut = new ClaimsRepository(dbContext, claimMapperMock.Object);

			// Act
			sut.Add(claim);

			// Assert
			claimMapperMock.VerifyAll();

		}

		[Theory, FixtureData]
		public void Add_ShouldSaveTheClaim(
			IMap<DbClaims, UserClaim> claimsMapper,
			UserClaim claim)
		{
			// Arrange
			claim.Id = default;

			var dbContext = GetDbContext();

			var sut = new ClaimsRepository(dbContext, claimsMapper);

			// Act
			var result = sut.Add(claim);
			var savedItem = dbContext.Claims.Find(result.Id);

			// Assert
			savedItem.ShouldNotBeNull();
			savedItem.Id.ShouldNotBe(default);
			savedItem.Id.ShouldBe(result.Id);
			savedItem.UserId.ShouldBe(result.UserId);
			savedItem.ClaimType.ShouldBe(result.Type);
			savedItem.ClaimValue.ShouldBe(result.Value);
		}

		[Theory, FixtureData]
		public void Add_ShouldNotSaveTheClaimTwice(
			IMap<DbClaims, UserClaim> claimsMapper,
			UserClaim claim)
		{
			// Arrange
			claim.Id = default;

			var dbContext = GetDbContext();

			var sut = new ClaimsRepository(dbContext, claimsMapper);

			var result = sut.Add(claim);
			var dbItem = dbContext.Claims.Find(result.Id);
			dbContext.Entry(dbItem).State = EntityState.Detached;

			// Act
			var exception = Record.Exception(() => sut.Add(result));
			
			// Assert
			exception.ShouldNotBeNull();
			exception.ShouldBeOfType<ArgumentException>();
			exception.Message.ShouldBe("An item with the same key has already been added. Key: 1");
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
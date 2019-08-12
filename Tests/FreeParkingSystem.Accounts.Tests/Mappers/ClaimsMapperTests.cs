using AutoFixture;
using FreeParkingSystem.Accounts.Contract;
using FreeParkingSystem.Accounts.Data.Mappers;
using FreeParkingSystem.Accounts.Data.Models;
using FreeParkingSystem.Common.ExtensionMethods;
using FreeParkingSystem.Testing;
using Shouldly;
using Xunit;

namespace FreeParkingSystem.Accounts.Tests.Mappers
{
	public class ClaimsMapperTests
	{
		private static void ContainerSetup(IFixture fixture)
		{
			fixture.Build<DbClaims>()
				.Without(claim => claim.User)
				.ToCustomization()
				.Customize(fixture);
		}

		[Theory, FixtureData]
		public void Map_WhenDbClaimsIsNull_ShouldReturnNull(
			ClaimsMapper sut)
		{
			// Act
			var result = sut.Map((DbClaims)null);

			// Assert
			result.ShouldBeNull();
		}

		[Theory, FixtureData]
		public void Map_WhenUserClaimIsNull_ShouldReturnNull(
			ClaimsMapper sut)
		{
			// Act
			var result = sut.Map((UserClaim)null);

			// Assert
			result.ShouldBeNull();
		}

		[Theory, FixtureData]
		public void Map_DbClaims_WhenValid_ShouldReturnTheMappedInstance(
			DbClaims dbClaim,
			ClaimsMapper sut)
		{
			// Arrange

			// Act
			var result = sut.Map(dbClaim);

			// Assert
			result.ShouldNotBeNull();
			result.Id.ShouldBe(dbClaim.Id);
			result.UserId.ShouldBe(dbClaim.UserId);
			result.Type.ShouldBe(dbClaim.ClaimType);
			result.Value.ShouldBe(dbClaim.ClaimValue);
		}


		[Theory, FixtureData]
		public void Map_UserClaim_WhenValid_ShouldReturnTheMappedInstance(
				UserClaim claim,
				ClaimsMapper sut)
		{
			// Arrange

			// Act

			var result = sut.Map(claim);

			// Assert
			result.ShouldNotBeNull();
			result.Id.ShouldBe(claim.Id);
			result.ClaimType.ShouldBe(claim.Type);
			result.ClaimValue.ShouldBe(claim.Value);
			result.UserId.ShouldBe(claim.UserId);
		}
	}
}
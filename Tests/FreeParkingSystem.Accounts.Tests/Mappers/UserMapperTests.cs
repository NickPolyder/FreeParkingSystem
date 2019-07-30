using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AutoFixture;
using AutoFixture.Xunit;
using FreeParkingSystem.Accounts.Contract;
using FreeParkingSystem.Accounts.Data.Mappers;
using FreeParkingSystem.Accounts.Data.Models;
using FreeParkingSystem.Common;
using FreeParkingSystem.Common.ExtensionMethods;
using FreeParkingSystem.Testing;
using Moq;
using Shouldly;
using Xunit;

namespace FreeParkingSystem.Accounts.Tests.Mappers
{
	public class UserMapperTests
	{
		private static void ContainerSetup(IFixture fixture)
		{
			BuildUser(fixture);

			fixture.Build<IMap<DbClaims, Claim>>()
				.FromFactory(() => new ClaimsMapper())
				.ToCustomization()
				.Customize(fixture);
		}

		private static void BuildUser(IFixture fixture)
		{
			fixture.Build<Contract.User>()
				.Without(p => p.Claims)
				.ToCustomization()
				.Customize(fixture);

			fixture.Build<DbClaims>()
				.Without(p => p.User)
				.ToCustomization()
				.Customize(fixture);
		}

		[Theory, FixtureData]
		public void Map_WhenInputIsNull_ShouldReturnNull(
			UserMapper sut)
		{
			// Act
			var result = sut.Map(null);

			// Assert
			result.ShouldBeNull();
		}

		[Theory, FixtureData(ContainerMethod = nameof(BuildUser))]
		public void Map_WhenInputDoesNotHaveClaims_ShouldNotCallClaimMapper(
			[Frozen] Mock<IMap<DbClaims, Claim>> claimMapperMock,
			DbUser dbUser,
			UserMapper sut)
		{
			// Arrange
			dbUser.Claims = null;

			// Act
			sut.Map(dbUser);

			// Assert
			claimMapperMock.Verify(mapper => mapper.Map(It.IsAny<DbClaims>(), It.IsAny<IDictionary<object, object>>()), Times.Never);
		}

		[Theory, FixtureData(ContainerMethod = nameof(BuildUser))]
		public void Map_WhenInputItHasClaims_ShouldCallClaimMapper(
			[Frozen] Mock<IMap<DbClaims, Claim>> claimMapperMock,
			DbUser dbUser,
			UserMapper sut)
		{
			// Arrange
			claimMapperMock.Setup(mapper => mapper.Map(It.IsAny<DbClaims>(), It.IsAny<IDictionary<object, object>>()))
				.Returns(new Claim("type", "value"));

			// Act
			sut.Map(dbUser);

			// Assert
			claimMapperMock.Verify(mapper => mapper.Map(It.IsAny<DbClaims>(), It.IsAny<IDictionary<object, object>>()),
				Times.Exactly(dbUser.Claims.Count));
		}

		[Theory, FixtureData(ContainerMethod = nameof(BuildUser))]
		public void Map_WhenInputHasClaims_ShouldReturnTheMappedInstanceWithClaims(
			[Frozen] Mock<IMap<DbClaims, Claim>> claimMapperMock,
			DbUser dbUser,
			UserMapper sut)
		{
			// Arrange
			claimMapperMock.Setup(mapper => mapper.Map(It.IsAny<DbClaims>(), It.IsAny<IDictionary<object, object>>()))
				.Returns(new Claim("type", "value"));

			// Act
			var result = sut.Map(dbUser);

			// Assert
			result.ShouldNotBeNull();
			result.Id.ShouldBe(dbUser.Id);
			result.UserName.ShouldBe(dbUser.UserName);
			result.Password.IsEncrypted.ShouldBeTrue();
			result.Password.IsHashed.ShouldBeTrue();
			result.Password.Salt.ShouldBe(dbUser.Salt);
			result.Password.ToString().ShouldBe(dbUser.Password);
			result.Claims.Count.ShouldBe(dbUser.Claims.Count);
		}

		[Theory, FixtureData]
		public void Map_ShouldReturnMappedData(
			DbUser dbUser,
			UserMapper sut)
		{
			// Arrange
			dbUser.Claims.ForEach((dbUserClaim) => dbUserClaim.UserId = dbUser.Id);

			// Act
			var result = sut.Map(dbUser);

			// Assert
			result.ShouldNotBeNull();
			result.Id.ShouldBe(dbUser.Id);
			result.UserName.ShouldBe(dbUser.UserName);
			result.Password.IsEncrypted.ShouldBeTrue();
			result.Password.IsHashed.ShouldBeTrue();
			result.Password.Salt.ShouldBe(dbUser.Salt);
			result.Password.ToString().ShouldBe(dbUser.Password);

			result.Claims.ForEach((resultClaim) =>
			{
				var claimValue = dbUser.Claims.FirstOrDefault(claim => claim.ClaimType == resultClaim.Type &&
				                                                       claim.ClaimValue == resultClaim.Value &&
				                                                       claim.Id == resultClaim.GetId() &&
				                                                       claim.UserId == dbUser.Id);

				claimValue.ShouldNotBeNull();
			});
		}
	}
}
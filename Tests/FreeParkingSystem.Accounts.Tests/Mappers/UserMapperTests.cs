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

			fixture.Build<IMap<DbClaims, UserClaim>>()
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
		public void Map_WhenDbUserIsNull_ShouldReturnNull(
			UserMapper sut)
		{
			// Act
			var result = sut.Map((DbUser)null);

			// Assert
			result.ShouldBeNull();
		}


		[Theory, FixtureData]
		public void Map_WhenUserIsNull_ShouldReturnNull(
			UserMapper sut)
		{
			// Act
			var result = sut.Map((Contract.User)null);

			// Assert
			result.ShouldBeNull();
		}

		[Theory, FixtureData(ContainerMethod = nameof(BuildUser))]
		public void Map_WhenDbUserDoesNotHaveClaims_ShouldNotCallClaimMapper(
			[Frozen] Mock<IMap<DbClaims, UserClaim>> claimMapperMock,
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
		public void Map_WhenDbUserItHasClaims_ShouldCallClaimMapper(
			[Frozen] Mock<IMap<DbClaims, UserClaim>> claimMapperMock,
			DbUser dbUser,
			UserClaim claim,
			UserMapper sut)
		{
			// Arrange
			claimMapperMock.Setup(mapper => mapper.Map(It.IsAny<DbClaims>(), It.IsAny<IDictionary<object, object>>()))
				.Returns(claim);

			// Act
			sut.Map(dbUser);

			// Assert
			claimMapperMock.Verify(mapper => mapper.Map(It.IsAny<DbClaims>(), It.IsAny<IDictionary<object, object>>()),
				Times.Exactly(dbUser.Claims.Count));
		}

		[Theory, FixtureData(ContainerMethod = nameof(BuildUser))]
		public void Map_WhenDbUserHasClaims_ShouldReturnTheMappedInstanceWithClaims(
			[Frozen] Mock<IMap<DbClaims, UserClaim>> claimMapperMock,
			DbUser dbUser,
			UserClaim claim,
			UserMapper sut)
		{
			// Arrange
			claimMapperMock.Setup(mapper => mapper.Map(It.IsAny<DbClaims>(), It.IsAny<IDictionary<object, object>>()))
				.Returns(claim);

			// Act
			var result = sut.Map(dbUser);

			// Assert
			result.ShouldNotBeNull();
			result.Id.ShouldBe(dbUser.Id);
			result.UserName.ShouldBe(dbUser.UserName);
			result.Password.IsEncrypted.ShouldBeTrue();
			result.Password.IsHashed.ShouldBeTrue();
			result.Password.Salt.ShouldBe(dbUser.SaltAsString());
			result.Password.ToString().ShouldBe(dbUser.Password);
			result.Claims.Count.ShouldBe(dbUser.Claims.Count);
		}

		[Theory, FixtureData]
		public void Map_DbUser_ShouldReturnMappedData(
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
			result.Password.Salt.ShouldBe(dbUser.SaltAsString());
			result.Password.ToString().ShouldBe(dbUser.Password);

			result.Claims.ForEach((resultClaim) =>
			{
				var claimValue = dbUser.Claims.FirstOrDefault(claim => claim.ClaimType == resultClaim.Type &&
																	   claim.ClaimValue == resultClaim.Value &&
																	   claim.Id == resultClaim.Id &&
																	   claim.UserId == dbUser.Id);

				claimValue.ShouldNotBeNull();
			});
		}


		[Theory, FixtureData(ContainerMethod = nameof(BuildUser))]
		public void Map_WhenUserDoesNotHaveClaims_ShouldNotCallClaimMapper(
			[Frozen] Mock<IMap<DbClaims, UserClaim>> claimMapperMock,
			Contract.User user,
			UserMapper sut)
		{
			// Arrange
			user.Claims = null;

			// Act
			sut.Map(user);

			// Assert
			claimMapperMock.Verify(mapper => mapper.Map(It.IsAny<UserClaim>(), It.IsAny<IDictionary<object, object>>()), Times.Never);
		}

		[Theory, FixtureData(ContainerMethod = nameof(BuildUser))]
		public void Map_WhenUserHasClaims_ShouldReturnTheMappedInstanceWithClaims(
			[Frozen] Mock<IMap<DbClaims, UserClaim>> claimMapperMock,
			Contract.User user,
			List<UserClaim> claims,
			DbClaims dbClaim,
			UserMapper sut)
		{
			// Arrange
			user.Claims = claims;

			claimMapperMock.Setup(mapper => mapper.Map(It.IsAny<UserClaim>(), It.IsAny<IDictionary<object, object>>()))
				.Returns(dbClaim);

			// Act
			sut.Map(user);

			// Assert
			claimMapperMock.Verify(mapper => mapper.Map(It.IsAny<UserClaim>(), It.IsAny<IDictionary<object, object>>()),
				Times.Exactly(user.Claims.Count));
		}

		[Theory, FixtureData(ContainerMethod = nameof(BuildUser))]
		public void Map_WhenUserItHasClaims_ShouldCallClaimMapper(
			[Frozen] Mock<IMap<DbClaims, UserClaim>> claimMapperMock,
			Contract.User user,
			List<UserClaim> claims,
			DbClaims dbClaim,
			UserMapper sut)
		{
			// Arrange
			user.Claims = claims;

			claimMapperMock.Setup(mapper => mapper.Map(It.IsAny<UserClaim>(), It.IsAny<IDictionary<object, object>>()))
				.Returns(dbClaim);

			// Act
			var result = sut.Map(user);

			// Assert
			result.ShouldNotBeNull();
			result.Id.ShouldBe(user.Id);
			result.UserName.ShouldBe(user.UserName);
			result.Salt.ShouldBe(user.Password.SaltAsBytes());
			result.Password.ShouldBe(user.Password.ToString());
			result.Claims.Count.ShouldBe(user.Claims.Count);
		}


		[Theory, FixtureData]
		public void Map_User_ShouldReturnMappedData(
			Contract.User user,
			List<UserClaim> claims,
			UserMapper sut)
		{
			// Arrange
			claims.ForEach(claim => claim.UserId = user.Id);
			user.Claims = claims;

			// Act
			var result = sut.Map(user);

			// Assert
			result.ShouldNotBeNull();
			result.Id.ShouldBe(user.Id);
			result.UserName.ShouldBe(user.UserName);
			result.Salt.ShouldBe(user.Password.SaltAsBytes());
			result.Password.ShouldBe(user.Password.ToString());
			result.Claims.ForEach((resultClaim) =>
			{
				var claimValue = user.Claims.FirstOrDefault(claim => claim.Type == resultClaim.ClaimType &&
				                                                       claim.Value == resultClaim.ClaimValue &&
				                                                       claim.Id == resultClaim.Id);

				claimValue.ShouldNotBeNull();

				resultClaim.UserId.ShouldBe(user.Id);
			});
		}
	}
}
using System.Collections.Generic;
using System.Security.Claims;
using AutoFixture;
using FreeParkingSystem.Accounts.Contract;
using FreeParkingSystem.Accounts.Data.Mappers;
using FreeParkingSystem.Accounts.Data.Models;
using FreeParkingSystem.Common;
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
				.Without(p => p.User)
				.ToCustomization()
				.Customize(fixture);

			fixture.Build<Contract.User>()
				.Without(p => p.Claims)
				.ToCustomization()
				.Customize(fixture);
		}

		[Theory, FixtureData]
		public void Map_WhenInputIsNull_ShouldReturnNull(
			ClaimsMapper sut)
		{
			// Act
			var result = sut.Map(null);

			// Assert
			result.ShouldBeNull();
		}

		[Theory, FixtureData]
		public void Map_WhenInputIsNotNull_ShouldReturnTheMappedInstance(
			DbClaims dbClaim,
			ClaimsMapper sut)
		{
			// Arrange

			// Act
			var result = sut.Map(dbClaim);

			// Assert
			result.ShouldNotBeNull();
			result.Type.ShouldBe(dbClaim.ClaimType);
			result.Value.ShouldBe(dbClaim.ClaimValue);
			result.GetId().ShouldBe(dbClaim.Id);
		}

		[Theory, FixtureData]
		public void ReverseMap_WhenInputIsNull_ShouldReturnNull(
			ClaimsMapper sut)
		{
			// Act
			var result = sut.ReverseMap(null);

			// Assert
			result.ShouldBeNull();
		}

		[Theory, FixtureData]
		public void ReverseMap_WhenContextUserIsNotProvided_ShouldThrowException(
			string claimType,
			string claimValue,
			ClaimsMapper sut)
		{
			// Arrange
			var claim = new Claim(claimType, claimValue);

			// Act

			var result = Record.Exception(() => sut.ReverseMap(claim));

			// Assert
			result.ShouldNotBeNull();
			result.ShouldBeOfType<MappingContextException>();
			result.Message.ShouldBe(Contract.Resources.Validations.MappingContext_MissingUser);
		}


		[Theory, FixtureData]
		public void ReverseMap_WhenValid_ShouldReturnTheMappedInstance(
			Contract.User user,
			string claimType,
			string claimValue,
			ClaimsMapper sut)
		{
			// Arrange
			var claim = new Claim(claimType, claimValue);
			var dictionary = new Dictionary<object, object>
			{
				[typeof(Contract.User)] = user
			};

			// Act

			var result = sut.ReverseMap(claim, dictionary);

			// Assert
			result.ShouldNotBeNull();
			result.Id.ShouldBe(claim.GetId());
			result.ClaimType.ShouldBe(claim.Type);
			result.ClaimValue.ShouldBe(claim.Value);
			result.UserId.ShouldBe(user.Id);
		}
	}
}
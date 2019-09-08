using AutoFixture;
using FreeParkingSystem.Accounts.Contract;
using FreeParkingSystem.Accounts.Mappers;
using FreeParkingSystem.Common;
using FreeParkingSystem.Common.ExtensionMethods;
using FreeParkingSystem.Testing;
using Shouldly;
using Xunit;
using SecurityClaim = System.Security.Claims.Claim;
using SecurityClaimTypes = System.Security.Claims.ClaimTypes;

namespace FreeParkingSystem.Accounts.Tests.Mappers
{
	public class SecurityClaimsMapperTests
	{
		private static void ContainerSetup(IFixture fixture)
		{
			fixture.Build<SecurityClaim>()
				.FromFactory((string type, string value) => new SecurityClaim(type, value))
				.ToCustomization()
				.Customize(fixture);
		}

		[Theory,FixtureData]
		public void Map_ToUserClaim_WhenTheTypeIsSecurityEmail_ShouldChange_ToUserEmail(
			string value,
			SecurityClaimsMapper sut
			)
		{
			// Arrange
			var claim = new SecurityClaim(SecurityClaimTypes.Email,value);

			// Act
			var result = sut.Map(claim);

			// Assert
			result.Id.ShouldBe(default);
			result.Type.ShouldBe(ClaimTypes.Email.ToString());
			result.Value.ShouldBe(value);
		}

		[Theory, FixtureData]
		public void Map_ToUserClaim_WhenTheTypeIsSecurityRole_ShouldChange_ToUserRole(
			string value,
			SecurityClaimsMapper sut
		)
		{
			// Arrange
			var claim = new SecurityClaim(SecurityClaimTypes.Role, value);

			// Act
			var result = sut.Map(claim);

			// Assert
			result.Id.ShouldBe(default);
			result.Type.ShouldBe(ClaimTypes.Role.ToString());
			result.Value.ShouldBe(value);
		}


		[Theory, FixtureData]
		public void Map_ToUserClaim_ShouldReturnMappedClaim(
			SecurityClaim claim,
			SecurityClaimsMapper sut
		)
		{
			// Arrange
			
			// Act
			var result = sut.Map(claim);

			// Assert
			result.Id.ShouldBe(default);
			result.Type.ShouldBe(claim.Type);
			result.Value.ShouldBe(claim.Value);
		}

		[Theory, FixtureData]
		public void Map_ToClaim_WhenTheTypeIsUserEmail_ShouldChange_ToSecurityEmail(
			UserClaim claim,
			SecurityClaimsMapper sut
		)
		{
			// Arrange
			claim.Type = ClaimTypes.Email.ToString();

			// Act
			var result = sut.Map(claim);

			// Assert
			result.Type.ShouldBe(SecurityClaimTypes.Email);
			result.Value.ShouldBe(claim.Value);
		}

		[Theory, FixtureData]
		public void Map_ToClaim_WhenTheTypeIsUserRole_ShouldChange_ToSecurityRole(
			UserClaim claim,
			SecurityClaimsMapper sut
		)
		{
			// Arrange
			claim.Type = ClaimTypes.Role.ToString();

			// Act
			var result = sut.Map(claim);

			// Assert
			result.Type.ShouldBe(SecurityClaimTypes.Role);
			result.Value.ShouldBe(claim.Value);
		}


		[Theory, FixtureData]
		public void Map_ToClaim_ShouldReturnMappedClaim(
			UserClaim claim,
			SecurityClaimsMapper sut
		)
		{
			// Arrange

			// Act
			var result = sut.Map(claim);

			// Assert
			result.Type.ShouldBe(claim.Type);
			result.Value.ShouldBe(claim.Value);
		}
	}
}
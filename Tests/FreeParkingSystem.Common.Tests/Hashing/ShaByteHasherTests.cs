using FreeParkingSystem.Common.Hashing;
using FreeParkingSystem.Testing;
using Shouldly;
using Xunit;

namespace FreeParkingSystem.Common.Tests.Hashing
{
	public class ShaByteHasherTests
	{
		[Theory, FixtureData]
		public void ShouldHashValues(
			byte[] input,
			ShaByteHasher sut)
		{
			// Arrange

			// Act
			var result = sut.Hash(input);

			// Assert
			result.ShouldNotBe(input);

		}
	}
}
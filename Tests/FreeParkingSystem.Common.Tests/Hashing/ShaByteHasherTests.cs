using FreeParkingSystem.Common.Hashing;
using FreeParkingSystem.Testing;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace FreeParkingSystem.Common.Tests.Hashing
{
	public class ShaByteHasherTests
	{
		private readonly ITestOutputHelper _testOutputHelper;

		public ShaByteHasherTests(ITestOutputHelper testOutputHelper)
		{
			_testOutputHelper = testOutputHelper;
		}

		[Theory, ClassFixtureData(typeof(ShaByteData))]
		public void ShouldHashValues(
			byte[] input,
			byte[] expected,
			ShaByteHasher sut)
		{
			// Arrange

			// Act
			var result = sut.Hash(input);
			_testOutputHelper.WriteLine($"input: [{string.Join(", ", input)}] = [{string.Join(", ", result)}]");

			// Assert
			result.ShouldBe(expected);

		}
	}
}
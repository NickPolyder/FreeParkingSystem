using AutoFixture;
using AutoFixture.Xunit;
using FreeParkingSystem.Common.Hashing;
using FreeParkingSystem.Testing;
using Moq;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace FreeParkingSystem.Common.Tests.Hashing
{
	public class ShaStringHasherTests
	{
		private readonly ITestOutputHelper _testOutputHelper;

		public ShaStringHasherTests(ITestOutputHelper testOutputHelper)
		{
			_testOutputHelper = testOutputHelper;
		}


		private static void ContainerSetup(IFixture fixture)
		{
			fixture.Build<IHash<byte[]>>()
					   .FromFactory(() => new ShaByteHasher())
					   .ToCustomization()
					   .Customize(fixture);
		}

		[Theory, FixtureData(RunContainerSetup = false)]
		public void ShouldCall_ByteHasher(
			[Frozen]Mock<IHash<byte[]>> byteHasherMock,
			string input,
			ShaStringHasher sut)
		{
			// Arrange
			byteHasherMock
				.Setup(hasher => hasher.Hash(It.IsAny<byte[]>()))
				.Returns(new byte[0]);

			// Act
			sut.Hash(input);

			// Assert
			byteHasherMock.Verify(hasher => hasher.Hash(It.IsAny<byte[]>()), Times.Once);
		}

		[Theory]
		[InlineFixtureData("inputd9c54f6e-9659-476d-bd99-bea9dcbf2bc0", "41nHappakeRGp1HUQJK7cy7CTbfT8VMbRO6kaaw7SMkje+zeG2toyP8NMEOLU7IcUQVxQAPDLlb9oqRz3NaFaw==")]
		[InlineFixtureData("inputf5597d6c-396e-423d-bedd-3ab142aa497b", "JlFTgECJj09XxMdY6tq4FRP6rfaFHHz2j6r1chnoTQF6fmgOt/+VilaWIH4XgM2zChVGcjthYQ43jL/bWyCfTQ==")]
		[InlineFixtureData("input26891dc4-ed43-4f70-a3e3-2c0a16ecbc35", "DaQRwEmgBIwjjRnU12TDnMWcOGars5RomC3NCDXm8lEUyQx/YqoKH5wEBMw+d2j7hCNILCUfiRMwxEzI1Q0W/w==")]
		public void ShouldReturnHashedValue(
			string input,
			string expected,
			ShaStringHasher sut)
		{
			// Arrange

			// Act
			var result = sut.Hash(input);
			_testOutputHelper.WriteLine($"input: {input} = {result}");

			// Assert
			result.ShouldBe(expected);
		}
	}
}
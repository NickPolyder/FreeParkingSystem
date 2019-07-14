using AutoFixture;
using AutoFixture.Xunit;
using FreeParkingSystem.Common.Hashing;
using FreeParkingSystem.Testing;
using Moq;
using Shouldly;
using Xunit;

namespace FreeParkingSystem.Common.Tests.Hashing
{
	public class ShaStringHasherTests
	{

		public static void ContainerSetup(IFixture fixture)
		{
			fixture.Build<IHash<byte[]>>()
					   .FromFactory(() => new ShaByteHasher())
					   .ToCustomization()
					   .Customize(fixture);
		}

		[Theory, FixtureData]
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

		[Theory, FixtureData(typeof(ShaStringHasherTests))]
		public void ShouldReturnHashedValue(
			string input,
			ShaStringHasher sut)
		{
			// Arrange

			// Act
			var result = sut.Hash(input);

			// Assert
			result.ShouldNotBe(input);

		}
	}
}
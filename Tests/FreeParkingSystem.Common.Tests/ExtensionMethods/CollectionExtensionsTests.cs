using System;
using System.Collections.Generic;
using System.Linq;
using FreeParkingSystem.Common.ExtensionMethods;
using FreeParkingSystem.Testing;
using Shouldly;
using Xunit;

namespace FreeParkingSystem.Common.Tests.ExtensionMethods
{
	public class CollectionExtensionsTests
	{
		[Theory, FixtureData]
		public void ForEach_WhenActionIsNull_ShouldReturnImmediately(IEnumerable<string> array)
		{
			// Arrange
			Action<string> action = null;

			// Act
			var exception = Record.Exception(() => array.ForEach(action));

			// Assert
			exception.ShouldBeNull();
		}

		[Theory, FixtureData]
		public void ForEach_WhenActionIsNotNull_ShouldCallTheActionForEachItemInArray(IEnumerable<string> array)
		{
			// Arrange
			int counter = 0;
			Action<string> action = (str) => counter++;

			// Act
			array.ForEach(action);

			// Assert
			counter.ShouldBe(array.Count());
		}

		[Theory, FixtureData]
		public void ForEach_WithIndex_WhenActionIsNull_ShouldReturnImmediately(IEnumerable<string> array)
		{
			// Arrange
			Action<string, int> action = null;

			// Act
			var exception = Record.Exception(() => array.ForEach(action));

			// Assert
			exception.ShouldBeNull();
		}

		[Theory, FixtureData]
		public void ForEach_WithIndex_WhenActionIsNotNull_ShouldCallTheActionForEachItemInArray(IEnumerable<string> array)
		{
			// Arrange
			int counter = 0;
			Action<string, int> action = (str, index) => counter = index + 1;

			// Act
			array.ForEach(action);

			// Assert
			counter.ShouldBe(array.Count());
		}
	}
}
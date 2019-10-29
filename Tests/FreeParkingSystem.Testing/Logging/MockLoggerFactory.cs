using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace FreeParkingSystem.Common.Tests.Logging
{
	[ExcludeFromCodeCoverage]
	public class MockLoggerFactory: ILoggerFactory
	{
		private readonly ITestOutputHelper _testOutputHelper;

		public MockLoggerFactory(ITestOutputHelper testOutputHelper = null)
		{
			_testOutputHelper = testOutputHelper;
		}
		public void Dispose()
		{
		}

		public ILogger CreateLogger(string categoryName)
		{
			return new MockLogger<string>(_testOutputHelper, categoryName);
		}

		public void AddProvider(ILoggerProvider provider)
		{
			
		}
	}
}
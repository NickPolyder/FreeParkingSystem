using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;

namespace FreeParkingSystem.Common.Tests.Logging
{
	[ExcludeFromCodeCoverage]
	public class MockLoggerFactory: ILoggerFactory
	{
		public void Dispose()
		{
		}

		public ILogger CreateLogger(string categoryName)
		{
			return new MockLogger<string>(categoryName);
		}

		public void AddProvider(ILoggerProvider provider)
		{
			
		}
	}
}
using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace FreeParkingSystem.Common.Tests.Logging
{
	[ExcludeFromCodeCoverage]
	public class MockLogger<TClass> : ILogger<TClass>
	{
		private readonly ITestOutputHelper _testOutputHelper;
		private readonly string _category;

		public MockLogger(ITestOutputHelper testOutputHelper, string category = null)
		{
			_testOutputHelper = testOutputHelper;
			_category = category;
		}
		public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
		{
			var format = formatter ?? DefaultLog<TState>;

			Write($"{logLevel.ToString()}: {eventId} --> {format(state, exception)}");
		}

		public bool IsEnabled(LogLevel logLevel)
		{
			return true;
		}

		public IDisposable BeginScope<TState>(TState state)
		{
			return new LoggerScope<TState>(this, state);
		}

		private string DefaultLog<TState>(TState state, Exception exception)
		{
			return $"{state}: {exception.Message}{Environment.NewLine}{exception}";
		}

		private void Write(string msg)
		{
			var categoryFormat = string.IsNullOrWhiteSpace(_category) ? $"{_category} :==>" : null;
			_testOutputHelper?.WriteLine($"{categoryFormat}{msg}");
			System.Diagnostics.Debug.WriteLine($"{categoryFormat}{msg}");
		}

		private class LoggerScope<TState> : IDisposable
		{
			private readonly MockLogger<TClass> _logger;

			public LoggerScope(MockLogger<TClass> logger, TState state)
			{
				_logger = logger;
				_logger.Write($"BEGIN: {state}");
			}

			public void Dispose()
			{
				_logger.Write("END");
			}
		}
	}
}
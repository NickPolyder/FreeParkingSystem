using System;
using Microsoft.Extensions.Logging;

namespace FreeParkingSystem.Common.ExtensionMethods
{
	public static class FunctionExtensions
	{
		public static void AddLogging(this Action del, ILoggerFactory loggerFactory)
		{
			var logger = loggerFactory.CreateLogger("Delegate.Logging");
			AddLogging(del, logger);
		}
		public static void AddLogging(this Action del, ILogger logger)
		{
			using (var scope = logger.BeginScope($"Invoking: {del.Method.Name}"))
			{
				try
				{
					del.Invoke();
				}
				catch (Exception ex)
				{
					logger.Log(LogLevel.Error, ex, ex.Message);
				}
			}
		}
	}
}
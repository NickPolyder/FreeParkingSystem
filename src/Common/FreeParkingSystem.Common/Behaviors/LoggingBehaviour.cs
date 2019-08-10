using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FreeParkingSystem.Common.Behaviors
{
	public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest,TResponse>
	{
		private readonly ILogger _logger;

		public LoggingBehaviour(ILoggerFactory loggerFactory)
		{
			_logger = loggerFactory.CreateLogger($"{typeof(TRequest).Name} - {typeof(TResponse).Name}");
		}

		public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
		{
			TResponse response;
			using (_logger.BeginScope("Request: {0}, Response: {1}",typeof(TRequest).Name,typeof(TResponse).Name))
			{
				_logger.Log(LogLevel.Information,"Request Object: {0}",request);

				response = await next();

				_logger.Log(LogLevel.Information, "Response Object: {0}", response);
			}

			return response;
		}
	}
}
using System;
using System.Threading;
using System.Threading.Tasks;
using FreeParkingSystem.Common.ExtensionMethods;
using FreeParkingSystem.Common.Messages;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FreeParkingSystem.Common.Behaviors
{
	public class ExceptionBehaviour<TRequest> : IPipelineBehavior<TRequest, BaseResponse> where TRequest : BaseRequest
	{
		private readonly ILogger _logger;

		public ExceptionBehaviour(ILoggerFactory loggerFactory)
		{
			_logger = loggerFactory.CreateLogger($"{typeof(TRequest).Name}"); ;
		}

		public async Task<BaseResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<BaseResponse> next)
		{
			try
			{
				return await next();
			}
			catch (ValidationException ex)
			{
				_logger.LogError(ex, ex.Message);
				return request.ToValidationResponse(ex);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				return request.ToUnhandledResponse(ex);
			}
		}
	}
}

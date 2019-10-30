using System;
using FreeParkingSystem.Common.ExtensionMethods;
using Microsoft.Extensions.Logging;

namespace FreeParkingSystem.Common.MessageBroker.Contract
{
	public abstract class BaseMessageBrokerHandler<TMessage> : IMessageBrokerHandler<TMessage> where TMessage : BaseMessageBrokerRequest
	{
		protected readonly ILogger Logger;
		protected BaseMessageBrokerHandler(ILoggerFactory loggerFactory)
		{
			Logger = loggerFactory.CreateLogger<TMessage>();
		}

		public void Handle(TMessage message)
		{
			using (Logger.BeginScope(Contract.Resources.Messages.Process_Event.WithArgs(typeof(TMessage).Name, message.Id)))
			{
				try
				{
					Process(message);
				}
				catch (Exception ex)
				{
					ex.Data[nameof(BaseMessageBrokerRequest.Id)] = message.Id;
					ex.Data[nameof(BaseMessageBrokerRequest.CreationDate)] = message.CreationDate;

					Logger.LogCritical(ex,ex.Message);
				}
			}
		}

		public abstract void Process(TMessage message);
	}
}
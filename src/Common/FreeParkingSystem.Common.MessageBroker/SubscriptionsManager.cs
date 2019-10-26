using System;
using System.Collections.Generic;
using System.Linq;
using FreeParkingSystem.Common.ExtensionMethods;

namespace FreeParkingSystem.Common.MessageBroker
{
	public class SubscriptionsManager : ISubscriptionsManager
	{
		private bool _disposed;
		private readonly Dictionary<string, List<Type>> _messageHandlers;
		private readonly List<Type> _messageTypes;

		public SubscriptionsManager()
		{
			_messageHandlers = new Dictionary<string, List<Type>>();
			_messageTypes = new List<Type>();
		}
		public bool IsEmpty => _messageHandlers.Keys.Count == 0;

		public void Clear()
		{
			_messageTypes.Clear();
			_messageHandlers.Clear();
		}

		public void AddSubscription<TMessage, THandler>() where TMessage : BaseMessageBrokerRequest where THandler : IMessageBrokerHandler<TMessage>
		{
			var messageName = GetMessageName<TMessage>();

			var handlerType = typeof(THandler);

			if (!HasSubscriptions(messageName))
			{
				_messageHandlers.Add(messageName, new List<Type>());
			}

			if (_messageHandlers[messageName].All(type => type != handlerType))
			{
				_messageHandlers[messageName].Add(handlerType);
			}

			var messageType = typeof(TMessage);
			if (!_messageTypes.Contains(messageType))
			{
				_messageTypes.Add(messageType);
			}
		}

		public bool RemoveSubscription<TMessage, THandler>() where TMessage : BaseMessageBrokerRequest where THandler : IMessageBrokerHandler<TMessage>
		{
			var handlerType = typeof(THandler);
			var messageName = GetMessageName<TMessage>();

			var handlerToRemove = HasSubscriptions(messageName)
				? _messageHandlers[messageName].SingleOrDefault(type => type == handlerType)
				: null;

			if (handlerToRemove == null) return false;

			_messageHandlers[messageName].Remove(handlerToRemove);

			if (_messageHandlers[messageName].IsEmpty())
			{
				_messageHandlers.Remove(messageName);
				var messageType = _messageTypes.SingleOrDefault(e => e.Name == messageName);
				if (messageType != null)
				{
					_messageTypes.Remove(messageType);
				}
			}

			return true;

		}

		public bool HasSubscriptions<TMessage>() where TMessage : BaseMessageBrokerRequest
		{
			var messageName = GetMessageName<TMessage>();
			return HasSubscriptions(messageName);
		}

		public bool HasSubscriptions(string messageName)
		{
			return _messageHandlers.ContainsKey(messageName);
		}

		public Type GetMessageTypeByName(string messageName)
		{
			return _messageTypes.SingleOrDefault(t => t.Name == messageName);
		}

		public IEnumerable<Type> GetHandlers<TMessage>() where TMessage : BaseMessageBrokerRequest
		{
			var messageName = GetMessageName<TMessage>();
			return GetHandlers(messageName);
		}

		public IEnumerable<Type> GetHandlers(string messageName) => _messageHandlers[messageName];

		public string GetMessageName<TMessage>()
		{
			return typeof(TMessage).Name;
		}


		public void Dispose()
		{
			if (_disposed) return;

			_disposed = true;
			_messageTypes.Clear();
			_messageHandlers.Clear();
		}
	}
}
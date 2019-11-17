using System;
using System.IO;
using System.Net.Sockets;
using FreeParkingSystem.Common.MessageBroker.Contract;
using FreeParkingSystem.Common.MessageBroker.Contract.Options;
using Microsoft.Extensions.Logging;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace FreeParkingSystem.Common.MessageBroker
{
	public class RabbitMqBrokerConnection : IBrokerConnection
	{
		private readonly IConnectionFactory _connectionFactory;
		private readonly MessageBrokerOptions _options;
		private readonly ILogger _logger;
		private IConnection _connection;
		private bool _disposed;
		private readonly object _lock = new object();

		public RabbitMqBrokerConnection(IConnectionFactory connectionFactory,
			ILoggerFactory loggerFactory,
			MessageBrokerOptions options)
		{
			_connectionFactory = connectionFactory;
			_options = options;
			_logger = loggerFactory.CreateLogger<RabbitMqBrokerConnection>();
		}

		public bool IsConnected => _connection != null && _connection.IsOpen && !_disposed;

		public IModel CreateModel()
		{
			if (!Connect())
			{
				throw new InvalidOperationException(Contract.Resources.Errors.NotConnected);
			}

			return _connection.CreateModel();
		}

		public bool Connect()
		{
			if (_disposed) return false;

			if (IsConnected) return IsConnected;

			lock (_lock)
			{
				var policy = Policy.Handle<SocketException>()
					.Or<BrokerUnreachableException>()
					.WaitAndRetry(_options.RetryAttempts, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
						{
							_logger.LogWarning(ex, string.Format(Contract.Resources.Errors.RetryFailure, ex.Message));
						}
					);

				policy.Execute(() =>
				{
					_connection = _connectionFactory
						.CreateConnection();
				});

				if (IsConnected)
				{
					_connection.ConnectionShutdown += OnConnectionShutdown;
					_connection.CallbackException += OnCallbackException;
					_connection.ConnectionBlocked += OnConnectionBlocked;
				}
				else
				{
					_logger.LogCritical(Contract.Resources.Errors.ConnectionFailed);
				}

				return IsConnected;
			}
		}

		private void OnConnectionBlocked(object sender, ConnectionBlockedEventArgs e)
		{
			if (_disposed) return;

			Connect();
		}

		void OnCallbackException(object sender, CallbackExceptionEventArgs e)
		{
			if (_disposed) return;

			_logger.LogError(e.Exception, e.Exception.Message);

			Connect();
		}

		void OnConnectionShutdown(object sender, ShutdownEventArgs reason)
		{
			if (_disposed) return;

			_logger.LogWarning(reason.ReplyText);

			Connect();
		}

		public void Dispose()
		{
			if (_disposed) return;

			_disposed = true;

			try
			{
				if (_connection != null)
				{
					_connection.Close();
					_connection.ConnectionShutdown -= OnConnectionShutdown;
					_connection.CallbackException -= OnCallbackException;
					_connection.ConnectionBlocked -= OnConnectionBlocked;
					_connection.Dispose();
					_connection = null;
				}
			}
			catch (IOException ex)
			{
				_logger.LogCritical(ex, ex.Message);
			}
		}
	}
}
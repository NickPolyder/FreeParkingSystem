using System;
using System.Threading;
using System.Threading.Tasks;

namespace FreeParkingSystem.Common.Client
{

	public interface IClient : IDisposable
	{
		Task LoginAsync(string username, string password, CancellationToken cancellationToken = default);

		Task LogoutAsync(CancellationToken cancellationToken = default);


	}

	public delegate IClient ClientFactory(Type clientType, ClientOptions options);
	public interface IClientManager : IDisposable
	{
		Task<TClient> CreateClient<TClient>(ClientOptions options) where TClient : IClient;
	}


	public class ClientManager : IClientManager
	{
		private readonly ClientFactory _clientFactory;

		public ClientManager(ClientFactory clientFactory)
		{
			_clientFactory = clientFactory;
		}

		public Task<TClient> CreateClient<TClient>(ClientOptions options) where TClient : IClient
		{
			try
			{
				var client = (TClient)_clientFactory(typeof(TClient), options);

				return Task.FromResult(client);
			}
			catch (Exception ex)
			{
				return Task.FromException<TClient>(ex);
			}
		}

		public void Dispose()
		{
		}
	}


	public class ClientOptions
	{
		public Uri AuthUri { get; }
		public Uri BaseUri { get; }

		public ClientOptions(Uri uri) : this(uri, uri)
		{ }

		public ClientOptions(Uri baseUri, Uri authUri)
		{
			BaseUri = baseUri;
			AuthUri = authUri;
		}
	}
}
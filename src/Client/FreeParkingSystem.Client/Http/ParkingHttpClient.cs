using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace FreeParkingSystem.Common.Client.Http
{
	public class ParkingHttpClient : IHttpClient
	{
		private readonly HttpClient _httpClient;
		public HttpRequestHeaders DefaultRequestHeaders => _httpClient.DefaultRequestHeaders;

		/// <inheritdoc />
		public Uri BaseAddress
		{
			get => _httpClient.BaseAddress;
			set => _httpClient.BaseAddress = value;
		}

		/// <inheritdoc />
		public TimeSpan Timeout
		{
			get => _httpClient.Timeout;
			set => _httpClient.Timeout = value;
		}

		/// <inheritdoc />
		public long MaxResponseContentBufferSize
		{
			get => _httpClient.MaxResponseContentBufferSize;
			set => _httpClient.MaxResponseContentBufferSize = value;
		}

		public ParkingHttpClient(HttpClient httpClient)
		{
			_httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
		}

		/// <inheritdoc />
		public Task<string> GetStringAsync(string requestUri)
		{
			return _httpClient.GetStringAsync(requestUri);
		}

		/// <inheritdoc />
		public Task<string> GetStringAsync(Uri requestUri)
		{
			return _httpClient.GetStringAsync(requestUri);
		}

		/// <inheritdoc />
		public Task<byte[]> GetByteArrayAsync(string requestUri)
		{
			return _httpClient.GetByteArrayAsync(requestUri);
		}

		/// <inheritdoc />
		public Task<byte[]> GetByteArrayAsync(Uri requestUri)
		{
			return _httpClient.GetByteArrayAsync(requestUri);
		}

		/// <inheritdoc />
		public Task<Stream> GetStreamAsync(string requestUri)
		{
			return _httpClient.GetStreamAsync(requestUri);
		}

		/// <inheritdoc />
		public Task<Stream> GetStreamAsync(Uri requestUri)
		{
			return _httpClient.GetStreamAsync(requestUri);
		}

		/// <inheritdoc />
		public Task<HttpResponseMessage> GetAsync(string requestUri)
		{
			return _httpClient.GetAsync(requestUri);
		}

		/// <inheritdoc />
		public Task<HttpResponseMessage> GetAsync(Uri requestUri)
		{
			return _httpClient.GetAsync(requestUri);
		}

		/// <inheritdoc />
		public Task<HttpResponseMessage> GetAsync(string requestUri, HttpCompletionOption completionOption)
		{
			return _httpClient.GetAsync(requestUri, completionOption);
		}

		/// <inheritdoc />
		public Task<HttpResponseMessage> GetAsync(Uri requestUri, HttpCompletionOption completionOption)
		{
			return _httpClient.GetAsync(requestUri, completionOption);
		}

		/// <inheritdoc />
		public Task<HttpResponseMessage> GetAsync(string requestUri, CancellationToken cancellationToken)
		{
			return _httpClient.GetAsync(requestUri, cancellationToken);
		}

		/// <inheritdoc />
		public Task<HttpResponseMessage> GetAsync(Uri requestUri, CancellationToken cancellationToken)
		{
			return _httpClient.GetAsync(requestUri, cancellationToken);
		}

		/// <inheritdoc />
		public Task<HttpResponseMessage> GetAsync(string requestUri, HttpCompletionOption completionOption, CancellationToken cancellationToken)
		{
			return _httpClient.GetAsync(requestUri, completionOption, cancellationToken);
		}

		/// <inheritdoc />
		public Task<HttpResponseMessage> GetAsync(Uri requestUri, HttpCompletionOption completionOption, CancellationToken cancellationToken)
		{
			return _httpClient.GetAsync(requestUri, completionOption, cancellationToken);
		}

		/// <inheritdoc />
		public Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content)
		{
			return _httpClient.PostAsync(requestUri, content);
		}

		/// <inheritdoc />
		public Task<HttpResponseMessage> PostAsync(Uri requestUri, HttpContent content)
		{
			return _httpClient.PostAsync(requestUri, content);
		}

		/// <inheritdoc />
		public Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content, CancellationToken cancellationToken)
		{
			return _httpClient.PostAsync(requestUri, content, cancellationToken);
		}

		/// <inheritdoc />
		public Task<HttpResponseMessage> PostAsync(Uri requestUri, HttpContent content, CancellationToken cancellationToken)
		{
			return _httpClient.PostAsync(requestUri, content, cancellationToken);
		}

		/// <inheritdoc />
		public Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content)
		{
			return _httpClient.PutAsync(requestUri, content);
		}

		/// <inheritdoc />
		public Task<HttpResponseMessage> PutAsync(Uri requestUri, HttpContent content)
		{
			return _httpClient.PutAsync(requestUri, content);
		}

		/// <inheritdoc />
		public Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content, CancellationToken cancellationToken)
		{
			return _httpClient.PutAsync(requestUri, content, cancellationToken);
		}

		/// <inheritdoc />
		public Task<HttpResponseMessage> PutAsync(Uri requestUri, HttpContent content, CancellationToken cancellationToken)
		{
			return _httpClient.PutAsync(requestUri, content, cancellationToken);
		}

		/// <inheritdoc />
		public Task<HttpResponseMessage> DeleteAsync(string requestUri)
		{
			return _httpClient.DeleteAsync(requestUri);
		}

		/// <inheritdoc />
		public Task<HttpResponseMessage> DeleteAsync(Uri requestUri)
		{
			return _httpClient.DeleteAsync(requestUri);
		}

		/// <inheritdoc />
		public Task<HttpResponseMessage> DeleteAsync(string requestUri, CancellationToken cancellationToken)
		{
			return _httpClient.DeleteAsync(requestUri, cancellationToken);
		}

		/// <inheritdoc />
		public Task<HttpResponseMessage> DeleteAsync(Uri requestUri, CancellationToken cancellationToken)
		{
			return _httpClient.DeleteAsync(requestUri, cancellationToken);
		}

		/// <inheritdoc />
		public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
		{
			return _httpClient.SendAsync(request);
		}

		/// <inheritdoc />
		public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			return _httpClient.SendAsync(request, cancellationToken);
		}

		/// <inheritdoc />
		public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, HttpCompletionOption completionOption)
		{
			return _httpClient.SendAsync(request, completionOption);
		}

		/// <inheritdoc />
		public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, HttpCompletionOption completionOption, CancellationToken cancellationToken)
		{
			return _httpClient.SendAsync(request, completionOption, cancellationToken);
		}

		/// <inheritdoc />
		public void CancelPendingRequests()
		{
			_httpClient.CancelPendingRequests();
		}


		public void Dispose()
		{
			_httpClient.Dispose();
		}
	}
}
using System;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using FreeParkingSystem.Client.Http.Attributes;

namespace FreeParkingSystem.Client.Http
{

	public interface IHttpService : IDisposable
	{
		Task<TResponse> SendAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default);
	}

	public class HttpService : IHttpService
	{
		private readonly IHttpClient _httpClient;

		private readonly IHttpSerializer _httpSerializer;
		public HttpService(IHttpClient httpClient, IHttpSerializer httpSerializer)
		{
			_httpClient = httpClient;
			_httpSerializer = httpSerializer;
		}

		public async Task<TResponse> SendAsync<TRequest, TResponse>(TRequest request,
			CancellationToken cancellationToken = default)
		{
			var httpRoute = typeof(TRequest).GetCustomAttribute<HttpRouteAttribute>();
			if (httpRoute == null)
				throw new ArgumentNullException(nameof(httpRoute));

			var requestMessage = new HttpRequestMessage(httpRoute.Method, new Uri(new Uri("http://localhost:5001"), httpRoute.Path))
			{
				Content = await _httpSerializer.Serialize(request)
			};
			var response = await _httpClient.SendAsync(requestMessage, cancellationToken);

			if (response.IsSuccessStatusCode)
			{
				return await _httpSerializer.Deserialize<TResponse>(response.Content);
			}

			throw new Exception();
			//switch (response.StatusCode)
			//{
			//	case HttpStatusCode.BadRequest:
			//		throw new BadRequestException();
			//	default:
			//		throw new UnexpectedException();
			//}

		}

		public void Dispose()
		{
			_httpClient?.Dispose();
		}
	}
}
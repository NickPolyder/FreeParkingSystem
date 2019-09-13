using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace FreeParkingSystem.Common.Client.Tests
{
	public class HttpMessageHandlerMock : DelegatingHandler
	{
		public HttpRequestMessage CurrentRequest { get; private set; }

		public HttpMessageHandlerMock()
		{
		}
		protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			CurrentRequest = request;
			return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
			{
				Content = request.Content
			});
		}
	}
}
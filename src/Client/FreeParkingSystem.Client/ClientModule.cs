using System.Net.Http;
using Autofac;
using FreeParkingSystem.Client.Http;
using FreeParkingSystem.Client.Http.HttpHandlers;

namespace FreeParkingSystem.Client
{
	public class CommonClientModule: Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<HttpJsonSerializer>().AsImplementedInterfaces();

			builder.Register<IHttpClient>((container) =>
			{
				var httpHandler = new HttpClientHandler();
				var authenticationHandler =
					new AuthenticationHttpHandler(container.Resolve<IClientAuthorizationService>(), httpHandler);

				return CreateParkingHttpClient(authenticationHandler);
			});

			builder.Register<IClientAuthorizationService>((container) =>
			{
				var httpHandler = new HttpClientHandler();
				var parkingHttpClient = CreateParkingHttpClient(httpHandler);
				var httpService = CreateHttpService(parkingHttpClient, container);

				return new DefaultClientAuthorizationService(httpService);
			});

		}

		private static HttpService CreateHttpService(IHttpClient client, IComponentContext container)
		{
			return new HttpService(client,container.Resolve<IHttpSerializer>());
		}
		private static ParkingHttpClient CreateParkingHttpClient(HttpMessageHandler httpHandler)
		{
			return new ParkingHttpClient(new HttpClient(httpHandler, true));
		}
	}
}
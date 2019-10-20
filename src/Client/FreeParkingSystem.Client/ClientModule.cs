using System.Net.Http;
using Autofac;
using FreeParkingSystem.Client.Http;
using FreeParkingSystem.Client.Http.HttpHandlers;
using FreeParkingSystem.Common.Authorization;

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
					new AuthenticationHttpHandler(container.Resolve<IUserContextAccessor>(), httpHandler);

				return new ParkingHttpClient(new HttpClient(authenticationHandler, true));
			});

			builder.RegisterType<HttpService>().AsImplementedInterfaces();
		}
	}
}
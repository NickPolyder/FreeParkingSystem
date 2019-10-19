using System.Net.Http;
using Autofac;
using FreeParkingSystem.Common.Client.Http;
using FreeParkingSystem.Common.Client.Http.HttpHandlers;

namespace FreeParkingSystem.Common.Client
{
	public class CommonClientModule: Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.Register<IHttpClient>((container) =>
			{
				var httpHandler = new HttpClientHandler();
				var authenticationHandler =
					new AuthenticationHttpHandler(container.Resolve<IClientAuthorizationService>(), httpHandler);

				return new ParkingHttpClient(new HttpClient(authenticationHandler,true));
			});
			
		}
	}
}
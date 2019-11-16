using Autofac;

namespace FreeParkingSystem.Common.Data
{
	public class CommonDataModule : Autofac.Module
	{

		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<CommonFunctionsRepository>().AsImplementedInterfaces();
		}
	}
}

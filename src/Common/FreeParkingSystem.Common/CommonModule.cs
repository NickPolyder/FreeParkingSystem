using System.Reflection;
using Autofac;
using FreeParkingSystem.Common.Behaviors;
using FreeParkingSystem.Common.Encryption;
using FreeParkingSystem.Common.Hashing;
using MediatR;

namespace FreeParkingSystem.Common
{
	public class CommonModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();

			builder.Register<ServiceFactory>(ctx =>
			{
				var c = ctx.Resolve<IComponentContext>();
				return t => c.Resolve(t);
			});

			builder.RegisterGeneric(typeof(LoggingBehaviour<,>)).As(typeof(IPipelineBehavior<,>));
			builder.RegisterGeneric(typeof(ExceptionBehaviour<>)).As(typeof(IPipelineBehavior<,>));

			builder.RegisterType<ShaByteHasher>().AsImplementedInterfaces();
			builder.RegisterType<ShaStringHasher>().AsImplementedInterfaces();
			builder.RegisterType<AesByteEncryptor>().AsImplementedInterfaces();
			builder.RegisterType<AesStringEncryptor>().AsImplementedInterfaces();
		}
	}
}

using System.Reflection;
using Autofac;
using MediatR;

namespace FreeParkingSystem.Common.ExtensionMethods
{
	public static class ContainerBuilderExtensions
	{
		public static ContainerBuilder AddAssemblyForMediatR<T>(this ContainerBuilder builder)
		{
			return builder.AddAssemblyForMediatR(typeof(T).GetTypeInfo().Assembly);
		}

		public static ContainerBuilder AddAssemblyForMediatR(this ContainerBuilder builder, Assembly assembly)
		{
			var mediatrOpenTypes = new[]
			{
				typeof(IRequestHandler<,>),
				typeof(INotificationHandler<>),
			};

			foreach (var mediatrOpenType in mediatrOpenTypes)
			{
				builder
					.RegisterAssemblyTypes(assembly)
					.AsClosedTypesOf(mediatrOpenType)
					.AsImplementedInterfaces();
			}

			return builder;
		}
	}
}
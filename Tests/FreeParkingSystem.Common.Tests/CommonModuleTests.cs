using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Autofac;
using FreeParkingSystem.Common.Authorization;
using FreeParkingSystem.Common.Behaviors;
using FreeParkingSystem.Common.Encryption;
using FreeParkingSystem.Common.ExtensionMethods;
using FreeParkingSystem.Common.Hashing;
using FreeParkingSystem.Common.Messages;
using FreeParkingSystem.Common.Tests.Logging;
using FreeParkingSystem.Testing;
using MediatR;
using Shouldly;
using Xunit;

namespace FreeParkingSystem.Common.Tests
{
	public class CommonModuleTests : IDisposable
	{
		private readonly IContainer _sut;
		public CommonModuleTests()
		{
			var containerBuilder = new Autofac.ContainerBuilder();

			// dummy setup
			containerBuilder.RegisterInstance(new EncryptionOptions(CommonTestsConstants.SecretKey));

			containerBuilder.RegisterInstance(new MockLoggerFactory()).AsImplementedInterfaces();

			containerBuilder.RegisterInstance(new MockUserContextAccessor()).AsImplementedInterfaces();

			containerBuilder.RegisterModule<CommonModule>();

			_sut = containerBuilder.Build();
		}

		[Fact]
		public void Container_ShouldHave_Mediator()
		{
			// Act
			var result = _sut.Resolve<IMediator>();

			// Assert
			result.ShouldNotBeNull();
			result.ShouldBeOfType<Mediator>();
		}

		[Fact]
		public void Container_ShouldHave_ServiceFactory()
		{
			// Act
			var result = _sut.Resolve<ServiceFactory>();

			// Assert
			result.ShouldNotBeNull();
			result.ShouldBeOfType<ServiceFactory>();
		}

		[Fact]
		public void Container_ShouldHave_PipelineBehaviours()
		{
			// Act
			var result = _sut.Resolve<IEnumerable<IPipelineBehavior<BaseRequest, BaseResponse>>>()?.ToArray();

			// Assert
			var expectedLength = 3;
			result.ShouldNotBeNull();
			result.Length.ShouldBe(expectedLength);
			result.ForEach(pipeline =>
			{
				var type = pipeline.GetType();
				var isTheCorrectBehaviour = type == typeof(LoggingBehaviour<BaseRequest, BaseResponse>) ||
					   type == typeof(ExceptionBehaviour<BaseRequest>) ||
					   type == typeof(AuthorizeBehaviour<BaseRequest>);

				isTheCorrectBehaviour.ShouldBeTrue();
			});
		}

		[Fact]
		public void Container_ShouldHave_ShaByteHasher()
		{
			// Act
			var result = _sut.Resolve<IHash<byte[]>>();

			// Assert
			result.ShouldNotBeNull();
			result.ShouldBeOfType<ShaByteHasher>();
		}

		[Fact]
		public void Container_ShouldHave_ShaStringHasher()
		{
			// Act
			var result = _sut.Resolve<IHash<string>>();

			// Assert
			result.ShouldNotBeNull();
			result.ShouldBeOfType<ShaStringHasher>();
		}

		[Fact]
		public void Container_ShouldHave_AesByteEncryptor()
		{
			// Act
			var result = _sut.Resolve<IEncrypt<byte[]>>();

			// Assert
			result.ShouldNotBeNull();
			result.ShouldBeOfType<AesByteEncryptor>();
		}

		[Fact]
		public void Container_ShouldHave_AesStringEncryptor()
		{
			// Act
			var result = _sut.Resolve<IEncrypt<string>>();

			// Assert
			result.ShouldNotBeNull();
			result.ShouldBeOfType<AesStringEncryptor>();
		}

		public void Dispose()
		{
			_sut?.Dispose();
		}
	}
}
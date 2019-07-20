using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Kernel;
using FreeParkingSystem.Common;
using Xunit.Sdk;

namespace FreeParkingSystem.Testing
{
	/// <summary>
	/// Modified attribute in order to make AutoFixture.AutoDataAttribute work with .net standard projects.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
	public class FixtureDataAttribute : DataAttribute
	{
		private readonly Lazy<IFixture> _lazyFixture;

		public bool RunContainerSetup { get; set; } = true;

		public FixtureDataAttribute() : this(CreateDefaultFixture)
		{

		}

		protected FixtureDataAttribute(Func<IFixture> fixtureFactory)
		{
			_lazyFixture = new Lazy<IFixture>(fixtureFactory, LazyThreadSafetyMode.None);
		}

		public override IEnumerable<object[]> GetData(MethodInfo testMethod)
		{
			if (testMethod == null) throw new ArgumentNullException(nameof(testMethod));

			if (RunContainerSetup)
				testMethod.DeclaringType?.GetMethod("ContainerSetup")?.Invoke(null, new object[] { _lazyFixture.Value });

			var specimens = new List<object>();
			var context = new SpecimenContext(_lazyFixture.Value);

			foreach (var p in testMethod.GetParameters())
			{
				CustomizeFixture(p);

				var specimen = context.Resolve(p);
				specimens.Add(specimen);
			}

			return new[] { specimens.ToArray() };
		}

		private void CustomizeFixture(ParameterInfo p)
		{
			var customizeAttributes = p.GetCustomAttributes()
				.OfType<IParameterCustomizationSource>()
				.OrderBy(x => x);

			foreach (var ca in customizeAttributes)
			{
				var c = ca.GetCustomization(p);
				_lazyFixture.Value.Customize(c);
			}
		}

		protected static IFixture CreateDefaultFixture()
		{
			return new Fixture().Customize(new CompositeCustomization(new AutoMoqCustomization()));
		}
	}

}
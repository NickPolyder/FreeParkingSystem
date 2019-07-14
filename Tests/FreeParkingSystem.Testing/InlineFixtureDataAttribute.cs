using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FreeParkingSystem.Testing
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
	public class InlineFixtureDataAttribute : FixtureDataAttribute
	{
		private readonly object[] _data;

		public InlineFixtureDataAttribute(params object[] data)
		{
			_data = data;
		}

		public InlineFixtureDataAttribute(Type setupType, params object[] data) : base(setupType)
		{
			_data = data;
		}

		public override IEnumerable<object[]> GetData(MethodInfo testMethod)
		{
			var fullList = new List<object>();

			fullList.AddRange(_data);

			var baseData = base.GetData(testMethod).FirstOrDefault();

			if (baseData != null)
			{
				for (var index = fullList.Count; index < baseData.Length; index++)
				{
					fullList.Add(baseData[index]);
				}
			}

			return new[] { fullList.ToArray() };
		}
	}
}
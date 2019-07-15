using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FreeParkingSystem.Testing
{
	public class ClassFixtureDataAttribute : FixtureDataAttribute
	{
		private readonly Type _typeData;

		public ClassFixtureDataAttribute(Type typeData)
		{
			_typeData = typeData;
		}
		
		public override IEnumerable<object[]> GetData(MethodInfo testMethod)
		{
			if (testMethod == null) throw new ArgumentNullException(nameof(testMethod));

			IEnumerable<object[]> list = Activator.CreateInstance(_typeData) as IEnumerable<object[]>;

			if (list == null)
				throw new ArgumentException(_typeData.FullName + " must implement IEnumerable<object[]> to be used as ClassData for the test method named '" + testMethod.Name + "' on " + testMethod.DeclaringType.FullName);


			var fullList = new List<object>();

			foreach (var item in list)
			{
				fullList.AddRange(item);

				var baseData = base.GetData(testMethod).FirstOrDefault();

				if (baseData != null)
				{
					for (var index = fullList.Count; index < baseData.Length; index++)
					{
						fullList.Add(baseData[index]);
					}
				}

				yield return fullList.ToArray();

				fullList.Clear();
			}

		}
	}
}
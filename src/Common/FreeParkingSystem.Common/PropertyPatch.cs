using System;
using System.Collections.Generic;

namespace FreeParkingSystem.Common
{

	public class PropertyPatch<T, TProperty> : IPropertyPatch<T, TProperty>
	{
		private readonly T _instance;
		private readonly Func<T, TProperty> _getter;
		private readonly Action<T, TProperty> _setter;

		public PropertyPatch(T instance, Func<T, TProperty> getter, Action<T, TProperty> setter)
		{
			_instance = instance;
			_getter = getter;
			_setter = setter;
		}

		public bool HasChanged { get; private set; }

		public void Patch(T obj)
		{
			Patch(obj, EqualityComparer<TProperty>.Default);
		}

		public void Patch(T obj, IEqualityComparer<TProperty> propertyEqualityComparer)
		{
			if (propertyEqualityComparer == null)
				throw new ArgumentNullException(nameof(propertyEqualityComparer));

			var currentValue = _getter(_instance);
			var newValue = _getter(obj);

			if (currentValue == null && newValue == null)
				return;

			if (propertyEqualityComparer.Equals(currentValue, newValue))
				return;

			_setter(_instance, newValue);
			HasChanged = true;
		}
	}
}
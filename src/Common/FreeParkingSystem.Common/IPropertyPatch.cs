using System.Collections.Generic;

namespace FreeParkingSystem.Common
{
	public interface IPropertyPatch<T>
	{
		bool HasChanged { get; }
		void Patch(T obj);
	}
	public interface IPropertyPatch<T, TProperty>: IPropertyPatch<T>
	{
		void Patch(T obj, IEqualityComparer<TProperty> propertyEqualityComparer);
	}
}
using System;
using System.Linq.Expressions;
using System.Reflection;
using FreeParkingSystem.Common.Resources;

namespace FreeParkingSystem.Common.ExtensionMethods
{
	public static class PatchExtensions
	{
		public static IPropertyPatch<T, TProperty> CreatePatch<T, TProperty>(this T obj,
			Expression<Func<T, TProperty>> expression)
		{
			var member = (expression.Body as MemberExpression)?.Member ?? throw new ArgumentException(
							 Validations.Expression_MustBeAMemberExpression, nameof(expression));

			if (!(member is PropertyInfo propertyInfo))
			{
				throw new ArgumentException(Validations.Expressions_FieldMustBeAProperty, nameof(expression));
			}

			var getterMethod = propertyInfo.GetGetMethod();

			var getterFunc = new Func<T, TProperty>((T instance) => (TProperty)getterMethod.Invoke(instance, Array.Empty<object>()));

			var setterMethod = propertyInfo.GetGetMethod();

			var setterAction = new Action<T, TProperty>((T instance, TProperty value) => setterMethod.Invoke(instance, new object[] { value }));

			return new PropertyPatch<T, TProperty>(obj,getterFunc, setterAction);
		}
	}
}
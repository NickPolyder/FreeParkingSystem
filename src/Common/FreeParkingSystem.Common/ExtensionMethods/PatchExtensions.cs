using System;
using System.Collections.Generic;
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
			return CreatePatch(obj, expression, EqualityComparer<TProperty>.Default);
		}

		public static IPropertyPatch<T, TProperty> CreatePatch<T, TProperty>(this T obj,
			Expression<Func<T, TProperty>> expression,
			IEqualityComparer<TProperty> equalityComparer)
		{
			var accessors = GenerateAccessors(expression);
			return new PropertyPatch<T, TProperty>(obj, accessors.getter, accessors.setter, equalityComparer);
		}

		private static (Func<T, TProperty> getter, Action<T, TProperty> setter) GenerateAccessors<T, TProperty>(
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

			var setterMethod = propertyInfo.GetSetMethod();

			var setterAction = new Action<T, TProperty>((T instance, TProperty value) => setterMethod.Invoke(instance, new object[] { value }));

			return (getterFunc, setterAction);
		}
	}
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FreeParkingSystem.Common.ExtensionMethods
{
	public static class CollectionExtensions
	{
		public static void ForEach<TModel>(this IEnumerable<TModel> enumerable, Action<TModel> action)
		{
			if (action == null) return;

			foreach (var model in enumerable)
			{
				action(model);
			}
		}

		public static void ForEach<TModel>(this IEnumerable<TModel> enumerable, Action<TModel, int> action)
		{
			if (action == null) return;

			var index = 0;
			foreach (var model in enumerable)
			{
				action(model, index);
				index++;
			}
		}

		public static bool HasItems<T>(this IEnumerable<T> enumerable)
		{
			return enumerable.Any();
		}

		public static bool IsEmpty<T>(this IEnumerable<T> enumerable)
		{
			return !enumerable.HasItems();
		}
	}
}
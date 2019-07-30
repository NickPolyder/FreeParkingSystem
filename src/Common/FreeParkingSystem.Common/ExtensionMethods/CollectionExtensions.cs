using System;
using System.Collections.Generic;

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
	}
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace FreeParkingSystem.Common.ExtensionMethods
{
	public static class MapperExtensions
	{
		public static TOutput Map<TInput, TOutput>(this IMap<TInput, TOutput> map, TInput input)
		{
			return map.Map(input, new Dictionary<object, object>(0));
		}

		public static TInput Map<TInput, TOutput>(this IMap<TInput, TOutput> map, TOutput input)
		{
			return map.Map(input, new Dictionary<object, object>(0));
		}

		public static IEnumerable<TOutput> Map<TInput, TOutput>(this IMap<TInput, TOutput> map, IEnumerable<TInput> input, IDictionary<object, object> dictionary = null)
		{
			if (input == null)
				return new List<TOutput>(0);

			if (dictionary == null)
				dictionary = new Dictionary<object, object>(0);

			return input.Select(toMap => map.Map(toMap, dictionary));
		}

		public static IEnumerable<TInput> Map<TInput, TOutput>(this IMap<TInput, TOutput> map, IEnumerable<TOutput> input, IDictionary<object, object> dictionary = null)
		{
			if (input == null)
				return new List<TInput>(0);

			if (dictionary == null)
				dictionary = new Dictionary<object, object>(0);

			return input.Select(toMap => map.Map(toMap, dictionary));
		}
	}
}
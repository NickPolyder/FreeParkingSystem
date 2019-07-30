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

		public static TInput ReverseMap<TInput, TOutput>(this IMap<TInput, TOutput> map, TOutput input)
		{
			return map.ReverseMap(input, new Dictionary<object, object>(0));
		}

		public static IEnumerable<TOutput> Map<TInput, TOutput>(this IMap<TInput, TOutput> map, IEnumerable<TInput> input, IDictionary<object, object> dictionary = null)
		{
			if (input == null)
				return Array.Empty<TOutput>();

			if (dictionary == null)
				dictionary = new Dictionary<object, object>(0);

			return input.Select(toMap => map.Map(toMap, dictionary));
		}

		public static IEnumerable<TInput> ReverseMap<TInput, TOutput>(this IMap<TInput, TOutput> map, IEnumerable<TOutput> input, IDictionary<object, object> dictionary = null)
		{
			if (input == null)
				return Array.Empty<TInput>();

			if (dictionary == null)
				dictionary = new Dictionary<object, object>(0);

			return input.Select(toMap => map.ReverseMap(toMap, dictionary));
		}
	}
}
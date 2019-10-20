using System;

namespace FreeParkingSystem.Client.Infrastructure
{
	public static class AppDomainAccessorExtensions
	{
		public static T GetData<T>(this IAppDomainAccessor @this, string key)
		{
			var data = @this.GetData(key);

			return (T)data;
		}

		public static bool TryGetData<T>(this IAppDomainAccessor @this, string key, out T data)
		{
			data = default;

			if (@this.TryGetData(key, out var result))
			{
				data = (T) result;

				return true;
			}

			return false;
		}

		public static T GetOrCreate<T>(this IAppDomainAccessor @this, string key, Func<T> factoryFunc)
		{
			if(factoryFunc == null)
				throw new ArgumentNullException(nameof(factoryFunc));

			var data = @this.GetData<T>(key);

			if (data == null)
			{
				data = factoryFunc();
				@this.TrySetData(key, data);
			}

			return data;
		}
	}
}
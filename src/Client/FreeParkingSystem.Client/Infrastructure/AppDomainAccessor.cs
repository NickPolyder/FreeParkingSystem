using System;

namespace FreeParkingSystem.Client.Infrastructure
{
	public class AppDomainAccessor : IAppDomainAccessor
	{
		private readonly AppDomain _appDomain;
		public AppDomainAccessor() : this(AppDomain.CurrentDomain)
		{
		}

		public AppDomainAccessor(AppDomain appDomain)
		{
			_appDomain = appDomain;
		}

		public object this[string key]
		{
			get => GetData(key);
			set => SetData(key, value);
		}

		public object GetData(string key)
		{
			return _appDomain.GetData(key);
		}

		public bool ContainsKey(string key)
		{
			return !string.IsNullOrWhiteSpace(key) && GetData(key) != null;
		}

		public bool TryGetData(string key, out object data)
		{
			data = GetData(key);
			return data != null;
		}

		public void SetData(string key, object data)
		{
			_appDomain.SetData(key, data);
		}

		public bool TrySetData(string key, object data)
		{
			try
			{
				SetData(key, data);
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}
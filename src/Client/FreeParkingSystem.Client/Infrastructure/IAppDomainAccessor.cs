using System.Threading.Tasks;

namespace FreeParkingSystem.Client.Infrastructure
{
	public interface IAppDomainAccessor
	{
		object this[string key]
		{
			get;
			set;
		}

		object GetData(string key);

		bool ContainsKey(string key);

		bool TryGetData(string key,out object data);

		void SetData(string key, object data);

		bool TrySetData(string key, object data);
		
	}
}
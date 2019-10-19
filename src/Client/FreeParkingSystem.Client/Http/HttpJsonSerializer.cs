using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FreeParkingSystem.Client.Http
{
	public interface IHttpSerializer
	{
		Task<HttpContent> Serialize<TObject>(TObject obj);

		Task<TObject> Deserialize<TObject>(HttpContent content);
	}

	public class HttpJsonSerializer: IHttpSerializer
	{
		public const string MediaType = "application/json";
		public Task<HttpContent> Serialize<TObject>(TObject obj)
		{
			var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, MediaType);

			return Task.FromResult((HttpContent)content);
		}

		public async Task<TObject> Deserialize<TObject>(HttpContent content)
		{
			var jsonString = await content.ReadAsStringAsync();

			return JsonConvert.DeserializeObject<TObject>(jsonString);
		}
	}
}
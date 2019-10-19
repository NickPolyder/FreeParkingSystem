using System;
using System.Net.Http;

namespace FreeParkingSystem.Common.Client.Http.Attributes
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public class HttpRouteAttribute : Attribute
	{
		public HttpMethod Method { get; }
		public string Path { get; }

		public HttpRouteAttribute(string httpMethod, string path)
		{
			Method = GetHttpMethod(httpMethod);
			if (!Uri.TryCreate(path, UriKind.Relative, out Uri _))
			{
				throw new UriFormatException(path);
			}

			Path = path;
		}

		private HttpMethod GetHttpMethod(string httpMethod)
		{
			switch (httpMethod?.ToUpperInvariant())
			{
				case HttpMethodConstants.POST:
					return HttpMethod.Post;
				case HttpMethodConstants.GET:
					return HttpMethod.Get;
				case HttpMethodConstants.DELETE:
					return HttpMethod.Delete;
				case HttpMethodConstants.HEAD:
					return HttpMethod.Head;
				case HttpMethodConstants.OPTIONS:
					return HttpMethod.Options;
				case HttpMethodConstants.PUT:
					return HttpMethod.Put;
				case HttpMethodConstants.TRACE:
					return HttpMethod.Trace;
				default:
					throw new ArgumentOutOfRangeException(nameof(httpMethod), httpMethod, "Http Method not recognized.");

			}
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace FreeParkingSystem.Common.Messages
{
	public class Error
	{
		public string Title { get; }

		public string Message { get; }

		public List<KeyValuePair<string, string>> Meta { get; }

		public Error(string title, string message) : this(title, message, Array.Empty<KeyValuePair<string, string>>())
		{

		}

		public Error(string title, string message, IEnumerable<KeyValuePair<string,string>> meta)
		{
			Title = title;
			Message = message;
			Meta = meta?.ToList() ?? new List<KeyValuePair<string, string>>();
		}
	}
}
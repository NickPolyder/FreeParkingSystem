using System;
using System.Collections.Generic;

namespace FreeParkingSystem.Common.Messages
{
	public class ErrorBuilder
	{
		private string _title;

		private string _message;

		private readonly List<KeyValuePair<string, string>> _meta;

		public ErrorBuilder()
		{
			_meta = new List<KeyValuePair<string, string>>();
		}

		public ErrorBuilder AddTitle(string title)
		{
			_title = title;

			return this;
		}

		public ErrorBuilder AddMessage(string message)
		{
			_message = message;

			return this;
		}

		public ErrorBuilder AddRequestId(BaseResponse response)
		{
			AddMeta(nameof(BaseResponse.RequestId), response?.RequestId.ToString());
			return this;
		}

		public ErrorBuilder AddException(Exception exception)
		{
			AddMeta(nameof(Exception.HelpLink), exception.HelpLink);

			return this;
		}

		public ErrorBuilder AddMeta(string key, string value)
		{
			if (!string.IsNullOrWhiteSpace(key) && !string.IsNullOrWhiteSpace(value))
				_meta.Add(new KeyValuePair<string, string>(key, value));

			return this;
		}

		public Error Build()
		{
			return new Error(_title, _message, _meta);
		}
	}
}
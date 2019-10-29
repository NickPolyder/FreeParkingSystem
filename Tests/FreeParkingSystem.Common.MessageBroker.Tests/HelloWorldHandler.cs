using System;
using System.Threading.Tasks;
using FreeParkingSystem.Common.MessageBroker.Contract;
using Xunit.Abstractions;

namespace FreeParkingSystem.Common.MessageBroker.Tests
{
	public class HelloWorldHandler: IMessageBrokerHandler<HelloWorldMessage>
	{
		private readonly ITestOutputHelper _outputHelper;
		private readonly TaskCompletionSource<bool> _taskCompletionSource;
		public Task<bool> ReceivingTask { get; private set; }
		public HelloWorldHandler(ITestOutputHelper outputHelper)
		{
			_outputHelper = outputHelper;
			_taskCompletionSource = new TaskCompletionSource<bool>();
			ReceivingTask = _taskCompletionSource.Task;
			Task.Delay(TimeSpan.FromSeconds(30)).ContinueWith(_ => _taskCompletionSource.TrySetResult(false));
		}
		public void Handle(HelloWorldMessage message)
		{
			_outputHelper.WriteLine($"Incoming Message: {nameof(HelloWorldMessage)}: {message.Message}");
			_taskCompletionSource.TrySetResult(true);
		}
	}
}
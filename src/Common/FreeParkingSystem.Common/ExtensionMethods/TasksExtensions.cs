using System;
using System.Threading.Tasks;

namespace FreeParkingSystem.Common.ExtensionMethods
{
	public static class TasksExtensions
	{
		public static Task<T> AsTask<T>(this T obj)
		{
			return Task.FromResult(obj);
		}

		public static Task<T> AsTask<T>(this Exception exception)
		{
			if (exception == null)
				throw new ArgumentNullException(nameof(exception));

			return Task.FromException<T>(exception);
		}

		public static T RunSync<T>(this Task<T> task)
		{
			if (task == null)
				throw new ArgumentNullException(nameof(task));

			return task.GetAwaiter().GetResult();
		}

		public static void RunSync(this Task task)
		{
			if (task == null)
				throw new ArgumentNullException(nameof(task));

			task.GetAwaiter().GetResult();
		}

	}
}
﻿using System.Collections.Generic;

namespace FreeParkingSystem.Common
{
	public interface IMap<TInput, TOutput>
	{
		TOutput Map(TInput input, IDictionary<object,object> context);

		TInput Map(TOutput input, IDictionary<object, object> context);
	}
}
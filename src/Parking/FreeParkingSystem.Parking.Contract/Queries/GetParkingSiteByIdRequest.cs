﻿using FreeParkingSystem.Common.Authorization;
using FreeParkingSystem.Common.Messages;

namespace FreeParkingSystem.Parking.Contract.Queries
{
	[AuthorizeRequest]
	public class GetParkingSiteByIdRequest : BaseRequest
	{
		public int Id { get; }

		public GetParkingSiteByIdRequest(int id)
		{
			Id = id;
		}
	}
}
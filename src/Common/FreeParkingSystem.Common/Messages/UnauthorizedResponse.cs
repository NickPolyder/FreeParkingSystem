using System;
using FreeParkingSystem.Common.Authorization;

namespace FreeParkingSystem.Common.Messages
{
	public class UnauthorizedResponse : BaseResponse
	{
		public Role[] Roles { get; }
		public UnauthorizedResponse(Guid id, Role[] roles) : base(id)
		{
			Roles = roles;
		}
	}
}
using System;
using FreeParkingSystem.Common.Authorization;
using FreeParkingSystem.Common.ExtensionMethods;

namespace FreeParkingSystem.Common.Messages
{
	public class UnauthorizedResponse : BaseResponse
	{
		public Error Error { get; }
		public Role[] Roles { get; }
		public UnauthorizedResponse(Guid requestId, Role[] roles) : base(requestId)
		{
			Roles = roles;
			var builder = new ErrorBuilder()
				.AddTitle(Resources.Validations.Unauthorized_Title)
				.AddMessage(Resources.Validations.Unauthorized_Message)
				.AddRequestId(this);

			Roles.ForEach((role) => builder = builder.AddMeta(nameof(Role), role.ToString()));

			Error = builder.Build();

		}
	}
}
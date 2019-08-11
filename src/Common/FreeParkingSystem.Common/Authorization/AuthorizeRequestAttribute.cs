using System;

namespace FreeParkingSystem.Common.Authorization
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class AuthorizeRequestAttribute : Attribute
	{
		public Role? ProtectedBy { get; }

		public AuthorizeRequestAttribute()
		{
			ProtectedBy = null;
		}

		public AuthorizeRequestAttribute(Role protectedBy)
		{
			ProtectedBy = protectedBy;
		}
	}
}
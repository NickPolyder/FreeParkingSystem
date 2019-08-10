using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FreeParkingSystem.Common.Data;

namespace FreeParkingSystem.Accounts.Data.Models
{
	public class DbUser: IEntity
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string UserName { get; set; }

		public string Password { get; set; }

		public byte[] Salt { get; set; }

		[InverseProperty(nameof(DbClaims.User))]
		public ICollection<DbClaims> Claims { get; set; }


		public string SaltAsString() => Convert.ToBase64String(Salt);
	}
}
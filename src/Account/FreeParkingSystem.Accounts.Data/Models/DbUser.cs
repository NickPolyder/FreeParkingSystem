﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FreeParkingSystem.Accounts.Data.Models
{
	public class DbUser
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string UserName { get; set; }

		public string Password { get; set; }

		public string Salt { get; set; }

		[InverseProperty(nameof(DbClaims.User))]
		public ICollection<DbClaims> Claims { get; set; }
	}
}
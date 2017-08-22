﻿using System.ComponentModel.DataAnnotations;

namespace FreeParkingSystem.Common.Models
{
    public class User : BaseModel, IUser
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }

        [Phone]
        public string Phone { get; set; }

        public bool Active { get; set; }

        [Required]
        public IRole Role { get; set; }

        public string FullName()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
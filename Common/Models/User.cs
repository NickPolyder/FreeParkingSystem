using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FreeParkingSystem.Common.Helpers;
using FreeParkingSystem.Common.Services.Validation.Attributes;

namespace FreeParkingSystem.Common.Models
{
    public class User : BaseModel, IUser
    {

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [ValidateEmail]
        public string Email { get; set; }

        public string Password { get; set; }

        [Phone]
        public string Phone { get; set; }

        public bool Active { get; set; }

        [ListCount(1)]
        public IList<IRole> Roles { get; set; }

        public void AddRole(IRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            if (Roles == null)
            {
                Roles = new List<IRole> { role };
            }
            else if (Roles.Count == 0)
            {
                Roles.Add(role);
            }
            else if (Roles.All(rl => rl.Id != role.Id))
            {
                Roles.Add(role);
            }
        }

        public void ReplaceRole(IRole toReplace, IRole target)
        {
            if (toReplace == null)
            {
                throw new ArgumentNullException(nameof(toReplace));
            }

            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (Roles == null || Roles.Count == 0)
            {
                AddRole(target);
            }

            if (!RemoveRole(toReplace))
            {
                throw new InvalidOperationException($"The {toReplace.AccessLevel}:{toReplace.Description} does not exist.");
            }
            Roles.Add(target);
        }

        public bool RemoveRole(IRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            if (Roles == null || Roles.Count == 0)
            {
                return false;
            }

            var index = Roles.GetIndex(rl => role.Id == rl.Id);
            if (index < 0) return false;

            Roles.RemoveAt(index);
            return true;
        }

        public string FullName()
        {
            return $"{FirstName} {LastName}";
        }
    }
}

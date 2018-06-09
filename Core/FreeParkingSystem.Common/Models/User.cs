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

        private static object _lock = new object();

        private volatile static IUser _sysAdmin;

        public static IUser SysAdmin
        {
            get
            {
                _createSysAdminUser();

                return _sysAdmin;
            }
        }

        private static void _createSysAdminUser()
        {
            if (_sysAdmin != null) return;

            lock (_lock)
            {
                if (_sysAdmin == null)
                {
                    var createdAt = new DateTimeOffset(1992, 12, 24, 07, 30, 10, TimeSpan.Zero);
                    var user = new User
                    {
                        Id = Guid.NewGuid().ToString(),
                        FirstName = "Nick",
                        LastName = "Polyderopoulos",
                        Email = "nickpolyder@hotmail.gr",
                        Phone = "003052342342423",
                        Active = true,
                        CreatedAt = createdAt,
                        UpdatedAt = createdAt,
                        IsDeleted = false,
                        CreatedBy = null,
                    };
                    user.AddRole(Role.Administrator());
                    _sysAdmin = user;
                }
            }
        }
    }
}

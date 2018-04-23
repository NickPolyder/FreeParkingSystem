using System;
using System.Collections.Generic;
using FreeParkingSystem.Common.Models;
using Microsoft.AspNetCore.Identity;

namespace FreeParkingSystem.Accounts.Infastructure.Models
{
    public class ApplicationUser : IdentityUser<string>, IUser
    {
        private readonly IUser _user;
        public ApplicationUser()
        {
            _user = new User();
        }

        public override string Id { get => _user.Id; set => _user.Id = value; }
        /// <inheritdoc />
        public DateTimeOffset CreatedAt { get => _user.CreatedAt; set => _user.CreatedAt = value; }

        /// <inheritdoc />
        public DateTimeOffset UpdatedAt { get => _user.CreatedAt; set => _user.CreatedAt = value; }

        /// <inheritdoc />
        public bool IsDeleted { get => _user.IsDeleted; set => _user.IsDeleted = value; }

        /// <inheritdoc />
        public IUser CreatedBy { get => _user.CreatedBy; set => _user.CreatedBy = value; }

        /// <inheritdoc />
        public string FirstName { get => _user.FirstName; set => _user.FirstName = value; }

        /// <inheritdoc />
        public string LastName { get => _user.LastName; set => _user.LastName = value; }

        public override string Email { get => _user.Email; set => _user.Email = value; }

        /// <inheritdoc />
        public string Phone { get => _user.Phone; set => _user.Phone = value; }

        /// <inheritdoc />
        public bool Active { get => _user.Active; set => _user.Active = value; }

        /// <inheritdoc />
        public string FullName() => _user.FullName();

        /// <inheritdoc />
        public IList<IRole> Roles { get => _user.Roles; set => _user.Roles = value; }

        /// <inheritdoc />
        public void AddRole(IRole role) => _user.AddRole(role);

        /// <inheritdoc />
        public void ReplaceRole(IRole toReplace, IRole target) => _user.ReplaceRole(toReplace, target);

        /// <inheritdoc />
        public bool RemoveRole(IRole role) => _user.RemoveRole(role);
    }
}
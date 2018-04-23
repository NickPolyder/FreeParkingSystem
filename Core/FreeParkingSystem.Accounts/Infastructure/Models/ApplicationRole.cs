using System;
using FreeParkingSystem.Common.Models;
using Microsoft.AspNetCore.Identity;

namespace FreeParkingSystem.Accounts.Infastructure.Models
{
    public class ApplicationRole : IdentityRole<string>, IRole
    {
        private readonly IRole _role;
        public ApplicationRole()
        {
            _role = new Role();
        }

        public override string Id { get => _role.Id; set => _role.Id = value; }

        /// <inheritdoc />
        public DateTimeOffset CreatedAt { get => _role.CreatedAt; set => _role.CreatedAt = value; }

        /// <inheritdoc />
        public DateTimeOffset UpdatedAt { get => _role.UpdatedAt; set => _role.UpdatedAt = value; }

        /// <inheritdoc />
        public bool IsDeleted { get => _role.IsDeleted; set => _role.IsDeleted = value; }

        /// <inheritdoc />
        public IUser CreatedBy { get => _role.CreatedBy; set => _role.CreatedBy = value; }

        public override string Name { get => _role.Name; set => _role.Name = value; }

        /// <inheritdoc />
        public string Description { get => _role.Description; set => _role.Description = value; }

        /// <inheritdoc />
        public AccessLevel AccessLevel { get => _role.AccessLevel; set => _role.AccessLevel = value; }
    }
}
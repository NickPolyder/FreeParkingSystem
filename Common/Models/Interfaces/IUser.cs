// ReSharper disable CheckNamespace

using System.Collections.Generic;

namespace FreeParkingSystem.Common.Models
{
    public interface IUser : IBaseModel
    {

        string FirstName { get; set; }

        string LastName { get; set; }

        string Email { get; set; }

        string Password { get; set; }

        string Phone { get; set; }

        bool Active { get; set; }

        string FullName();

        IList<IRole> Roles { get; set; }

        void AddRole(IRole role);

        void ReplaceRole(IRole toReplace, IRole target);

        void RemoveRole(IRole role);

    }
}

using System.Collections.Generic;
using FreeParkingSystem.Common.Models;

namespace FreeParkingSystem.Common.Services
{
    public interface IAccountService : IStoreService<IUser>
    {
        IServiceResult<IUser> FindByEmail(string email);

        IServiceResult<IEnumerable<IUser>> GetUsersByRole(IRole role);

        IServiceResult<IUser> ToggleActive(IUser user, bool active = false);

        IServiceResult<IUser> CreateUser(string email, string firstName, string lastName, IRole role = null);

        IServiceResult<IUser> CreatePassword(IUser user, string password);

        IServiceResult<IUser> Update(IUser user);

        IServiceResult<bool> Delete(IUser user);

    }
}

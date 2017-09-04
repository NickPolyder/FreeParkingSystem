using FreeParkingSystem.Common.Models;

namespace FreeParkingSystem.Common.Services
{
    public interface IRoleService : IStoreService<IRole>
    {
        IServiceResult<IRole> FindByName(string name);

        IServiceResult<IRole> Add(string roleName, AccessLevel accessLevel = AccessLevel.Anonymous, string description = "");

        IServiceResult<IRole> Add(IRole role);

        IServiceResult<IRole> Update(IRole role);

        IServiceResult<IRole> Delete(IRole role);
    }
}

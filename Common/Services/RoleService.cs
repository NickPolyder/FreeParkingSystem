using System;
using System.Collections.Generic;
using System.Linq;
using FreeParkingSystem.Common.Models;
using FreeParkingSystem.Common.Repositories;
using FreeParkingSystem.Common.Services.Helpers;
using FreeParkingSystem.Common.Services.Validation;
using X.PagedList;

namespace FreeParkingSystem.Common.Services
{
    public class RoleService : IRoleService
    {
        private readonly IBaseRepository<Role> _roleRepository;

        private IValidationManager _validationManager;

        public RoleService(IBaseRepository<Role> roleRepository) : this(roleRepository, new ValidationManager())
        { }

        public RoleService(IBaseRepository<Role> roleRepository, IValidationManager validationManager)
        {
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _validationManager = validationManager ?? new ValidationManager();
        }

        public IServiceResult<IRole> Find(string id)
        {
            return ServiceResult.Wrap(() =>
            {
                var role = _roleRepository.GetById(id);
                return ServiceResult.Return<IRole>(role);
            });
        }

        public IServiceResult<IRole> Find(Func<IRole, bool> predicate)
        {
            return ServiceResult.Wrap(() =>
            {
                var role = _roleRepository.GetOneByFilter(predicate);
                return ServiceResult.Return<IRole>(role);
            });
        }

        public IServiceResult<IEnumerable<IRole>> Get<TKey>(Func<IRole, bool> wherePredicate = null, Func<IRole, TKey> orderPredicate = null)
        {
            return ServiceResult.Wrap(() =>
            {
                var roles = wherePredicate == null ?
                _roleRepository.GetAll(orderPredicate) :
                _roleRepository.GetByFilter(wherePredicate, orderPredicate);

                return roles == null ? ServiceResult.Return<IEnumerable<IRole>>() : ServiceResult.Return<IEnumerable<IRole>>(roles);
            });
        }

        public IServiceResult<IPagedList<IRole>> Get<TKey>(int skip, int take, Func<IRole, TKey> orderPredicate, Func<IRole, bool> wherePredicate = null)
        {
            if (_roleRepository is IBasePagedRepository<Role> pagedRepo)
            {
                return ServiceResult.Wrap(() =>
                {
                    var roles = wherePredicate == null
                        ? pagedRepo.GetAll(orderPredicate, skip, take)
                        : pagedRepo.GetByFilter(wherePredicate, orderPredicate, skip, take);

                    return ServiceResult.Return<IPagedList<IRole>>(roles);
                });
            }

            return ServiceResult.Return<IPagedList<IRole>>(new NotImplementedException());
        }

        public IServiceResult<IRole> FindByName(string name)
        {
            return ServiceResult.Wrap(() =>
            {
                var role = _roleRepository.GetOneByFilter(rl => rl.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
                return ServiceResult.Return<IRole>(role);
            });
        }

        public virtual IServiceResult<IRole> Add(string roleName, AccessLevel accessLevel = AccessLevel.Anonymous, string description = "")
        {
            return ServiceResult.Wrap(() =>
            {
                var role = new Role(accessLevel, roleName, description);

                return Add(role);
            });
        }

        public virtual IServiceResult<IRole> Add(IRole role)
        {
            return ServiceResult.Wrap(() =>
            {
                var validationResult = _validationManager.Validate(role);
                if (!validationResult.IsValid) return _createErrorFromValidationResult(validationResult);

                _roleRepository.Insert(role as Role);
                return ServiceResult.Return(role);
            });
        }

        public virtual IServiceResult<IRole> Update(IRole role)
        {
            return ServiceResult.Wrap(() =>
            {
                var validationResult = _validationManager.Validate(role);
                if (!validationResult.IsValid) return _createErrorFromValidationResult(validationResult);

                _roleRepository.Update(role as Role);
                return ServiceResult.Return(role);
            });
        }

        public virtual IServiceResult<IRole> Delete(IRole role)
        {
            return ServiceResult.Wrap(() =>
            {
                var validationResult = _validationManager.Validate(role);
                if (!validationResult.IsValid) return _createErrorFromValidationResult(validationResult);

                role.IsDeleted = true;
                _roleRepository.Update(role as Role);
                return ServiceResult.Return(role);
            });
        }

        private IServiceResult<IRole> _createErrorFromValidationResult(IValidationResult result)
        {
            var errorsArray = result.Errors.ToArray();
            return ServiceResult.Return<IRole>(errorsArray.Length == 1
                ? errorsArray[0]
                : new AggregateException(errorsArray));
        }
    }
}

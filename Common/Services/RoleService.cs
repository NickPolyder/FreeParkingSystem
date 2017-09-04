﻿using System;
using System.Collections.Generic;
using FreeParkingSystem.Common.Models;
using FreeParkingSystem.Common.Repositories;
using FreeParkingSystem.Common.Services.Helpers;
using X.PagedList;

namespace FreeParkingSystem.Common.Services
{
    public class RoleService : IRoleService
    {
        private readonly IBaseRepository<Role> _roleRepository;

        public RoleService(IBaseRepository<Role> roleRepository)
        {
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
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
                _roleRepository.Insert(role);
                return ServiceResult.Return<IRole>(role);
            });
        }

        public virtual IServiceResult<IRole> Add(IRole role)
        {
            return ServiceResult.Wrap(() =>
            {
                _roleRepository.Insert(role as Role);
                return ServiceResult.Return(role);
            });
        }

        public virtual IServiceResult<IRole> Update(IRole role)
        {
            return ServiceResult.Wrap(() =>
            {
                _roleRepository.Update(role as Role);
                return ServiceResult.Return(role);
            });
        }

        public virtual IServiceResult<IRole> Delete(IRole role)
        {
            return ServiceResult.Wrap(() =>
            {
                _roleRepository.Delete(role as Role);
                return ServiceResult.Return(role);
            });
        }
    }
}
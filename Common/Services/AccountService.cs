using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using FreeParkingSystem.Common.Models;
using FreeParkingSystem.Common.Repositories;
using FreeParkingSystem.Common.Services.Helpers;
using X.PagedList;

namespace FreeParkingSystem.Common.Services
{
    public class AccountService : IAccountService
    {
        private readonly IBaseRepository<User> _accountRepo;

        private readonly IRoleService _roleService;

        public AccountService(IBaseRepository<User> accountRepo, IRoleService roleService)
        {
            _accountRepo = accountRepo ?? throw new ArgumentNullException(nameof(accountRepo));
            _roleService = roleService ?? throw new ArgumentNullException(nameof(roleService));
        }

        public IServiceResult<IUser> Find(string id)
        {
            return ServiceResult.Wrap(() =>
            {
                var user = _accountRepo.GetById(id);

                return ServiceResult.Return<IUser>(user);
            });
        }

        public IServiceResult<IUser> Find(Func<IUser, bool> predicate)
        {
            return ServiceResult.Wrap(() =>
            {
                var user = _accountRepo.GetOneByFilter(predicate);

                return ServiceResult.Return<IUser>(user);
            });
        }

        public IServiceResult<IEnumerable<IUser>> Get<TKey>(Func<IUser, bool> wherePredicate = null, Func<IUser, TKey> orderPredicate = null)
        {
            return ServiceResult.Wrap(() =>
            {
                var users = wherePredicate == null ? _accountRepo.GetAll(orderPredicate)
                : _accountRepo.GetByFilter(wherePredicate, orderPredicate);

                return ServiceResult.Return<IEnumerable<IUser>>(users);
            });
        }

        public IServiceResult<IPagedList<IUser>> Get<TKey>(int skip, int take, Func<IUser, TKey> orderPredicate, Func<IUser, bool> wherePredicate = null)
        {
            if (_accountRepo is IBasePagedRepository<User> pagedRepo)
            {
                return ServiceResult.Wrap(() =>
                {
                    var users = wherePredicate == null
                        ? pagedRepo.GetAll(orderPredicate, skip, take)
                        : pagedRepo.GetByFilter(wherePredicate, orderPredicate, skip, take);

                    return ServiceResult.Return<IPagedList<IUser>>(users);
                });
            }

            return ServiceResult.Return<IPagedList<IUser>>(new NotImplementedException());
        }

        public IServiceResult<IUser> FindByEmail(string email)
        {
            return ServiceResult.Wrap(() =>
            {
                var user = _accountRepo.GetOneByFilter(usr => usr.Email.Equals(email));

                return ServiceResult.Return<IUser>(user);
            });
        }

        public IServiceResult<IEnumerable<IUser>> GetUsersByRole(IRole role)
        {
            return ServiceResult.Wrap(() =>
            {
                var users = _accountRepo.GetByFilter<int>((usr) => usr.Roles.Any(role.Equals));

                return ServiceResult.Return<IEnumerable<IUser>>(users);
            });
        }

        public virtual IServiceResult<IUser> ToggleActive(IUser user, bool active = false)
        {
            return ServiceResult.Wrap(() =>
            {
                user.Active = active;
                _accountRepo.Update(user as User);
                return ServiceResult.Return(user);
            });
        }

        public virtual IServiceResult<IUser> CreateUser(string email, string firstName, string lastName, IRole role = null)
        {
            return ServiceResult.Wrap(() =>
            {
                var user = new User
                {
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName
                };
                user.AddRole(role ?? Role.Member());
                _accountRepo.Insert(user);
                return ServiceResult.Return<IUser>(user);
            });
        }

        public virtual IServiceResult<IUser> CreatePassword(IUser user, string password)
        {
            return ServiceResult.Wrap(() =>
            {
                user.Password = _createHashedPassword(password);
                _accountRepo.Update(user as User);
                return ServiceResult.Return(user);
            });
        }

        public virtual IServiceResult<IUser> Update(IUser user)
        {
            return ServiceResult.Wrap(() =>
            {
                _accountRepo.Update(user as User);
                return ServiceResult.Return(user);
            });
        }

        public virtual IServiceResult<bool> Delete(IUser user)
        {
            return ServiceResult.Wrap(() =>
            {
                _accountRepo.Delete(user as User);
                return ServiceResult.Return(true);
            });
        }

        protected virtual string _createHashedPassword(string password)
        {
            var sha512 = SHA512.Create();
            var byteArray = Encoding.UTF8.GetBytes(password);
            var computedHash = sha512.ComputeHash(sha512.ComputeHash(byteArray));

            return Encoding.UTF8.GetString(computedHash);
        }
    }
}

using System;
using System.Linq;
using FreeParkingSystem.Common.Models;
using FreeParkingSystem.Common.Repositories;

namespace FreeParkingSystem.Common.Services.Validation
{
    public class CanUpdateRoleValidation<TRole> : IValidationComponent where TRole : IRole
    {
        private IBaseRepository<TRole> _baseRepository;

        public CanUpdateRoleValidation(IBaseRepository<TRole> baseRepo)
        {
            _baseRepository = baseRepo ?? throw new ArgumentNullException(nameof(baseRepo));
        }

        public bool CanValidate(object obj)
        {
            return obj is IRole;
        }

        public IValidationResult Validate(object obj)
        {
            if (!(obj is IRole role))
                return ValidationResult.CreateErrorResult(new MemberValidationException(obj,
                        $"The {nameof(obj)} is not of type {nameof(IRole)}")
                );

            var exists = _baseRepository.GetById(role.Id);
            if (exists == null)
            {
                return ValidationResult.CreateErrorResult(new MemberValidationException(obj,
                    $"The {nameof(IRole)} with Id: {role.Id} does not exist."));

            }

            var hasTheSame = _baseRepository.GetByFilter<TRole>(rr =>
                rr.Id != role.Id &&
                rr.Name.Equals(role.Name, StringComparison.InvariantCultureIgnoreCase) &&
                rr.Description.Equals(role.Description, StringComparison.InvariantCultureIgnoreCase) &&
                rr.AccessLevel == role.AccessLevel).Any();

            return !hasTheSame ? ValidationResult.CreateSuccessResult()
                : ValidationResult.CreateErrorResult(new MemberValidationException(obj,
                    $"The {nameof(IRole)} with {nameof(IRole.Name)}: {role.Name}\n" +
                    $"already exists."));


        }
    }
}

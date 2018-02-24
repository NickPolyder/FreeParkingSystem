using System;
using System.Linq;
using FreeParkingSystem.Common.Models;
using FreeParkingSystem.Common.Repositories;

namespace FreeParkingSystem.Common.Services.Validation
{
    public class RoleAlreadyExistsValidation<TRole> : IValidationComponent where TRole : IRole
    {
        private readonly IBaseRepository<TRole> _baseRepository;

        public RoleAlreadyExistsValidation(IBaseRepository<TRole> baseRepo)
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
                var hasTheSame = _baseRepository.GetByFilter<TRole>(rr =>
                     rr.Id != role.Id &&
                     rr.Name.Equals(role.Name, StringComparison.InvariantCultureIgnoreCase) &&
                     rr.Description.Equals(role.Description, StringComparison.InvariantCultureIgnoreCase) &&
                     rr.AccessLevel == role.AccessLevel).Any();
                if (!hasTheSame)
                    return ValidationResult.CreateSuccessResult();

                return ValidationResult.CreateErrorResult(new MemberValidationException(obj,
                    $"The {nameof(IRole)} with Name: {role.Name} already exists."));

            }

            return ValidationResult.CreateErrorResult(new MemberValidationException(obj,
                    $"The {nameof(IRole)} with Id: {role.Id} already exists.")
            );
        }
    }
}

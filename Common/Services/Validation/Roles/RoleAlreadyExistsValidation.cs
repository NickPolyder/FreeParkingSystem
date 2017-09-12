using System;
using FreeParkingSystem.Common.Models;
using FreeParkingSystem.Common.Repositories;

namespace FreeParkingSystem.Common.Services.Validation
{
    public class RoleAlreadyExistsValidation<TRole> : IValidationComponent where TRole : IRole
    {
        private IBaseRepository<TRole> _baseRepository;
        public RoleAlreadyExistsValidation(IBaseRepository<TRole> baseRepo)
        {
            _baseRepository = baseRepo ?? throw new ArgumentNullException(nameof(baseRepo));
        }

        public bool CanValidate(object obj)
        {
            return obj is IRole; //obj.GetType().IsSubclassOf(typeof(IRole));
        }

        public IValidationResult Validate(object obj)
        {
            if ((obj is IRole role))
            {
                var exists = _baseRepository.GetById(role.Id);
                if (exists == null) return ValidationResult.CreateSuccessResult();

                return ValidationResult.CreateErrorResult(
                    new MemberValidationException(obj,
                        $"The {nameof(IRole)} with Id: {role.Id} already exists.")
                );
            }

            return ValidationResult.CreateErrorResult(
                new MemberValidationException(obj,
                    $"The {nameof(obj)} is not of type {nameof(IRole)}")
            );
        }
    }
}
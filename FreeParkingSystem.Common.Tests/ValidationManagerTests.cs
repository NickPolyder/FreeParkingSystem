using System;
using System.Collections.Generic;
using System.Text;
using FreeParkingSystem.Common.Models;
using FreeParkingSystem.Common.Services.Validation;
using Xunit;

namespace FreeParkingSystem.Common.Tests
{
    public class ValidationManagerTests
    {
        [Fact]
        public void Member_Validation_Returns_Correct_Type()
        {
            // arrange  
            var user = new User
            {
                Active = true,
                FirstName = "Nick",
                LastName = "Pol"
            };
            user.CreatedAt = user.UpdatedAt = DateTime.Now;

            // act  

            var exception = new MemberValidationException(user, "Testing");

            // assert  

            Assert.Equal(typeof(User), exception.ValidationType);

        }

        [Fact]
        public void Validation_Manager_Returns_Validate_False()
        {
            // arrange  
            var user = new User
            {
                Active = true,
                FirstName = "Nick",
                LastName = "Pol"
            };
            user.CreatedAt = user.UpdatedAt = DateTime.Now;
            var validationManager = new ValidationManager();
            // act  

            var result = validationManager.Validate(user);

            // assert  
            Assert.Equal(false, result.IsValid);

        }

    }
}

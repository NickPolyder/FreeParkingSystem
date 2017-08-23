using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FreeParkingSystem.Common.Models;
using FreeParkingSystem.Common.Services.Validation;
using Xunit;

namespace FreeParkingSystem.Common.Tests
{
    public class ValidationManagerTests
    {
        [Fact]
        public void Member_Validation_Exception_Returns_Correct_Type()
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
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("el-gr");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("el-gr");
            var result = validationManager.Validate(user);

            // assert  
            Assert.Equal(false, result.IsValid);
            //It will be 2 because i require email and Role and none is provided.
            Assert.Equal(2, result.Errors?.Count() ?? 0);
        }

        [Fact]
        public void Validation_Manager_Returns_Validate_True()
        {
            // arrange  
            var user = new User
            {
                Active = true,
                FirstName = "Nick",
                LastName = "Pol",
                Email = "MyNewEmail@mail.com",
                Role = new Role() { AccessLevel = AccessLevel.Administrator, Description = "Administrator" }

            };
            user.CreatedAt = user.UpdatedAt = DateTime.Now;
            var validationManager = new ValidationManager();
            // act  

            var result = validationManager.Validate(user);

            // assert  
            Assert.Equal(true, result.IsValid);
            Assert.Equal(0, result.Errors?.Count() ?? 0);
        }

        [Fact]
        public void Validation_Manager_Returns_Email_Validation_Error()
        {
            // arrange  
            var user = new User
            {
                Active = true,
                FirstName = "Nick",
                LastName = "Pol",
                Email = "MyNewEmail",
                Role = new Role() { AccessLevel = AccessLevel.Administrator, Description = "Administrator" }

            };
            user.CreatedAt = user.UpdatedAt = DateTime.Now;
            var validationManager = new ValidationManager();
            // act  

            var result = validationManager.Validate(user);

            // assert  
            Assert.Equal(false, result.IsValid);
            Assert.Equal(1, result.Errors?.Count() ?? 0);
        }

    }
}

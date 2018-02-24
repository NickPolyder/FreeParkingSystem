using System;
using System.Collections.Generic;
using System.Text;
using FreeParkingSystem.Common.Models;
using FreeParkingSystem.Common.Services.Validation;
using Xunit;

namespace FreeParkingSystem.Common.Tests
{
    public class UserTests
    {
        [Fact]
        public void Member_Add_Role_Admin()
        {
            // arrange  
            var user = new User
            {
                Active = true,
                FirstName = "Nick",
                LastName = "Pol"
            };
            user.CreatedAt = user.UpdatedAt = DateTime.Now;
            var role = Role.Administrator();
            // act  

            user.AddRole(role);

            // assert  

            Assert.Equal(1, user.Roles.Count);
        }

        [Fact]
        public void Member_Add_Same_Role()
        {
            // arrange  
            var user = new User
            {
                Active = true,
                FirstName = "Nick",
                LastName = "Pol"
            };
            user.CreatedAt = user.UpdatedAt = DateTime.Now;
            var role = Role.Administrator();
            // act  

            user.AddRole(role);
            user.AddRole(role);
            // assert  

            Assert.Equal(1, user.Roles.Count);
        }

        [Fact]
        public void Member_Replace_Role()
        {
            // arrange  
            var user = new User
            {
                Active = true,
                FirstName = "Nick",
                LastName = "Pol"
            };
            user.CreatedAt = user.UpdatedAt = DateTime.Now;
            var admin = Role.Administrator();
            var anonymous = Role.Anonymous();
            user.AddRole(admin);
            // act  

            user.ReplaceRole(admin, anonymous);

            // assert  
            Assert.Equal(1, user.Roles.Count);
            Assert.Equal(anonymous, user.Roles[0]);
        }

        [Fact]
        public void Member_Replace_Wrong_Role()
        {
            // arrange  
            var user = new User
            {
                Active = true,
                FirstName = "Nick",
                LastName = "Pol"
            };
            user.CreatedAt = user.UpdatedAt = DateTime.Now;
            var admin = Role.Administrator();
            var anonymous = Role.Anonymous();
            user.AddRole(admin);

            // act  
            Action Throws = () => { user.ReplaceRole(anonymous, anonymous); };

            // assert  
            Assert.Throws<InvalidOperationException>(Throws);
        }

    }
}

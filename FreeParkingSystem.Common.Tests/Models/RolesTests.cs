using System;
using System.Collections.Generic;
using System.Text;
using FreeParkingSystem.Common.Models;
using Xunit;

namespace FreeParkingSystem.Common.Tests.Models
{
    public class RolesTests
    {

        [Fact]
        public void Role_Equals()
        {
            // arrange  
            var admin = Role.Administrator();
            var admin2 = Role.Administrator();

            // act  
            admin2.Id = admin.Id;

            // assert  
            Assert.Equal(true, admin.Equals(admin2));
        }
    }
}

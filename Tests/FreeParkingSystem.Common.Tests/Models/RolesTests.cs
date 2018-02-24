using System;
using System.Collections.Generic;
using System.Text;
using FreeParkingSystem.Common.Models;
using FreeParkingSystem.Common.Repositories;
using FreeParkingSystem.Common.Services.Validation;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace FreeParkingSystem.Common.Tests.Models
{
    public class RolesTests
    {
        private readonly ITestOutputHelper _output;

        public RolesTests(ITestOutputHelper output)
        {
            this._output = output;
        }

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

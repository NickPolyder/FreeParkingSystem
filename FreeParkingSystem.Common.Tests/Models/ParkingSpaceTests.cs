using System;
using System.Collections.Generic;
using System.Text;
using FreeParkingSystem.Common.Models;
using FreeParkingSystem.Common.Repositories;
using FreeParkingSystem.Common.Services;
using FreeParkingSystem.Common.Services.Helpers;
using FreeParkingSystem.Common.Services.Validation;
using Moq;
using Xunit;
using Xunit.Abstractions;
using System.Linq;

namespace FreeParkingSystem.Common.Tests.Models
{
    public class ParkingSpaceTests
    {
        private readonly ITestOutputHelper output;

        private ValidationManager validationManager;

        public ParkingSpaceTests(ITestOutputHelper output)
        {
            this.output = output;
            validationManager = new ValidationManager();
        }

        [Fact]
        public void Parking_Space_Floors_Not_Positive()
        {
            // arrange  
            var parkSpace = _createMock();
            parkSpace.Floors = -1;
            // act  
            var result = validationManager.Validate(parkSpace);

            // assert  
            var errors = result.Errors.ToList();
            Assert.Equal(false, result.IsValid);
            Assert.True(errors.Count > 0);
            foreach(var error in errors)
            {
                output.WriteLine(error.Message);
            }
        }


        private static ParkingSpace _createMock()
        {
            return new ParkingSpace()
            {
                ParkingType = new ParkingType { Text = "Text" },
                Position = new Position(0, 0, "Address"),
                Floors = 10,
                ParkSpaces = 10,

            };
        }



    }
}

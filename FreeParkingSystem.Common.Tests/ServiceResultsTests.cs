using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FreeParkingSystem.Common.Models;
using FreeParkingSystem.Common.Services;
using FreeParkingSystem.Common.Services.Helpers;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace FreeParkingSystem.Common.Tests
{
    public class ServiceResultsTests
    {
        private readonly ITestOutputHelper output;

        public ServiceResultsTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void Failure_Result_On_Exception()
        {
            // arrange  
            void Throws() => throw new Exception("Testing");

            // act  
            var result = ServiceResult.Wrap(Throws, (ex) =>
            {
                output.WriteLine("Exception: {0}", ex.Message);
                return ex;
            });
            // assert  

            Assert.Equal(true, result.IsFailure());
            Assert.NotNull((result as IFailureServiceResult)?.Exception);
        }
    }
}

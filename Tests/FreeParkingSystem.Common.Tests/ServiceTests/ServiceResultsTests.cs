using System;
using FreeParkingSystem.Common.Services;
using FreeParkingSystem.Common.Services.Helpers;
using Xunit;
using Xunit.Abstractions;

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

        [Fact]
        public void Returns_Empty_Result()
        {
            // arrange  

            // act  
            var result = ServiceResult.Return();

            // assert  
            Assert.Equal(true, result.IsEmpty());
        }

        [Fact]
        public void Returns_Success_Result()
        {
            // arrange  

            // act  
            var result = ServiceResult.Return(1);
            // assert  

            Assert.Equal(true, result.IsSuccess());
            Assert.Equal(1, (result as ISuccessServiceResult<int>).Value);
        }
    }
}

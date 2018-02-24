using FreeParkingSystem.Common.Services.Validation;
using FreeParkingSystem.Common.Services.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace FreeParkingSystem.Common.Tests.ServiceTests.Validation
{
    public class ValidationAttributesTests
    {
        private readonly ITestOutputHelper output;

        private ValidationManager validationManager;

        public ValidationAttributesTests(ITestOutputHelper output)
        {
            this.output = output;
            validationManager = new ValidationManager();
        }

        [Fact]
        public void Is_Positive_Attribute_On_Not_Null()
        {
            // arrange  
            var test = TestAttributes.Create();
            test.Number1 = -1;
            // act  
            var result = validationManager.Validate(test);

            // assert  
            var errors = result.Errors.ToList();
            Assert.Equal(false, result.IsValid);
            Assert.True(errors.Count > 0);
            foreach (var error in errors)
            {
                output.WriteLine(error.Message);
            }
        }

        [Fact]
        public void Is_Positive_Attribute_On_Nullable()
        {
            // arrange  
            var test = TestAttributes.Create();
            test.Number2 = -1;
            // act  
            var result = validationManager.Validate(test);

            // assert  
            var errors = result.Errors.ToList();
            Assert.Equal(false, result.IsValid);
            Assert.True(errors.Count > 0);
            foreach (var error in errors)
            {
                output.WriteLine(error.Message);
            }
        }

        [Fact]
        public void Is_Positive_Attribute_On_Null_Field()
        {
            // arrange  
            var test = TestAttributes.Create();
            test.Number2 = null;
            // act  
            var result = validationManager.Validate(test);

            // assert  
            var errors = result.Errors.ToList();
            Assert.Equal(true, result.IsValid);
            Assert.True(errors.Count == 0);

        }

        [Fact]
        public void List_Count_Attribute_On_Null_Field()
        {
            // arrange  
            var test = TestAttributes.Create();
            test.ListNumber = null;
            // act  
            var result = validationManager.Validate(test);

            // assert  
            var errors = result.Errors.ToList();
            Assert.Equal(false, result.IsValid);
            Assert.True(errors.Count > 0);
            foreach (var error in errors)
            {
                output.WriteLine(error.Message);
            }
        }

        [Fact]
        public void List_Count_Attribute_On_Empty_List()
        {
            // arrange  
            var test = TestAttributes.Create();
            test.ListNumber = new List<int>();
            // act  
            var result = validationManager.Validate(test);

            // assert  
            var errors = result.Errors.ToList();
            Assert.Equal(false, result.IsValid);
            Assert.True(errors.Count > 0);
            foreach (var error in errors)
            {
                output.WriteLine(error.Message);
            }
        }

        private class TestAttributes
        {

            [IsPositive]
            public int Number1 { get; set; }

            [IsPositive]
            public int? Number2 { get; set; }

            [ListCount(1)]
            public List<int> ListNumber { get; set; }

            public static TestAttributes Create()
            {
                return new TestAttributes
                {
                    Number1 = 1,
                    Number2 = 1,
                    ListNumber = new List<int>() { 1 }
                };
            }

        }
    }
}

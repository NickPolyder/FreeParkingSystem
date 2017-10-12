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

namespace FreeParkingSystem.Common.Tests.Models
{
    public class RolesTests
    {
        private readonly ITestOutputHelper output;

        private List<IRole> _roles = new List<IRole>();

        public RolesTests(ITestOutputHelper output)
        {
            this.output = output;
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

        [Fact]
        public void RoleService_Find_Success()
        {
            // arrange  
            var mockRepo = new Mock<IBaseRepository<Role>>();
            var mockRole = Role.Administrator();
            mockRepo.Setup(repo => repo.GetById("1")).Returns(mockRole as Role);
            var roleService = new RoleService(mockRepo.Object);

            // act  
            var getRole = roleService.Find("1");

            // assert  
            Assert.Equal(true, getRole.IsSuccess());
            Assert.Equal(mockRole.Name, getRole.AsSuccess().Value.Name);
        }

        [Fact]
        public void RoleService_Add()
        {

            // arrange  
            var mockRepo = new Mock<IBaseRepository<Role>>();
            var mockRole = Role.Administrator();
            mockRepo.Setup(repo => repo.Insert(It.IsAny<Role>())).Callback<Role>((role) =>
            {
                _roles.Add(role);
            });
            var roleService = new RoleService(mockRepo.Object);

            // act  
            var addRole = roleService.Add(mockRole);

            // assert  
            Assert.Equal(true, addRole.IsSuccess());
            Assert.Equal(mockRole.Name, addRole.AsSuccess().Value.Name);
            Assert.Equal(mockRole.Name, _roles[0].Name);
            Assert.Equal(1, _roles.Count);
        }


        [Fact]
        public void RoleService_AddWithParams()
        {
            // arrange  
            var mockRepo = new Mock<IBaseRepository<Role>>();
            var mockRole = Role.Administrator();
            mockRepo.Setup(repo => repo.Insert(It.IsAny<Role>())).Callback<Role>((role) =>
            {
                _roles.Add(role);
            });
            var roleService = new RoleService(mockRepo.Object);

            // act  
            var addRole = roleService.Add(mockRole.Name, mockRole.AccessLevel, mockRole.Description);

            // assert  
            Assert.Equal(true, addRole.IsSuccess());
            Assert.Equal(mockRole.Name, addRole.AsSuccess().Value.Name);
            Assert.Equal(mockRole.Name, _roles[0].Name);
            Assert.Equal(1, _roles.Count);
        }

        [Fact]
        public void RoleService_ThrowAlreadyExists()
        {
            // arrange  
            var mockRepo = new Mock<IBaseRepository<Role>>();
            var mockRole = Role.Administrator();
            mockRepo.Setup(repo => repo.Insert(It.IsAny<Role>())).Callback<Role>((role) =>
            {
                _roles.Add(role);
            });
            mockRepo.Setup(repo => repo.GetById(It.IsAny<string>())).Returns(() => mockRole as Role);
            var roleService = new RoleService(mockRepo.Object);

            // act  
            var addRole = roleService.Add(mockRole.Name, mockRole.AccessLevel, mockRole.Description);

            // assert  
            Assert.Equal(true, addRole.IsFailure());
            Assert.Equal(typeof(MemberValidationException), addRole.AsFailure().Exception.GetType());
            output.WriteLine("Exception: {0}", addRole.AsFailure().Exception.Message);
        }

        [Fact]
        public void RoleService_Update()
        {
            // arrange  
            var mockRepo = new Mock<IBaseRepository<Role>>();
            var mockRole = Role.Administrator();
            mockRepo.Setup(repo => repo.Update(It.IsAny<Role>())).Callback<Role>((role) =>
            {
                var index = _roles.FindIndex(tt => tt.Id == role.Id);
                _roles[index] = role;
            });
            mockRepo.Setup(repo => repo.GetById(It.IsAny<string>())).Returns(() => mockRole as Role);
            var roleService = new RoleService(mockRepo.Object);

            // act  
            var addRole = roleService.Add(mockRole.Name, mockRole.AccessLevel, mockRole.Description);

            // assert  
            Assert.Equal(true, addRole.IsFailure());
            Assert.Equal(typeof(MemberValidationException), addRole.AsFailure().Exception.GetType());
            output.WriteLine("Exception: {0}", addRole.AsFailure().Exception.Message);
        }

    }
}

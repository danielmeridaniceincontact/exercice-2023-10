using Moq;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Enums;
using Sat.Recruitment.Api.Interfaces;
using Sat.Recruitment.Api.Models;
using System;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UserControllerUnitTests
    {
        private readonly Mock<IUserService> _mockIUserService;
        private readonly Mock<IErrorService> _mockIErrorService;

        public UserControllerUnitTests()
        {
            _mockIUserService = new Mock<IUserService>();
            _mockIErrorService = new Mock<IErrorService>();
        }

        [Fact]
        public void Test_Validate_Creation_User_OK()
        {
            var userController = new UsersController(_mockIUserService.Object, _mockIErrorService.Object);
            var result = userController.CreateUser("Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354215", UserType.Normal, "124").Result;

            Assert.True(result.IsSuccess);
            Assert.Equal("User Created", result.Errors);
        }

        [Fact]
        public void Test_Validate_Creation_User_Failed_Duplicated()
        {
            _mockIUserService.Setup(x => x.CheckDuplicatedUser(It.IsAny<User>())).Returns(true);
            var userController = new UsersController(_mockIUserService.Object, _mockIErrorService.Object);
            var result = userController.CreateUser("Agustina", "Agustina@gmail.com", "Av. Juan G", "+349 1122354215", UserType.Normal, "124").Result;
            
            Assert.False(result.IsSuccess);
            Assert.Equal("The user is duplicated", result.Errors);
        }

        [Fact]
        public void Test_Validate_Creation_User_Failed_Any_Error()
        {
            var customExceptionMessage = "Custom Exception";
            _mockIUserService.Setup(x => x.CheckDuplicatedUser(It.IsAny<User>())).Throws(new Exception(customExceptionMessage));
            var userController = new UsersController(_mockIUserService.Object, _mockIErrorService.Object);
            var result = userController.CreateUser("Agustina", "Agustina@gmail.com", "Av. Juan G", "+349 1122354215", UserType.Normal, "124").Result;

            Assert.False(result.IsSuccess);
            Assert.Equal(customExceptionMessage, result.Errors);
        }

        [Fact]
        public void Test_Validate_Creation_User_Check_Field_Failed()
        {
            var customExceptionMessage = "The name is required";
            _mockIErrorService.Setup(x => x.ValidateErrors(string.Empty, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), ref It.Ref<string>.IsAny))
                .Callback(new MockTryParseCallback((string name, string email, string address, string phone, ref string errors) => errors = customExceptionMessage));
            var userController = new UsersController(_mockIUserService.Object, _mockIErrorService.Object);
            var result = userController.CreateUser(string.Empty, "Agustina@gmail.com", "Av. Juan G", "+349 1122354215", UserType.Normal, "124").Result;

            Assert.False(result.IsSuccess);
            Assert.Equal(customExceptionMessage, result.Errors);
        }

        delegate void MockTryParseCallback(string name, string email, string address, string phone, ref string errors);
    }
}

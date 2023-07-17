using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMyEvent.BLL.Contracts;
using BookMyEvent.BLL.Models;
using BookMyEvent.xUnitTests.BookMyEvent.BLL.tests.MockData;
using BookMyEvent.DLL.Contracts;
using db.Models;
using Moq;
using AutoMapper;
using BookMyEvent.BLL.Services;
using Microsoft.IdentityModel.Tokens;

namespace BookMyEvent.xUnitTests.BookMyEvent.BLL.tests.Services.tests
{
    public class UserServiceTest
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly IUserService _userService;
        private readonly Mapper _mapper;
        private readonly Mock<IAccountCredentialsRepository> _mockAccountCredentialsRepository;

        public UserServiceTest()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockAccountCredentialsRepository = new Mock<IAccountCredentialsRepository>();
            _userService = new UserService(_mockUserRepository.Object, _mockAccountCredentialsRepository.Object);
            _mapper = Automapper.InitializeAutomapper();
        }

        [Fact]
        public async Task AddUser_SuccessfullAddingTest()
        {
            // Arrange
            var user = UsersMockData.CreateUser();
            user.Password = "Test@123";
            var accountCredential = new AccountCredential
            {
                AccountCredentialsId = Guid.NewGuid(),
                Password = user.Password
            };

            // Setup the AddCredential method to return the accountCredential when called with any AccountCredential input
            _mockAccountCredentialsRepository.Setup(x => x.AddCredential(It.Is<AccountCredential>(cred => cred.Password == accountCredential.Password))).ReturnsAsync(accountCredential);
            var userDAL = _mapper.Map<User>(user);
            userDAL.AccountCredentialsId = accountCredential.AccountCredentialsId;
            // Setup the AddUser method to return (true, "User Added Successfully")
            _mockUserRepository.Setup(x => x.AddUser(It.Is<User>(x=>x.UserId == user.UserId && x.AccountCredentialsId == userDAL.AccountCredentialsId))).ReturnsAsync((true, "User Added Successfully"));

            // Act
            var result = await _userService.AddUser(user);
            user.AccountCredentialsId = accountCredential.AccountCredentialsId;

            // Assert
            Assert.True(result.IsUserAdded);
            Assert.Equal("User Added Successfully", result.Message);
        }


        [Fact]
        public async Task AddUser_FailedAddingTest()
        {
            //Arrange
            var user = UsersMockData.CreateUser();
            user.Password = "Test@123";
            var accountCredential = new AccountCredential
            {
                AccountCredentialsId = Guid.NewGuid(),
                Password = user.Password
            };

            _mockAccountCredentialsRepository.Setup(x => x.AddCredential(It.Is<AccountCredential>(cred => cred.Password == accountCredential.Password))).ReturnsAsync(accountCredential);
            var userDAL = _mapper.Map<User>(user);
            userDAL.AccountCredentialsId = accountCredential.AccountCredentialsId;
            _mockUserRepository.Setup(x => x.AddUser(It.Is<User>(x => x.UserId == user.UserId && x.AccountCredentialsId == userDAL.AccountCredentialsId))).ReturnsAsync((false, "oops something went wrong"));
            //Act
            var result = await _userService.AddUser(user);
            user.AccountCredentialsId = accountCredential.AccountCredentialsId;

            //Assert
            Assert.False(result.IsUserAdded);
            Assert.Equal("oops something went wrong", result.Message);
        }

        [Fact]
        public async Task BlockUser_SuccessfullBlockingTest()
        {
            //Arrange
            var userId = Guid.NewGuid();
            _mockUserRepository.Setup(x => x.BlockUser(userId)).ReturnsAsync((userId, "User Blocked Successfully"));
            //Act
            var result = await _userService.BlockUser(userId);
            //Assert
            Assert.Equal(userId, result.UserId);
            Assert.Equal("User Blocked Successfully", result.Message);
        }

        [Fact]
        public async Task BlockUser_FailedBlockingTest()
        {
            //Arrange
            var userId = Guid.NewGuid();
            _mockUserRepository.Setup(x => x.BlockUser(userId)).ReturnsAsync((userId, "User Not Blocked"));
            //Act
            var result = await _userService.BlockUser(userId);
            //Assert
            Assert.Equal(userId, result.UserId);
            Assert.Equal("User Not Blocked", result.Message);
        }

        [Fact]
        public async Task DeleteUser_SuccessfullDeletingTest()
        {
            //Arrange
            var userId = Guid.NewGuid();
            _mockUserRepository.Setup(x => x.DeleteUser(userId)).ReturnsAsync((true, "User Deleted Successfully"));
            //Act
            var result = await _userService.DeleteUser(userId);
            //Assert
            Assert.True(result.IsDeleted);
            Assert.Equal("User Deleted Successfully", result.Message);
        }

        [Fact]
        public async Task DeleteUser_FailedDeletingTest()
        {
            //Arrange
            var userId = Guid.NewGuid();
            _mockUserRepository.Setup(x => x.DeleteUser(userId)).ReturnsAsync((false, "User Not Found"));
            //Act
            var result = await _userService.DeleteUser(userId);
            //Assert
            Assert.False(result.IsDeleted);
            Assert.Equal("User Not Found", result.Message);
        }

        [Fact]
        public async Task GetAllUsers_SuccessfullGettingTest()
        {
            //Arrange
            var users = UsersMockData.GetUsers();
            var usersDAL = _mapper.Map<List<User>>(users);
            _mockUserRepository.Setup(x => x.GetAllUsers()).ReturnsAsync(usersDAL);
            //Act
            var result = await _userService.GetUsers();
            //Assert
            Assert.Equal(users.Count, result.Count);
        }

        [Fact]
        public async Task ToggleIsActiveById_SuccessfullyToggledTest()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var deletedBy = Guid.NewGuid();
            _mockUserRepository.Setup(x => x.ToggleIsActiveById(userId, deletedBy)).ReturnsAsync((new User()));
            //Act
            var result = await _userService.ToggleIsActiveById(userId, deletedBy);
            //Assert
            Assert.True(result is not null);
        }

        [Fact]
        public async Task ToggleIsActiveById_FailedTogglingTest()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var deletedBy = Guid.NewGuid();
            _mockUserRepository.Setup(x => x.ToggleIsActiveById(userId, deletedBy)).ReturnsAsync((User)null);
            //Act
            var result = await _userService.ToggleIsActiveById(userId, deletedBy);
            //Assert
            Assert.True(result is null);
        }

        [Fact]
        public async Task GetUserById_SuccessfullGettingTest()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var user = UsersMockData.GetUser(userId);
            var userDAL = _mapper.Map<User>(user);
            _mockUserRepository.Setup(x => x.GetUserById(userId)).ReturnsAsync(userDAL);
            //Act
            var result = await _userService.GetUserById(userId);
            //Assert
            Assert.Equal(userId, result.UserId);
        }

        [Fact]
        public async Task GetUserById_FailedGettingTest()
        {
            //Arrange
            var userId = Guid.NewGuid();
            _mockUserRepository.Setup(x => x.GetUserById(userId)).ReturnsAsync((User)null);
            //Act
            var result = await _userService.GetUserById(userId);
            //Assert
            Assert.True(result is null);
        }

        [Fact]
        public async Task UpdateUser_SuccessfullUpdatingTest()
        {
            //Arrange
            var user = UsersMockData.GetUser(Guid.NewGuid());
            var userDAL = _mapper.Map<BLUser, User>(user);
            _mockUserRepository.Setup(x => x.UpdateUser(It.Is<User>(x => x.UserId == user.UserId))).ReturnsAsync((userDAL, "User Updated Successfully"));
            //Act
            var result = await _userService.UpdateUser(user);
            //Assert
            Assert.Equal(user.UserId, result.Item1.UserId);
            Assert.Equal("User Updated Successfully", result.Message);
        }

        [Fact]
        public async Task UpdateUser_FailedUpdatingTest()
        {
            //Arrange
            var user = UsersMockData.GetUser(Guid.NewGuid());
            var userDAL = _mapper.Map<User>(user);
            _mockUserRepository.Setup(x => x.UpdateUser(It.Is<User>(x => x.UserId == user.UserId))).ReturnsAsync((null, "User Not Found"));
            //Act
            var result = await _userService.UpdateUser(user);
            //Assert
            //Assert.False(result.IsUserUpdated);
            Assert.Equal("User Not Found", result.Message);
        }

        [Fact]
        public async Task UpdateUserPassword_SuccessfullUpdatingTest()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var password = "test@123";

            _mockUserRepository.Setup(x => x.ChangePassword(userId, password)).ReturnsAsync(true);
            //Act
            var result = await _userService.ChangePassword(userId, password);
            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task UpdateUserPassword_FailedUpdatingTest()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var password = "test@123";
            _mockUserRepository.Setup(x => x.ChangePassword(userId, password)).ReturnsAsync(false);
            //Act
            var result = await _userService.ChangePassword(userId, password);
            //Assert
            Assert.False(result);
        }

        [Fact]
        public async Task GetUserByEmail_SuccessfullTest()
        {
            //Arrange
            var user = UsersMockData.GetUser(Guid.NewGuid());
            var userDAL = _mapper.Map<User>(user);
            _mockUserRepository.Setup(x => x.GetUserByEmail(user.Email)).ReturnsAsync(userDAL);
            //Act
            var result = await _userService.GetUserByEmail(user.Email);
            //Assert
            Assert.Equal(user.UserId, result.UserId);
        }

        [Fact]
        public async Task GetUserByEmail_FailedTest()
        {
            //Arrange
            var email = "tester@abc.com";
            _mockUserRepository.Setup(x => x.GetUserByEmail(email)).ReturnsAsync((User)null);
            //Act
            var result = await _userService.GetUserByEmail(email);
            //Assert
            Assert.True(result is null);
        }

        [Fact]
        public async Task IsUserAvailableWithEmail_SuccessfullTest()
        {
            //Arrange
            var email = "tester@abc.com";
            _mockUserRepository.Setup(x => x.IsEmailExists(email)).ReturnsAsync(true);
            //Act
            var result = await _userService.IsUserAvailableWithEmail(email);
            //Assert
            Assert.True(result.IsUserEmailExists);
            Assert.Equal("Email already exists", result.Message);
        }

        [Fact]
        public async Task IsUserAvailableWithEmail_FailedTest()
        {
            //Arrange
            var email = "tester@abc.com";
            _mockUserRepository.Setup(x => x.IsEmailExists(email)).ReturnsAsync(false);
            //Act
            var result = await _userService.IsUserAvailableWithEmail(email);
            //Assert
            Assert.False(result.IsUserEmailExists);
            Assert.Equal("Email doesn't exists", result.Message);
        }

        [Fact]
        public async Task GetFilteredUsers_SuccessfullTest()
        {
            //Arrange
            var users = UsersMockData.GetUsers();
            var usersDAL = _mapper.Map<List<User>>(users);
            _mockUserRepository.Setup(x => x.GetFilteredUsers("abc", "tester@abc.com", "9999999999", true)).ReturnsAsync(usersDAL);
            //Act
            var result = await _userService.GetFilteredUsers("abc", "tester@abc.com", "9999999999", true);
            //Assert
            Assert.Equal(users.Count, result.Count);
        }

        [Fact]
        public async Task GetFilteredUsers_FailedTest()
        {
            //Arrange
            var users = UsersMockData.GetUsers();
            var usersDAL = _mapper.Map<List<User>>(users);
            _mockUserRepository.Setup(x => x.GetFilteredUsers("abc", "tester@abc.com", "9999999999", true)).ReturnsAsync((List<User>)null);
            //Act
            var result = await _userService.GetFilteredUsers("abc", "tester@abc.com", "9999999999", true);
            //Assert
            Assert.True(result.IsNullOrEmpty());
        }
    }
}

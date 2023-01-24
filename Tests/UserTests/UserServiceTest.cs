using Domain.Models;
using Domain.Repositories;
using Domain.UseCases;
using Moq;
using Xunit;

namespace Tests.UserTests
{
    public class UserServiceTest
    {
        private UserService userService;
        private Mock<IUserRepository> repositoryMock;

        public UserServiceTest()
        {
            repositoryMock = new Mock<IUserRepository>();
            userService = new UserService(repositoryMock.Object);
        }

        [Fact]
        public void EmptyLogin()
        {
            var res = userService.GetUserByLogin(string.Empty);

            Assert.True(res.IsFailure);
            Assert.Equal("Login is empty", res.Message);
        }

        [Fact]
        public void NotFound()
        {
            repositoryMock.Setup(repository => repository.GetUserByLogin(It.IsAny<string>()))
                .Returns(() => null);

            var res = userService.GetUserByLogin("n4gib4t0r");

            Assert.True(res.IsFailure);
            Assert.Equal("User not found", res.Message);
        }

        [Fact]
        public void UserDoesNotExist()
        {
            var res = userService.UserExists(string.Empty);

            Assert.True(res.IsFailure);
            Assert.Equal("Login is empty", res.Message);
        }

        [Fact]
        public void UserDoesNotExist2()
        {
            repositoryMock.Setup(repository => repository.UserExists(It.IsAny<string>()))
                .Returns(() => false);

            var res = userService.UserExists("n4gib4t0r");

            Assert.True(res.Res);
            Assert.False(res.Value);
        }

        [Fact]
        public void RegistrationEmptyUser()
        {
            var res = userService.Register(new User());

            Assert.True(res.IsFailure);
            Assert.Equal("Username is empty", res.Message);
        }

        [Fact]
        public void RegistrationUserAlreadyExists()
        {
            repositoryMock.Setup(repository => repository.UserExists(It.IsAny<string>()))
                .Returns(() => true);

            var res = userService.Register(new User(1, "a", "a", Role.User, "a", "a"));

            Assert.True(res.IsFailure);
            Assert.Equal("Username already exists", res.Message);
        }

        [Fact]
        public void RegistrationError()
        {
            repositoryMock.Setup(repository => repository.UserExists(It.IsAny<string>()))
                .Returns(() => false);

            repositoryMock.Setup(repository => repository.Create(It.IsAny<User>()))
                .Returns(() => false);

            var res = userService.Register(new User(1, "a", "a", Role.User, "a", "a"));

            Assert.True(res.IsFailure);
            Assert.Equal("User creating failure", res.Message);
        }
        [Fact]
        public void RegisterSuccess()
        {
            repositoryMock.Setup(repository => repository.UserExists(It.IsAny<string>()))
                .Returns(() => false);

            repositoryMock.Setup(repository => repository.Create(It.IsAny<User>()))
                .Returns(() => true);

            var res = userService.Register(new User(1, "a", "a", Role.User, "a", "a"));
            Assert.True(res.Res);
        }
    }
}

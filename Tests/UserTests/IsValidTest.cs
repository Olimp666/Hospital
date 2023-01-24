using Domain.Models;
using Xunit;

namespace Tests.UserTests
{
    public class IsValidTest
    {
        [Fact]
        public void EmptyUser()
        {
            var user = new User();
            var check = user.IsValid();

            Assert.True(check.IsFailure);
            Assert.Equal("Username is empty", check.Message);
        }

        [Fact]
        public void PhoneError()
        {
            var user = new User(2, "", "a", Role.Administrator, "a", "a");
            var check = user.IsValid();

            Assert.True(check.IsFailure);
            Assert.Equal("Phone number is empty", check.Message);
        }

        [Fact]
        public void FullNameError()
        {
            var user = new User(3, "a", "", Role.Administrator, "a", "a");
            var check = user.IsValid();

            Assert.True(check.IsFailure);
            Assert.Equal("Name is empty", check.Message);
        }

        [Fact]
        public void PassError()
        {
            var user = new User(1, "a", "a", Role.Administrator, "a", "");
            var check = user.IsValid();

            Assert.True(check.IsFailure);
            Assert.Equal("Password is empty", check.Message);
        }
    }
}
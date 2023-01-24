using Domain.Models;
using Xunit;

namespace Tests.SessionTests
{
    public class IsValidTest
    {
        [Fact]
        public void EmptySession()
        {
            var app = new Session();
            var check = app.IsValid();

            Assert.True(check.Res);
        }

        [Fact]
        public void IvalidTime_F()
        {
            var app = new Session(0, DateTime.MaxValue, DateTime.MinValue, 0, 0);
            var check = app.IsValid();

            Assert.True(check.IsFailure);
            Assert.Equal("Invalid time", check.Message);
        }

        [Fact]
        public void ValidAppintment_P()
        {
            var app = new Session(0, DateTime.Now, DateTime.Now, 0, 0);
            var check = app.IsValid();

            Assert.True(check.Res);
        }
    }
}
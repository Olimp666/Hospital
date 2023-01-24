using Domain.Models;
using Xunit;

namespace Tests.DoctorTests
{
    public class IsValidTest
    {

        [Fact]
        public void EmptyDoctor_F()
        {
            var doctor = new Doctor();
            var check = doctor.IsValid();

            Assert.True(check.IsFailure);
            Assert.Equal("Name is empty", check.Message);
        }

        [Fact]
        public void EmptyName()
        {
            var doctor = new Doctor(1, "", 1);
            var check = doctor.IsValid();

            Assert.True(check.IsFailure);
            Assert.Equal("Name is empty", check.Message);
        }

        [Fact]
        public void ValidDoctor()
        {
            var doctor = new Doctor(0, "a", 1);
            var check = doctor.IsValid();

            Assert.True(check.Res);
        }
    }
}
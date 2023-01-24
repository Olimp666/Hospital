using Domain.Models;
using Xunit;

namespace Tests.ScheduleTests
{
    public class IsValidTest
    {
        [Fact]
        public void EmptySchedule()
        {
            var schedule = new Schedule();
            var check = schedule.IsValid();

            Assert.True(check.Res);
        }

        [Fact]
        public void InvalidTime()
        {
            var schedule = new Schedule(0, 0, DateTime.MaxValue, DateTime.MinValue);
            var check = schedule.IsValid();

            Assert.True(check.IsFailure);
            Assert.Equal("Invalid time", check.Message);
        }

        [Fact]
        public void ValidID()
        {
            var schedule = new Schedule(0, 0, DateTime.Now, DateTime.Now);
            var check = schedule.IsValid();

            Assert.True(check.Res);
        }
    }
}
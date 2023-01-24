using Domain.Models;
using Domain.Repositories;
using Domain.UseCases;
using Moq;
using Xunit;

namespace Tests.ScheduleTests
{
    public class ScheduleServiceTests
    {
        private ScheduleService scheduleService;
        private Mock<IScheduleRepository> repositoryMock;

        public ScheduleServiceTests()
        {
            repositoryMock = new Mock<IScheduleRepository>();
            scheduleService = new ScheduleService(repositoryMock.Object);
        }

        [Fact]
        public void GetIvalid()
        {
            var doctor = new Doctor();
            var result = scheduleService.GetSchedule(doctor);

            Assert.True(result.IsFailure);
            Assert.Equal("Name is empty", result.Message);
        }

        [Fact]
        public void GetValid()
        {
            List<Schedule> scheds = new List<Schedule>();
            scheds.Add(new Schedule());
            scheds.Add(new Schedule());

            IEnumerable<Schedule> s = scheds;
            repositoryMock.Setup(rep => rep.GetSchedule(It.IsAny<Doctor>())).Returns(() => s);

            var doctor = new Doctor(0, "a", 1);
            var result = scheduleService.GetSchedule(doctor);

            Assert.True(result.Res);
        }


        [Fact]
        public void AddError()
        {
            repositoryMock.Setup(rep => rep.Create(It.IsAny<Schedule>())).Returns(() => false);

            var schedule = new Schedule();
            var result = scheduleService.AddSchedule(schedule);

            Assert.True(result.IsFailure);
            Assert.Equal("Unable to add schedule", result.Message);
        }

        [Fact]
        public void AddValid()
        {
            repositoryMock.Setup(rep => rep.Create(It.IsAny<Schedule>())).Returns(() => true);

            var schedule = new Schedule();
            var result = scheduleService.AddSchedule(schedule);

            Assert.True(result.Res);
        }

        [Fact]
        public void UpdateError()
        {
            repositoryMock.Setup(rep => rep.Update(It.IsAny<Schedule>())).Returns(() => false);

            var schedule = new Schedule();
            var result = scheduleService.UpdateSchedule(schedule);

            Assert.True(result.IsFailure);
            Assert.Equal("Unable to update schedule", result.Message);
        }

        [Fact]
        public void UpdateValid()
        {
            repositoryMock.Setup(rep => rep.Update(It.IsAny<Schedule>())).Returns(() => true);

            var schedule = new Schedule();
            var result = scheduleService.UpdateSchedule(schedule);

            Assert.True(result.Res);
        }
    }
}

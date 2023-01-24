using Domain.Models;
using Domain.Repositories;
using Domain.UseCases;
using Moq;
using Xunit;

namespace Tests.SessionTests
{
    public class SessionServiceTest
    {
        private SessionService sessionService;
        private Mock<ISessionRepository> repositoryMock;

        public SessionServiceTest()
        {
            repositoryMock = new Mock<ISessionRepository>();
            sessionService = new SessionService(repositoryMock.Object);
        }

        [Fact]
        public void SaveInvalidTime()
        {
            var session = new Session(0, DateTime.MaxValue, DateTime.MaxValue, 0, 0);
            var sched = new Schedule(0, 0, DateTime.MinValue, DateTime.MinValue);
            var res = sessionService.SaveSession(session, sched);

            Assert.True(res.IsFailure);
            Assert.Equal("Session out of schedule", res.Message);
        }

        [Fact]
        public void SaveTakenTimeBetween()
        {
            List<Session> sessions = new()
            {
                new Session(0, DateTime.MinValue, DateTime.Parse("1000-01-10"), 0, 0),
                new Session(0, DateTime.Parse("1000-01-10"), DateTime.MaxValue, 0, 0)
            };
            repositoryMock.Setup(x => x.GetSessions(It.IsAny<ulong>())).Returns(() => sessions);

            var session = new Session(0, DateTime.Parse("1000-01-01"), DateTime.Parse("1000-01-20"), 0, 0);
            var sched = new Schedule(0, 0, DateTime.MinValue, DateTime.MaxValue);
            var res = sessionService.SaveSession(session, sched);

            Assert.True(res.IsFailure);
            Assert.Equal("Session time already taken", res.Message);
        }

        [Fact]
        public void SaveTakenTimeInner()
        {
            List<Session> sessions = new List<Session>();
            sessions.Add(new Session(0, DateTime.MinValue, DateTime.Parse("1000-01-01"), 0, 0));
            sessions.Add(new Session(0, DateTime.Parse("1000-01-01"), DateTime.Parse("1000-01-20"), 0, 0));
            sessions.Add(new Session(0, DateTime.Parse("1000-01-20"), DateTime.MaxValue, 0, 0));

            repositoryMock.Setup(x => x.GetSessions(It.IsAny<ulong>())).Returns(() => sessions);

            var session = new Session(0, DateTime.Parse("1000-01-05"), DateTime.Parse("1000-01-15"), 0, 0);
            var sched = new Schedule(0, 0, DateTime.MinValue, DateTime.MaxValue);
            var res = sessionService.SaveSession(session, sched);

            Assert.True(res.IsFailure);
            Assert.Equal("Session time already taken", res.Message);
        }

        [Fact]
        public void SaveTakenTimeEqualBoundaries()
        {
            List<Session> sessions = new List<Session>();
            sessions.Add(new Session(0, DateTime.MinValue, DateTime.Parse("1000-01-01"), 0, 0));
            sessions.Add(new Session(0, DateTime.Parse("1000-01-20"), DateTime.MaxValue, 0, 0));

            repositoryMock.Setup(x => x.GetSessions(It.IsAny<ulong>())).Returns(() => sessions);
            repositoryMock.Setup(x => x.Create(It.IsAny<Session>())).Returns(() => true);

            var session = new Session(0, DateTime.Parse("1000-01-01"), DateTime.Parse("1000-01-20"), 0, 0);
            var sched = new Schedule(0, 0, DateTime.MinValue, DateTime.MaxValue);
            var res = sessionService.SaveSession(session, sched);

            Assert.True(res.Res);
        }

        [Fact]
        public void SaveSaveError()
        {
            List<Session> sessions = new();
            repositoryMock.Setup(x => x.GetSessions(It.IsAny<ulong>())).Returns(() => sessions);
            repositoryMock.Setup(x => x.Create(It.IsAny<Session>())).Returns(() => false);

            var session = new Session();
            var sched = new Schedule(0, 0, DateTime.MinValue, DateTime.MaxValue);
            var res = sessionService.SaveSession(session, sched);

            Assert.True(res.IsFailure);
            Assert.Equal("Unable to save session", res.Message);
        }

        [Fact]
        public void SaveValid()
        {
            List<Session> sessions = new();
            repositoryMock.Setup(x => x.GetSessions(It.IsAny<ulong>())).Returns(() => sessions);
            repositoryMock.Setup(x => x.Create(It.IsAny<Session>())).Returns(() => true);

            var session = new Session();
            var sched = new Schedule(0, 0, DateTime.MinValue, DateTime.MaxValue);
            var res = sessionService.SaveSession(session, sched);

            Assert.True(res.Res);
        }

        [Fact]
        public void GetInvalidSpec()
        {
            var spec = new Specialization();
            var sched = new Schedule();

            var res = sessionService.GetFreeSessions(spec, sched);

            Assert.True(res.IsFailure);
            Assert.Equal("Name is empty", res.Message);

            var res2 = sessionService.GetExistingSessions(spec);

            Assert.True(res2.IsFailure);
            Assert.Equal("Name is empty", res2.Message);
        }

        [Fact]
        public void GetExistingValid()
        {
            List<Session> sessions = new()
            {
                new Session(),
                new Session()
            };

            repositoryMock.Setup(repository => repository.GetExistingSessions(It.IsAny<Specialization>())).Returns(() => sessions);

            var spec = new Specialization(0, "a");
            var res = sessionService.GetExistingSessions(spec);

            Assert.True(res.Res);
        }

        [Fact]
        public void GetFreeValid()
        {
            List<DateTime> dates = new List<DateTime>();
            dates.Add(new DateTime());
            dates.Add(new DateTime());


            repositoryMock.Setup(repository => repository.GetFreeSessions(It.IsAny<Specialization>(), It.IsAny<Schedule>())).Returns(() => dates);

            var spec = new Specialization(0, "a");
            var sched = new Schedule(0, 0, DateTime.MinValue, DateTime.MinValue);
            var res = sessionService.GetFreeSessions(spec, sched);

            Assert.True(res.Res);
        }
    }
}

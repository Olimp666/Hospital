using Domain.Models;
using Domain.Repositories;
using Domain.UseCases;
using Moq;
using Xunit;

namespace Tests.DoctorTests
{
    public class DoctorServiceTests
    {
        private DoctorService doctorService;
        private Mock<IDoctorRepository> doctorRepositoryMock;
        private Mock<ISessionRepository> appRepositoryMock;

        public DoctorServiceTests()
        {
            doctorRepositoryMock = new Mock<IDoctorRepository>();
            appRepositoryMock = new Mock<ISessionRepository>();
            doctorService = new DoctorService(doctorRepositoryMock.Object, appRepositoryMock.Object);
        }

        [Fact]
        public void CreateInvalid()
        {
            var doctor = new Doctor();
            var result = doctorService.CreateDoctor(doctor);

            Assert.True(result.IsFailure);
            Assert.Equal("Name is empty", result.Message);
        }

        [Fact]
        public void CreateIdError()
        {
            doctorRepositoryMock.Setup(r => r.GetItem(It.IsAny<ulong>())).Returns(() => new Doctor(0, "a", 1));
            var doctor = new Doctor(0, "a", 1);
            var result = doctorService.CreateDoctor(doctor);

            Assert.True(result.IsFailure);
            Assert.Equal("Doctor alredy exists", result.Message);
        }

        [Fact]
        public void CreateCreateError()
        {
            doctorRepositoryMock.Setup(repository => repository.Create(It.IsAny<Doctor>())).Returns(() => false);
            var doctor = new Doctor(0, "a", 1);
            var result = doctorService.CreateDoctor(doctor);

            Assert.True(result.IsFailure);
            Assert.Equal("Unable to create doctor", result.Message);
        }

        [Fact]
        public void CreateValid()
        {
            doctorRepositoryMock.Setup(repository => repository.Create(It.IsAny<Doctor>())).Returns(() => true);
            var doctor = new Doctor(0, "a", 1);
            var result = doctorService.CreateDoctor(doctor);

            Assert.True(result.Res);
        }

        [Fact]
        public void DeleteIdNotFound()
        {
            List<Session> apps = new();

            var result = doctorService.DeleteDoctor(0);

            Assert.True(result.IsFailure);
            Assert.Equal("Doctor not found", result.Message);
        }

        [Fact]
        public void DeleteAppsNotEmpty()
        {
            List<Session> apps = new List<Session>();
            apps.Add(new Session());
            appRepositoryMock.Setup(r => r.GetSessions(It.IsAny<ulong>())).Returns(() => apps);

            var result = doctorService.DeleteDoctor(0);

            Assert.True(result.IsFailure);
            Assert.Equal("Unable to delete doctor: Doctor has sessions", result.Message);
        }

        [Fact]
        public void DeleteDoctorNotFound()
        {
            List<Session> apps = new()
            {
                new Session()
            };
            appRepositoryMock.Setup(r => r.GetSessions(It.IsAny<ulong>())).Returns(() => apps);
            doctorRepositoryMock.Setup(repository => repository.GetItem(It.IsAny<ulong>())).Returns(() => null);

            var result = doctorService.DeleteDoctor(0);

            Assert.True(result.IsFailure);
            Assert.Equal("Unable to delete doctor: Doctor has sessions", result.Message);
        }

        [Fact]
        public void DeleteDeleteError()
        {
            List<Session> apps = new();
            doctorRepositoryMock.Setup(repository => repository.GetItem(It.IsAny<ulong>())).Returns(() => new Doctor(0, "a", 1));
            doctorRepositoryMock.Setup(repository => repository.Delete(It.IsAny<ulong>())).Returns(() => false);

            var result = doctorService.DeleteDoctor(0);

            Assert.True(result.IsFailure);
            Assert.Equal("Unable to delete doctor", result.Message);
        }

        [Fact]
        public void DeleteValid()
        {
            List<Session> apps = new();
            doctorRepositoryMock.Setup(repository => repository.GetItem(It.IsAny<ulong>())).Returns(() => new Doctor(0, "a", 1));
            doctorRepositoryMock.Setup(repository => repository.Delete(It.IsAny<ulong>())).Returns(() => true);

            var result = doctorService.DeleteDoctor(0);

            Assert.True(result.Res);
        }

        [Fact]
        public void GetAll()
        {
            List<Doctor> doctors = new()
            {
                new Doctor(0, "a", 1),
                new Doctor(1, "as", 1)
            };
            IEnumerable<Doctor> d = doctors;
            doctorRepositoryMock.Setup(repository => repository.GetAll()).Returns(() => d);

            var result = doctorService.GetAllDoctors();

            Assert.True(result.Res);
        }

        [Fact]
        public void FindIDNotFound()
        {
            doctorRepositoryMock.Setup(repository => repository.GetItem(It.IsAny<ulong>())).Returns(() => null);

            var result = doctorService.FindDoctor(0);

            Assert.True(result.IsFailure);
            Assert.Equal("Doctor not found", result.Message);
        }

        [Fact]
        public void FindIDValid()
        {
            doctorRepositoryMock.Setup(repository => repository.GetItem(It.IsAny<ulong>())).Returns(() => new Doctor(0, "a", 1));

            var result = doctorService.FindDoctor(0);

            Assert.True(result.Res);
        }

        [Fact]
        public void FindSpecInvalid()
        {
            var result = doctorService.FindDoctors(new Specialization());

            Assert.True(result.IsFailure);
            Assert.Equal("Name is empty", result.Message);
        }

        [Fact]
        public void FindSpecNotFound()
        {
            doctorRepositoryMock.Setup(repository => repository.FindDoctors(It.IsAny<Specialization>())).Returns(() => new List<Doctor>());

            var result = doctorService.FindDoctors(new Specialization(0, "a"));

            Assert.True(result.IsFailure);
            Assert.Equal("Doctors not found", result.Message);
        }

        [Fact]
        public void FindSpecValid()
        {
            List<Doctor> list = new()
            {
                new Doctor(0, "a", 1)
            };
            doctorRepositoryMock.Setup(repository => repository.FindDoctors(It.IsAny<Specialization>())).Returns(() => list);

            var result = doctorService.FindDoctors(new Specialization(0, "a"));

            Assert.True(result.Res);
        }

    }
}

using Database.Repository;
using Domain.Models;
using Domain.UseCases;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Tests.DBTests
{
    public class DBTests
    {
        private readonly DbContextOptionsBuilder<Database.AppContext> _optionsBuilder;

        public DBTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<Database.AppContext>();
            optionsBuilder.UseNpgsql(
                $"Host=localhost;Port=5432;Database=backend_db;Username=postgres;Password=niggers");
            _optionsBuilder = optionsBuilder;
        }

        [Fact]
        public void Test1()
        {
            var context = new Database.AppContext(_optionsBuilder.Options);

            var userRep = new UserRepository(context);

            var serv = new UserService(userRep);

            var user = serv.Register(new Domain.Models.User(1, "88005553535", "Nate Higgers", Role.User, "Name", "Pass"));

            context.SaveChanges();

            Assert.True(context.Users.Any(u => u.UserName == "Name"));
        }

        [Fact]
        public void Test3()
        {
            using var context = new Database.AppContext(_optionsBuilder.Options);
            var u = context.Users.FirstOrDefault(u => u.UserName == "Name");
            context.Users.Remove(u);
            context.SaveChanges();

            Assert.True(!context.Users.Any(u => u.UserName == "Name"));
        }

        [Fact]
        public void Test4()
        {

            using var context = new Database.AppContext(_optionsBuilder.Options);
            var userRepository = new UserRepository(context);
            var userService = new UserService(userRepository);

            var create = userService.Register(new Domain.Models.User(1, "88005553535", "Nate Higgers", Role.User, "Name", "Pass"));

            context.SaveChanges();

            var res = userService.GetUserByLogin("Name");

            Assert.NotNull(res.Value);
        }
    }
}

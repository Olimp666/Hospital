using Domain.Models;

namespace Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        bool UserExists(string login);
        User? GetUserByLogin(string login);
    }
}
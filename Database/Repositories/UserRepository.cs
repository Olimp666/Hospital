using Database.Converters;
using Domain.Models;
using Domain.Repositories;

namespace Database.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppContext _context;

        public UserRepository(AppContext context)
        {
            _context = context;
        }

        public bool Create(User item)
        {
            _context.Users.Add(item.ToModel());
            return true;
        }

        public bool Delete(ulong? id)
        {
            var user = _context.Users.FirstOrDefault(u => u.ID == id);
            if (user == default)
                return false;

            _context.Users.Remove(user);
            return true;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.Select(u => u.ToDomain());
        }

        public User? GetItem(ulong? id)
        {
            var user = _context.Users.FirstOrDefault(u => u.ID == id);
            return user?.ToDomain();
        }

        public User? GetUserByLogin(string login)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == login);
            return user?.ToDomain();
        }

        public bool UserExists(string login)
        {
            return _context.Users.Any(u => u.UserName == login);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public bool Update(User item)
        {
            _context.Users.Update(item.ToModel());
            return true;
        }
    }
}

using Domain.Models;
using Domain.Repositories;

namespace Domain.UseCases
{
    public class UserService
    {
        private IUserRepository _db;

        public UserService(IUserRepository db)
        {
            _db = db;
        }

        public Result<User> Register(User user)
        {
            var check = user.IsValid();
            if (check.IsFailure)
                return Result.Fail<User>(check.Message);

            if (_db.UserExists(user.UserName))
                return Result.Fail<User>("Username already exists");

            if (_db.Create(user))
            {
                _db.Save();
                return Result.Success(user);
            }
            return Result.Fail<User>("User creating failure");
        }

        public Result<User> GetUserByLogin(string login)
        {
            if (string.IsNullOrEmpty(login))
                return Result.Fail<User>("Login is empty");

            var user = _db.GetUserByLogin(login);

            return user != null ? Result.Success(user) : Result.Fail<User>("User not found");
        }

        public Result<bool> UserExists(string login)
        {
            if (string.IsNullOrEmpty(login))
                return Result.Fail<bool>("Login is empty");

            return Result.Success(_db.UserExists(login));
        }
        public Result<IEnumerable<User>> GetAll()
        {
            return Result.Success(_db.GetAll());
        }
    }
}
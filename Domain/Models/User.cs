namespace Domain.Models
{
    public class User
    {
        public ulong? ID { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public Role Role { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public User()
        {
            ID = 0;
            PhoneNumber = string.Empty;
            FullName = string.Empty;
            Role = Role.User;
            UserName = string.Empty;
            Password = string.Empty;
        }

        public User(ulong? id, string phoneNumber, string fullName, Role role, string userName, string passWord)
        {
            ID = id;
            PhoneNumber = phoneNumber;
            FullName = fullName;
            Role = role;
            UserName = userName;
            Password = passWord;
        }

        public Result IsValid()
        {
            if (string.IsNullOrEmpty(UserName))
                return Result.Fail("Username is empty");

            if (string.IsNullOrEmpty(Password))
                return Result.Fail("Password is empty");

            if (string.IsNullOrEmpty(PhoneNumber))
                return Result.Fail("Phone number is empty");

            if (string.IsNullOrEmpty(FullName))
                return Result.Fail("Name is empty");

            return Result.Success();
        }
    }
}

using Domain.Models;
namespace Database.Models
{
    public class User
    {
        public ulong? ID { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public Role Role { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }

    }
}

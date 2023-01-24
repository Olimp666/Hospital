namespace Database.Converters
{
    public static class UserConverter
    {
        public static Database.Models.User ToModel(this Domain.Models.User model)
        {
            return new Database.Models.User
            {
                ID = model.ID,
                PhoneNumber = model.PhoneNumber,
                FullName = model.FullName,
                Role = model.Role,
                UserName = model.UserName,
                Password = model.Password,
            };
        }
        public static Domain.Models.User ToDomain(this Database.Models.User model)
        {
            return new Domain.Models.User
            {
                ID = model.ID,
                PhoneNumber = model.PhoneNumber,
                FullName = model.FullName,
                Role = model.Role,
                UserName = model.UserName,
                Password = model.Password,
            };
        }
    }
}

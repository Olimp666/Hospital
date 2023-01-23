namespace Database.Converters
{
    public static class DoctorConverter
    {
        public static Database.Models.Doctor ToModel(this Domain.Models.Doctor model)
        {
            return new Database.Models.Doctor
            {
                ID = model.ID,
                FullName = model.FullName,
                Specialization = model.Specialization.ToModel()
            };
        }

        public static Domain.Models.Doctor ToDomain(this Database.Models.Doctor model)
        {
            return new Domain.Models.Doctor
            {
                ID = model.ID,
                FullName = model.FullName,
                Specialization = model.Specialization.ToDomain()
            };
        }
    }
}

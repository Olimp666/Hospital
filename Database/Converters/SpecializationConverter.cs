namespace Database.Converters
{
    public static class DomainModelSpecializationConverter
    {
        public static Database.Models.Specialization ToModel(this Domain.Models.Specialization model)
        {
            return new Database.Models.Specialization
            {
                ID = model.ID,
                Name = model.Name
            };
        }

        public static Domain.Models.Specialization ToDomain(this Database.Models.Specialization model)
        {
            return new Domain.Models.Specialization
            {
                ID = model.ID,
                Name = model.Name
            };
        }
    }
}

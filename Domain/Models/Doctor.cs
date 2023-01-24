namespace Domain.Models
{
    public class Doctor
    {
        public ulong? ID { get; set; }
        public string FullName { get; set; }
        public ulong? SpecializationID { get; set; }

        public Doctor()
        {
            ID = 0;
            FullName = string.Empty;
            SpecializationID = 0;
        }
        public Doctor(ulong? id, string fullName, ulong? specializationID)
        {
            ID = id;
            FullName = fullName;
            SpecializationID = specializationID;
        }
        public Result IsValid()
        {
            if (string.IsNullOrEmpty(FullName))
                return Result.Fail("Name is empty");
            return Result.Success();
        }
    }
}

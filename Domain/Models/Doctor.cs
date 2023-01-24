namespace Domain.Models
{
    public class Doctor
    {
        public ulong ID { get; set; }
        public string FullName { get; set; }
        public Specialization Specialization { get; set; }

        public Doctor()
        {
            ID = 0;
            FullName = string.Empty;
            Specialization = new Specialization();
        }
        public Doctor(ulong id, string fullName, Specialization specialization)
        {
            ID = id;
            FullName = fullName;
            Specialization = specialization;
        }
        public Result IsValid()
        {
            if (string.IsNullOrEmpty(FullName))
                return Result.Fail("Name is empty");

            var result = Specialization.IsValid();
            if (result.IsFailure)
                return Result.Fail(result.Message);

            return Result.Success();
        }
    }
}

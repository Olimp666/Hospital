namespace Domain.Models
{
    public class Specialization
    {
        public ulong? ID { get; set; }
        public string Name { get; set; }

        public Specialization()
        {
            ID = 0;
            Name = string.Empty;
        }

        public Specialization(ulong? id, string name)
        {
            ID = id;
            Name = name;
        }

        public Result IsValid()
        {
            if (string.IsNullOrEmpty(Name))
                return Result.Fail("Name is empty");

            return Result.Success();
        }
    }


}

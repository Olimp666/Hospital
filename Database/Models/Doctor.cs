namespace Database.Models
{
    public class Doctor
    {
        public ulong? ID { get; set; }
        public string FullName { get; set; }
        public ulong? SpecializationID { get; set; }
    }
}

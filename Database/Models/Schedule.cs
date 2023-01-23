namespace Database.Models
{
    public class Schedule
    {
        public ulong ID { get; set; }
        public ulong DoctorID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}

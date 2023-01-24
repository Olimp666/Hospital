namespace Database.Models
{
    public class Session
    {
        public ulong? ID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public ulong? PatientID { get; set; }
        public ulong? DoctorID { get; set; }
    }
}

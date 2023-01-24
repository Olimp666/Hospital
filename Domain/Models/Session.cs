namespace Domain.Models
{
    public class Session
    {
        public ulong? ID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public ulong? PatientID { get; set; }
        public ulong? DoctorID { get; set; }

        public Session() { }
        public Session(ulong? id, DateTime startTime, DateTime endTime, ulong? patientId, ulong? doctorId)
        {
            ID = id;
            StartTime = startTime;
            EndTime = endTime;
            PatientID = patientId;
            DoctorID = doctorId;
        }

        public Result IsValid()
        {
            if (StartTime > EndTime)
                return Result.Fail("Invalid time");

            return Result.Success();
        }
    }
}

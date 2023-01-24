namespace Domain.Models
{
    public class Schedule
    {
        public ulong ID;
        public ulong DoctorID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public Schedule()
        {
            ID = 0;
            DoctorID = 0;
            StartTime = DateTime.MinValue;
            EndTime = DateTime.MinValue;
        }

        public Schedule(ulong id, ulong doctorId, DateTime startTime, DateTime endTime)
        {
            ID = id;
            DoctorID = doctorId;
            StartTime = startTime;
            EndTime = endTime;
        }

        public Result IsValid()
        {
            if (StartTime > EndTime)
                return Result.Fail("Invalid time");

            return Result.Success();
        }
    }
}

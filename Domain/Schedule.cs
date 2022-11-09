using System;

namespace Domain
{
    internal class Schedule
    {
        public ulong DoctorID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}

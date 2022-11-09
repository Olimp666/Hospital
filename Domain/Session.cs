using System;

namespace Domain
{
    internal class Session
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public ulong PatientID { get; set; }
        public ulong DoctorID { get; set; }
    }
}

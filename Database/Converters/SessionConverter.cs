namespace Database.Converters
{
    public static class SessionConverter
    {
        public static Database.Models.Session ToModel(this Domain.Models.Session model)
        {
            return new Database.Models.Session
            {
                ID = model.ID,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                PatientID = model.PatientID,
                DoctorID = model.DoctorID
            };
        }

        public static Domain.Models.Session ToDomain(this Database.Models.Session model)
        {
            return new Domain.Models.Session
            {
                ID = model.ID,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                PatientID = model.PatientID,
                DoctorID = model.DoctorID
            };
        }
    }
}

namespace Database.Converters
{
    public static class ScheduleConverter
    {
        public static Database.Models.Schedule ToModel(this Domain.Models.Schedule model)
        {
            return new Database.Models.Schedule
            {
                ID = model.ID,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                DoctorID = model.DoctorID
            };
        }

        public static Domain.Models.Schedule ToDomain(this Database.Models.Schedule model)
        {
            return new Domain.Models.Schedule
            {
                ID = model.ID,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                DoctorID = model.DoctorID
            };
        }
    }
}

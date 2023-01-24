using Domain.Models;
using Domain.Repositories;

namespace Domain.UseCases
{
    public class ScheduleService
    {
        private readonly IScheduleRepository _db;

        public ScheduleService(IScheduleRepository db)
        {
            _db = db;
        }

        public Result<IEnumerable<Schedule>> GetSchedule(Doctor doctor)
        {
            var result = doctor.IsValid();
            if (result.IsFailure)
                return Result.Fail<IEnumerable<Schedule>>(result.Message);

            return Result.Success(_db.GetSchedule(doctor));
        }
        public Result AddSchedule(Schedule schedule)
        {
            var result = schedule.IsValid();
            if (result.IsFailure)
                return Result.Fail(result.Message);

            return _db.Create(schedule) ? Result.Success() : Result.Fail<Schedule>("Unable to add schedule");
        }
        public Result UpdateSchedule(Schedule schedule)
        {
            var result = schedule.IsValid();
            if (result.IsFailure)
                return Result.Fail(result.Message);

            return _db.Update(schedule) ? Result.Success() : Result.Fail("Unable to update schedule");
        }
    }
}
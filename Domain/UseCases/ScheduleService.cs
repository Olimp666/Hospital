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
        public Result<Schedule> GetSchedule(ulong? doctorId)
        {
            var res = _db.GetItem(doctorId);
            if (res == default)
                return Result.Fail<Schedule>("Schedule not found");
            return Result.Success(res);
        }
        public Result AddSchedule(Schedule schedule)
        {
            var result = schedule.IsValid();
            if (result.IsFailure)
                return Result.Fail(result.Message);

            if (_db.Create(schedule))
            {
                _db.Save();
                return Result.Success();
            }
            return Result.Fail<Schedule>("Unable to add schedule");
        }
        public Result UpdateSchedule(Schedule schedule)
        {
            var result = schedule.IsValid();
            if (result.IsFailure)
                return Result.Fail(result.Message);

            if (_db.Update(schedule))
            {
                _db.Save();
                return Result.Success();
            }
            return Result.Fail("Unable to update schedule");
        }
    }
}
using Domain.Models;

namespace Domain.Repositories
{
    public interface IScheduleRepository : IRepository<Schedule>
    {
        IEnumerable<Schedule> GetSchedule(Doctor doctor);
    }
}
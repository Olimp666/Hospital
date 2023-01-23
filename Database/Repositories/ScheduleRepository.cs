using Database.Converters;
using Domain.Models;
using Domain.Repositories;

namespace Database.Repository
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly AppContext _context;

        public ScheduleRepository(AppContext context)
        {
            _context = context;
        }

        public bool Create(Schedule item)
        {
            _context.Schedules.Add(item.ToModel());
            return true;
        }

        public bool Delete(ulong id)
        {
            var sched = GetItem(id);
            if (sched == default)
                return false;

            _context.Schedules.Remove(sched.ToModel());
            return true;
        }

        public IEnumerable<Schedule> GetAll()
        {
            return _context.Schedules.Select(s => s.ToDomain());
        }

        public Schedule? GetItem(ulong id)
        {
            return _context.Schedules.FirstOrDefault(s => s.ID == id)?.ToDomain();
        }

        public IEnumerable<Schedule> GetSchedule(Doctor doctor)
        {
            return _context.Schedules.Where(s => s.DoctorID == doctor.ID).Select(s => s.ToDomain());
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public bool Update(Schedule item)
        {
            _context.Schedules.Update(item.ToModel());
            return true;
        }
    }
}

using Database.Converters;
using Domain.Models;
using Domain.Repositories;
namespace Database.Repository
{
    public class SessionsRepository : ISessionRepository
    {
        private readonly AppContext _context;

        public SessionsRepository(AppContext context)
        {
            _context = context;
        }

        public bool Create(Session item)
        {
            _context.Sessions.Add(item.ToModel());
            return true;
        }

        public bool Delete(ulong? id)
        {
            var session = _context.Sessions.FirstOrDefault(a => a.ID == id);
            if (session == default)
                return false;

            _context.Sessions.FirstOrDefault(a => a.ID == id);
            return true;
        }

        public IEnumerable<Session> GetAll()
        {
            return _context.Sessions.Select(a => a.ToDomain());
        }

        public IEnumerable<Session> GetSessions(ulong? doctorId)
        {
            return _context.Sessions.Where(a => a.DoctorID == doctorId).Select(a => a.ToDomain());
        }

        public IEnumerable<Session> GetExistingSessions(Specialization specialization)
        {
            var docs = _context.Doctors.Where(d => d.SpecializationID == specialization.ID);
            return _context.Sessions.Where(a => docs.Any(d => d.ID == a.DoctorID)).Select(a => a.ToDomain());
        }

        public IEnumerable<DateTime> GetFreeSessions(Specialization specialization, Schedule schedule)
        {
            var docs = _context.Doctors.Where(d => d.SpecializationID == specialization.ID && d.ID == schedule.DoctorID);
            var existing = _context.Sessions.Where(a => docs.Any(d => d.ID == a.DoctorID)).Select(a => a.StartTime);
            List<DateTime> free = new List<DateTime>();
            for (DateTime dt = schedule.StartTime; dt < schedule.EndTime; dt.AddMinutes(30))
            {
                if (existing.All(a => a != dt))
                    free.Append(dt);
            }
            return free;
        }

        public Session? GetItem(ulong? id)
        {
            return _context.Sessions.FirstOrDefault(a => a.ID == id)?.ToDomain();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public bool Update(Session item)
        {
            _context.Sessions.Update(item.ToModel());
            return true;
        }
    }
}

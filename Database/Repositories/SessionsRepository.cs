using Database.Converters;
using Domain.Models;
using Domain.Repositories;

namespace Database.Repository
{
    internal class SessionsRepository : ISessionRepository
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

        public bool Delete(ulong id)
        {
            var session = GetItem(id);
            if (session == default)
                return false;

            _context.Sessions.Remove(session.ToModel());
            return true;
        }

        public IEnumerable<Session> GetAll()
        {
            return _context.Sessions.Select(a => a.ToDomain());
        }

        public IEnumerable<Session> GetSessions(ulong doctorId)
        {
            return _context.Sessions.Where(a => a.DoctorID == doctorId).Select(a => a.ToDomain());
        }

        public IEnumerable<Session> GetExistingSessions(Specialization specialization)
        {
            var docs = _context.Doctors.Where(d => d.Specialization == specialization.ToModel());
            return _context.Sessions.Where(a => docs.Any(d => d.ID == a.DoctorID)).Select(a => a.ToDomain());
        }

        public IEnumerable<DateTime> GetFreeSessions(Specialization specialization)
        {
            var docs = _context.Doctors.Where(d => d.Specialization == specialization.ToModel());
            return _context.Sessions.Where(a => a.PatientID == 1 && docs.Any(d => d.ID == a.DoctorID)).Select(a => a.StartTime);
        }

        public Session? GetItem(ulong id)
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

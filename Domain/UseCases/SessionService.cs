using Domain.Models;
using Domain.Repositories;

namespace Domain.UseCases
{
    public class SessionService
    {
        private ISessionRepository _db;
        private Dictionary<ulong?, Mutex> _mutex = new Dictionary<ulong?, Mutex>();

        public SessionService(ISessionRepository db)
        {
            _db = db;
        }

        public Result<Session> SaveSession(Session session, Schedule schedule)
        {
            var result = session.IsValid();
            if (result.IsFailure)
                return Result.Fail<Session>(result.Message);

            var result1 = schedule.IsValid();
            if (result1.IsFailure)
                return Result.Fail<Session>(result1.Message);

            if (schedule.StartTime > session.StartTime || schedule.EndTime < session.EndTime)
                return Result.Fail<Session>("Session out of schedule");

            var sessions = _db.GetSessions(session.DoctorID);
            if (sessions.Any(a => session.StartTime < a.EndTime && a.StartTime < session.EndTime))
                return Result.Fail<Session>("Session time already taken");

            if (!_mutex.ContainsKey(session.DoctorID))
            {
                _mutex.Add(session.DoctorID, new Mutex());
            }
            _mutex.First(d => d.Key == session.DoctorID).Value.WaitOne();

            if (_db.Create(session))
            {
                _db.Save();
                _mutex.First(d => d.Key == session.DoctorID).Value.ReleaseMutex();
                return Result.Success(session);
            }

            _mutex.First(d => d.Key == session.DoctorID).Value.ReleaseMutex();
            return Result.Fail<Session>("Unable to save session");
        }

        public Result<IEnumerable<Session>> GetExistingSessions(Specialization specialization)
        {
            var result = specialization.IsValid();
            if (result.IsFailure)
                return Result.Fail<IEnumerable<Session>>(result.Message);

            return Result.Success(_db.GetExistingSessions(specialization));
        }

        public Result<IEnumerable<DateTime>> GetFreeSessions(Specialization specialization, Schedule schedule)
        {
            var result = specialization.IsValid();
            if (result.IsFailure)
                return Result.Fail<IEnumerable<DateTime>>(result.Message);
            result = schedule.IsValid();
            if (result.IsFailure)
                return Result.Fail<IEnumerable<DateTime>>(result.Message);

            return Result.Success(_db.GetFreeSessions(specialization, schedule));
        }
    }
}
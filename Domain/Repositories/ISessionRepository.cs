using Domain.Models;

namespace Domain.Repositories
{
    public interface ISessionRepository : IRepository<Session>
    {
        IEnumerable<Session> GetSessions(ulong doctorId);
        IEnumerable<Session> GetExistingSessions(Specialization specialization);
        IEnumerable<DateTime> GetFreeSessions(Specialization specialization);
    }
}

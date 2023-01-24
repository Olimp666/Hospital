using Domain.Models;

namespace Domain.Repositories
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        //Doctor? FindDoctor(Specialization specialization);
        IEnumerable<Doctor> FindDoctors(Specialization specialization);
    }
}

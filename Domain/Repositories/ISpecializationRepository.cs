using Domain.Models;

namespace Domain.Repositories
{
    public interface ISpecializationRepository : IRepository<Specialization>
    {
        public Specialization? GetByName(string name);
    }
}

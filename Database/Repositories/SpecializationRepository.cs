using Database.Converters;
using Domain.Models;
using Domain.Repositories;

namespace Database.Repository
{
    public class SpecializationRepository : IRepository<Specialization>
    {
        private readonly AppContext _context;

        public SpecializationRepository(AppContext context)
        {
            _context = context;
        }

        public bool Create(Specialization item)
        {
            _context.Specializations.Add(item.ToModel());
            return true;
        }

        public bool Delete(ulong id)
        {
            var spec = GetItem(id);
            if (spec == default)
                return false;

            _context.Specializations.Remove(spec.ToModel());
            return true;
        }

        public IEnumerable<Specialization> GetAll()
        {
            return _context.Specializations.Select(s => s.ToDomain());
        }

        public Specialization? GetItem(ulong id)
        {
            return _context.Specializations.FirstOrDefault(s => s.ID == id)?.ToDomain();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public bool Update(Specialization item)
        {
            _context.Specializations.Update(item.ToModel());
            return true;
        }
    }
}

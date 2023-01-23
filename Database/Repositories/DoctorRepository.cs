using Database.Converters;
using Domain.Models;
using Domain.Repositories;

namespace Database.Repository
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly AppContext _context;

        public DoctorRepository(AppContext context)
        {
            _context = context;
        }

        public bool Create(Doctor item)
        {
            _context.Doctors.Add(item.ToModel());
            return true;
        }

        public bool Delete(ulong id)
        {
            var doctor = GetItem(id);
            if (doctor == default)
                return false;

            _context.Remove(doctor.ToModel());
            return true;
        }

        public Doctor? FindDoctor(Specialization specialization)
        {
            return _context.Doctors.FirstOrDefault(d => d.Specialization == specialization.ToModel())?.ToDomain();
        }

        public IEnumerable<Doctor> FindDoctors(Specialization specialization)
        {
            return _context.Doctors.Where(d => d.Specialization == specialization.ToModel()).Select(d => d.ToDomain());
        }

        public IEnumerable<Doctor> GetAll()
        {
            return _context.Doctors.Select(d => d.ToDomain());
        }

        public Doctor? GetItem(ulong id)
        {
            return _context.Doctors.FirstOrDefault(d => d.ID == id)?.ToDomain();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public bool Update(Doctor item)
        {
            _context.Doctors.Update(item.ToModel());
            return true;
        }
    }
}

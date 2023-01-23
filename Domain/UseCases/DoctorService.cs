using Domain.Models;
using Domain.Repositories;

namespace Domain.UseCases
{
    public class DoctorService
    {
        private readonly IDoctorRepository _db;
        private readonly ISessionRepository _sesssiondb;

        public DoctorService(IDoctorRepository db, ISessionRepository sesssiondb)
        {
            _db = db;
            _sesssiondb = sesssiondb;
        }

        public Result<Doctor> CreateDoctor(Doctor doctor)
        {
            var result = doctor.IsValid();
            if (result.IsFailure)
                return Result.Fail<Doctor>(result.Message);

            var result1 = FindDoctor(doctor.ID);
            if (result1.Res)
                return Result.Fail<Doctor>("Doctor alredy exists");

            return _db.Create(doctor) ? Result.Success(doctor) : Result.Fail<Doctor>("Unable to create doctor");
        }

        public Result<Doctor> DeleteDoctor(ulong id)
        {
            if (_sesssiondb.GetSessions(id).Any())
                return Result.Fail<Doctor>("Unable to delete doctor: Doctor has sessions");

            var result = FindDoctor(id);
            if (result.IsFailure)
                return Result.Fail<Doctor>(result.Message);

            return _db.Delete(id) ? result : Result.Fail<Doctor>("Unable to delete doctor");
        }

        public Result<IEnumerable<Doctor>> GetAllDoctors()
        {
            return Result.Success(_db.GetAll());
        }

        public Result<Doctor> FindDoctor(ulong id)
        {
            if (id < 0)
                return Result.Fail<Doctor>("Invalid id");

            var doctor = _db.GetItem(id);

            return doctor != null ? Result.Success(doctor) : Result.Fail<Doctor>("Doctor not found");
        }
        public Result<IEnumerable<Doctor>> FindDoctors(Specialization specialization)
        {
            var result = specialization.IsValid();
            if (result.IsFailure)
                return Result.Fail<IEnumerable<Doctor>>(result.Message);

            var doctors = _db.FindDoctors(specialization);

            return doctors.Any() ? Result.Success(doctors) : Result.Fail<IEnumerable<Doctor>>("Doctors not found");
        }
    }
}

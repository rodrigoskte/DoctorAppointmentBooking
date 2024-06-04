using DoctorAppointmentBooking.Domain.Entities;

namespace DoctorAppointmentBooking.Domain.Interfaces;

public interface ISpecialtyRepository : IBaseRepository<Specialty>
{
    IList<Doctor> GetSpecialtiesWithDoctors();
    IList<Doctor> GetSpecialtiesWithDoctorsId(int id);

    bool IsSpecialtyExists(Specialty specialty);
    IList<Specialty> GetAllSpecialtyActive();
}
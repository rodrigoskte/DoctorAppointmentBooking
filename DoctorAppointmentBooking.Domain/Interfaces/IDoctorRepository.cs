using DoctorAppointmentBooking.Domain.Entities;

namespace DoctorAppointmentBooking.Domain.Interfaces;

public interface IDoctorRepository : IBaseRepository<Doctor>
{
    IList<Doctor> GetDoctorsWithSpecialties();
    IList<Doctor> GetDoctorsWithSpecialtiesId(int id);
    void AddDoctor(Doctor doctor);
    IList<Doctor> GetAllDoctorsActive();
    IList<Doctor> GetAllDoctorsActiveById(int doctorId);
    bool IsDoctorExists(Doctor doctor);
}
using DoctorAppointmentBooking.Domain.Entities;

namespace DoctorAppointmentBooking.Domain.Interfaces;

public interface IDoctorService
{
    IList<Doctor> GetDoctorsWithSpecialties();
    IList<Doctor> GetDoctorsWithSpecialtiesId(int id);
    void AddDoctor(Doctor doctor);
    bool Validations(Doctor doctor);
    IList<Doctor> GetAllDoctorsActive();
    IList<Doctor> GetAllDoctorsActiveById(int doctorId);
}
using DoctorAppointmentBooking.Domain.Entities;

namespace DoctorAppointmentBooking.Domain.Interfaces;

public interface IDoctorService
{
    IList<Doctor> GetDoctorsWithSpecialties();
    IList<Doctor> GetDoctorsWithSpecialtiesId(int id);
    void AddDoctor(Doctor doctor);
}
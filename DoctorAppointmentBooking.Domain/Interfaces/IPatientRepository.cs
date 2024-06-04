using DoctorAppointmentBooking.Domain.Entities;

namespace DoctorAppointmentBooking.Domain.Interfaces;

public interface IPatientRepository
{
    bool IsPatientExists(Patient patient);
    IList<Patient> GetAllPatientActive();
}
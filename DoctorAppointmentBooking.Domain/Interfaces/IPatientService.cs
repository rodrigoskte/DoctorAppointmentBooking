using DoctorAppointmentBooking.Domain.Entities;

namespace DoctorAppointmentBooking.Domain.Interfaces;

public interface IPatientService
{
    bool Validations(Patient patient);
    IList<Patient> GetAllPatientActive();
}
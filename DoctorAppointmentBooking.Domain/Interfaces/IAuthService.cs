using DoctorAppointmentBooking.Domain.Entities;

namespace DoctorAppointmentBooking.Domain.Interfaces
{
    public interface IAuthService
    {
        Task<string> GenerateJwtToken(string email);
        void CreateDoctorUser(Doctor doctor, string email);
        void CreatePatientUser(Patient patient, string email);
    }
}

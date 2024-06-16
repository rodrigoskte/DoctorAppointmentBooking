using DoctorAppointmentBooking.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace DoctorAppointmentBooking.Domain.Interfaces
{
    public interface IAuthService
    {
        Task<string> GenerateJwtToken(string email);
        void CreateDoctorUser(Doctor doctor, string email);
        void CreatePatientUser(Patient patient, string email);
        Task<string> CreateDoctorPatient(
            IdentityUser user,
            string email,
            string role);
    }
}

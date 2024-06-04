using DoctorAppointmentBooking.Domain.Entities;

namespace DoctorAppointmentBooking.Domain.Interfaces;

public interface ISpecialtyService
{
    bool Validations(Specialty specialty);
    IList<Specialty> GetAllSpecialtyActive();
}
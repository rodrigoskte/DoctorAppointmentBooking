using DoctorAppointmentBooking.Domain.Entities;

namespace DoctorAppointmentBooking.Domain.Exceptions;

public class SpecialtyException : Exception
{
    public SpecialtyException(Specialty specialty)
        : base($"The specialty: {specialty.Description} is already exists.")
    {
    }
}
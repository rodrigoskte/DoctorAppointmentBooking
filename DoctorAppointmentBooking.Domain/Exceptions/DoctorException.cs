using DoctorAppointmentBooking.Domain.Entities;

namespace DoctorAppointmentBooking.Domain.Exceptions;

public class DoctorException : Exception
{
    public DoctorException(Doctor doctor)
        : base($"The doctor: {doctor.Name} or Email: {doctor.Email} is already exists.")
    {
    }
}
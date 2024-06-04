using DoctorAppointmentBooking.Domain.Entities;

namespace DoctorAppointmentBooking.Domain.Exceptions;

public class DoctorException : Exception
{
    public DoctorException(Doctor doctor)
        : base($"The doctor: {doctor.Name} or code(CRM): {doctor.Code} is already exists.")
    {
    }
}
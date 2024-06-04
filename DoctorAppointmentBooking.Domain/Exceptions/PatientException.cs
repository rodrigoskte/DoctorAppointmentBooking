using DoctorAppointmentBooking.Domain.Entities;

namespace DoctorAppointmentBooking.Domain.Exceptions;

public class PatientException : Exception
{
    public PatientException(string message) : base(message) { }
}

public class PatientExistesException : PatientException
{
    public PatientExistesException(Patient patient)
        : base($"The patient: {patient.Name} and e-mail : {patient.Email} is already exists.")
    {
    }
}
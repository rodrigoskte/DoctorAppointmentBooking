namespace DoctorAppointmentBooking.Domain.Exceptions;

public class DoctorSpecialtyException: Exception
{
    public DoctorSpecialtyException(string doctorName, string specialtyDescription)
        : base($"The doctor: {doctorName} is already setted for this specialty: {specialtyDescription}.")
    {
    }
}
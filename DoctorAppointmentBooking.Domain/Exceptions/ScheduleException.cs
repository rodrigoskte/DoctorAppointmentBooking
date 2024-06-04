using DoctorAppointmentBooking.Domain.Entities;

namespace DoctorAppointmentBooking.Domain.Exceptions;

public class ScheduleException : Exception
{
    public ScheduleException(string message) : base(message) { }
}

public class ScheduleConflictException : ScheduleException
{
    public ScheduleConflictException(Schedule schedule)
        : base($"The schedule for doctor: {schedule.Doctor.Name}, patient: {schedule.Patient.Name} and date: {schedule.DateTimeSchedule} already exists.")
    {
    }
}

public class ScheduleDoctorConflictException : ScheduleException
{
    public ScheduleDoctorConflictException(Schedule schedule)
        : base($"The schedule for doctor: {schedule.Doctor.Name} and date: {schedule.DateTimeSchedule} already exists.")
    {
    }
}

public class InvalidScheduleTimeException : ScheduleException
{
    public InvalidScheduleTimeException(DateTime dateTime)
        : base($"The schedule time {dateTime} is invalid.")
    {
    }
}

public class PatientNotAvailableException : ScheduleException
{
    public PatientNotAvailableException(Schedule schedule)
        : base($"The patient {schedule.Patient.Name} is not available at {schedule.DateTimeSchedule}.")
    {
    }
}

public class DoctorScheduleActiveException : ScheduleException
{
    public DoctorScheduleActiveException(IList<Schedule> doctorSchedule)
        : base($"Is not available remove. The Doctor has active schedulesId: {string.Join(", ", doctorSchedule
            .Select(ds => 
                ds.Id))}")
    {
    }
}

public class PatientScheduleActiveException : ScheduleException
{
    public PatientScheduleActiveException(IList<Schedule> patientSchedule)
        : base($"Is not available remove. The Patient has active schedulesId: {string.Join(", ", patientSchedule
            .Select(ds => 
                ds.Id))}")
    {
    }
}
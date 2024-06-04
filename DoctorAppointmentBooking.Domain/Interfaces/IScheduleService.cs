using DoctorAppointmentBooking.Domain.Entities;

namespace DoctorAppointmentBooking.Domain.Interfaces;

public interface IScheduleService
{
    IList<Schedule> GetAllWithDetails();
    IList<Schedule> GetAllWithDetailsId(int id);
    bool Validations(Schedule schedule);
    bool IsDoctorActiveSchedule(
        int doctorId);
    bool IsPatientActiveSchedule(
        int patientId);
    IList<Schedule> GetAllSchedulePatientId(int patientId);
    IList<Schedule> GetAllScheduleDoctorId(int doctorId);
    void CancelSchedulePatient(int patientId);

    void CancelScheduleDoctor(int doctorId);
}
using DoctorAppointmentBooking.Domain.Entities;

namespace DoctorAppointmentBooking.Domain.Interfaces;

public interface IScheduleRepository: IBaseRepository<Schedule>
{
    IList<Schedule> GetAllWithDetails();
    IList<Schedule> GetAllWithDetailsId(int id);
    bool IsScheduleExistsWithDocPatDat(Schedule schedule);
    bool IsDoctorBusy(int doctorId, DateTime dateTimeSchedule);
    Schedule GetDoctorBusy(int doctorId);
    bool IsScheduleExistsWithPatient(int patientId, DateTime dateTimeSchedule);
    bool IsDoctorActiveSchedule(
        int doctorId);
    bool IsPatientActiveSchedule(
        int patientId);
    IList<Schedule> GetAllSchedulePatientId(int patientId);
    IList<Schedule> GetAllScheduleDoctorId(int doctorId);
    Schedule GetScheduleWithDocPatDat(Schedule schedule);
    Schedule GetScheduleWithPatient(int patientId);
    void CancelSchedulePatient(int patientId);

    void CancelScheduleDoctor(int doctorId);
}
using DoctorAppointmentBooking.Domain.Entities;
using DoctorAppointmentBooking.Domain.Exceptions;
using DoctorAppointmentBooking.Domain.Interfaces;

namespace DoctorAppointmentBooking.Application.Services;

public class ScheduleService : IScheduleService
{
    private readonly IScheduleRepository _scheduleRepository;

    public ScheduleService(
        IScheduleRepository scheduleRepository)
    {
        _scheduleRepository = scheduleRepository;
    }

    public IList<Schedule> GetAllWithDetails()
    {
        return _scheduleRepository.GetAllWithDetails();
    }

    public IList<Schedule> GetAllWithDetailsId(int id)
    {
        return _scheduleRepository.GetAllWithDetailsId(id);
    }

    public Schedule GetScheduleById(int id)
    {
        return _scheduleRepository.GetScheduleById(id);
    }

    public bool Validations(Schedule schedule)
    {
        if (IsScheduleExistsWithDocPatDat(schedule))
            throw new ScheduleConflictException(_scheduleRepository.GetScheduleWithDocPatDat(schedule));

        if (IsDoctorBusy(schedule))
            throw new ScheduleDoctorConflictException(_scheduleRepository.GetDoctorBusy(schedule.DoctorId));

        if (IsValidScheduleTime(schedule.DateTimeSchedule))
            throw new InvalidScheduleTimeException(schedule.DateTimeSchedule);

        if (IsPatientAvailable(schedule))
            throw new PatientNotAvailableException(_scheduleRepository.GetScheduleWithPatient(schedule.PatientId));

        return false;
    }

    private bool IsScheduleExistsWithDocPatDat(Schedule schedule)
    {
        return _scheduleRepository.IsScheduleExistsWithDocPatDat(schedule);
    }

    private bool IsDoctorBusy(Schedule schedule)
    {
        return _scheduleRepository.IsDoctorBusy(schedule.DoctorId, schedule.DateTimeSchedule);
    }

    private bool IsValidScheduleTime(DateTime dateTime)
    {
        return dateTime <= DateTime.Today;
    }

    private bool IsPatientAvailable(Schedule schedule)
    {
        return _scheduleRepository.IsScheduleExistsWithPatient(schedule.PatientId, schedule.DateTimeSchedule);
    }

    public bool IsDoctorActiveSchedule(int doctorId)
    {
        if (_scheduleRepository.IsDoctorActiveSchedule(doctorId))
            throw new DoctorScheduleActiveException(_scheduleRepository.GetAllScheduleDoctorId(doctorId));

        return false;
    }
    
    public bool IsPatientActiveSchedule(int patientId)
    {
        if (_scheduleRepository.IsPatientActiveSchedule(patientId))
            throw new PatientScheduleActiveException(_scheduleRepository.GetAllSchedulePatientId(patientId));

        return false;
    }

    public IList<Schedule> GetAllSchedulePatientId(int patientId)
    {
        return _scheduleRepository.GetAllSchedulePatientId(patientId);
    }

    public IList<Schedule> GetAllScheduleDoctorId(int doctorId)
    {
        return _scheduleRepository.GetAllScheduleDoctorId(doctorId);
    }

    public void CancelSchedulePatient(int patientId)
    {
        _scheduleRepository.CancelSchedulePatient(patientId);
    }
    
    public void CancelScheduleDoctor(int doctorId)
    {
        _scheduleRepository.CancelScheduleDoctor(doctorId);
    }
}
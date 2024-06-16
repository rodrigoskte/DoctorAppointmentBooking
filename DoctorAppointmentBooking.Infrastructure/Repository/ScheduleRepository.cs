using System;
using System.Collections.Generic;
using System.Linq;
using DoctorAppointmentBooking.Domain.Entities;
using DoctorAppointmentBooking.Domain.Interfaces;
using DoctorAppointmentBooking.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointmentBooking.Infrastructure.Repository;

public class ScheduleRepository : BaseRepository<Schedule>, IScheduleRepository
{
    protected readonly SqlDbContext _dbContext;

    public ScheduleRepository(SqlDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public IList<Schedule> GetAllWithDetails()
    {
        return _dbContext.Schedules
            .Where(e => !e.IsDeleted)
            .Include(ds => ds.Doctor)
            .Include(ds => ds.Patient)
            .ToList();
    }

    public IList<Schedule> GetAllWithDetailsId(int id)
    {
        return _dbContext.Schedules
            .Where(e => e.Id == id)
            .Include(ds => ds.Doctor)
            .Include(ds => ds.Patient)
            .ToList();
    }

    public Schedule GetScheduleById(int id)
    {
        return _dbContext.Schedules
            .Include(ds => ds.Doctor)
            .Include(ds => ds.Patient)
            .FirstOrDefault(e => e.Id == id);
    }

    public bool IsScheduleExistsWithDocPatDat(Schedule schedule)
    {
        return _dbContext.Schedules.Any(e =>
            e.DoctorId == schedule.DoctorId &&
            e.PatientId == schedule.PatientId &&
            !e.IsDeleted &&
            e.DateTimeSchedule == schedule.DateTimeSchedule);
    }

    public Schedule GetScheduleWithDocPatDat(Schedule schedule)
    {
        return _dbContext.Schedules.Where(e =>
                e.DoctorId == schedule.DoctorId &&
                e.PatientId == schedule.PatientId &&
                !e.IsDeleted &&
                e.DateTimeSchedule == schedule.DateTimeSchedule)
            .Include(ds => ds.Doctor)
            .Include(ds => ds.Patient).First();
    }

    public bool IsDoctorBusy(int doctorId, DateTime dateTimeSchedule)
    {
        return _dbContext.Schedules.Any(e => 
            e.DoctorId == doctorId &&
            !e.IsDeleted &&
            e.DateTimeSchedule == dateTimeSchedule);
    }

    public Schedule GetDoctorBusy(int doctorId)
    {
        return _dbContext.Schedules.Where(e =>
                e.DoctorId == doctorId && 
                !e.IsDeleted)
            .Include(ds => ds.Doctor)
            .Include(ds => ds.Patient).First();
    }

    public bool IsScheduleExistsWithPatient(int patientId, DateTime dateTimeSchedule)
    {
        return _dbContext.Schedules.Any(e =>
            e.PatientId == patientId &&
            !e.IsDeleted &&
            e.DateTimeSchedule == dateTimeSchedule);
    }

    public Schedule GetScheduleWithPatient(int patientId)
    {
        return _dbContext.Schedules.Where(e =>
                e.PatientId == patientId &&
                !e.IsDeleted)
            .Include(ds => ds.Doctor)
            .Include(ds => ds.Patient).First();
    }

    public void CancelSchedulePatient(int patientId)
    {
        var schedules = _dbContext.Schedules
            .Where(s => s.PatientId == patientId && !s.IsDeleted)
            .ToList();

        foreach (var schedule in schedules)
            schedule.IsDeleted = true;

        _dbContext.SaveChanges();
    }
    
    public void CancelScheduleDoctor(int doctorId)
    {
        var schedules = _dbContext.Schedules
            .Where(s => s.DoctorId == doctorId && !s.IsDeleted)
            .ToList();

        foreach (var schedule in schedules)
            schedule.IsDeleted = true;

        _dbContext.SaveChanges();
    }

    public bool IsDoctorActiveSchedule(
        int doctorId)
    {
        return _dbContext.Schedules.Any(e =>
            e.DoctorId == doctorId &&
            !e.IsDeleted);
    }

    public bool IsPatientActiveSchedule(
        int patientId)
    {
        return _dbContext.Schedules.Any(e =>
            e.PatientId == patientId &&
            !e.IsDeleted);
    }

    public IList<Schedule> GetAllSchedulePatientId(int patientId)
    {
        return _dbContext.Schedules
            .Where(e => e.PatientId == patientId)
            .Include(ds => ds.Doctor)
            .Include(ds => ds.Patient)
            .ToList();
    }

    public IList<Schedule> GetAllScheduleDoctorId(int doctorId)
    {
        return _dbContext.Schedules
            .Where(e => e.DoctorId == doctorId)
            .Include(ds => ds.Doctor)
            .Include(ds => ds.Patient)
            .ToList();
    }
}
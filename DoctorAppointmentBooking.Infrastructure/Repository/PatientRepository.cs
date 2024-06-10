using System.Collections.Generic;
using System.Linq;
using DoctorAppointmentBooking.Domain.Entities;
using DoctorAppointmentBooking.Domain.Interfaces;
using DoctorAppointmentBooking.Infrastructure.Context;

namespace DoctorAppointmentBooking.Infrastructure.Repository;

public class PatientRepository : BaseRepository<Patient>, IPatientRepository
{
    protected readonly SqlDbContext _dbContext;

    public PatientRepository(SqlDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public bool IsPatientExists(Patient patient)
    {
        if(_dbContext.Patients.Any(e => e.Name == patient.Name))
            return true;

        if(_dbContext.Patients.Any(e => e.Email == patient.Email))
            return true;

        return false;
    }

    public IList<Patient> GetAllPatientActive()
    {
        return _dbContext.Patients.Where(e => !e.IsDeleted).ToList();
    }

    public Patient GetPatientByUserId(string userId)
    {
        return _dbContext.Patients.FirstOrDefault(e => e.UserId == userId);
    }
}
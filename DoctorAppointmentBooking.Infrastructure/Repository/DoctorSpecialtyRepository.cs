using System.Collections.Generic;
using System.Linq;
using DoctorAppointmentBooking.Domain.Entities;
using DoctorAppointmentBooking.Domain.Interfaces;
using DoctorAppointmentBooking.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointmentBooking.Infrastructure.Repository;

public class DoctorSpecialtyRepository : BaseRepository<DoctorSpecialty>, IDoctorSpecialtyRepository
{
    protected readonly SqlDbContext _dbContext;
    
    public DoctorSpecialtyRepository(SqlDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public IList<DoctorSpecialty> GetAllWithDetails()
    {
        return _dbContext.DoctorSpecialties
            .Where(e => !e.IsDeleted)
            .Include(ds => ds.Doctor)
            .Include(ds => ds.Specialty)
            .ToList();
    }

    public IList<DoctorSpecialty> GetAllWithDetailsId(int id)
    {
        return _dbContext.DoctorSpecialties
            .Where(e => 
                e.Id == id)
            .Include(ds => ds.Doctor)
            .Include(ds => ds.Specialty)
            .ToList();
    }
    
    public bool IsDoctorSpecialtyExists(
        int doctorId,
        int specialtyId)
    {
        return _dbContext.DoctorSpecialties.Any(e => e.SpecialtyId == specialtyId && e.DoctorId == doctorId);
    }
}
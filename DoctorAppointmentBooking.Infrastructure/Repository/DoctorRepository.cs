using System.Collections.Generic;
using System.Linq;
using DoctorAppointmentBooking.Domain.Entities;
using DoctorAppointmentBooking.Domain.Interfaces;
using DoctorAppointmentBooking.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointmentBooking.Infrastructure.Repository;

public class DoctorRepository : BaseRepository<Doctor>, IDoctorRepository
{
    protected readonly SqlDbContext _dbContext;

    public DoctorRepository(SqlDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    
    public IList<Doctor> GetDoctorsWithSpecialties()
    {
        return _dbContext.Doctors
                         .Where(d => !d.IsDeleted) 
                         .Include(d => d.DoctorSpecialties)
                         .ThenInclude(ds => ds.Specialty)
                         .ToList();
    }
    
    public IList<Doctor> GetDoctorsWithSpecialtiesId(int id)
    {
        return _dbContext.Doctors
            .Where(e => e.Id == id)
            .Include(d => d.DoctorSpecialties)
            .ThenInclude(ds => ds.Specialty)
            .ToList();
    }

    public void AddDoctor(Doctor doctor)
    {
        _dbContext.Set<Doctor>().Add(doctor);
        _dbContext.SaveChanges();
    }
}
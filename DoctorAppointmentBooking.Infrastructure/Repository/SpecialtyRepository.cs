using System.Collections.Generic;
using System.Linq;
using DoctorAppointmentBooking.Domain.Entities;
using DoctorAppointmentBooking.Domain.Interfaces;
using DoctorAppointmentBooking.Infrastructure.Context;

namespace DoctorAppointmentBooking.Infrastructure.Repository;

public class SpecialtyRepository  : BaseRepository<Specialty>, ISpecialtyRepository
{
    protected readonly SqlDbContext _dbContext;

    public SpecialtyRepository(SqlDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public IList<Doctor> GetSpecialtiesWithDoctors()
    {
        throw new System.NotImplementedException();
    }

    public IList<Doctor> GetSpecialtiesWithDoctorsId(int id)
    {
        throw new System.NotImplementedException();
    }

    public bool IsSpecialtyExists(Specialty specialty)
    {
        return _dbContext.Specialties.Any(e => e.Description == specialty.Description);
    }
    
    public IList<Specialty> GetAllSpecialtyActive()
    {
        return _dbContext.Specialties.Where(e => !e.IsDeleted).ToList();
    }
}
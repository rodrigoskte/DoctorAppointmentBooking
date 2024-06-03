using System.Collections.Generic;
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
}
using DoctorAppointmentBooking.Domain.Entities;
using DoctorAppointmentBooking.Infrastructure.Mapping;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointmentBooking.Infrastructure.Context
{
    public class SqlDbContext : DbContext
    {
        public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options) { }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<DoctorSpecialty> DoctorSpecialties { get; set; }
        public DbSet<Schedule> Schedules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new PatientMap());
            modelBuilder.ApplyConfiguration(new DoctorMap());
            modelBuilder.ApplyConfiguration(new SpecialtyMap());
            modelBuilder.ApplyConfiguration(new DoctorSpecialtyMap());
            modelBuilder.ApplyConfiguration(new ScheduleMap());                     
        }
    }
}

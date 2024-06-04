using System;
using DoctorAppointmentBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoctorAppointmentBooking.Infrastructure.Mapping
{
    public class ScheduleMap : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            builder.ToTable("Schedule");

            builder.HasKey(x => x.Id);

            builder.Property(prop => prop.IsDeleted)
                .IsRequired()
                .HasColumnName("IsDeleted")
                .HasColumnType("bit");

            builder.Property(prop => prop.DateTimeSchedule)
                .IsRequired()
                .HasColumnName("DateTimeSchedule")
                .HasColumnType("datetime");

            builder
                .HasOne(x => x.Doctor)
                .WithMany(x => x.Schedules)
                .HasForeignKey(x => x.DoctorId);

            builder
                .HasOne(x => x.Patient)
                .WithMany(x => x.Schedules)
                .HasForeignKey(x => x.PatientId);

            builder.HasData(
                new Schedule {Id = 1, DateTimeSchedule = DateTime.Today.AddDays(10), IsDeleted = false, PatientId = 1, DoctorId = 1},
                new Schedule {Id = 2, DateTimeSchedule = DateTime.Today.AddDays(20), IsDeleted = false, PatientId = 2, DoctorId = 2},
                new Schedule {Id = 3, DateTimeSchedule = DateTime.Today.AddDays(12), IsDeleted = false, PatientId = 3, DoctorId = 3},
                new Schedule {Id = 4, DateTimeSchedule = DateTime.Today.AddDays(11), IsDeleted = false, PatientId = 2, DoctorId = 2},
                new Schedule {Id = 5, DateTimeSchedule = DateTime.Today.AddDays(4), IsDeleted = false, PatientId = 3, DoctorId = 3},
                new Schedule {Id = 6, DateTimeSchedule = DateTime.Today.AddDays(6), IsDeleted = false, PatientId = 1, DoctorId = 1}
                );
        }
    }
}

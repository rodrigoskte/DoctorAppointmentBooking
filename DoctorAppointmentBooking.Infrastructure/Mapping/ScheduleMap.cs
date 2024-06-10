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
                .HasColumnType("datetime2");

            builder.HasOne(x => x.Doctor)
                .WithMany(d => d.Schedules)
                .HasForeignKey(x => x.DoctorId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Patient)
                .WithMany(p => p.Schedules)
                .HasForeignKey(x => x.PatientId)
                .OnDelete(DeleteBehavior.NoAction); 
        }
    }
}

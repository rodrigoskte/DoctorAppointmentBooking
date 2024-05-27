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
        }
    }
}

using DoctorAppointmentBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoctorAppointmentBooking.Infrastructure.Mapping
{
    public class DoctorMap : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.ToTable("Doctor");

            builder.HasKey(x => x.Id);

            builder.Property(prop => prop.Name)
                 .HasConversion(prop => prop.ToString(), prop => prop)
                 .IsRequired()
                 .HasColumnName("Name")
                 .HasColumnType("varchar(100)");

            builder.Property(prop => prop.Email)
                 .HasConversion(prop => prop.ToString(), prop => prop)
                 .IsRequired()
                 .HasColumnName("Email")
                 .HasColumnType("varchar(100)");


            builder.Property(prop => prop.IsDeleted)
                .IsRequired()
                .HasColumnName("IsDeleted")
                .HasColumnType("bit");

            builder
                .HasMany(x => x.DoctorSpecialties)
                .WithOne(x => x.Doctor)
                .HasForeignKey(x => x.DoctorId);
            
            builder.Property(prop => prop.UserId)
                .HasConversion(prop => prop.ToString(), prop => prop)
                .HasColumnName("UserId")
                .HasColumnType("varchar(100)");
        }
    }
}

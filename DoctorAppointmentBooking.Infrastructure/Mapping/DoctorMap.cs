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

            builder.Property(prop => prop.Code)
                 .HasConversion(prop => prop.ToString(), prop => prop)
                 .IsRequired()
                 .HasColumnName("Code")
                 .HasColumnType("varchar(10)");


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

            builder.HasData(
                new Doctor { Id = 1, Name = "Christiano Coccuza", IsDeleted = false, Code = "123", UserId = ""},
                new Doctor { Id = 2, Name = "Ida Fortini", IsDeleted = false, Code = "456", UserId = ""},
                new Doctor { Id = 3, Name = "Bárbara Martins", IsDeleted = false, Code = "789", UserId = ""},
                new Doctor { Id = 4, Name = "Ronu Muole", IsDeleted = false, Code = "001", UserId = ""},
                new Doctor { Id = 5, Name = "Mayfe Puesl", IsDeleted = false, Code = "002", UserId = ""},
                new Doctor { Id = 6, Name = "Deko Gapuobri", IsDeleted = true, Code = "003", UserId = ""}
                );
        }
    }
}

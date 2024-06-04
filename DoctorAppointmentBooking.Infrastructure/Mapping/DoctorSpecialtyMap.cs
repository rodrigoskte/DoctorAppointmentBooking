using DoctorAppointmentBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoctorAppointmentBooking.Infrastructure.Mapping
{
    public class DoctorSpecialtyMap : IEntityTypeConfiguration<DoctorSpecialty>
    {
        public void Configure(EntityTypeBuilder<DoctorSpecialty> builder)
        {
            builder.ToTable("DoctorSpecialty");

            builder.HasKey(x => new { x.Id});

            builder
                .HasOne(x => x.Doctor)
                .WithMany(x => x.DoctorSpecialties)
                .HasForeignKey(x => x.DoctorId);

            builder
                .HasOne(x => x.Specialty)
                .WithMany(x => x.DoctorSpecialties)
                .HasForeignKey(x => x.SpecialtyId);

            builder.HasData(
                new DoctorSpecialty{Id = 1, DoctorId = 1, SpecialtyId = 1},
                new DoctorSpecialty{Id = 2, DoctorId = 2, SpecialtyId = 2},
                new DoctorSpecialty{Id = 3, DoctorId = 3, SpecialtyId = 3},
                new DoctorSpecialty{Id = 4, DoctorId = 4, SpecialtyId = 4},
                new DoctorSpecialty{Id = 5, DoctorId = 5, SpecialtyId = 5}
            );
        }
    }
}
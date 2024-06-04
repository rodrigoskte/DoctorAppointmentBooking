using DoctorAppointmentBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoctorAppointmentBooking.Infrastructure.Mapping
{
    public class SpecialtyMap : IEntityTypeConfiguration<Specialty>
    {
        public void Configure(EntityTypeBuilder<Specialty> builder)
        {
            builder.ToTable("Specialty");

            builder.HasKey(x => x.Id);

            builder.Property(prop => prop.Description)
                 .HasConversion(prop => prop.ToString(), prop => prop)
                 .IsRequired()
                 .HasColumnName("Description")
                 .HasColumnType("varchar(100)");

            builder.Property(prop => prop.IsDeleted)
                .IsRequired()
                .HasColumnName("IsDeleted")
                .HasColumnType("bit");

            builder
                .HasMany(x => x.DoctorSpecialties)
                .WithOne(x => x.Specialty)
                .HasForeignKey(x => x.SpecialtyId);

            builder.HasData(
                new Specialty{Id = 1, Description = "Nefrologista", IsDeleted = false},
                new Specialty{Id = 2, Description = "Neurologista", IsDeleted = false},
                new Specialty{Id = 3, Description = "Nutricionista", IsDeleted = false},
                new Specialty{Id = 4, Description = "Gastro", IsDeleted = false},
                new Specialty{Id = 5, Description = "Oftalmologista", IsDeleted = false},
                new Specialty{Id = 6, Description = "Oncologista", IsDeleted = false},
                new Specialty{Id = 7, Description = "Clinico Geral", IsDeleted = false}
                );
        }
    }
}

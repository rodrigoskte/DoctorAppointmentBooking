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
        }
    }
}

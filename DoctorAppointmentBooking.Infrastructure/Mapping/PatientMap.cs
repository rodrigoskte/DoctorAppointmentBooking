using DoctorAppointmentBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoctorAppointmentBooking.Infrastructure.Mapping
{
    public class PatientMap : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patient");

            builder.HasKey(prop => prop.Id);

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
            
            builder.Property(prop => prop.UserId)
                .HasConversion(prop => prop.ToString(), prop => prop)
                .HasColumnName("UserId")
                .HasColumnType("varchar(100)");

            builder.HasData(
                new Patient{ Id = 1, Name = "Rodrigo Carvalhomaru",Email = "r@rrr.com.br", IsDeleted = false, UserId = string.Empty},
                new Patient{ Id = 2, Name = "Enzo Shutzing",Email = "shutzing@enzo.com.br", IsDeleted = false, UserId = string.Empty},
                new Patient{ Id = 3, Name = "Cléber Bluedragon",Email = "cleber@bluedragon.com.br", IsDeleted = false, UserId = string.Empty},
                new Patient{ Id = 4, Name = "Neville Bernard",Email = "neville@bernard.com.br", IsDeleted = false, UserId = string.Empty},
                new Patient{ Id = 5, Name = "Wendell Kessner",Email = "wendell@kessner.com.br", IsDeleted = false, UserId = string.Empty},
                new Patient{ Id = 6, Name = "Adare Gerbitz",Email = "adare@gerbitz.com.br", IsDeleted = false, UserId = string.Empty},
                new Patient{ Id = 7, Name = "Sanders Cameron",Email = "sanders@cameron.com.br", IsDeleted = false, UserId = string.Empty},
                new Patient{ Id = 8, Name = "Agata Wanner",Email = "agata@wanner.com.br", IsDeleted = false, UserId = string.Empty},
                new Patient{ Id = 9, Name = "Senalda Ramírez",Email = "senalda@ramirez.com.br", IsDeleted = false, UserId = string.Empty}
                );
        }
    }
}

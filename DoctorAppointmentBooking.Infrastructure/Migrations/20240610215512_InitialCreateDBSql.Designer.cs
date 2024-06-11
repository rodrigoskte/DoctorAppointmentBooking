﻿// <auto-generated />
using System;
using DoctorAppointmentBooking.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DoctorAppointmentBooking.Infrastructure.Migrations
{
    [DbContext(typeof(SqlDbContext))]
    [Migration("20240610215512_InitialCreateDBSql")]
    partial class InitialCreateDBSql
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DoctorAppointmentBooking.Domain.Entities.Doctor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("varchar(10)")
                        .HasColumnName("Code");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("IsDeleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Name");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("UserId");

                    b.HasKey("Id");

                    b.ToTable("Doctor", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Code = "123",
                            IsDeleted = false,
                            Name = "Christiano Coccuza",
                            UserId = ""
                        },
                        new
                        {
                            Id = 2,
                            Code = "456",
                            IsDeleted = false,
                            Name = "Ida Fortini",
                            UserId = ""
                        },
                        new
                        {
                            Id = 3,
                            Code = "789",
                            IsDeleted = false,
                            Name = "Bárbara Martins",
                            UserId = ""
                        },
                        new
                        {
                            Id = 4,
                            Code = "001",
                            IsDeleted = false,
                            Name = "Ronu Muole",
                            UserId = ""
                        },
                        new
                        {
                            Id = 5,
                            Code = "002",
                            IsDeleted = false,
                            Name = "Mayfe Puesl",
                            UserId = ""
                        },
                        new
                        {
                            Id = 6,
                            Code = "003",
                            IsDeleted = true,
                            Name = "Deko Gapuobri",
                            UserId = ""
                        });
                });

            modelBuilder.Entity("DoctorAppointmentBooking.Domain.Entities.DoctorSpecialty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("SpecialtyId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.HasIndex("SpecialtyId");

                    b.ToTable("DoctorSpecialty", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DoctorId = 1,
                            IsDeleted = false,
                            SpecialtyId = 1
                        },
                        new
                        {
                            Id = 2,
                            DoctorId = 2,
                            IsDeleted = false,
                            SpecialtyId = 2
                        },
                        new
                        {
                            Id = 3,
                            DoctorId = 3,
                            IsDeleted = false,
                            SpecialtyId = 3
                        },
                        new
                        {
                            Id = 4,
                            DoctorId = 4,
                            IsDeleted = false,
                            SpecialtyId = 4
                        },
                        new
                        {
                            Id = 5,
                            DoctorId = 5,
                            IsDeleted = false,
                            SpecialtyId = 5
                        });
                });

            modelBuilder.Entity("DoctorAppointmentBooking.Domain.Entities.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Email");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("IsDeleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Name");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("UserId");

                    b.HasKey("Id");

                    b.ToTable("Patient", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "r@rrr.com.br",
                            IsDeleted = false,
                            Name = "Rodrigo Carvalhomaru",
                            UserId = ""
                        },
                        new
                        {
                            Id = 2,
                            Email = "shutzing@enzo.com.br",
                            IsDeleted = false,
                            Name = "Enzo Shutzing",
                            UserId = ""
                        },
                        new
                        {
                            Id = 3,
                            Email = "cleber@bluedragon.com.br",
                            IsDeleted = false,
                            Name = "Cléber Bluedragon",
                            UserId = ""
                        },
                        new
                        {
                            Id = 4,
                            Email = "neville@bernard.com.br",
                            IsDeleted = false,
                            Name = "Neville Bernard",
                            UserId = ""
                        },
                        new
                        {
                            Id = 5,
                            Email = "wendell@kessner.com.br",
                            IsDeleted = false,
                            Name = "Wendell Kessner",
                            UserId = ""
                        },
                        new
                        {
                            Id = 6,
                            Email = "adare@gerbitz.com.br",
                            IsDeleted = false,
                            Name = "Adare Gerbitz",
                            UserId = ""
                        },
                        new
                        {
                            Id = 7,
                            Email = "sanders@cameron.com.br",
                            IsDeleted = false,
                            Name = "Sanders Cameron",
                            UserId = ""
                        },
                        new
                        {
                            Id = 8,
                            Email = "agata@wanner.com.br",
                            IsDeleted = false,
                            Name = "Agata Wanner",
                            UserId = ""
                        },
                        new
                        {
                            Id = 9,
                            Email = "senalda@ramirez.com.br",
                            IsDeleted = false,
                            Name = "Senalda Ramírez",
                            UserId = ""
                        });
                });

            modelBuilder.Entity("DoctorAppointmentBooking.Domain.Entities.Schedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateTimeSchedule")
                        .HasColumnType("datetime2")
                        .HasColumnName("DateTimeSchedule");

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("IsDeleted");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

                    b.ToTable("Schedule", (string)null);
                });

            modelBuilder.Entity("DoctorAppointmentBooking.Domain.Entities.Specialty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Description");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("IsDeleted");

                    b.HasKey("Id");

                    b.ToTable("Specialty", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Nefrologista",
                            IsDeleted = false
                        },
                        new
                        {
                            Id = 2,
                            Description = "Neurologista",
                            IsDeleted = false
                        },
                        new
                        {
                            Id = 3,
                            Description = "Nutricionista",
                            IsDeleted = false
                        },
                        new
                        {
                            Id = 4,
                            Description = "Gastro",
                            IsDeleted = false
                        },
                        new
                        {
                            Id = 5,
                            Description = "Oftalmologista",
                            IsDeleted = false
                        },
                        new
                        {
                            Id = 6,
                            Description = "Oncologista",
                            IsDeleted = false
                        },
                        new
                        {
                            Id = 7,
                            Description = "Clinico Geral",
                            IsDeleted = false
                        });
                });

            modelBuilder.Entity("DoctorAppointmentBooking.Domain.Entities.DoctorSpecialty", b =>
                {
                    b.HasOne("DoctorAppointmentBooking.Domain.Entities.Doctor", "Doctor")
                        .WithMany("DoctorSpecialties")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DoctorAppointmentBooking.Domain.Entities.Specialty", "Specialty")
                        .WithMany("DoctorSpecialties")
                        .HasForeignKey("SpecialtyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("Specialty");
                });

            modelBuilder.Entity("DoctorAppointmentBooking.Domain.Entities.Schedule", b =>
                {
                    b.HasOne("DoctorAppointmentBooking.Domain.Entities.Doctor", "Doctor")
                        .WithMany("Schedules")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DoctorAppointmentBooking.Domain.Entities.Patient", "Patient")
                        .WithMany("Schedules")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("DoctorAppointmentBooking.Domain.Entities.Doctor", b =>
                {
                    b.Navigation("DoctorSpecialties");

                    b.Navigation("Schedules");
                });

            modelBuilder.Entity("DoctorAppointmentBooking.Domain.Entities.Patient", b =>
                {
                    b.Navigation("Schedules");
                });

            modelBuilder.Entity("DoctorAppointmentBooking.Domain.Entities.Specialty", b =>
                {
                    b.Navigation("DoctorSpecialties");
                });
#pragma warning restore 612, 618
        }
    }
}
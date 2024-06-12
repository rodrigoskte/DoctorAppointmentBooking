using Xunit;
using Moq;
using System.Collections.Generic;
using DoctorAppointmentBooking.Domain.Entities;
using DoctorAppointmentBooking.Domain.Exceptions;
using DoctorAppointmentBooking.Domain.Interfaces;
using DoctorAppointmentBooking.Application.Services;

public class SpecialtyServiceTest
{
    private readonly SpecialtyService _specialtyService;
    private readonly Mock<ISpecialtyRepository> _specialtyRepositoryMock;

    public SpecialtyServiceTest()
    {
        _specialtyRepositoryMock = new Mock<ISpecialtyRepository>();
        _specialtyService = new SpecialtyService(_specialtyRepositoryMock.Object);
    }

    [Fact]
    public void Validations_SpecialtyExists_ThrowsSpecialtyException()
    {
        // Arrange
        var specialty = new Specialty { Id = 1, Description = "Cardiology" };
        _specialtyRepositoryMock.Setup(repo => repo.IsSpecialtyExists(specialty)).Returns(true);

        // Act & Assert
        Assert.Throws<SpecialtyException>(() => _specialtyService.Validations(specialty));
    }

    [Fact]
    public void Validations_SpecialtyDoesNotExist_ReturnsFalse()
    {
        // Arrange
        var specialty = new Specialty { Id = 1, Description = "Cardiology" };
        _specialtyRepositoryMock.Setup(repo => repo.IsSpecialtyExists(specialty)).Returns(false);

        // Act
        var result = _specialtyService.Validations(specialty);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void GetAllSpecialtyActive_ReturnsActiveSpecialties()
    {
        // Arrange
        var specialties = new List<Specialty> { new Specialty { Id = 1, Description = "Cardiology" } };
        _specialtyRepositoryMock.Setup(repo => repo.GetAllSpecialtyActive()).Returns(specialties);

        // Act
        var result = _specialtyService.GetAllSpecialtyActive();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(specialties.Count, result.Count);
    }
}
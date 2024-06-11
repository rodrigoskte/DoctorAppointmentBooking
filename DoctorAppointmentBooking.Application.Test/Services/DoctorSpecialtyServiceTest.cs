using Moq;
using DoctorAppointmentBooking.Domain.Entities;
using DoctorAppointmentBooking.Domain.Exceptions;
using DoctorAppointmentBooking.Domain.Interfaces;
using DoctorAppointmentBooking.Application.Services;

public class DoctorSpecialtyServiceTest
{
    private readonly DoctorSpecialtyService _doctorSpecialtyService;
    private readonly Mock<IDoctorSpecialtyRepository> _doctorSpecialtyRepositoryMock;
    private readonly Mock<IBaseService<Specialty>> _baseSpecialtyServiceMock;
    private readonly Mock<IBaseService<Doctor>> _baseDoctorServiceMock;

    public DoctorSpecialtyServiceTest()
    {
        _doctorSpecialtyRepositoryMock = new Mock<IDoctorSpecialtyRepository>();
        _baseSpecialtyServiceMock = new Mock<IBaseService<Specialty>>();
        _baseDoctorServiceMock = new Mock<IBaseService<Doctor>>();
        _doctorSpecialtyService = new DoctorSpecialtyService(
            _doctorSpecialtyRepositoryMock.Object, 
            _baseSpecialtyServiceMock.Object, 
            _baseDoctorServiceMock.Object);
    }

    [Fact]
    public void GetAllWithDetails_ReturnsDoctorSpecialties()
    {
        // Arrange
        var doctorSpecialties = new List<DoctorSpecialty> { new DoctorSpecialty { DoctorId = 1, SpecialtyId = 1 } };
        _doctorSpecialtyRepositoryMock.Setup(repo => repo.GetAllWithDetails()).Returns(doctorSpecialties);

        // Act
        var result = _doctorSpecialtyService.GetAllWithDetails();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(doctorSpecialties.Count, result.Count);
    }

    [Fact]
    public void GetAllWithDetailsId_ReturnsDoctorSpecialties()
    {
        // Arrange
        var id = 1;
        var doctorSpecialties = new List<DoctorSpecialty> { new DoctorSpecialty { DoctorId = 1, SpecialtyId = 1 } };
        _doctorSpecialtyRepositoryMock.Setup(repo => repo.GetAllWithDetailsId(id)).Returns(doctorSpecialties);

        // Act
        var result = _doctorSpecialtyService.GetAllWithDetailsId(id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(doctorSpecialties.Count, result.Count);
    }

    [Fact]
    public void Validations_DoctorSpecialtyExists_ThrowsDoctorSpecialtyException()
    {
        // Arrange
        var doctorId = 1;
        var specialtyId = 1;
        var doctor = new Doctor { Id = doctorId, Name = "Dr. John Doe" };
        var specialty = new Specialty { Id = specialtyId, Description = "Cardiology" };

        _doctorSpecialtyRepositoryMock.Setup(repo => repo.IsDoctorSpecialtyExists(doctorId, specialtyId)).Returns(true);
        _baseDoctorServiceMock.Setup(service => service.GetById(doctorId)).Returns(doctor);
        _baseSpecialtyServiceMock.Setup(service => service.GetById(specialtyId)).Returns(specialty);

        // Act & Assert
        var exception = Assert.Throws<DoctorSpecialtyException>(() => _doctorSpecialtyService.Validations(doctorId, specialtyId));
        Assert.Equal($"The doctor: Dr. John Doe is already setted for this specialty: Cardiology.", exception.Message);
    }

    [Fact]
    public void Validations_DoctorSpecialtyDoesNotExist_ReturnsFalse()
    {
        // Arrange
        var doctorId = 1;
        var specialtyId = 1;

        _doctorSpecialtyRepositoryMock.Setup(repo => repo.IsDoctorSpecialtyExists(doctorId, specialtyId)).Returns(false);

        // Act
        var result = _doctorSpecialtyService.Validations(doctorId, specialtyId);

        // Assert
        Assert.False(result);
    }
}

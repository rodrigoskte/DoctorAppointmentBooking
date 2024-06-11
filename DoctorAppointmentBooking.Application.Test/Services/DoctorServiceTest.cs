using Moq;
using DoctorAppointmentBooking.Application.Constants;
using DoctorAppointmentBooking.Application.Services;
using DoctorAppointmentBooking.Domain.Entities;
using DoctorAppointmentBooking.Domain.Exceptions;
using DoctorAppointmentBooking.Domain.Interfaces;

public class DoctorServiceTest
{
    private readonly DoctorService _doctorService;
    private readonly Mock<IDoctorRepository> _doctorRepositoryMock;
    private readonly Mock<ISpecialtyRepository> _specialtyRepositoryMock;
    private int ID_DOCTOR_SUCESS = 1; 

    public DoctorServiceTest()
    {
        _doctorRepositoryMock = new Mock<IDoctorRepository>();
        _specialtyRepositoryMock = new Mock<ISpecialtyRepository>();
        _doctorService = new DoctorService(_doctorRepositoryMock.Object, _specialtyRepositoryMock.Object);
    }

    [Fact]
    public void GetDoctorsWithSpecialties_ReturnsDoctors()
    {
        // Arrange
        var doctors = new List<Doctor> { new Doctor { Id = ID_DOCTOR_SUCESS } };
        _doctorRepositoryMock.Setup(repo => repo.GetDoctorsWithSpecialties()).Returns(doctors);

        // Act
        var result = _doctorService.GetDoctorsWithSpecialties();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(doctors.Count, result.Count);
    }

    [Fact]
    public void GetDoctorsWithSpecialtiesId_ReturnsDoctors()
    {
        // Arrange
        var doctorId = 1;
        var doctors = new List<Doctor> { new Doctor { Id = ID_DOCTOR_SUCESS} };
        _doctorRepositoryMock.Setup(repo => repo.GetDoctorsWithSpecialtiesId(doctorId)).Returns(doctors);

        // Act
        var result = _doctorService.GetDoctorsWithSpecialtiesId(doctorId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(doctors.Count, result.Count);
    }

    [Fact]
    public void AddDoctor_WithValidSpecialties_AddsDoctor()
    {
        // Arrange
        var doctor = new Doctor
        {
            Id = ID_DOCTOR_SUCESS,
            DoctorSpecialties = new List<DoctorSpecialty>
            {
                new DoctorSpecialty { SpecialtyId = 1 },
                new DoctorSpecialty { SpecialtyId = 2 }
            }
        };

        _specialtyRepositoryMock.Setup(repo => repo.Select(It.IsAny<int>())).Returns(new Specialty());

        // Act
        _doctorService.AddDoctor(doctor);

        // Assert
        _doctorRepositoryMock.Verify(repo => repo.AddDoctor(doctor), Times.Once);
    }

    [Fact]
    public void AddDoctor_WithInvalidSpecialties_ThrowsArgumentException()
    {
        // Arrange
        var doctor = new Doctor
        {
            Id = ID_DOCTOR_SUCESS,
            DoctorSpecialties = new List<DoctorSpecialty>
            {
                new DoctorSpecialty { SpecialtyId = 1 },
                new DoctorSpecialty { SpecialtyId = 2 }
            }
        };

        _specialtyRepositoryMock.Setup(repo => repo.Select(It.IsAny<int>())).Returns((Specialty)null);

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => _doctorService.AddDoctor(doctor));
        Assert.Equal(MessageConstants.SpecialtyNotFound, exception.Message);
    }

    [Fact]
    public void Validations_DoctorExists_ThrowsDoctorException()
    {
        // Arrange
        var doctor = new Doctor { Id = ID_DOCTOR_SUCESS };
        _doctorRepositoryMock.Setup(repo => repo.IsDoctorExists(doctor)).Returns(true);

        // Act & Assert
        Assert.Throws<DoctorException>(() => _doctorService.Validations(doctor));
    }

    [Fact]
    public void Validations_DoctorDoesNotExist_ReturnsFalse()
    {
        // Arrange
        var doctor = new Doctor { Id = ID_DOCTOR_SUCESS };
        _doctorRepositoryMock.Setup(repo => repo.IsDoctorExists(doctor)).Returns(false);

        // Act
        var result = _doctorService.Validations(doctor);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void GetAllDoctorsActive_ReturnsDoctors()
    {
        // Arrange
        var doctors = new List<Doctor> { new Doctor { Id = ID_DOCTOR_SUCESS } };
        _doctorRepositoryMock.Setup(repo => repo.GetAllDoctorsActive()).Returns(doctors);

        // Act
        var result = _doctorService.GetAllDoctorsActive();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(doctors.Count, result.Count);
    }

    [Fact]
    public void GetAllDoctorsActiveById_ReturnsDoctors()
    {
        // Arrange
        var doctorId = 1;
        var doctors = new List<Doctor> { new Doctor { Id = ID_DOCTOR_SUCESS} };
        _doctorRepositoryMock.Setup(repo => repo.GetAllDoctorsActiveById(doctorId)).Returns(doctors);

        // Act
        var result = _doctorService.GetAllDoctorsActiveById(doctorId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(doctors.Count, result.Count);
    }

    [Fact]
    public void GetDoctorByUserId_ReturnsDoctor()
    {
        // Arrange
        var userId = "user123";
        var doctor = new Doctor { Id = ID_DOCTOR_SUCESS, UserId = userId };
        _doctorRepositoryMock.Setup(repo => repo.GetDoctorByUserId(userId)).Returns(doctor);

        // Act
        var result = _doctorService.GetDoctorByUserId(userId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userId, result.UserId);
    }
}

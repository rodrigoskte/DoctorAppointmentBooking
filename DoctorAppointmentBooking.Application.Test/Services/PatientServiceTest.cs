using DoctorAppointmentBooking.Application.Services;
using DoctorAppointmentBooking.Domain.Entities;
using DoctorAppointmentBooking.Domain.Exceptions;
using DoctorAppointmentBooking.Domain.Interfaces;
using Moq;

public class PatientServiceTests
{
    private readonly Mock<IPatientRepository> _patientRepositoryMock;
    private readonly PatientService _patientService;

    public PatientServiceTests()
    {
        _patientRepositoryMock = new Mock<IPatientRepository>();
        _patientService = new PatientService(_patientRepositoryMock.Object);
    }

    [Fact]
    public void Validations_WhenPatientExists_ThrowsPatientExistsException()
    {
        // Arrange
        var existingPatient = new Patient();
        _patientRepositoryMock.Setup(repo => repo.IsPatientExists(existingPatient)).Returns(true);

        // Act & Assert
        Assert.Throws<PatientExistesException>(() => _patientService.Validations(existingPatient));
    }

    [Fact]
    public void Validations_WhenPatientDoesNotExist_ReturnsFalse()
    {
        // Arrange
        var newPatient = new Patient();
        _patientRepositoryMock.Setup(repo => repo.IsPatientExists(newPatient)).Returns(false);

        // Act
        var result = _patientService.Validations(newPatient);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void GetAllPatientActive_ReturnsListOfActivePatients()
    {
        // Arrange
        var activePatients = new List<Patient> { new Patient(), new Patient() };
        _patientRepositoryMock.Setup(repo => repo.GetAllPatientActive()).Returns(activePatients);

        // Act
        var result = _patientService.GetAllPatientActive();

        // Assert
        Assert.Equal(activePatients, result);
    }

    [Fact]
    public void GetPatientByUserId_ReturnsPatientWithMatchingUserId()
    {
        // Arrange
        var userId = "123";
        var patient = new Patient { UserId = userId };
        _patientRepositoryMock.Setup(repo => repo.GetPatientByUserId(userId)).Returns(patient);

        // Act
        var result = _patientService.GetPatientByUserId(userId);

        // Assert
        Assert.Equal(patient, result);
    }
}

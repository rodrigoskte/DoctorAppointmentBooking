using DoctorAppointmentBooking.Application.Validators;
using DoctorAppointmentBooking.Domain.Entities;
using FluentValidation.TestHelper;

public class PatientValidatorTest
{
    private readonly PatientValidator _validator;

    public PatientValidatorTest()
    {
        _validator = new PatientValidator();
    }

    [Fact]
    public void Name_IsEmpty_ShouldHaveValidationError()
    {
        // Arrange
        var patient = new Patient { Name = "", Email = "test@example.com" };

        // Act & Assert
        var result = _validator.TestValidate(patient);
        result.ShouldHaveValidationErrorFor(p => p.Name)
              .WithErrorMessage("Please enter the name.");
    }

    [Fact]
    public void Name_IsNotEmpty_ShouldNotHaveValidationError()
    {
        // Arrange
        var patient = new Patient { Name = "John Doe", Email = "test@example.com" };

        // Act & Assert
        var result = _validator.TestValidate(patient);
        result.ShouldNotHaveValidationErrorFor(p => p.Name);
    }

    [Fact]
    public void Email_IsEmpty_ShouldHaveValidationError()
    {
        // Arrange
        var patient = new Patient { Name = "John Doe", Email = "" };

        // Act & Assert
        var result = _validator.TestValidate(patient);
        result.ShouldHaveValidationErrorFor(p => p.Email)
              .WithErrorMessage("Please enter the email.");
    }

    [Fact]
    public void Email_IsNotEmpty_ShouldNotHaveValidationError()
    {
        // Arrange
        var patient = new Patient { Name = "John Doe", Email = "test@example.com" };

        // Act & Assert
        var result = _validator.TestValidate(patient);
        result.ShouldNotHaveValidationErrorFor(p => p.Email);
    }

    [Fact]
    public void Email_IsInvalid_ShouldHaveValidationError()
    {
        // Arrange
        var patient = new Patient { Name = "John Doe", Email = "invalid-email"};

        // Act & Assert
        var result = _validator.TestValidate(patient);
        result.ShouldHaveValidationErrorFor(p => p.Email)
              .WithErrorMessage("Email is not valid");
    }

    [Fact]
    public void Schedules_IsNotNull_ShouldNotHaveValidationError()
    {
        // Arrange
        var patient = new Patient { Name = "John Doe", Email = "test@example.com" };

        // Act & Assert
        var result = _validator.TestValidate(patient);
        result.ShouldNotHaveValidationErrorFor(p => p.Schedules);
    }
}

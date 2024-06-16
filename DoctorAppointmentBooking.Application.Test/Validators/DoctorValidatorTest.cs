using Xunit;
using FluentValidation.TestHelper;
using DoctorAppointmentBooking.Domain.Entities;
using DoctorAppointmentBooking.Application.Validators;

public class DoctorValidatorTest
{
    private readonly DoctorValidator _validator;

    public DoctorValidatorTest()
    {
        _validator = new DoctorValidator();
    }

    [Fact]
    public void Name_IsEmpty_ShouldHaveValidationError()
    {
        // Arrange
        var doctor = new Doctor { Name = "", Email = "123456" };

        // Act & Assert
        var result = _validator.TestValidate(doctor);
        result.ShouldHaveValidationErrorFor(d => d.Name)
            .WithErrorMessage("Please enter the name.");
    }

    [Fact]
    public void Name_IsNotEmpty_ShouldNotHaveValidationError()
    {
        // Arrange
        var doctor = new Doctor { Name = "Dr. John Doe", Email = "123456" };

        // Act & Assert
        var result = _validator.TestValidate(doctor);
        result.ShouldNotHaveValidationErrorFor(d => d.Name);
    }

    [Fact]
    public void Code_IsEmpty_ShouldHaveValidationError()
    {
        // Arrange
        var doctor = new Doctor { Name = "Dr. John Doe", Email = "" };

        // Act & Assert
        var result = _validator.TestValidate(doctor);
        result.ShouldHaveValidationErrorFor(d => d.Email)
            .WithErrorMessage("Please enter the CRM.");
    }

    [Fact]
    public void Code_IsNotEmpty_ShouldNotHaveValidationError()
    {
        // Arrange
        var doctor = new Doctor { Name = "Dr. John Doe", Email = "123456" };

        // Act & Assert
        var result = _validator.TestValidate(doctor);
        result.ShouldNotHaveValidationErrorFor(d => d.Email);
    }
}
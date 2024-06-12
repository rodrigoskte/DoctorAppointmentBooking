using FluentValidation.TestHelper;
using DoctorAppointmentBooking.Domain.Entities;
using DoctorAppointmentBooking.Application.Validators;

public class DoctorSpecialtyValidatorTest
{
    private readonly DoctorSpecialtyValidator _validator;

    public DoctorSpecialtyValidatorTest()
    {
        _validator = new DoctorSpecialtyValidator();
    }

    [Fact]
    public void DoctorId_IsEmpty_ShouldHaveValidationError()
    {
        // Arrange
        var doctorSpecialty = new DoctorSpecialty { SpecialtyId = 1 };

        // Act & Assert
        var result = _validator.TestValidate(doctorSpecialty);
        result.ShouldHaveValidationErrorFor(ds => ds.DoctorId)
            .WithErrorMessage("Please enter the doctor.");
    }

    [Fact]
    public void DoctorId_IsNotNullOrEmpty_ShouldNotHaveValidationError()
    {
        // Arrange
        var doctorSpecialty = new DoctorSpecialty { DoctorId = 1, SpecialtyId = 1 };

        // Act & Assert
        var result = _validator.TestValidate(doctorSpecialty);
        result.ShouldNotHaveValidationErrorFor(ds => ds.DoctorId);
    }

    [Fact]
    public void SpecialtyId_IsEmpty_ShouldHaveValidationError()
    {
        // Arrange
        var doctorSpecialty = new DoctorSpecialty { DoctorId = 1, SpecialtyId = 0 };

        // Act & Assert
        var result = _validator.TestValidate(doctorSpecialty);
        result.ShouldHaveValidationErrorFor(ds => ds.SpecialtyId)
            .WithErrorMessage("Please enter the specialty.");
    }

    [Fact]
    public void SpecialtyId_IsNotNullOrEmpty_ShouldNotHaveValidationError()
    {
        // Arrange
        var doctorSpecialty = new DoctorSpecialty { DoctorId = 1, SpecialtyId = 1 };

        // Act & Assert
        var result = _validator.TestValidate(doctorSpecialty);
        result.ShouldNotHaveValidationErrorFor(ds => ds.SpecialtyId);
    }
}
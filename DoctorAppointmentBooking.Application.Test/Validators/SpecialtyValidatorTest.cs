using DoctorAppointmentBooking.Application.Validators;
using DoctorAppointmentBooking.Domain.Entities;
using FluentValidation.TestHelper;

public class SpecialtyValidatorTest
{
    private readonly SpecialtyValidator _validator;

    public SpecialtyValidatorTest()
    {
        _validator = new SpecialtyValidator();
    }

    [Fact]
    public void Description_IsEmpty_ShouldHaveValidationError()
    {
        // Arrange
        var specialty = new Specialty { Description = "", DoctorSpecialties = {  }};

        // Act & Assert
        var result = _validator.TestValidate(specialty);
        result.ShouldHaveValidationErrorFor(s => s.Description)
              .WithErrorMessage("Please enter the Specialty Description.");
    }

    [Fact]
    public void Description_IsNotEmpty_ShouldNotHaveValidationError()
    {
        // Arrange
        var specialty = new Specialty { Description = "Cardiology", DoctorSpecialties = {  }};

        // Act & Assert
        var result = _validator.TestValidate(specialty);
        result.ShouldNotHaveValidationErrorFor(s => s.Description);
    }

    [Fact]
    public void DoctorSpecialties_IsNotNull_ShouldNotHaveValidationError()
    {
        // Arrange
        var specialty = new Specialty { Description = "Cardiology", DoctorSpecialties = {  }};

        // Act & Assert
        var result = _validator.TestValidate(specialty);
        result.ShouldNotHaveValidationErrorFor(s => s.DoctorSpecialties);
    }
}

using DoctorAppointmentBooking.Application.Validators;
using DoctorAppointmentBooking.Domain.Entities;
using FluentValidation.TestHelper;

public class ScheduleValidatorTest
{
    private readonly ScheduleValidator _validator;

    public ScheduleValidatorTest()
    {
        _validator = new ScheduleValidator();
    }

    [Fact]
    public void DoctorId_IsEmpty_ShouldHaveValidationError()
    {
        // Arrange
        var schedule = new Schedule { PatientId = 1, DateTimeSchedule = DateTime.Now };

        // Act & Assert
        var result = _validator.TestValidate(schedule);
        result.ShouldHaveValidationErrorFor(s => s.DoctorId)
              .WithErrorMessage("Please enter the Doctor.");
    }

    [Fact]
    public void DoctorId_IsNotEmpty_ShouldNotHaveValidationError()
    {
        // Arrange
        var schedule = new Schedule { DoctorId = 1, PatientId = 2, DateTimeSchedule = DateTime.Now };

        // Act & Assert
        var result = _validator.TestValidate(schedule);
        result.ShouldNotHaveValidationErrorFor(s => s.DoctorId);
    }

    [Fact]
    public void PatientId_IsEmpty_ShouldHaveValidationError()
    {
        // Arrange
        var schedule = new Schedule { DoctorId = 1, DateTimeSchedule = DateTime.Now };

        // Act & Assert
        var result = _validator.TestValidate(schedule);
        result.ShouldHaveValidationErrorFor(s => s.PatientId)
              .WithErrorMessage("Please enter the Patient.");
    }

    [Fact]
    public void PatientId_IsNotEmpty_ShouldNotHaveValidationError()
    {
        // Arrange
        var schedule = new Schedule { DoctorId = 1, PatientId = 2, DateTimeSchedule = DateTime.Now };

        // Act & Assert
        var result = _validator.TestValidate(schedule);
        result.ShouldNotHaveValidationErrorFor(s => s.PatientId);
    }

    [Fact]
    public void DateTimeSchedule_IsEmpty_ShouldHaveValidationError()
    {
        // Arrange
        var schedule = new Schedule { DoctorId = 1, PatientId = 2, DateTimeSchedule = default(DateTime) };

        // Act & Assert
        var result = _validator.TestValidate(schedule);
        result.ShouldHaveValidationErrorFor(s => s.DateTimeSchedule)
              .WithErrorMessage("Please enter the Date.");
    }

    [Fact]
    public void DateTimeSchedule_IsNotEmpty_ShouldNotHaveValidationError()
    {
        // Arrange
        var schedule = new Schedule { DoctorId = 1, PatientId = 2, DateTimeSchedule = DateTime.Now };

        // Act & Assert
        var result = _validator.TestValidate(schedule);
        result.ShouldNotHaveValidationErrorFor(s => s.DateTimeSchedule);
    }
}

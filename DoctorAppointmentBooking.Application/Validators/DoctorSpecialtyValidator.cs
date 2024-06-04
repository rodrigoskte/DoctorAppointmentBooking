using DoctorAppointmentBooking.Domain.Entities;
using FluentValidation;

namespace DoctorAppointmentBooking.Application.Validators;

public class DoctorSpecialtyValidator : AbstractValidator<DoctorSpecialty>
{
    public DoctorSpecialtyValidator()
    {
        RuleFor(c => c.DoctorId)
            .NotEmpty().WithMessage("Please enter the doctor.")
            .NotNull().WithMessage("Please enter the doctor.");
        
        RuleFor(c => c.SpecialtyId)
            .NotEmpty().WithMessage("Please enter the specialty.")
            .NotNull().WithMessage("Please enter the specialty.");
    }
}
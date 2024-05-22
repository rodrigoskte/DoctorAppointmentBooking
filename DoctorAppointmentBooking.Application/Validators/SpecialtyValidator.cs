using DoctorAppointmentBooking.Domain.Entities;
using FluentValidation;

namespace DoctorAppointmentBooking.Application.Validators
{
    public class SpecialtyValidator : AbstractValidator<Specialty>
    {
        public SpecialtyValidator()
        {
            RuleFor(c => c.Description)
                    .NotEmpty().WithMessage("Please enter the Specialty Description.")
                    .NotNull().WithMessage("Please enter the Specialty Description.");
        }
    }
}

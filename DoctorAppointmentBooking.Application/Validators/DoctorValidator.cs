using DoctorAppointmentBooking.Domain.Entities;
using FluentValidation;

namespace DoctorAppointmentBooking.Application.Validators
{
    public class DoctorValidator : AbstractValidator<Doctor>
    {
        public DoctorValidator() 
        { 
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Please enter the name.")
                .NotNull().WithMessage("Please enter the name.");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("Please enter the Email.")
                .NotNull().WithMessage("Please enter the Email.");
        }
    }
}

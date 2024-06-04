using DoctorAppointmentBooking.Domain.Entities;
using FluentValidation;

namespace DoctorAppointmentBooking.Application.Validators
{
    public class PatientValidator : AbstractValidator<Patient>
    {
        public PatientValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Please enter the name.")
                .NotNull().WithMessage("Please enter the name.");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("Please enter the email.")
                .NotNull().WithMessage("Please enter the email.")
                .EmailAddress().WithMessage("Email is not valid");
            
            RuleFor(s => s.Schedules).NotNull().WithMessage("Schedules should not be null.");
        }
    }
}

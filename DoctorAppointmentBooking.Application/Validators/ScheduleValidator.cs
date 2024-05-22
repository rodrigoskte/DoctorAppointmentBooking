using DoctorAppointmentBooking.Domain.Entities;
using FluentValidation;

namespace DoctorAppointmentBooking.Application.Validators
{
    public class ScheduleValidator : AbstractValidator<Schedule>
    {
        public ScheduleValidator()
        {
            RuleFor(c => c.Doctor)
                    .NotEmpty().WithMessage("Please enter the Doctor.")
                    .NotNull().WithMessage("Please enter the Doctor.");

            RuleFor(c => c.Patient)
                    .NotEmpty().WithMessage("Please enter the Patient.")
                    .NotNull().WithMessage("Please enter the Patient.");

            RuleFor(c => c.DateTimeSchedule)
                    .NotEmpty().WithMessage("Please enter the Date.")
                    .NotNull().WithMessage("Please enter the Date.");
        }
    }
}

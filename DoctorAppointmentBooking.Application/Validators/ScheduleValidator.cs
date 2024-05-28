using DoctorAppointmentBooking.Domain.Entities;
using FluentValidation;

namespace DoctorAppointmentBooking.Application.Validators
{
    public class ScheduleValidator : AbstractValidator<Schedule>
    {
        public ScheduleValidator()
        {
            RuleFor(c => c.DoctorId)
                    .NotEmpty().WithMessage("Please enter the Doctor.")
                    .NotNull().WithMessage("Please enter the Doctor.");

            RuleFor(c => c.PatientId)
                    .NotEmpty().WithMessage("Please enter the Patient.")
                    .NotNull().WithMessage("Please enter the Patient.");

            RuleFor(c => c.DateTimeSchedule)
                    .NotEmpty().WithMessage("Please enter the Date.")
                    .NotNull().WithMessage("Please enter the Date.");
        }
    }
}

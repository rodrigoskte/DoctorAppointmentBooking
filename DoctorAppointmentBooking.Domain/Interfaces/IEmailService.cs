namespace DoctorAppointmentBooking.Domain.Interfaces
{
    public interface IEmailService
    {
        Task SendEmail(string emailRecipient, string emailSubject, string emailBody);
    }
}

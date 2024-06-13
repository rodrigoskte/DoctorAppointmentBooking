using DoctorAppointmentBooking.Application.DTOs;
using DoctorAppointmentBooking.Domain.Interfaces;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace DoctorAppointmentBooking.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(
            IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmail(string emailRecipient, string emailSubject, string emailBody)
        {
            string smtpServer = _emailSettings.ServidorSMTP ?? string.Empty;
            int smtpPort = _emailSettings.PortaSMTP;
            string smtpUser = _emailSettings.Usuario ?? string.Empty;
            string smtpPass = _emailSettings.Senha ?? string.Empty;

            var smtpClient = new SmtpClient(smtpServer)
            {
                Port = smtpPort,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(smtpUser, smtpPass),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(smtpUser),
                Subject = emailSubject,
                Body = emailBody,
                IsBodyHtml = false
            };
            mailMessage.To.Add(emailRecipient);

            try
            {
                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao enviar e-mail: " + ex.Message);
                throw;
            }
        }
    }
}
